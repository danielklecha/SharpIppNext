using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SharpIpp.Generators.Models;

namespace SharpIpp.Generators;

[Generator]
public class MapperSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var typeDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => s is BaseTypeDeclarationSyntax,
                transform: static (ctx, _) => ctx.SemanticModel.GetDeclaredSymbol((BaseTypeDeclarationSyntax)ctx.Node))
            .Where(static s => s != null)
            .Select(static (s, _) => s!);

        var compilationAndTypes = context.CompilationProvider.Combine(typeDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndTypes, static (spc, source) =>
        {
            Execute(source.Left, source.Right, spc);
        });
    }

    internal static void Execute(Compilation compilation, ImmutableArray<INamedTypeSymbol> types, SourceProductionContext context)
    {
        var iKeywordOrNameEnumSymbol = compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.IKeywordOrNameEnum");
        var iKeywordEnumSymbol = compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.IKeywordEnum");
        var ippAttributeNamesSymbol = compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.IppAttributeNames");
        var ippAttributeAttrSymbol = compilation.GetTypeByMetadataName("SharpIpp.Mapping.IppAttributeAttribute");
        var ippRequestAttrSymbol = compilation.GetTypeByMetadataName("SharpIpp.Mapping.IppRequestAttribute");
        var ippResponseAttrSymbol = compilation.GetTypeByMetadataName("SharpIpp.Mapping.IppResponseAttribute");
        var mapperConfigAttrSymbol = compilation.GetTypeByMetadataName("SharpIpp.Mapping.MapperConfigurationAttribute");
        var iIppCollectionSymbol = compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.IIppCollection");
        var iIppStructuredStringSymbol = compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.IIppStructuredString");
        var ippSectionAttrSymbol = compilation.GetTypeByMetadataName("SharpIpp.Mapping.IppSectionAttribute");
        var ippValueGenericSymbol = compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.IppValue`1");

        var nameToConst = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (ippAttributeNamesSymbol != null)
        {
            foreach (var member in ippAttributeNamesSymbol.GetMembers())
            {
                if (member is IFieldSymbol field && field.ConstantValue is string val)
                {
                    nameToConst[val] = field.Name;
                }
            }
        }

        var conversions = new HashSet<ConversionMapping>();
        var enumTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var keywordEnumTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var annotatedModels = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var annotatedRequests = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var annotatedResponses = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var annotatedSections = new List<AnnotatedSectionType>();
        var configuredMappers = new List<ConfiguredMapperType>();
        var structuredStringTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        var handledTypes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);

        foreach (var typeSymbol in types)
        {

            if (typeSymbol.IsGenericType)
                continue;


            if (typeSymbol.TypeKind == TypeKind.Enum)
            {
                if (typeSymbol.ContainingNamespace.ToDisplayString() == "SharpIpp.Protocol.Models" &&
                    typeSymbol.Name != "Tag" && typeSymbol.Name != "SectionTag")
                {
                    enumTypes.Add(typeSymbol);
                }
                continue;
            }

            if (typeSymbol.TypeKind == TypeKind.Class && !typeSymbol.IsAbstract && ImplementsOrInherits(typeSymbol, iIppStructuredStringSymbol))
            {
                structuredStringTypes.Add(typeSymbol);
            }

            if (typeSymbol.TypeKind == TypeKind.Struct && ImplementsOrInherits(typeSymbol, iKeywordEnumSymbol))
            {
                keywordEnumTypes.Add(typeSymbol);
            }

            if (typeSymbol.TypeKind == TypeKind.Struct || typeSymbol.TypeKind == TypeKind.Class)
            {
                foreach (var member in typeSymbol.GetMembers())
                {
                    if (member is IMethodSymbol method && method.MethodKind == MethodKind.Conversion && method.Parameters.Length == 1)
                    {
                        var srcType = method.Parameters[0].Type;
                        var dstType = method.ReturnType;

                        if (srcType.Name == "IppValue" || dstType.Name == "IppValue")
                            continue;
                        if (SymbolEqualityComparer.Default.Equals(srcType, dstType))
                            continue;

                        conversions.Add(new ConversionMapping(srcType, dstType));
                    }
                }
            }

            if (typeSymbol.TypeKind == TypeKind.Class)
            {
                if (HasAttribute(typeSymbol, mapperConfigAttrSymbol))
                {
                    var configAttr = typeSymbol.GetAttributes().FirstOrDefault(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, mapperConfigAttrSymbol));
                    if (configAttr != null)
                    {
                        int order = 0;
                        foreach (var arg in configAttr.ConstructorArguments)
                        {
                            if (arg.Kind == TypedConstantKind.Primitive)
                            {
                                order = (int)arg.Value!;
                            }
                            else
                            {
                                ExtractHandledTypes(arg, handledTypes);
                            }
                        }

                        foreach (var named in configAttr.NamedArguments)
                        {
                            if (named.Key == "Order")
                            {
                                order = (int)named.Value.Value!;
                            }
                            else if (named.Key == "HandledTypes" || named.Key == "HandledType")
                            {
                                ExtractHandledTypes(named.Value, handledTypes);
                            }
                        }

                        ExtractHandledTypesFromConfiguredMapper(typeSymbol, compilation, handledTypes);
                        configuredMappers.Add(new ConfiguredMapperType(typeSymbol, order));
                    }
                }

                if (!typeSymbol.IsAbstract)
                {
                    if (HasAttribute(typeSymbol, ippSectionAttrSymbol))
                    {
                        var sectionAttr = GetAttribute(typeSymbol, ippSectionAttrSymbol)!;
                        if (sectionAttr.ConstructorArguments.Length > 0)
                        {
                            var tagVal = Convert.ToByte(sectionAttr.ConstructorArguments[0].Value);
                            annotatedSections.Add(new AnnotatedSectionType(typeSymbol, tagVal));
                        }
                    }

                    if (HasAttribute(typeSymbol, ippRequestAttrSymbol))
                    {
                        annotatedRequests.Add(typeSymbol);
                    }
                    else if (HasAttribute(typeSymbol, ippResponseAttrSymbol))
                    {
                        annotatedResponses.Add(typeSymbol);
                    }
                    else if (HasIppAttributeAnnotations(typeSymbol, ippAttributeAttrSymbol) || ImplementsOrInherits(typeSymbol, iIppCollectionSymbol))
                    {
                        annotatedModels.Add(typeSymbol);
                    }
                }
            }
        }

        annotatedModels.RemoveWhere(m => handledTypes.Contains(m));
        annotatedRequests.RemoveWhere(r => handledTypes.Contains(r));
        annotatedResponses.RemoveWhere(r => handledTypes.Contains(r));
        annotatedSections.RemoveAll(s => handledTypes.Contains(s.TypeSymbol));

        var ippValueTypes = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        var collectionElementTypes = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        var allTypesWithProps = annotatedModels
            .Concat(annotatedRequests)
            .Concat(annotatedResponses)
            .Concat(annotatedSections.Select(s => s.TypeSymbol));
        foreach (var m in allTypesWithProps)
        {
            var props = GetAnnotatedProperties(m, annotatedModels, ippAttributeAttrSymbol, iIppCollectionSymbol, iKeywordOrNameEnumSymbol, iKeywordEnumSymbol, iIppStructuredStringSymbol, ippValueGenericSymbol);
            foreach (var p in props)
            {
                if (p.IsIppValue)
                {
                    ippValueTypes.Add(p.UnwrappedType);
                }
                if (p.ElementType != null)
                {
                    AddTypeIfValid(collectionElementTypes, p.ElementType);
                }
                if (p.IppValueElementType != null)
                {
                    AddTypeIfValid(collectionElementTypes, p.IppValueElementType);
                }
            }
        }

        foreach (var model in annotatedModels)
        {
            AddTypeIfValid(collectionElementTypes, model);
        }
        foreach (var e in enumTypes)
        {
            AddTypeIfValid(collectionElementTypes, e);
        }
        foreach (var se in keywordEnumTypes)
        {
            AddTypeIfValid(collectionElementTypes, se);
        }
        foreach (var ss in structuredStringTypes)
        {
            AddTypeIfValid(collectionElementTypes, ss);
        }
        foreach (var h in handledTypes)
        {
            AddTypeIfValid(collectionElementTypes, h);
        }

        AddTypeIfValid(collectionElementTypes, compilation.GetSpecialType(SpecialType.System_String));
        AddTypeIfValid(collectionElementTypes, compilation.GetSpecialType(SpecialType.System_Int32));
        AddTypeIfValid(collectionElementTypes, compilation.GetSpecialType(SpecialType.System_Boolean));
        AddTypeIfValid(collectionElementTypes, compilation.GetTypeByMetadataName("System.DateTimeOffset"));
        AddTypeIfValid(collectionElementTypes, compilation.GetTypeByMetadataName("System.Uri"));
        AddTypeIfValid(collectionElementTypes, compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.Range"));
        AddTypeIfValid(collectionElementTypes, compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.Resolution"));
        AddTypeIfValid(collectionElementTypes, compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.OctetString"));
        AddTypeIfValid(collectionElementTypes, compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.StringWithLanguage"));
        AddTypeIfValid(collectionElementTypes, compilation.GetTypeByMetadataName("SharpIpp.Protocol.Models.IppAttribute"));

        foreach (var conv in conversions)
        {
            if (conv.SourceType is not IArrayTypeSymbol)
                AddTypeIfValid(collectionElementTypes, conv.SourceType);
            if (conv.DestType is not IArrayTypeSymbol)
                AddTypeIfValid(collectionElementTypes, conv.DestType);
        }

        if (ippValueGenericSymbol != null)
        {
            foreach (var se in keywordEnumTypes)
            {
                ippValueTypes.Add(ippValueGenericSymbol.Construct(se));
            }
            foreach (var model in annotatedModels)
            {
                ippValueTypes.Add(ippValueGenericSymbol.Construct(model));
            }
            foreach (var e in enumTypes)
            {
                ippValueTypes.Add(ippValueGenericSymbol.Construct(e));
            }
            foreach (var elem in collectionElementTypes)
            {
                if (elem is not IArrayTypeSymbol)
                {
                    ippValueTypes.Add(ippValueGenericSymbol.Construct(elem));
                }
            }
            foreach (var conv in conversions)
            {
                if (conv.SourceType is not IArrayTypeSymbol)
                    ippValueTypes.Add(ippValueGenericSymbol.Construct(conv.SourceType));
                if (conv.DestType is not IArrayTypeSymbol)
                    ippValueTypes.Add(ippValueGenericSymbol.Construct(conv.DestType));
            }
        }

        GenerateTypeConverters(conversions, enumTypes, keywordEnumTypes, structuredStringTypes, ippValueTypes, collectionElementTypes, iKeywordEnumSymbol, iKeywordOrNameEnumSymbol, iIppStructuredStringSymbol, context);
        GenerateMapperRegistry(annotatedModels, annotatedRequests, annotatedResponses, annotatedSections, configuredMappers, nameToConst, ippAttributeAttrSymbol, iIppCollectionSymbol, context);
        if (annotatedModels.Count > 0 || annotatedRequests.Count > 0 || annotatedResponses.Count > 0)
        {
            GenerateModelMappers(annotatedModels, annotatedRequests, annotatedResponses, nameToConst, ippAttributeAttrSymbol, ippRequestAttrSymbol, iIppCollectionSymbol, iKeywordOrNameEnumSymbol, iKeywordEnumSymbol, iIppStructuredStringSymbol, ippValueGenericSymbol, context);
        }
    }

    internal static void AddTypeIfValid(HashSet<ITypeSymbol> set, ITypeSymbol? type)
    {
        if (type != null && type.TypeKind != TypeKind.Error && type.TypeKind != TypeKind.TypeParameter)
        {
            set.Add(type);
        }
    }

    internal static INamedTypeSymbol? UnwrapPotentialModel(ITypeSymbol? type)
    {
        if (type == null)
            return null;

        if (type is IArrayTypeSymbol ats)
            return UnwrapPotentialModel(ats.ElementType);

        if (type is INamedTypeSymbol nts)
        {
            if (nts.IsGenericType && (nts.Name == "List" || nts.Name == "IEnumerable" || nts.Name == "IReadOnlyCollection" || nts.Name == "IppValue"))
            {
                return UnwrapPotentialModel(nts.TypeArguments[0]);
            }
            return nts;
        }

        return null;
    }

    internal static bool IsPotentialHandledModel(INamedTypeSymbol nts)
    {
        if (nts.SpecialType != SpecialType.None)
            return false;

        if (nts.TypeKind != TypeKind.Class && nts.TypeKind != TypeKind.Struct)
            return false;

        var ns = nts.ContainingNamespace!.ToDisplayString();
        if (ns.StartsWith("System") || ns.StartsWith("Microsoft"))
            return false;

        if (nts.Name == "IppAttribute" ||
            nts.Name == "IppRequestMessage" ||
            nts.Name == "IppResponseMessage" ||
            nts.Name == "IIppRequestMessage" ||
            nts.Name == "IIppResponseMessage" ||
            nts.Name == "IMapper" ||
            nts.Name == "IMapperApplier" ||
            nts.Name == "IMapperConstructor" ||
            nts.Name == "NoValue" ||
            nts.Name == "Range" ||
            nts.Name == "Resolution" ||
            nts.Name == "OctetString" ||
            nts.Name == "StringWithLanguage")
        {
            return false;
        }

        return true;
    }

    internal static void ExtractHandledTypesFromConfiguredMapper(
        INamedTypeSymbol configuredMapper,
        Compilation compilation,
        HashSet<INamedTypeSymbol> handledTypes)
    {
        foreach (var syntaxRef in configuredMapper.DeclaringSyntaxReferences)
        {
            var syntax = syntaxRef.GetSyntax();
            var semanticModel = compilation.GetSemanticModel(syntax.SyntaxTree);

            foreach (var inv in syntax.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                string? methodName = null;
                if (inv.Expression is MemberAccessExpressionSyntax ma)
                {
                    methodName = ma.Name.Identifier.Text;
                }
                else if (inv.Expression is SimpleNameSyntax sn)
                {
                    methodName = sn.Identifier.Text;
                }

                if (methodName == "CreateMap" || methodName == "CreateIppMap")
                {
                    SimpleNameSyntax nameSyntax = inv.Expression is MemberAccessExpressionSyntax m
                        ? m.Name
                        : (SimpleNameSyntax)inv.Expression;

                    if (nameSyntax is GenericNameSyntax gn)
                    {
                        foreach (var typeArg in gn.TypeArgumentList.Arguments)
                        {
                            var typeInfo = semanticModel.GetTypeInfo(typeArg);
                            AddHandledTypeIfValid(typeInfo.Type, handledTypes);
                        }
                    }
                    else
                    {
                        foreach (var arg in inv.ArgumentList.Arguments)
                        {
                            if (arg.Expression is TypeOfExpressionSyntax toe)
                            {
                                var typeInfo = semanticModel.GetTypeInfo(toe.Type);
                                AddHandledTypeIfValid(typeInfo.Type, handledTypes);
                            }
                        }
                    }
                }
            }
        }
    }

    internal static void ExtractHandledTypes(TypedConstant constant, HashSet<INamedTypeSymbol> handledTypes)
    {
        if (constant.Kind == TypedConstantKind.Array)
        {
            foreach (var elem in constant.Values)
            {
                ExtractHandledTypes(elem, handledTypes);
            }
        }
        else if (constant.Value is ITypeSymbol ts)
        {
            AddHandledTypeIfValid(ts, handledTypes);
        }
    }

    internal static void AddHandledTypeIfValid(ITypeSymbol? typeSymbol, HashSet<INamedTypeSymbol> handledTypes)
    {
        var unwrapped = UnwrapPotentialModel(typeSymbol);
        if (unwrapped != null && IsPotentialHandledModel(unwrapped))
        {
            handledTypes.Add(unwrapped);
        }
    }

    internal static bool HasIppAttributeAnnotations(INamedTypeSymbol typeSymbol, INamedTypeSymbol? ippAttributeAttrSymbol)
    {
        if (ippAttributeAttrSymbol == null)
            return false;

        foreach (var curr in GetTypeAndBaseTypes(typeSymbol))
        {
            if (curr.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, ippAttributeAttrSymbol)))
                return true;

            foreach (var member in curr.GetMembers())
            {
                if (member is IPropertySymbol prop && prop.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, ippAttributeAttrSymbol)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static void GenerateTypeConverters(
        HashSet<ConversionMapping> conversions,
        HashSet<INamedTypeSymbol> enumTypes,
        HashSet<INamedTypeSymbol> keywordEnumTypes,
        HashSet<INamedTypeSymbol> structuredStringTypes,
        HashSet<ITypeSymbol> ippValueTypes,
        HashSet<ITypeSymbol> collectionElementTypes,
        INamedTypeSymbol? iKeywordEnumSymbol,
        INamedTypeSymbol? iKeywordOrNameEnumSymbol,
        INamedTypeSymbol? iIppStructuredStringSymbol,
        SourceProductionContext context)
    {
        var sb = new StringBuilder();
        sb.AppendLine("// <auto-generated/>");
        sb.AppendLine("#nullable enable");
        sb.AppendLine("#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604");
        sb.AppendLine();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using System.Linq;");
        sb.AppendLine("using SharpIpp.Mapping;");
        sb.AppendLine("using SharpIpp.Mapping.Extensions;");
        sb.AppendLine("using SharpIpp.Protocol.Models;");
        sb.AppendLine();
        sb.AppendLine("namespace SharpIpp.Mapping");
        sb.AppendLine("{");
        sb.AppendLine("    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]");
        sb.AppendLine("    [global::System.CodeDom.Compiler.GeneratedCode(\"SharpIpp.Generators\", \"1.0.0.0\")]");
        sb.AppendLine("    internal static partial class GeneratedTypeConverters");
        sb.AppendLine("    {");
        sb.AppendLine("        public static void Register(IMapperConstructor mapper)");
        sb.AppendLine("        {");
        sb.AppendLine("            RegisterConversions(mapper);");
        sb.AppendLine("            RegisterEnums(mapper);");
        sb.AppendLine("            RegisterIppValueTypes(mapper);");
        sb.AppendLine("            RegisterCollections(mapper);");
        sb.AppendLine("            RegisterStructuredStrings(mapper);");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        private static void RegisterConversions(IMapperConstructor mapper)");
        sb.AppendLine("        {");

        var sortedConversions = conversions
            .OrderBy(c => c.SourceType.ToDisplayString())
            .ThenBy(c => c.DestType.ToDisplayString())
            .ToList();

        foreach (var conv in sortedConversions)
        {
            var srcFqn = conv.SourceType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var dstFqn = conv.DestType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            sb.AppendLine($"            mapper.CreateIppMap<{srcFqn}, {dstFqn}>((src, _) => ({dstFqn})src);");
            if (conv.DestType is not IArrayTypeSymbol)
                sb.AppendLine($"            mapper.CreateIppMap<{srcFqn}, IppValue<{dstFqn}>>((src, _) => new IppValue<{dstFqn}>(({dstFqn})src));");
            if (conv.SourceType is not IArrayTypeSymbol && conv.DestType is not IArrayTypeSymbol)
            {
                if (conv.DestType.IsReferenceType)
                {
                    void AppendRefCollectionMap(string targetType, string returnStatement)
                    {
                        sb.AppendLine($"            mapper.CreateMap<{srcFqn}[], {targetType}>((src, _) =>");
                        sb.AppendLine("            {");
                        sb.AppendLine($"                var list = new List<{dstFqn}>(src.Length);");
                        sb.AppendLine("                for (int i = 0; i < src.Length; i++)");
                        sb.AppendLine("                {");
                        sb.AppendLine($"                    var item = ({dstFqn})src[i];");
                        sb.AppendLine("                    if (item != null) list.Add(item);");
                        sb.AppendLine("                }");
                        sb.AppendLine($"                return {returnStatement};");
                        sb.AppendLine("            });");
                    }

                    AppendRefCollectionMap($"{dstFqn}[]", "list.ToArray()");
                    AppendRefCollectionMap($"List<{dstFqn}>", "list");
                }
                else
                {
                    sb.AppendLine($"            mapper.CreateMap<{srcFqn}[], {dstFqn}[]>((src, _) =>");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                var arr = new {dstFqn}[src.Length];");
                    sb.AppendLine($"                for (int i = 0; i < src.Length; i++) arr[i] = ({dstFqn})src[i];");
                    sb.AppendLine("                return arr;");
                    sb.AppendLine("            });");
                    sb.AppendLine($"            mapper.CreateMap<{srcFqn}[], List<{dstFqn}>>((src, _) =>");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                var list = new List<{dstFqn}>(src.Length);");
                    sb.AppendLine($"                for (int i = 0; i < src.Length; i++) list.Add(({dstFqn})src[i]);");
                    sb.AppendLine("                return list;");
                    sb.AppendLine("            });");
                }

                sb.AppendLine($"            mapper.CreateMap<{srcFqn}[], IEnumerable<{dstFqn}>>((src, map) => map.Map<{dstFqn}[]>(src));");
                sb.AppendLine($"            mapper.CreateMap<{srcFqn}[], IReadOnlyCollection<{dstFqn}>>((src, map) => map.Map<{dstFqn}[]>(src));");
            }
        }

        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        private static void RegisterEnums(IMapperConstructor mapper)");
        sb.AppendLine("        {");

        var sortedEnums = enumTypes
            .OrderBy(t => t.ToDisplayString())
            .ToList();

        foreach (var type in sortedEnums)
        {
            var fqn = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var isShort = type.EnumUnderlyingType!.SpecialType == SpecialType.System_Int16;
            var intCast = isShort ? $"({fqn})(short)src" : $"({fqn})src";
            sb.AppendLine($"            mapper.CreateIppMap<int, {fqn}>((src, _) => {intCast});");
            sb.AppendLine($"            mapper.CreateIppMap<{fqn}, int>((src, _) => (int)src);");
        }

        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        private static void RegisterStructuredStrings(IMapperConstructor mapper)");
        sb.AppendLine("        {");

        var sortedStructured = structuredStringTypes
            .OrderBy(t => t.ToDisplayString())
            .ToList();

        foreach (var type in sortedStructured)
        {
            var fqn = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var hasParseEnumerable = type.GetMembers("Parse")
                .OfType<IMethodSymbol>()
                .Any(m => m.IsStatic && m.Parameters.Length == 1 &&
                          (m.Parameters[0].Type is IArrayTypeSymbol arr && arr.ElementType.SpecialType == SpecialType.System_String ||
                           m.Parameters[0].Type is INamedTypeSymbol named && named.OriginalDefinition.ToDisplayString() == "System.Collections.Generic.IEnumerable<T>" && named.TypeArguments.Length == 1 && named.TypeArguments[0].SpecialType == SpecialType.System_String));
            if (hasParseEnumerable)
            {
                sb.AppendLine($"            mapper.CreateMap<string[], {fqn}>((src, _) => {fqn}.Parse(src));");
                sb.AppendLine($"            mapper.CreateMap<object[], {fqn}>((src, map) => map.Map<{fqn}>(map.Map<string[]>(src)));");
            }

            sb.AppendLine($"            mapper.CreateMap<object[], {fqn}[]>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var list = new List<{fqn}>(src.Length);");
            sb.AppendLine("                for (int i = 0; i < src.Length; i++)");
            sb.AppendLine("                {");
            sb.AppendLine("                    var item = src[i];");
            sb.AppendLine("                    if (item == null || item is global::SharpIpp.Protocol.Models.NoValue) continue;");
            sb.AppendLine($"                    var mapped = map.MapNullable<{fqn}>(item);");
            sb.AppendLine("                    if (mapped != null) list.Add(mapped);");
            sb.AppendLine("                }");
            sb.AppendLine("                return list.ToArray();");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<object[], List<{fqn}>>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var list = new List<{fqn}>(src.Length);");
            sb.AppendLine("                for (int i = 0; i < src.Length; i++)");
            sb.AppendLine("                {");
            sb.AppendLine("                    var item = src[i];");
            sb.AppendLine("                    if (item == null || item is global::SharpIpp.Protocol.Models.NoValue) continue;");
            sb.AppendLine($"                    var mapped = map.MapNullable<{fqn}>(item);");
            sb.AppendLine("                    if (mapped != null) list.Add(mapped);");
            sb.AppendLine("                }");
            sb.AppendLine("                return list;");
            sb.AppendLine("            });");
        }

        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        private static void RegisterIppValueTypes(IMapperConstructor mapper)");
        sb.AppendLine("        {");

        var sortedIppValues = ippValueTypes
            .OrderBy(t => t.ToDisplayString())
            .ToList();

        foreach (var type in sortedIppValues)
        {
            var fqn = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            if (type is INamedTypeSymbol nts)
            {
                var inner = nts.TypeArguments[0];
                var innerFqn = inner.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                sb.AppendLine($"            mapper.CreateIppMap<NoValue, {fqn}>((_, _) => default);");
                sb.AppendLine($"            mapper.CreateIppMap<{innerFqn}, {fqn}>((src, _) => new {fqn}(src));");
                if (inner is INamedTypeSymbol innerEnum && innerEnum.TypeKind == TypeKind.Enum)
                {
                    var isShort = innerEnum.EnumUnderlyingType!.SpecialType == SpecialType.System_Int16;
                    var intCast = isShort ? $"({innerFqn})(short)src" : $"({innerFqn})src";
                    sb.AppendLine($"            mapper.CreateIppMap<int, {fqn}>((src, _) => new {fqn}({intCast}));");
                    sb.AppendLine($"            mapper.CreateIppMap<string, {fqn}>((src, map) => new {fqn}(map.Map<{innerFqn}>(src)));");
                }
                else if (ImplementsOrInherits(inner, iKeywordEnumSymbol))
                {
                    sb.AppendLine($"            mapper.CreateIppMap<string, {fqn}>((src, map) => new {fqn}(map.Map<{innerFqn}>(src)));");
                }
                else if (inner.Name == "Uri")
                {
                    sb.AppendLine($"            mapper.CreateMap(typeof(string), typeof({fqn}), (src, map) => {{ var u = map.MapNullable<global::System.Uri>(src); return u != null ? (object)new {fqn}(u) : null; }});");
                }
                else if (inner.Name == "Range")
                {
                    sb.AppendLine($"            mapper.CreateIppMap<int, {fqn}>((src, _) => new {fqn}(new global::SharpIpp.Protocol.Models.Range(src, src)));");
                }
                else if (inner is IArrayTypeSymbol arrType)
                {
                    var elemFqn = arrType.ElementType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            mapper.CreateMap<object[], {fqn}>((src, map) => new {fqn}(map.Map<{elemFqn}[]>(src)));");
                }
            }
        }

        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        private static void RegisterCollections(IMapperConstructor mapper)");
        sb.AppendLine("        {");

        var sortedCollectionElements = collectionElementTypes
            .OrderBy(t => t.ToDisplayString())
            .ToList();

        foreach (var type in sortedCollectionElements)
        {
            var fqn = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            sb.AppendLine($"            mapper.CreateCollectionMap<{fqn}>();");
        }

        foreach (var type in sortedEnums)
        {
            var fqn = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var isShort = type.EnumUnderlyingType!.SpecialType == SpecialType.System_Int16;
            var intCast = isShort ? $"({fqn})(short)" : $"({fqn})";
            sb.AppendLine($"            mapper.CreateMap<int[], {fqn}[]>((src, _) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var arr = new {fqn}[src.Length];");
            sb.AppendLine($"                for (int i = 0; i < src.Length; i++) arr[i] = {intCast}src[i];");
            sb.AppendLine("                return arr;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<{fqn}[], int[]>((src, _) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                var arr = new int[src.Length];");
            sb.AppendLine("                for (int i = 0; i < src.Length; i++) arr[i] = (int)src[i];");
            sb.AppendLine("                return arr;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<int[], List<{fqn}>>((src, _) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var list = new List<{fqn}>(src.Length);");
            sb.AppendLine($"                for (int i = 0; i < src.Length; i++) list.Add({intCast}src[i]);");
            sb.AppendLine("                return list;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<List<{fqn}>, int[]>((src, _) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                var arr = new int[src.Count];");
            sb.AppendLine("                for (int i = 0; i < src.Count; i++) arr[i] = (int)src[i];");
            sb.AppendLine("                return arr;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<int[], IEnumerable<{fqn}>>((src, map) => map.Map<{fqn}[]>(src));");
            sb.AppendLine($"            mapper.CreateMap<int[], IReadOnlyCollection<{fqn}>>((src, map) => map.Map<{fqn}[]>(src));");
        }

        var sortedKeywordEnums = keywordEnumTypes
            .OrderBy(t => t.ToDisplayString())
            .ToList();

        foreach (var type in sortedKeywordEnums)
        {
            var fqn = type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var toStr = "src[i].ToString()!";
            sb.AppendLine($"            mapper.CreateMap<string[], {fqn}[]>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var arr = new {fqn}[src.Length];");
            sb.AppendLine($"                for (int i = 0; i < src.Length; i++) arr[i] = map.Map<{fqn}>(src[i]);");
            sb.AppendLine("                return arr;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<{fqn}[], string[]>((src, _) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                var arr = new string[src.Length];");
            sb.AppendLine($"                for (int i = 0; i < src.Length; i++) arr[i] = {toStr};");
            sb.AppendLine("                return arr;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<string[], List<{fqn}>>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                var list = new List<{fqn}>(src.Length);");
            sb.AppendLine($"                for (int i = 0; i < src.Length; i++) list.Add(map.Map<{fqn}>(src[i]));");
            sb.AppendLine("                return list;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<List<{fqn}>, string[]>((src, _) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                var arr = new string[src.Count];");
            sb.AppendLine($"                for (int i = 0; i < src.Count; i++) arr[i] = {toStr};");
            sb.AppendLine("                return arr;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<string[], IEnumerable<{fqn}>>((src, map) => map.Map<{fqn}[]>(src));");
            sb.AppendLine($"            mapper.CreateMap<string[], IReadOnlyCollection<{fqn}>>((src, map) => map.Map<{fqn}[]>(src));");
        }

        sb.AppendLine("        }");

        sb.AppendLine("    }");
        sb.AppendLine("}");

        context.AddSource("GeneratedTypeConverters.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    private static void GenerateMapperRegistry(
        HashSet<INamedTypeSymbol> annotatedModels,
        HashSet<INamedTypeSymbol> annotatedRequests,
        HashSet<INamedTypeSymbol> annotatedResponses,
        List<AnnotatedSectionType> annotatedSections,
        List<ConfiguredMapperType> configuredMappers,
        Dictionary<string, string> nameToConst,
        INamedTypeSymbol? ippAttributeAttrSymbol,
        INamedTypeSymbol? iIppCollectionSymbol,
        SourceProductionContext context)
    {
        var sortedModels = annotatedModels.OrderBy(t => t.ToDisplayString()).ToList();
        var sortedRequests = annotatedRequests.OrderBy(t => t.ToDisplayString()).ToList();
        var sortedResponses = annotatedResponses.OrderBy(t => t.ToDisplayString()).ToList();

        var sortedConfigs = configuredMappers
            .OrderBy(c => c.Order)
            .ThenBy(c => c.TypeSymbol.ToDisplayString())
            .ToList();

        var sb = new StringBuilder();
        sb.AppendLine("// <auto-generated/>");
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using System.Linq;");
        sb.AppendLine("using SharpIpp.Mapping;");
        sb.AppendLine("using SharpIpp.Mapping.Extensions;");
        sb.AppendLine("using SharpIpp.Protocol.Extensions;");
        sb.AppendLine("using SharpIpp.Protocol.Models;");
        sb.AppendLine();
        sb.AppendLine("namespace SharpIpp.Mapping");
        sb.AppendLine("{");
        sb.AppendLine("    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]");
        sb.AppendLine("    [global::System.CodeDom.Compiler.GeneratedCode(\"SharpIpp.Generators\", \"1.0.0.0\")]");
        sb.AppendLine("    internal static partial class GeneratedMapperRegistry");
        sb.AppendLine("    {");
        sb.AppendLine("        public static void RegisterAll(IMapperConstructor mapper)");
        sb.AppendLine("        {");
        sb.AppendLine("            GeneratedTypeConverters.Register(mapper);");
        foreach (var model in sortedModels)
        {
            var fqn = model.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var safeMethodName = GetSafeMethodName(model);
            var collectionNameExpr = GetCollectionAttributeNameExpression(model, ippAttributeAttrSymbol, nameToConst);
            sb.AppendLine($"            mapper.CreateMap<IDictionary<string, IppAttribute[]>, {fqn}>((src, dst, map) => GeneratedModelMappers.Read{safeMethodName}(src, dst, map));");
            sb.AppendLine($"            mapper.CreateMap<Dictionary<string, IppAttribute[]>, {fqn}>((src, dst, map) => GeneratedModelMappers.Read{safeMethodName}(src, dst, map));");
            sb.AppendLine($"            mapper.CreateMap<{fqn}, List<IppAttribute>>((src, dst, map) => GeneratedModelMappers.Write{safeMethodName}(src, dst, map));");
            sb.AppendLine($"            mapper.CreateMap<{fqn}, IEnumerable<IppAttribute>>((src, map) => GeneratedModelMappers.Write{safeMethodName}(src, null, map));");
            sb.AppendLine($"            mapper.CreateMap<{fqn}, IDictionary<string, IppAttribute[]>>((src, map) => map.Map<List<IppAttribute>>(src).ToIppDictionary());");
            sb.AppendLine($"            mapper.CreateMap<{fqn}, Dictionary<string, IppAttribute[]>>((src, map) => map.Map<List<IppAttribute>>(src).ToIppDictionary());");
            sb.AppendLine($"            mapper.CreateMap<List<List<IppAttribute>>, {fqn}[]>((src, map) => src.Select(x => map.Map<{fqn}>(x.ToIppDictionary())).ToArray());");
            sb.AppendLine($"            mapper.CreateMap<{fqn}[], List<List<IppAttribute>>>((src, map) => src.Select(x => map.Map<List<IppAttribute>>(x)).ToList());");
            sb.AppendLine($"            mapper.CreateMap<IDictionary<string, IppAttribute[]>, IppValue<{fqn}>>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                if (src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand()) return default;");
            sb.AppendLine($"                return new IppValue<{fqn}>(map.Map<{fqn}>(src));");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<Dictionary<string, IppAttribute[]>, IppValue<{fqn}>>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                if (src.Count == 1 && src.Values.First().Length == 1 && src.Values.First()[0].Tag.IsOutOfBand()) return default;");
            sb.AppendLine($"                return new IppValue<{fqn}>(map.Map<{fqn}>(src));");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<IppValue<{fqn}>, IEnumerable<IppAttribute>>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                if (!src.IsValue)");
            sb.AppendLine($"                    return new IppAttribute[] {{ new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {collectionNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance) }};");
            sb.AppendLine($"                return map.Map<IEnumerable<IppAttribute>>(src.Value);");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<IppValue<{fqn}>, List<IppAttribute>>((src, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                if (!src.IsValue)");
            sb.AppendLine($"                    return new List<IppAttribute> {{ new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {collectionNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance) }};");
            sb.AppendLine($"                return map.Map<List<IppAttribute>>(src.Value);");
            sb.AppendLine("            });");
        }
        foreach (var request in sortedRequests)
        {
            var fqn = request.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var safeMethodName = GetSafeMethodName(request);
            sb.AppendLine($"            mapper.CreateMap<{fqn}, global::SharpIpp.Protocol.Models.IppRequestMessage>((src, dst, map) => GeneratedModelMappers.Write{safeMethodName}(src, dst, map));");
            sb.AppendLine($"            mapper.CreateMap<{fqn}, global::SharpIpp.Protocol.IIppRequestMessage>((src, dst, map) => GeneratedModelMappers.Write{safeMethodName}(src, (global::SharpIpp.Protocol.Models.IppRequestMessage?)dst, map));");
            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.IIppRequestMessage, {fqn}>((src, dst, map) => GeneratedModelMappers.Read{safeMethodName}(src, dst, map));");
            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.Models.IppRequestMessage, {fqn}>((src, dst, map) => GeneratedModelMappers.Read{safeMethodName}(src, dst, map));");
        }
        foreach (var response in sortedResponses)
        {
            var fqn = response.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var safeMethodName = GetSafeMethodName(response);
            sb.AppendLine($"            mapper.CreateMap<{fqn}, global::SharpIpp.Protocol.Models.IppResponseMessage>((src, dst, map) => GeneratedModelMappers.Write{safeMethodName}(src, dst, map));");
            sb.AppendLine($"            mapper.CreateMap<{fqn}, global::SharpIpp.Protocol.IIppResponseMessage>((src, dst, map) => GeneratedModelMappers.Write{safeMethodName}(src, (global::SharpIpp.Protocol.Models.IppResponseMessage?)dst, map));");
            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.IIppResponseMessage, {fqn}>((src, dst, map) => GeneratedModelMappers.Read{safeMethodName}(src, dst, map));");
            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.Models.IppResponseMessage, {fqn}>((src, dst, map) => GeneratedModelMappers.Read{safeMethodName}(src, dst, map));");
        }
        foreach (var section in annotatedSections.OrderBy(s => s.TypeSymbol.ToDisplayString()))
        {
            var fqn = section.TypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var safeMethodName = GetSafeMethodName(section.TypeSymbol);
            var propName = GetSectionPropertyName(section.SectionTag);
            if (propName == null)
                continue;

            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.IIppResponseMessage, {fqn}>((src, map) =>");
            sb.AppendLine($"                map.Map<IDictionary<string, IppAttribute[]>, {fqn}>(src.{propName}.SelectMany(x => x).ToIppDictionary()));");
            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.Models.IppResponseMessage, {fqn}>((src, map) =>");
            sb.AppendLine($"                map.Map<IDictionary<string, IppAttribute[]>, {fqn}>(src.{propName}.SelectMany(x => x).ToIppDictionary()));");

            sb.AppendLine($"            mapper.CreateMap<{fqn}, global::SharpIpp.Protocol.Models.IppRequestMessage>((src, dst, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine("                dst ??= new global::SharpIpp.Protocol.Models.IppRequestMessage();");
            sb.AppendLine($"                GeneratedModelMappers.Write{safeMethodName}(src, dst.{propName}, map);");
            sb.AppendLine("                return dst;");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.IIppRequestMessage, {fqn}>((src, dst, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                return GeneratedModelMappers.Read{safeMethodName}(src.{propName}.ToIppDictionary(), dst, map);");
            sb.AppendLine("            });");
            sb.AppendLine($"            mapper.CreateMap<global::SharpIpp.Protocol.Models.IppRequestMessage, {fqn}>((src, dst, map) =>");
            sb.AppendLine("            {");
            sb.AppendLine($"                return GeneratedModelMappers.Read{safeMethodName}(src.{propName}.ToIppDictionary(), dst, map);");
            sb.AppendLine("            });");
        }
        sb.AppendLine("            mapper.CreateMap<global::SharpIpp.Protocol.IIppResponse, global::SharpIpp.Protocol.Models.IppResponseMessage>((src, dst, map) =>");
        sb.AppendLine("            {");
        sb.AppendLine("                dst ??= new global::SharpIpp.Protocol.Models.IppResponseMessage();");
        sb.AppendLine("                dst.Version = src.Version;");
        sb.AppendLine("                dst.RequestId = src.RequestId;");
        sb.AppendLine("                dst.StatusCode = src.StatusCode;");
        sb.AppendLine("                if (src.OperationAttributes != null)");
        sb.AppendLine("                {");
        sb.AppendLine("                    dst.OperationAttributes.Add(map.Map<List<IppAttribute>>(src.OperationAttributes));");
        sb.AppendLine("                }");
        sb.AppendLine("                else");
        sb.AppendLine("                {");
        sb.AppendLine("                    dst.OperationAttributes.Add(new List<IppAttribute>");
        sb.AppendLine("                    {");
        sb.AppendLine("                        new IppAttribute(global::SharpIpp.Protocol.Models.Tag.Charset, global::SharpIpp.Protocol.Models.IppAttributeNames.AttributesCharset, \"utf-8\"),");
        sb.AppendLine("                        new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NaturalLanguage, global::SharpIpp.Protocol.Models.IppAttributeNames.AttributesNaturalLanguage, \"en\")");
        sb.AppendLine("                    });");
        sb.AppendLine("                }");
        sb.AppendLine("                return dst;");
        sb.AppendLine("            });");
        sb.AppendLine("            mapper.CreateMap<global::SharpIpp.Protocol.Models.IppResponseMessage, global::SharpIpp.Protocol.IIppResponse>((src, dst, map) =>");
        sb.AppendLine("            {");
        sb.AppendLine("                dst = dst ?? throw new ArgumentNullException(nameof(dst));");
        sb.AppendLine("                dst.Version = src.Version;");
        sb.AppendLine("                dst.RequestId = src.RequestId;");
        sb.AppendLine("                dst.StatusCode = src.StatusCode;");
        sb.AppendLine("                if (src.OperationAttributes.Count > 0)");
        sb.AppendLine("                {");
        sb.AppendLine("                    var opDic = src.OperationAttributes.SelectMany(x => x).ToIppDictionary();");
        sb.AppendLine("                    var statusMessage = map.MapFromDicNullable<string?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.StatusMessage);");
        sb.AppendLine("                    var detailedStatusMessage = map.MapFromDicNullable<string?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.DetailedStatusMessage);");
        sb.AppendLine("                    var documentAccessError = map.MapFromDicNullable<string?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.DocumentAccessError);");
        sb.AppendLine("                    if (statusMessage != null || detailedStatusMessage != null || documentAccessError != null)");
        sb.AppendLine("                    {");
        sb.AppendLine("                        dst.OperationAttributes = map.Map<global::SharpIpp.Models.Responses.OperationAttributes>(opDic);");
        sb.AppendLine("                    }");
        sb.AppendLine("                }");
        sb.AppendLine("                return dst;");
        sb.AppendLine("            });");
        foreach (var config in sortedConfigs)
        {
            var fqn = config.TypeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            sb.AppendLine($"            {fqn}.Configure(mapper);");
        }
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("}");

        context.AddSource("GeneratedMapperRegistry.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    private static void GenerateModelMappers(
        HashSet<INamedTypeSymbol> models,
        HashSet<INamedTypeSymbol> annotatedRequests,
        HashSet<INamedTypeSymbol> annotatedResponses,
        Dictionary<string, string> nameToConst,
        INamedTypeSymbol? ippAttributeAttrSymbol,
        INamedTypeSymbol? ippRequestAttrSymbol,
        INamedTypeSymbol? iIppCollectionSymbol,
        INamedTypeSymbol? iKeywordOrNameEnumSymbol,
        INamedTypeSymbol? iKeywordEnumSymbol,
        INamedTypeSymbol? iIppStructuredStringSymbol,
        INamedTypeSymbol? ippValueGenericSymbol,
        SourceProductionContext context)
    {
        var sb = new StringBuilder();
        sb.AppendLine("// <auto-generated/>");
        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using System.Linq;");
        sb.AppendLine("using SharpIpp.Mapping;");
        sb.AppendLine("using SharpIpp.Mapping.Extensions;");
        sb.AppendLine("using SharpIpp.Protocol.Extensions;");
        sb.AppendLine("using SharpIpp.Protocol.Models;");
        sb.AppendLine();
        sb.AppendLine("namespace SharpIpp.Mapping");
        sb.AppendLine("{");
        sb.AppendLine("    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]");
        sb.AppendLine("    [global::System.CodeDom.Compiler.GeneratedCode(\"SharpIpp.Generators\", \"1.0.0.0\")]");
        sb.AppendLine("    internal static partial class GeneratedModelMappers");
        sb.AppendLine("    {");

        foreach (var model in models.OrderBy(m => m.ToDisplayString()))
        {
            var modelFqn = model.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var safeMethodName = GetSafeMethodName(model);
            bool isCollection = ImplementsOrInherits(model, iIppCollectionSymbol);
            var collectionNameExpr = GetCollectionAttributeNameExpression(model, ippAttributeAttrSymbol, nameToConst);

            bool hasAnnotatedBase = models.Contains(model.BaseType!);
            string? baseSafeMethodName = hasAnnotatedBase ? GetSafeMethodName(model.BaseType!) : null;

            var properties = GetAnnotatedProperties(model, models, ippAttributeAttrSymbol, iIppCollectionSymbol, iKeywordOrNameEnumSymbol, iKeywordEnumSymbol, iIppStructuredStringSymbol, ippValueGenericSymbol);

            // Generate Read
            sb.AppendLine($"        public static {modelFqn} Read{safeMethodName}(");
            sb.AppendLine("            IDictionary<string, IppAttribute[]> src,");
            sb.AppendLine($"            {modelFqn}? dst,");
            sb.AppendLine("            IMapperApplier map)");
            sb.AppendLine("        {");

            sb.AppendLine($"            dst ??= new {modelFqn}();");

            if (hasAnnotatedBase && baseSafeMethodName != null)
            {
                sb.AppendLine($"            Read{baseSafeMethodName}(src, dst, map);");
            }

            foreach (var p in properties)
            {
                var attrNameExpr = GetAttributeNameExpression(p.AttributeName, nameToConst);
                var propTypeFqn = p.Property.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                var readPropTypeFqn = (!p.IsNullable && p.Property.Type.IsValueType) ? $"{propTypeFqn}?" : propTypeFqn;

                if (p.IsIppDictArray)
                {
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine($"                dst.{p.Property.Name} = a_{p.Property.Name}.GroupBegCollection().Select(x => x.FromBegCollection().ToIppDictionary()).ToArray();");
                }
                else if (p.IsCollection)
                {
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine($"                dst.{p.Property.Name} = map.Map<{propTypeFqn}>(a_{p.Property.Name}.FromBegCollection().ToIppDictionary());");
                }
                else if (p.IsCollectionArray)
                {
                    var elemTypeFqn = p.ElementType!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine($"                dst.{p.Property.Name} = a_{p.Property.Name}.GroupBegCollection().Select(x => map.Map<{elemTypeFqn}>(x.FromBegCollection().ToIppDictionary())).ToArray();");
                }
                else if (p.IsStructuredString)
                {
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = null;");
                    sb.AppendLine("                else");
                    sb.AppendLine($"                    dst.{p.Property.Name} = {unwrappedFqn}.Parse(a_{p.Property.Name}.Select(x => x.Value?.ToString() ?? string.Empty));");
                    sb.AppendLine("            }");
                }
                else if (p.IsStructuredStringArray)
                {
                    var elemTypeFqn = p.ElementType!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            var a_{p.Property.Name} = map.MapFromDicSetNullable<{elemTypeFqn}[]>(src, {attrNameExpr});");
                    sb.AppendLine($"            if (a_{p.Property.Name} != null && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine($"                dst.{p.Property.Name} = a_{p.Property.Name};");
                }
                else if (p.IsKeywordOrNameEnum)
                {
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            dst.{p.Property.Name} = map.MapFromDicNullable<string, {readPropTypeFqn}>(src, {attrNameExpr}, (attribute, value) => new {unwrappedFqn}(value, attribute.Tag == global::SharpIpp.Protocol.Models.Tag.Keyword)) ?? dst.{p.Property.Name};");
                }
                else if (p.IsKeywordEnum)
                {
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            dst.{p.Property.Name} = map.MapFromDicNullable<string, {readPropTypeFqn}>(src, {attrNameExpr}, (attribute, value) => new {unwrappedFqn}(value)) ?? dst.{p.Property.Name};");
                }
                else if (p.IsKeywordOrNameEnumArray)
                {
                    var elemTypeFqn = p.ElementType!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            var a_{p.Property.Name} = map.MapFromDicSetNullable<string, {elemTypeFqn}>(src, {attrNameExpr}, (attribute, value) => new {elemTypeFqn}(value, attribute.Tag == global::SharpIpp.Protocol.Models.Tag.Keyword));");
                    sb.AppendLine($"            if (a_{p.Property.Name} != null && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine($"                dst.{p.Property.Name} = a_{p.Property.Name};");
                }
                else if (p.IsKeywordEnumArray)
                {
                    var elemTypeFqn = p.ElementType!.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            var a_{p.Property.Name} = map.MapFromDicSetNullable<string, {elemTypeFqn}>(src, {attrNameExpr}, (attribute, value) => new {elemTypeFqn}(value));");
                    sb.AppendLine($"            if (a_{p.Property.Name} != null && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine($"                dst.{p.Property.Name} = a_{p.Property.Name};");
                }
                else if (p.IsIppValue && p.IppValueInnerType != null && iKeywordOrNameEnumSymbol != null && ImplementsOrInherits(p.IppValueInnerType, iKeywordOrNameEnumSymbol))
                {
                    var innerFqn = p.IppValueInnerType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = default({unwrappedFqn});");
                    sb.AppendLine("                else");
                    sb.AppendLine($"                    dst.{p.Property.Name} = new {unwrappedFqn}(new {innerFqn}(a_{p.Property.Name}[0].Value?.ToString() ?? string.Empty, a_{p.Property.Name}[0].Tag == global::SharpIpp.Protocol.Models.Tag.Keyword));");
                    sb.AppendLine("            }");
                }
                else if (p.IsIppValue && p.IppValueInnerType != null && iKeywordEnumSymbol != null && ImplementsOrInherits(p.IppValueInnerType, iKeywordEnumSymbol))
                {
                    var innerFqn = p.IppValueInnerType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = default({unwrappedFqn});");
                    sb.AppendLine("                else");
                    sb.AppendLine($"                    dst.{p.Property.Name} = new {unwrappedFqn}(new {innerFqn}(a_{p.Property.Name}[0].Value?.ToString() ?? string.Empty));");
                    sb.AppendLine("            }");
                }
                else if (p.IsIppValue && p.IppValueInnerType != null && iIppStructuredStringSymbol != null && ImplementsOrInherits(p.IppValueInnerType, iIppStructuredStringSymbol))
                {
                    var innerFqn = p.IppValueInnerType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    bool hasEnumerableParse = p.IppValueInnerType.GetMembers("Parse").OfType<IMethodSymbol>().Any(m => m.Parameters.Length == 1 && m.Parameters[0].Type.Name == "IEnumerable");
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = default({unwrappedFqn});");
                    sb.AppendLine("                else");
                    if (hasEnumerableParse)
                        sb.AppendLine($"                    dst.{p.Property.Name} = new {unwrappedFqn}({innerFqn}.Parse(a_{p.Property.Name}.Select(x => x.Value?.ToString() ?? string.Empty)));");
                    else
                        sb.AppendLine($"                    dst.{p.Property.Name} = new {unwrappedFqn}({innerFqn}.Parse(a_{p.Property.Name}[0].Value?.ToString() ?? string.Empty));");
                    sb.AppendLine("            }");
                }
                else if (p.IsIppValue && p.IppValueInnerType != null && iIppCollectionSymbol != null && ImplementsOrInherits(p.IppValueInnerType, iIppCollectionSymbol))
                {
                    var innerFqn = p.IppValueInnerType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = default({unwrappedFqn});");
                    sb.AppendLine("                else");
                    sb.AppendLine($"                    dst.{p.Property.Name} = new {unwrappedFqn}(map.Map<{innerFqn}>(a_{p.Property.Name}.FromBegCollection().ToIppDictionary()));");
                    sb.AppendLine("            }");
                }
                else if (p.IsIppValueArray)
                {
                    var elem = p.IppValueElementType!;
                    var elemTypeFqn = elem.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    var unwrappedFqn = p.UnwrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    bool isElemKeywordOrNameEnum = ImplementsOrInherits(elem, iKeywordOrNameEnumSymbol);
                    bool isElemKeywordEnum = ImplementsOrInherits(elem, iKeywordEnumSymbol) && !isElemKeywordOrNameEnum;
                    bool isElemCollection = ImplementsOrInherits(elem, iIppCollectionSymbol);

                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = default({unwrappedFqn});");
                    sb.AppendLine("                else");
                    sb.AppendLine("                {");
                    if (isElemKeywordOrNameEnum)
                    {
                        sb.AppendLine($"                    var arr_{p.Property.Name} = map.MapFromDicSetNullable<string, {elemTypeFqn}>(src, {attrNameExpr}, (attribute, value) => new {elemTypeFqn}(value, attribute.Tag == global::SharpIpp.Protocol.Models.Tag.Keyword));");
                        sb.AppendLine($"                    if (arr_{p.Property.Name} != null)");
                        sb.AppendLine($"                        dst.{p.Property.Name} = new {unwrappedFqn}(arr_{p.Property.Name});");
                    }
                    else if (isElemKeywordEnum)
                    {
                        sb.AppendLine($"                    var arr_{p.Property.Name} = map.MapFromDicSetNullable<string, {elemTypeFqn}>(src, {attrNameExpr}, (attribute, value) => new {elemTypeFqn}(value));");
                        sb.AppendLine($"                    if (arr_{p.Property.Name} != null)");
                        sb.AppendLine($"                        dst.{p.Property.Name} = new {unwrappedFqn}(arr_{p.Property.Name});");
                    }
                    else if (isElemCollection)
                    {
                        sb.AppendLine($"                    dst.{p.Property.Name} = new {unwrappedFqn}(a_{p.Property.Name}.GroupBegCollection().Select(x => map.Map<{elemTypeFqn}>(x.FromBegCollection().ToIppDictionary())).ToArray());");
                    }
                    else
                    {
                        sb.AppendLine($"                    var arr_{p.Property.Name} = map.MapFromDicSetNullable<{elemTypeFqn}[]>(src, {attrNameExpr});");
                        sb.AppendLine($"                    if (arr_{p.Property.Name} != null && arr_{p.Property.Name}.Length > 0)");
                        sb.AppendLine($"                        dst.{p.Property.Name} = new {unwrappedFqn}(arr_{p.Property.Name});");
                    }
                    sb.AppendLine("                }");
                    sb.AppendLine("            }");
                }
                else if (p.IsArray)
                {
                    sb.AppendLine($"            var a_{p.Property.Name} = map.MapFromDicSetNullable<{propTypeFqn}>(src, {attrNameExpr});");
                    sb.AppendLine($"            if (a_{p.Property.Name} != null && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine($"                dst.{p.Property.Name} = a_{p.Property.Name};");
                }
                else if (p.IsStringWithLanguage)
                {
                    // StringWithLanguage — accept both WithoutLanguage (string) and WithLanguage (StringWithLanguage) tags
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = default(global::SharpIpp.Protocol.Models.StringWithLanguage);");
                    sb.AppendLine($"                else if (a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.StringWithLanguage swl_{p.Property.Name})");
                    sb.AppendLine($"                    dst.{p.Property.Name} = swl_{p.Property.Name};");
                    sb.AppendLine("                else");
                    sb.AppendLine($"                    dst.{p.Property.Name} = new global::SharpIpp.Protocol.Models.StringWithLanguage(null, a_{p.Property.Name}[0].Value?.ToString() ?? string.Empty);");
                    sb.AppendLine("            }");
                }
                else if (p.IsOctetString)
                {
                    sb.AppendLine($"            if (src.TryGetValue({attrNameExpr}, out var a_{p.Property.Name}) && a_{p.Property.Name}.Length > 0)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                if (a_{p.Property.Name}.Length == 1 && (a_{p.Property.Name}[0].Tag.IsOutOfBand() || a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.NoValue))");
                    sb.AppendLine($"                    dst.{p.Property.Name} = default(global::SharpIpp.Protocol.Models.OctetString);");
                    sb.AppendLine($"                else if (a_{p.Property.Name}[0].Value is global::SharpIpp.Protocol.Models.OctetString os_{p.Property.Name})");
                    sb.AppendLine($"                    dst.{p.Property.Name} = os_{p.Property.Name};");
                    sb.AppendLine($"                else if (a_{p.Property.Name}[0].Value is byte[] bytes_{p.Property.Name})");
                    sb.AppendLine($"                    dst.{p.Property.Name} = new global::SharpIpp.Protocol.Models.OctetString(bytes_{p.Property.Name});");
                    sb.AppendLine("                else");
                    sb.AppendLine($"                    dst.{p.Property.Name} = new global::SharpIpp.Protocol.Models.OctetString(a_{p.Property.Name}[0].Value?.ToString() ?? string.Empty);");
                    sb.AppendLine("            }");
                }
                else
                {
                    sb.AppendLine($"            dst.{p.Property.Name} = map.MapFromDicNullable<{readPropTypeFqn}>(src, {attrNameExpr}) ?? dst.{p.Property.Name};");
                }
            }

            sb.AppendLine("            return dst;");
            sb.AppendLine("        }");
            sb.AppendLine();

            // Generate Write
            sb.AppendLine($"        public static List<IppAttribute> Write{safeMethodName}(");
            sb.AppendLine($"            {modelFqn} src,");
            sb.AppendLine("            List<IppAttribute>? dst,");
            sb.AppendLine("            IMapperApplier map)");
            sb.AppendLine("        {");

            sb.AppendLine("            dst ??= new List<IppAttribute>();");

            if (hasAnnotatedBase && baseSafeMethodName != null)
            {
                sb.AppendLine($"            Write{baseSafeMethodName}(src, dst, map);");
            }

            foreach (var p in properties)
            {
                var attrNameExpr = GetAttributeNameExpression(p.AttributeName, nameToConst);
                var tagExpr = $"global::SharpIpp.Protocol.Models.Tag.{p.Tag}";

                if (p.DefaultValue != null)
                {
                    var access = p.IsNullable ? $"src.{p.Property.Name}?.ToString() ?? \"{p.DefaultValue}\"" : $"src.{p.Property.Name}.ToString()";
                    sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, ({access})!));");
                }
                else if (p.IsIppValueArray)
                {
                    var elem = p.IppValueElementType!;
                    bool isElemEnum = elem.TypeKind == TypeKind.Enum;
                    bool isElemUri = elem.Name == "Uri";
                    bool isElemStringWithLanguage = elem.Name == "StringWithLanguage";
                    bool isElemKeywordOrNameEnum = ImplementsOrInherits(elem, iKeywordOrNameEnumSymbol);
                    bool isElemKeywordEnum = ImplementsOrInherits(elem, iKeywordEnumSymbol) && !isElemKeywordOrNameEnum;
                    bool isElemStructuredString = ImplementsOrInherits(elem, iIppStructuredStringSymbol);
                    bool isElemCollection = ImplementsOrInherits(elem, iIppCollectionSymbol);

                    sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                var val_{p.Property.Name} = src.{p.Property.Name}.Value;");
                    sb.AppendLine($"                if (!val_{p.Property.Name}.IsValue)");
                    sb.AppendLine($"                    dst.Add(new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {attrNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance));");
                    sb.AppendLine($"                else if (val_{p.Property.Name}.Value != null)");
                    sb.AppendLine("                {");
                    if (isElemKeywordOrNameEnum)
                    {
                        sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute(x.ToIppTag(), {attrNameExpr}, x.ToString()!)));");
                    }
                    else if (isElemStringWithLanguage)
                    {
                        sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute(x.ToIppTag({tagExpr}), {attrNameExpr}, x)));");
                    }
                    else if (isElemKeywordEnum)
                    {
                        sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x.ToString()!)));");
                    }
                    else if (isElemStructuredString)
                    {
                        sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, new OctetString(x.ToString()))));");
                    }
                    else if (isElemCollection)
                    {
                        sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection({attrNameExpr})));");
                    }
                    else if (isElemEnum)
                    {
                        if (elem.Name == "Finishings")
                        {
                            sb.AppendLine($"                    var arr_{p.Property.Name} = val_{p.Property.Name}.Value.Length > 1 ? val_{p.Property.Name}.Value.Where(x => x != global::SharpIpp.Protocol.Models.Finishings.None).ToArray() : val_{p.Property.Name}.Value;");
                            sb.AppendLine($"                    dst.AddRange(arr_{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, (int)x)));");
                        }
                        else
                        {
                            sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, (int)x)));");
                        }
                    }
                    else if (isElemUri)
                    {
                        sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x.ToString())));");
                    }
                    else
                    {
                        sb.AppendLine($"                    dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                    }
                    sb.AppendLine("                }");
                    sb.AppendLine("            }");
                }
                else if (p.IsIppValue)
                {
                    var inner = p.IppValueInnerType!;
                    bool isInnerValueType = inner.IsValueType;
                    bool isInnerEnum = inner.TypeKind == TypeKind.Enum;
                    bool isInnerUri = inner.Name == "Uri";
                    bool isInnerRange = inner.Name == "Range";
                    bool isInnerStringWithLanguage = inner.Name == "StringWithLanguage";
                    bool isInnerKeywordOrNameEnum = ImplementsOrInherits(inner, iKeywordOrNameEnumSymbol);
                    bool isInnerKeywordEnum = ImplementsOrInherits(inner, iKeywordEnumSymbol) && !isInnerKeywordOrNameEnum;
                    bool isInnerStructuredString = ImplementsOrInherits(inner, iIppStructuredStringSymbol);
                    bool isInnerCollection = ImplementsOrInherits(inner, iIppCollectionSymbol);

                    sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                    sb.AppendLine("            {");
                    sb.AppendLine($"                var val_{p.Property.Name} = src.{p.Property.Name}.Value;");
                    sb.AppendLine($"                if (!val_{p.Property.Name}.IsValue)");
                    sb.AppendLine($"                    dst.Add(new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {attrNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance));");
                    sb.AppendLine("                else");
                    sb.AppendLine("                {");
                    if (isInnerKeywordOrNameEnum)
                    {
                        if (isInnerValueType)
                            sb.AppendLine($"                    dst.Add(new IppAttribute(val_{p.Property.Name}.Value.ToIppTag(), {attrNameExpr}, val_{p.Property.Name}.Value.ToString()!));");
                        else
                        {
                            sb.AppendLine($"                    if (val_{p.Property.Name}.Value != null)");
                            sb.AppendLine($"                        dst.Add(new IppAttribute(val_{p.Property.Name}.Value.ToIppTag(), {attrNameExpr}, val_{p.Property.Name}.Value.ToString()!));");
                        }
                    }
                    else if (isInnerStringWithLanguage)
                    {
                        sb.AppendLine($"                    dst.Add(new IppAttribute(val_{p.Property.Name}.Value.ToIppTag({tagExpr}), {attrNameExpr}, val_{p.Property.Name}.Value));");
                    }
                    else if (isInnerKeywordEnum)
                    {
                        if (isInnerValueType)
                            sb.AppendLine($"                    dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, val_{p.Property.Name}.Value.ToString()!));");
                        else
                        {
                            sb.AppendLine($"                    if (val_{p.Property.Name}.Value != null)");
                            sb.AppendLine($"                        dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, val_{p.Property.Name}.Value.ToString()!));");
                        }
                    }
                    else if (isInnerStructuredString)
                    {
                        sb.AppendLine($"                    if (val_{p.Property.Name}.Value != null)");
                        sb.AppendLine($"                        dst.AddRange(val_{p.Property.Name}.Value.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, new OctetString(x))));");
                    }
                    else if (isInnerCollection)
                    {
                        sb.AppendLine($"                    if (val_{p.Property.Name}.Value != null)");
                        sb.AppendLine($"                        dst.AddRange(map.Map<IEnumerable<IppAttribute>>(val_{p.Property.Name}.Value).ToBegCollection({attrNameExpr}));");
                    }
                    else if (isInnerEnum)
                    {
                        sb.AppendLine($"                    dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, (int)val_{p.Property.Name}.Value));");
                    }
                    else if (isInnerUri)
                    {
                        sb.AppendLine($"                    if (val_{p.Property.Name}.Value != null)");
                        sb.AppendLine($"                        dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, val_{p.Property.Name}.Value.ToString()));");
                    }
                    else if (isInnerRange)
                    {
                        if (p.HasExplicitTag)
                        {
                            sb.AppendLine($"                    dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, val_{p.Property.Name}.Value));");
                        }
                        else
                        {
                            sb.AppendLine($"                    var r_{p.Property.Name} = val_{p.Property.Name}.Value;");
                            sb.AppendLine($"                    dst.Add(r_{p.Property.Name}.Lower == r_{p.Property.Name}.Upper");
                            sb.AppendLine($"                        ? new IppAttribute(global::SharpIpp.Protocol.Models.Tag.Integer, {attrNameExpr}, r_{p.Property.Name}.Lower)");
                            sb.AppendLine($"                        : new IppAttribute(global::SharpIpp.Protocol.Models.Tag.RangeOfInteger, {attrNameExpr}, r_{p.Property.Name}));");
                        }
                    }
                    else
                    {
                        if (isInnerValueType)
                            sb.AppendLine($"                    dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, val_{p.Property.Name}.Value));");
                        else
                        {
                            sb.AppendLine($"                    if (val_{p.Property.Name}.Value != null)");
                            sb.AppendLine($"                        dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, val_{p.Property.Name}.Value));");
                        }
                    }
                    sb.AppendLine("                }");
                    sb.AppendLine("            }");
                }
                else if (p.IsIppDictArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.SelectMany(x => x.Values.SelectMany(y => y).ToBegCollection({attrNameExpr})));");
                }
                else if (p.IsCollection)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.{p.Property.Name}).ToBegCollection({attrNameExpr}));");
                }
                else if (p.IsCollectionArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection({attrNameExpr})));");
                }
                else if (p.IsStructuredString)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, new OctetString(x))));");
                }
                else if (p.IsStructuredStringArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, new OctetString(x.ToString()))));");
                }
                else if (p.IsKeywordOrNameEnum)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine("            {");
                        sb.AppendLine($"                var val_{p.Property.Name} = src.{p.Property.Name}.Value;");
                        sb.AppendLine($"                dst.Add(new IppAttribute(val_{p.Property.Name}.ToIppTag(), {attrNameExpr}, val_{p.Property.Name}.ToString()!));");
                        sb.AppendLine("            }");
                    }
                    else
                    {
                        sb.AppendLine($"            var val_{p.Property.Name} = src.{p.Property.Name};");
                        sb.AppendLine($"            dst.Add(new IppAttribute(val_{p.Property.Name}.ToIppTag(), {attrNameExpr}, val_{p.Property.Name}.ToString()!));");
                    }
                }
                else if (p.IsKeywordOrNameEnumArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute(x.ToIppTag(), {attrNameExpr}, x.ToString()!)));");
                }
                else if (p.IsKeywordEnum)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.Value.ToString()!));");
                    }
                    else
                    {
                        sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.ToString()!));");
                    }
                }
                else if (p.IsKeywordEnumArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x.ToString()!)));");
                }
                else if (p.IsString)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}));");
                }
                else if (p.IsStringArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                }
                else if (p.IsInt)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.Value));");
                    }
                    else
                    {
                        sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}));");
                    }
                }
                else if (p.IsIntArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                }
                else if (p.IsBool)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.Value));");
                    }
                    else
                    {
                        sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}));");
                    }
                }
                else if (p.IsBoolArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                }
                else if (p.IsDateTimeOffset)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.Value));");
                    }
                    else
                    {
                        sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}));");
                    }
                }
                else if (p.IsDateTimeOffsetArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                }
                else if (p.IsUri)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.ToString()));");
                }
                else if (p.IsUriArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x.ToString())));");
                }
                else if (p.IsEnum)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, (int)src.{p.Property.Name}.Value));");
                    }
                    else
                    {
                        sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, (int)src.{p.Property.Name}));");
                    }
                }
                else if (p.IsEnumArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine("            {");
                    if (p.ElementType!.Name == "Finishings")
                    {
                        sb.AppendLine($"                var arr_{p.Property.Name} = src.{p.Property.Name}.Length > 1 ? src.{p.Property.Name}.Where(x => x != global::SharpIpp.Protocol.Models.Finishings.None).ToArray() : src.{p.Property.Name};");
                        sb.AppendLine($"                dst.AddRange(arr_{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, (int)x)));");
                    }
                    else
                    {
                        sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, (int)x)));");
                    }
                    sb.AppendLine("            }");
                }
                else if (p.IsRange)
                {
                    if (p.HasExplicitTag)
                    {
                        if (p.IsNullable)
                        {
                            sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                            sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.Value));");
                        }
                        else
                        {
                            sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}));");
                        }
                    }
                    else
                    {
                        if (p.IsNullable)
                        {
                            sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                            sb.AppendLine("            {");
                            sb.AppendLine($"                var r_{p.Property.Name} = src.{p.Property.Name}.Value;");
                            sb.AppendLine($"                dst.Add(r_{p.Property.Name}.Lower == r_{p.Property.Name}.Upper");
                            sb.AppendLine($"                    ? new IppAttribute(global::SharpIpp.Protocol.Models.Tag.Integer, {attrNameExpr}, r_{p.Property.Name}.Lower)");
                            sb.AppendLine($"                    : new IppAttribute(global::SharpIpp.Protocol.Models.Tag.RangeOfInteger, {attrNameExpr}, r_{p.Property.Name}));");
                            sb.AppendLine("            }");
                        }
                        else
                        {
                            sb.AppendLine($"            dst.Add(src.{p.Property.Name}.Lower == src.{p.Property.Name}.Upper");
                            sb.AppendLine($"                ? new IppAttribute(global::SharpIpp.Protocol.Models.Tag.Integer, {attrNameExpr}, src.{p.Property.Name}.Lower)");
                            sb.AppendLine($"                : new IppAttribute(global::SharpIpp.Protocol.Models.Tag.RangeOfInteger, {attrNameExpr}, src.{p.Property.Name}));");
                        }
                    }
                }
                else if (p.IsRangeArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                }
                else if (p.IsResolution)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}.Value));");
                    }
                    else
                    {
                        sb.AppendLine($"            dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}));");
                    }
                }
                else if (p.IsResolutionArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                }
                else if (p.IsOctetString)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine("            {");
                        sb.AppendLine($"                var val_{p.Property.Name} = src.{p.Property.Name}.Value;");
                        sb.AppendLine($"                if (!val_{p.Property.Name}.IsValue)");
                        sb.AppendLine($"                    dst.Add(new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {attrNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance));");
                        sb.AppendLine("                else");
                        sb.AppendLine($"                    dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, val_{p.Property.Name}));");
                        sb.AppendLine("            }");
                    }
                    else
                    {
                        sb.AppendLine($"            if (!src.{p.Property.Name}.IsValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {attrNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance));");
                        sb.AppendLine("            else");
                        sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, src.{p.Property.Name}));");
                    }
                }
                else if (p.IsOctetStringArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x)));");
                }
                else if (p.IsStringWithLanguage)
                {
                    if (p.IsNullable)
                    {
                        sb.AppendLine($"            if (src.{p.Property.Name}.HasValue)");
                        sb.AppendLine("            {");
                        sb.AppendLine($"                var val_{p.Property.Name} = src.{p.Property.Name}.Value;");
                        sb.AppendLine($"                if (!val_{p.Property.Name}.IsValue)");
                        sb.AppendLine($"                    dst.Add(new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {attrNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance));");
                        sb.AppendLine("                else");
                        sb.AppendLine($"                    dst.Add(new IppAttribute(val_{p.Property.Name}.ToIppTag({tagExpr}), {attrNameExpr}, val_{p.Property.Name}));");
                        sb.AppendLine("            }");
                    }
                    else
                    {
                        sb.AppendLine($"            if (!src.{p.Property.Name}.IsValue)");
                        sb.AppendLine($"                dst.Add(new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NoValue, {attrNameExpr}, global::SharpIpp.Protocol.Models.NoValue.Instance));");
                        sb.AppendLine("            else");
                        sb.AppendLine($"                dst.Add(new IppAttribute(src.{p.Property.Name}.ToIppTag({tagExpr}), {attrNameExpr}, src.{p.Property.Name}));");
                    }
                }
                else if (p.IsStringWithLanguageArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute(x.ToIppTag({tagExpr}), {attrNameExpr}, x)));");
                }
                else if (p.IsArray)
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.AddRange(src.{p.Property.Name}.Select(x => new IppAttribute({tagExpr}, {attrNameExpr}, x.ToString()!)));");
                }
                else
                {
                    sb.AppendLine($"            if (src.{p.Property.Name} != null)");
                    sb.AppendLine($"                dst.Add(new IppAttribute({tagExpr}, {attrNameExpr}, map.Map<string>(src.{p.Property.Name})));");
                }
            }

            sb.AppendLine("            return dst;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        GenerateRequestMappers(annotatedRequests, ippRequestAttrSymbol, sb);
        GenerateResponseMappers(annotatedResponses, sb);

        sb.AppendLine("    }");
        sb.AppendLine("}");

        context.AddSource("GeneratedModelMappers.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
    }

    private static void GenerateRequestMappers(
        HashSet<INamedTypeSymbol> annotatedRequests,
        INamedTypeSymbol? ippRequestAttrSymbol,
        StringBuilder sb)
    {
        foreach (var request in annotatedRequests.OrderBy(t => t.ToDisplayString()))
        {
            var fqn = request.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var safeMethodName = GetSafeMethodName(request);
            var attr = GetAttribute(request, ippRequestAttrSymbol!);
            if (attr!.ConstructorArguments.Length == 0)
                continue;

            string opExpr;
            var enumType = attr.ConstructorArguments[0].Type as INamedTypeSymbol;
            var val = attr.ConstructorArguments[0].Value;
            string? foundName = null;
            if (enumType != null)
            {
                foreach (var member in enumType.GetMembers())
                {
                    if (member is IFieldSymbol field && field.HasConstantValue && Equals(field.ConstantValue, val))
                    {
                        foundName = field.Name;
                        break;
                    }
                }
            }
            if (foundName != null)
            {
                opExpr = $"global::SharpIpp.Protocol.Models.IppOperation.{foundName}";
            }
            else
            {
                opExpr = $"((global::SharpIpp.Protocol.Models.IppOperation){val})";
            }

            var allProps = new List<IPropertySymbol>();
            foreach (var curr in GetTypeAndBaseTypes(request))
            {
                foreach (var member in curr.GetMembers())
                {
                    if (member is IPropertySymbol prop && !allProps.Any(p => p.Name == prop.Name))
                    {
                        allProps.Add(prop);
                    }
                }
            }

            var opAttrProp = allProps.FirstOrDefault(p => p.Name == "OperationAttributes");
            var hasJobTemplate = allProps.Any(p => p.Name == "JobTemplateAttributes");
            var hasDocTemplate = allProps.Any(p => p.Name == "DocumentTemplateAttributes");
            var hasDocDesc = allProps.Any(p => p.Name == "DocumentDescriptionAttributes");
            var hasPrinterAttrs = allProps.Any(p => p.Name == "PrinterAttributes");
            var hasJobAttrs = allProps.Any(p => p.Name == "JobAttributes");
            var hasDocAttrs = allProps.Any(p => p.Name == "DocumentAttributes");
            var hasDoc = allProps.Any(p => p.Name == "Document" && p.Type.Name == "Stream");

            // Write method
            sb.AppendLine($"        public static global::SharpIpp.Protocol.Models.IppRequestMessage Write{safeMethodName}(");
            sb.AppendLine($"            {fqn} src,");
            sb.AppendLine("            global::SharpIpp.Protocol.Models.IppRequestMessage? dst,");
            sb.AppendLine("            IMapperApplier map)");
            sb.AppendLine("        {");
            sb.AppendLine("            dst ??= new global::SharpIpp.Protocol.Models.IppRequestMessage();");
            sb.AppendLine($"            dst.IppOperation = {opExpr};");
            sb.AppendLine("            dst.Version = src.Version;");
            sb.AppendLine("            dst.RequestId = src.RequestId;");
            if (hasDoc)
            {
                sb.AppendLine("            dst.Document = src.Document;");
            }
            if (opAttrProp != null)
            {
                sb.AppendLine("            if (src.OperationAttributes != null)");
                sb.AppendLine("                dst.OperationAttributes.AddRange(map.Map<List<IppAttribute>>(src.OperationAttributes));");
            }
            if (hasJobTemplate)
            {
                sb.AppendLine("            if (src.JobTemplateAttributes != null)");
                sb.AppendLine("                dst.JobAttributes.AddRange(map.Map<List<IppAttribute>>(src.JobTemplateAttributes));");
            }
            if (hasDocTemplate)
            {
                sb.AppendLine("            if (src.DocumentTemplateAttributes != null)");
                sb.AppendLine("                dst.DocumentAttributes.AddRange(map.Map<List<IppAttribute>>(src.DocumentTemplateAttributes));");
            }
            if (hasDocDesc)
            {
                sb.AppendLine("            if (src.DocumentDescriptionAttributes?.DocumentName != null)");
                sb.AppendLine("                dst.DocumentAttributes.Add(new IppAttribute(global::SharpIpp.Protocol.Models.Tag.NameWithoutLanguage, global::SharpIpp.Protocol.Models.IppAttributeNames.DocumentName, src.DocumentDescriptionAttributes.DocumentName));");
            }
            if (hasPrinterAttrs)
            {
                sb.AppendLine("            if (src.PrinterAttributes != null)");
                sb.AppendLine("                dst.PrinterAttributes.AddRange(map.Map<List<IppAttribute>>(src.PrinterAttributes));");
            }
            if (hasJobAttrs)
            {
                sb.AppendLine("            if (src.JobAttributes != null)");
                sb.AppendLine("                dst.JobAttributes.AddRange(map.Map<List<IppAttribute>>(src.JobAttributes));");
            }
            if (hasDocAttrs)
            {
                sb.AppendLine("            if (src.DocumentAttributes != null)");
                sb.AppendLine("                dst.DocumentAttributes.AddRange(map.Map<List<IppAttribute>>(src.DocumentAttributes));");
            }
            sb.AppendLine("            return dst;");
            sb.AppendLine("        }");
            sb.AppendLine();

            // Read method
            sb.AppendLine($"        public static {fqn} Read{safeMethodName}(");
            sb.AppendLine("            global::SharpIpp.Protocol.IIppRequestMessage src,");
            sb.AppendLine($"            {fqn}? dst,");
            sb.AppendLine("            IMapperApplier map)");
            sb.AppendLine("        {");
            sb.AppendLine($"            dst ??= new {fqn}();");
            sb.AppendLine("            dst.Version = src.Version;");
            sb.AppendLine("            dst.RequestId = src.RequestId;");
            if (hasDoc)
            {
                sb.AppendLine("            dst.Document = src.Document;");
            }
            if (opAttrProp != null)
            {
                var opAttrsFqn = opAttrProp.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                sb.AppendLine($"            dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, {opAttrsFqn}>(src.OperationAttributes.ToIppDictionary());");
            }
            if (hasJobTemplate)
            {
                sb.AppendLine("            dst.JobTemplateAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.JobTemplateAttributes>(src.JobAttributes.ToIppDictionary());");
            }
            if (hasDocTemplate)
            {
                sb.AppendLine("            if (src.DocumentAttributes.Any())");
                sb.AppendLine("                dst.DocumentTemplateAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.DocumentTemplateAttributes>(src.DocumentAttributes.ToIppDictionary());");
            }
            if (hasDocDesc)
            {
                sb.AppendLine("            if (src.DocumentAttributes.Any())");
                sb.AppendLine("            {");
                sb.AppendLine("                var docDict = src.DocumentAttributes.ToIppDictionary();");
                sb.AppendLine("                var docName = map.MapFromDicNullable<string?>(docDict, global::SharpIpp.Protocol.Models.IppAttributeNames.DocumentName);");
                sb.AppendLine("                if (docName != null)");
                sb.AppendLine("                {");
                sb.AppendLine("                    dst.DocumentDescriptionAttributes = new global::SharpIpp.Protocol.Models.DocumentDescriptionAttributes");
                sb.AppendLine("                    {");
                sb.AppendLine("                        DocumentName = docName");
                sb.AppendLine("                    };");
                sb.AppendLine("                }");
                sb.AppendLine("            }");
            }
            if (hasPrinterAttrs)
            {
                sb.AppendLine("            if (src.PrinterAttributes.Any())");
                sb.AppendLine("                dst.PrinterAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.PrinterDescriptionAttributes>(src.PrinterAttributes.ToIppDictionary());");
            }
            if (hasJobAttrs)
            {
                sb.AppendLine("            if (src.JobAttributes.Any())");
                sb.AppendLine("                dst.JobAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.JobStatusAttributes>(src.JobAttributes.ToIppDictionary());");
            }
            if (hasDocAttrs)
            {
                sb.AppendLine("            if (src.DocumentAttributes.Any())");
                sb.AppendLine("                dst.DocumentAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.DocumentAttributes>(src.DocumentAttributes.ToIppDictionary());");
            }
            sb.AppendLine("            return dst;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }
    }

    private static void GenerateResponseMappers(
        HashSet<INamedTypeSymbol> annotatedResponses,
        StringBuilder sb)
    {
        foreach (var response in annotatedResponses.OrderBy(t => t.ToDisplayString()))
        {
            var fqn = response.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var safeMethodName = GetSafeMethodName(response);
            var allProps = new List<IPropertySymbol>();
            foreach (var curr in GetTypeAndBaseTypes(response))
            {
                foreach (var member in curr.GetMembers())
                {
                    if (member is IPropertySymbol prop && !allProps.Any(p => p.Name == prop.Name))
                    {
                        allProps.Add(prop);
                    }
                }
            }

            var opAttrProp = allProps.FirstOrDefault(p => p.Name == "OperationAttributes");
            var hasJobAttrs = allProps.Any(p => p.Name == "JobAttributes" && p.Type.Name == "JobAttributes");
            var hasJobDescAttrs = allProps.Any(p => p.Name == "JobAttributes" && p.Type.Name == "JobDescriptionAttributes");
            var hasJobsAttrs = allProps.Any(p => p.Name == "JobsAttributes");
            var hasDocAttrs = allProps.Any(p => p.Name == "DocumentAttributes");
            var hasDocs = allProps.Any(p => p.Name == "Documents");
            var hasPrinterAttrs = allProps.Any(p => p.Name == "PrinterAttributes");
            var hasPrintersAttrs = allProps.Any(p => p.Name == "PrintersAttributes");
            var hasPrinterResourceIds = allProps.Any(p => p.Name == "PrinterResourceIds");
            var hasResourceAttrs = allProps.Any(p => p.Name == "ResourceAttributes");
            var hasResourcesAttrs = allProps.Any(p => p.Name == "ResourcesAttributes");
            var hasSystemAttrs = allProps.Any(p => p.Name == "SystemAttributes");
            var hasSystemDescAttrs = allProps.Any(p => p.Name == "SystemDescriptionAttributes");
            var hasSubscriptionsAttrs = allProps.Any(p => p.Name == "SubscriptionsAttributes");
            var hasSubscriptionAttrs = allProps.Any(p => p.Name == "SubscriptionAttributes");
            var hasJobIds = allProps.Any(p => p.Name == "JobIds");
            var hasOutputDeviceJobStates = allProps.Any(p => p.Name == "OutputDeviceJobStates");

            // Write method
            sb.AppendLine($"        public static global::SharpIpp.Protocol.Models.IppResponseMessage Write{safeMethodName}(");
            sb.AppendLine($"            {fqn} src,");
            sb.AppendLine("            global::SharpIpp.Protocol.Models.IppResponseMessage? dst,");
            sb.AppendLine("            IMapperApplier map)");
            sb.AppendLine("        {");
            sb.AppendLine("            dst ??= new global::SharpIpp.Protocol.Models.IppResponseMessage();");
            sb.AppendLine("            dst.Version = src.Version;");
            sb.AppendLine("            dst.RequestId = src.RequestId;");
            sb.AppendLine("            dst.StatusCode = src.StatusCode;");
            if (opAttrProp != null)
            {
                var opAttrsFqn = opAttrProp.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                sb.AppendLine("            if (src.OperationAttributes != null)");
                sb.AppendLine("                dst.OperationAttributes.Add(map.Map<List<IppAttribute>>(src.OperationAttributes));");
                sb.AppendLine("            else");
                sb.AppendLine($"                dst.OperationAttributes.Add(map.Map<List<IppAttribute>>(new {opAttrsFqn}()));");
            }

            if (hasJobIds)
            {
                sb.AppendLine("            if (src.JobIds != null && dst.OperationAttributes.Count > 0)");
                sb.AppendLine("                dst.OperationAttributes[0].AddRange(src.JobIds.Select(x => new IppAttribute(global::SharpIpp.Protocol.Models.Tag.Integer, global::SharpIpp.Protocol.Models.IppAttributeNames.JobIds, x)));");
            }
            if (hasOutputDeviceJobStates)
            {
                sb.AppendLine("            if (src.OutputDeviceJobStates != null && dst.OperationAttributes.Count > 0)");
                sb.AppendLine("                dst.OperationAttributes[0].AddRange(src.OutputDeviceJobStates.Select(x => new IppAttribute(global::SharpIpp.Protocol.Models.Tag.Enum, global::SharpIpp.Protocol.Models.IppAttributeNames.OutputDeviceJobStates, (int)x)));");
            }

            if (hasJobAttrs || hasJobDescAttrs)
            {
                sb.AppendLine("            if (src.JobAttributes != null)");
                sb.AppendLine("            {");
                sb.AppendLine("                var jobAttrs = new List<IppAttribute>();");
                sb.AppendLine("                jobAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.JobAttributes).Values.SelectMany(x => x));");
                sb.AppendLine("                dst.JobAttributes.Add(jobAttrs);");
                sb.AppendLine("            }");
            }
            if (hasJobsAttrs)
            {
                sb.AppendLine("            if (src.JobsAttributes != null)");
                sb.AppendLine("                dst.JobAttributes.AddRange(map.Map<global::SharpIpp.Protocol.Models.JobDescriptionAttributes[], List<List<IppAttribute>>>(src.JobsAttributes));");
            }
            if (hasDocAttrs)
            {
                sb.AppendLine("            if (src.DocumentAttributes != null)");
                sb.AppendLine("            {");
                sb.AppendLine("                var docAttrs = map.Map<IEnumerable<IppAttribute>>(src.DocumentAttributes).ToList();");
                sb.AppendLine("                dst.DocumentAttributes.Add(docAttrs);");
                sb.AppendLine("            }");
            }
            if (hasDocs)
            {
                sb.AppendLine("            if (src.Documents != null)");
                sb.AppendLine("                dst.DocumentAttributes.AddRange(src.Documents.Select(x => map.Map<IEnumerable<IppAttribute>>(x).ToList()));");
            }
            if (hasPrinterAttrs)
            {
                sb.AppendLine("            if (src.PrinterAttributes != null)");
                sb.AppendLine("            {");
                sb.AppendLine("                var printerAttrs = new List<IppAttribute>();");
                sb.AppendLine("                printerAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.PrinterAttributes).Values.SelectMany(x => x));");
                sb.AppendLine("                dst.PrinterAttributes.Add(printerAttrs);");
                sb.AppendLine("            }");
            }
            if (hasPrintersAttrs)
            {
                sb.AppendLine("            if (src.PrintersAttributes != null)");
                sb.AppendLine("                dst.PrinterAttributes.AddRange(src.PrintersAttributes.Select(x => map.Map<global::SharpIpp.Protocol.Models.PrinterDescriptionAttributes, List<IppAttribute>>(x)));");
            }
            if (hasPrinterResourceIds)
            {
                sb.AppendLine("            if (src.PrinterResourceIds != null)");
                sb.AppendLine("                dst.PrinterAttributes.Add(src.PrinterResourceIds.Select(id => new IppAttribute(global::SharpIpp.Protocol.Models.Tag.Integer, global::SharpIpp.Protocol.Models.IppAttributeNames.PrinterResourceIds, id)).ToList());");
            }
            if (hasResourceAttrs)
            {
                sb.AppendLine("            if (src.ResourceAttributes != null)");
                sb.AppendLine("            {");
                sb.AppendLine("                var resourceAttrs = new List<IppAttribute>();");
                sb.AppendLine("                resourceAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.ResourceAttributes).Values.SelectMany(x => x));");
                sb.AppendLine("                dst.ResourceAttributes.Add(resourceAttrs);");
                sb.AppendLine("            }");
            }
            if (hasResourcesAttrs)
            {
                sb.AppendLine("            if (src.ResourcesAttributes != null)");
                sb.AppendLine("                dst.ResourceAttributes.AddRange(map.Map<global::SharpIpp.Protocol.Models.ResourceDescriptionAttributes[], List<List<IppAttribute>>>(src.ResourcesAttributes));");
            }
            if (hasSystemAttrs || hasSystemDescAttrs)
            {
                sb.AppendLine("            var systemAttrs = new List<IppAttribute>();");
                if (hasSystemAttrs)
                {
                    sb.AppendLine("            if (src.SystemAttributes != null)");
                    sb.AppendLine("                systemAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.SystemAttributes).Values.SelectMany(x => x));");
                }
                if (hasSystemDescAttrs)
                {
                    sb.AppendLine("            if (src.SystemDescriptionAttributes != null)");
                    sb.AppendLine("                systemAttrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.SystemDescriptionAttributes).Values.SelectMany(x => x));");
                }
                sb.AppendLine("            if (systemAttrs.Count > 0)");
                sb.AppendLine("                dst.SystemAttributes.Add(systemAttrs);");
            }
            if (hasSubscriptionsAttrs)
            {
                sb.AppendLine("            if (src.SubscriptionsAttributes != null)");
                sb.AppendLine("                dst.SubscriptionAttributes.AddRange(map.Map<global::SharpIpp.Protocol.Models.SubscriptionDescriptionAttributes[], List<List<IppAttribute>>>(src.SubscriptionsAttributes));");
            }
            if (hasSubscriptionAttrs)
            {
                sb.AppendLine("            if (src.SubscriptionAttributes != null)");
                sb.AppendLine("            {");
                sb.AppendLine("                var attrs = new List<IppAttribute>();");
                sb.AppendLine("                attrs.AddRange(map.Map<IDictionary<string, IppAttribute[]>>(src.SubscriptionAttributes).Values.SelectMany(x => x));");
                sb.AppendLine("                dst.SubscriptionAttributes.Add(attrs);");
                sb.AppendLine("            }");
            }

            sb.AppendLine("            return dst;");
            sb.AppendLine("        }");
            sb.AppendLine();

            // Read method
            sb.AppendLine($"        public static {fqn} Read{safeMethodName}(");
            sb.AppendLine("            global::SharpIpp.Protocol.IIppResponseMessage src,");
            sb.AppendLine($"            {fqn}? dst,");
            sb.AppendLine("            IMapperApplier map)");
            sb.AppendLine("        {");
            sb.AppendLine($"            dst ??= new {fqn}();");
            sb.AppendLine("            dst.Version = src.Version;");
            sb.AppendLine("            dst.RequestId = src.RequestId;");
            sb.AppendLine("            dst.StatusCode = src.StatusCode;");
            if (opAttrProp != null || hasJobIds || hasOutputDeviceJobStates)
            {
                sb.AppendLine("            if (src.OperationAttributes.Count > 0)");
                sb.AppendLine("            {");
                sb.AppendLine("                var opDic = src.OperationAttributes.SelectMany(x => x).ToIppDictionary();");
                if (opAttrProp != null)
                {
                    var opAttrsFqn = opAttrProp.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                    if (opAttrsFqn == "global::SharpIpp.Models.Responses.OperationAttributes")
                    {
                        sb.AppendLine("                var statusMessage = map.MapFromDicNullable<string?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.StatusMessage);");
                        sb.AppendLine("                var detailedStatusMessage = map.MapFromDicNullable<string?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.DetailedStatusMessage);");
                        sb.AppendLine("                var documentAccessError = map.MapFromDicNullable<string?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.DocumentAccessError);");
                        sb.AppendLine("                if (statusMessage != null || detailedStatusMessage != null || documentAccessError != null)");
                        sb.AppendLine("                {");
                        sb.AppendLine($"                    dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, {opAttrsFqn}>(opDic);");
                        sb.AppendLine("                }");
                    }
                    else
                    {
                        sb.AppendLine($"                dst.OperationAttributes = map.Map<IDictionary<string, IppAttribute[]>, {opAttrsFqn}>(opDic);");
                    }
                }
                if (hasJobIds)
                    sb.AppendLine("                dst.JobIds = map.MapFromDicSetNullable<int[]?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.JobIds);");
                if (hasOutputDeviceJobStates)
                    sb.AppendLine("                dst.OutputDeviceJobStates = map.MapFromDicSetNullable<global::SharpIpp.Protocol.Models.JobState[]?>(opDic, global::SharpIpp.Protocol.Models.IppAttributeNames.OutputDeviceJobStates);");
                sb.AppendLine("            }");
            }

            if (hasJobAttrs)
            {
                sb.AppendLine("            if (src.JobAttributes.Count > 0)");
                sb.AppendLine("            {");
                sb.AppendLine("                var jobAttrs = new global::SharpIpp.Models.Responses.JobAttributes();");
                sb.AppendLine("                map.Map(src.JobAttributes.SelectMany(x => x).ToIppDictionary(), jobAttrs);");
                sb.AppendLine("                dst.JobAttributes = jobAttrs;");
                sb.AppendLine("            }");
            }
            if (hasJobDescAttrs)
            {
                sb.AppendLine("            if (src.JobAttributes.Any())");
                sb.AppendLine("                dst.JobAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.JobDescriptionAttributes>(src.JobAttributes.SelectMany(x => x).ToIppDictionary());");
            }
            if (hasJobsAttrs)
            {
                sb.AppendLine("            dst.JobsAttributes = map.Map<List<List<IppAttribute>>, global::SharpIpp.Protocol.Models.JobDescriptionAttributes[]>(src.JobAttributes);");
            }
            if (hasDocAttrs)
            {
                sb.AppendLine("            if (src.DocumentAttributes.Count > 0)");
                sb.AppendLine("            {");
                sb.AppendLine("                var docAttrs = new global::SharpIpp.Protocol.Models.DocumentAttributes();");
                sb.AppendLine("                map.Map(src.DocumentAttributes.SelectMany(x => x).ToIppDictionary(), docAttrs);");
                sb.AppendLine("                dst.DocumentAttributes = docAttrs;");
                sb.AppendLine("            }");
            }
            if (hasDocs)
            {
                sb.AppendLine("            dst.Documents = src.DocumentAttributes");
                sb.AppendLine("                .Select(x => map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.DocumentAttributes>(x.ToIppDictionary()))");
                sb.AppendLine("                .ToList();");
            }
            if (hasPrinterAttrs)
            {
                sb.AppendLine("            if (src.PrinterAttributes.Count > 0)");
                sb.AppendLine("                dst.PrinterAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.PrinterDescriptionAttributes>(src.PrinterAttributes.SelectMany(x => x).ToIppDictionary());");
            }
            if (hasPrintersAttrs)
            {
                sb.AppendLine("            dst.PrintersAttributes = src.PrinterAttributes");
                sb.AppendLine("                .Select(x => map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.PrinterDescriptionAttributes>(x.ToIppDictionary()))");
                sb.AppendLine("                .ToArray();");
            }
            if (hasPrinterResourceIds)
            {
                sb.AppendLine("            var printerAttrsDict = src.PrinterAttributes.SelectMany(x => x).ToIppDictionary();");
                sb.AppendLine("            dst.PrinterResourceIds = map.MapFromDicSetNullable<int[]?>(printerAttrsDict, global::SharpIpp.Protocol.Models.IppAttributeNames.PrinterResourceIds);");
            }
            if (hasResourceAttrs)
            {
                sb.AppendLine("            if (src.ResourceAttributes.Count > 0)");
                sb.AppendLine("                dst.ResourceAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.ResourceStatusAttributes>(src.ResourceAttributes.SelectMany(x => x).ToIppDictionary());");
            }
            if (hasResourcesAttrs)
            {
                sb.AppendLine("            if (src.ResourceAttributes.Count > 0)");
                sb.AppendLine("                dst.ResourcesAttributes = map.Map<List<List<IppAttribute>>, global::SharpIpp.Protocol.Models.ResourceDescriptionAttributes[]>(src.ResourceAttributes);");
            }
            if (hasSystemAttrs || hasSystemDescAttrs)
            {
                sb.AppendLine("            if (src.SystemAttributes.Count > 0)");
                sb.AppendLine("            {");
                sb.AppendLine("                var systemAttrs = src.SystemAttributes.SelectMany(x => x).ToIppDictionary();");
                if (hasSystemAttrs)
                    sb.AppendLine("                dst.SystemAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.SystemStatusAttributes>(systemAttrs);");
                if (hasSystemDescAttrs)
                    sb.AppendLine("                dst.SystemDescriptionAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.SystemDescriptionAttributes>(systemAttrs);");
                sb.AppendLine("            }");
            }
            if (hasSubscriptionsAttrs)
            {
                sb.AppendLine("            if (src.SubscriptionAttributes.Any())");
                sb.AppendLine("                dst.SubscriptionsAttributes = map.Map<global::SharpIpp.Protocol.Models.SubscriptionDescriptionAttributes[]>(src.SubscriptionAttributes);");
            }
            if (hasSubscriptionAttrs)
            {
                sb.AppendLine("            if (src.SubscriptionAttributes.Count > 0)");
                sb.AppendLine("                dst.SubscriptionAttributes = map.Map<IDictionary<string, IppAttribute[]>, global::SharpIpp.Protocol.Models.SubscriptionDescriptionAttributes>(src.SubscriptionAttributes.SelectMany(x => x).ToIppDictionary());");
            }

            sb.AppendLine("            return dst;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }
    }


    internal static bool HasAttribute(INamedTypeSymbol? typeSymbol, INamedTypeSymbol? attrSymbol)
    {
        if (typeSymbol == null || attrSymbol == null)
            return false;
        return typeSymbol.GetAttributes().Any(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, attrSymbol));
    }

    internal static AttributeData? GetAttribute(INamedTypeSymbol? typeSymbol, INamedTypeSymbol? attrSymbol)
    {
        if (typeSymbol == null || attrSymbol == null)
            return null;
        return typeSymbol.GetAttributes().FirstOrDefault(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, attrSymbol));
    }

    internal static IEnumerable<INamedTypeSymbol> GetTypeAndBaseTypes(INamedTypeSymbol? type)
    {
        var curr = type;
        while (curr != null && curr.SpecialType != SpecialType.System_Object)
        {
            yield return curr;
            curr = curr.BaseType;
        }
    }

    internal static string? TryGetEnumFieldName(ITypeSymbol? type, object? value)
    {
        if (type is not INamedTypeSymbol { TypeKind: TypeKind.Enum } enumType || value == null)
            return null;

        var tv = Convert.ToInt64(value);
        foreach (var member in enumType.GetMembers())
        {
            if (member is IFieldSymbol { HasConstantValue: true } field &&
                Convert.ToInt64(field.ConstantValue) == tv &&
                field.Name != "Unsupported")
            {
                return field.Name;
            }
        }
        return null;
    }

    internal static ITypeSymbol UnwrapNullable(ITypeSymbol type)
    {
        if (type is INamedTypeSymbol named && named.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
            return named.TypeArguments[0];
        return type;
    }

    internal static (bool IsIppValueArray, ITypeSymbol? ElementType) GetIppValueArrayInfo(bool isIppValue, ITypeSymbol? innerType)
    {
        if (!isIppValue || innerType == null)
            return (false, null);

        if (innerType is IArrayTypeSymbol ats)
            return (true, ats.ElementType);

        if (innerType is INamedTypeSymbol nts && nts.TypeArguments.Length == 1 &&
            (nts.Name is "IReadOnlyCollection" or "IEnumerable" or "List" or "IList"))
        {
            return (true, nts.TypeArguments[0]);
        }

        return (false, null);
    }

    private static string GetSafeMethodName(INamedTypeSymbol type)
    {
        return type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            .Replace("global::", "")
            .Replace(".", "_");
    }

    private static string GetCollectionAttributeNameExpression(
        INamedTypeSymbol model,
        INamedTypeSymbol? ippAttributeAttrSymbol,
        Dictionary<string, string> nameToConst)
    {
        if (ippAttributeAttrSymbol != null)
        {
            foreach (var attr in model.GetAttributes())
            {
                if (SymbolEqualityComparer.Default.Equals(attr.AttributeClass, ippAttributeAttrSymbol))
                {
                    if (attr.ConstructorArguments.Length > 0 && attr.ConstructorArguments[0].Value is string n)
                        return GetAttributeNameExpression(n, nameToConst);
                    if (attr.NamedArguments.Length > 0 && attr.NamedArguments.FirstOrDefault(na => na.Key == "Name").Value.Value is string nameVal)
                        return GetAttributeNameExpression(nameVal, nameToConst);
                }
            }
        }
        var kebab = ToKebabCase(model.Name);
        return GetAttributeNameExpression(kebab, nameToConst);
    }

    private static List<ModelPropertyInfo> GetAnnotatedProperties(
        INamedTypeSymbol model,
        HashSet<INamedTypeSymbol> annotatedModels,
        INamedTypeSymbol? ippAttributeAttrSymbol,
        INamedTypeSymbol? iIppCollectionSymbol,
        INamedTypeSymbol? iKeywordOrNameEnumSymbol,
        INamedTypeSymbol? iKeywordEnumSymbol,
        INamedTypeSymbol? iIppStructuredStringSymbol,
        INamedTypeSymbol? ippValueGenericSymbol)
    {
        var list = new List<ModelPropertyInfo>();
        int sourceIndex = 0;

        // Determine if base class is already an annotated model
        bool hasAnnotatedBase = annotatedModels.Contains(model.BaseType!);

        var hierarchy = new List<INamedTypeSymbol>();
        foreach (var curr in GetTypeAndBaseTypes(model))
        {
            hierarchy.Add(curr);
            if (hasAnnotatedBase)
                break; // Base handles its own properties
        }

        hierarchy.Reverse(); // Base to derived

        foreach (var type in hierarchy)
        {
            foreach (var member in type.GetMembers())
            {
                if (member is not IPropertySymbol prop)
                    continue;

                sourceIndex++;
                string? explicitName = null;
                string? tag = null;
                int order = int.MaxValue;
                string? defaultValue = null;

                if (ippAttributeAttrSymbol != null)
                {
                    foreach (var attr in prop.GetAttributes())
                    {
                        if (SymbolEqualityComparer.Default.Equals(attr.AttributeClass, ippAttributeAttrSymbol))
                        {
                            if (attr.ConstructorArguments.Length > 0 && attr.ConstructorArguments[0].Value is string nameArg)
                                explicitName = nameArg;

                            if (attr.ConstructorArguments.Length > 1)
                            {
                                var tagConst = attr.ConstructorArguments[1];
                                if (tagConst.Value != null)
                                {
                                    var fn = TryGetEnumFieldName(tagConst.Type, tagConst.Value);
                                    if (fn != null)
                                    {
                                        tag = fn;
                                    }
                                    else if (tagConst.Value is int ordVal && ordVal >= 0)
                                    {
                                        order = ordVal;
                                    }
                                }
                            }

                            if (attr.ConstructorArguments.Length > 2 && attr.ConstructorArguments[2].Value != null)
                            {
                                var ordVal = Convert.ToInt32(attr.ConstructorArguments[2].Value);
                                if (ordVal >= 0)
                                    order = ordVal;
                            }

                            foreach (var named in attr.NamedArguments)
                            {
                                if (named.Key == "Name" && named.Value.Value is string n)
                                    explicitName = n;
                                if (named.Key == "Tag" && named.Value.Value != null)
                                {
                                    var fn = TryGetEnumFieldName(named.Value.Type, named.Value.Value);
                                    if (fn != null)
                                        tag = fn;
                                }
                                if (named.Key == "Order" && named.Value.Value != null)
                                {
                                    var ov = Convert.ToInt32(named.Value.Value);
                                    if (ov >= 0)
                                        order = ov;
                                }
                                if (named.Key == "DefaultValue" && named.Value.Value is string dv)
                                {
                                    defaultValue = dv;
                                }
                            }
                        }
                    }
                }

                bool hasClassAttr = type.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, ippAttributeAttrSymbol));
                bool hasAttr = prop.GetAttributes().Any(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, ippAttributeAttrSymbol));

                if (!hasAttr && !hasClassAttr)
                    continue;

                // Ignore explicit interface implementation properties
                if (prop.ExplicitInterfaceImplementations.Length > 0)
                    continue;

                var attrName = explicitName ?? ToKebabCase(prop.Name);
                var (inferredTag, isCol, isColArr) = InferTag(prop.Type, iIppCollectionSymbol, iIppStructuredStringSymbol, ippValueGenericSymbol);

                var unwrapped = prop.Type;
                bool isNullable = false;
                if (prop.Type is INamedTypeSymbol nts && nts.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
                {
                    unwrapped = nts.TypeArguments[0];
                    isNullable = true;
                }

                bool isArray = prop.Type is IArrayTypeSymbol || (prop.Type is INamedTypeSymbol ntsColl && (ntsColl.Name == "IReadOnlyCollection" || ntsColl.Name == "IEnumerable" || ntsColl.Name == "List" || ntsColl.Name == "IList") && ntsColl.TypeArguments.Length == 1);
                ITypeSymbol? elemType = prop.Type is IArrayTypeSymbol ats ? ats.ElementType : (prop.Type is INamedTypeSymbol ntsCol2 && ntsCol2.TypeArguments.Length == 1 ? ntsCol2.TypeArguments[0] : null);

                bool isIppDictArray = isArray && elemType != null &&
                    elemType.Name.StartsWith("IDictionary") &&
                    elemType is INamedTypeSymbol ntsDict &&
                    ntsDict.TypeArguments.Length == 2 &&
                    ntsDict.TypeArguments[0].SpecialType == SpecialType.System_String &&
                    ntsDict.TypeArguments[1] is IArrayTypeSymbol atsVal &&
                    atsVal.ElementType.Name == "IppAttribute";

                if (isIppDictArray)
                    isColArr = false;

                if (tag == "BegCollection")
                {
                    if (isArray)
                    {
                        isColArr = true;
                        isCol = false;
                    }
                    else
                    {
                        isCol = true;
                        isColArr = false;
                    }
                }

                bool isStructuredString = ImplementsOrInherits(unwrapped, iIppStructuredStringSymbol);
                bool isStructuredStringArr = elemType != null && iIppStructuredStringSymbol != null && ImplementsOrInherits(elemType, iIppStructuredStringSymbol);

                bool isKeywordOrNameEnum = ImplementsOrInherits(unwrapped, iKeywordOrNameEnumSymbol);
                bool isKeywordOrNameEnumArr = elemType != null && iKeywordOrNameEnumSymbol != null && ImplementsOrInherits(elemType, iKeywordOrNameEnumSymbol);

                bool isKeywordEnum = ImplementsOrInherits(unwrapped, iKeywordEnumSymbol) && !isKeywordOrNameEnum;
                bool isKeywordEnumArr = elemType != null && iKeywordEnumSymbol != null && ImplementsOrInherits(elemType, iKeywordEnumSymbol) && !isKeywordOrNameEnumArr;

                bool isEnum = unwrapped.TypeKind == TypeKind.Enum;
                bool isEnumArr = elemType != null && elemType.TypeKind == TypeKind.Enum;

                bool isString = unwrapped.SpecialType == SpecialType.System_String;
                bool isStringArr = elemType != null && elemType.SpecialType == SpecialType.System_String;

                bool isInt = unwrapped.SpecialType == SpecialType.System_Int32;
                bool isIntArr = elemType != null && elemType.SpecialType == SpecialType.System_Int32;

                bool isBool = unwrapped.SpecialType == SpecialType.System_Boolean;
                bool isBoolArr = elemType != null && elemType.SpecialType == SpecialType.System_Boolean;

                bool isDateTimeOffset = unwrapped.Name == "DateTimeOffset";
                bool isDateTimeOffsetArr = elemType != null && elemType.Name == "DateTimeOffset";

                bool isUri = unwrapped.Name == "Uri";
                bool isUriArr = elemType != null && elemType.Name == "Uri";

                bool isRange = unwrapped.Name == "Range";
                bool isRangeArr = elemType != null && elemType.Name == "Range";

                bool isResolution = unwrapped.Name == "Resolution";
                bool isResolutionArr = elemType != null && elemType.Name == "Resolution";

                bool isOctetString = unwrapped.Name == "OctetString";
                bool isOctetStringArr = elemType != null && elemType.Name == "OctetString";

                bool isStringWithLanguage = unwrapped.Name == "StringWithLanguage";
                bool isStringWithLanguageArr = elemType != null && elemType.Name == "StringWithLanguage";

                bool isIppValue = unwrapped is INamedTypeSymbol ippValType &&
                    ippValType.TypeArguments.Length == 1 &&
                    ((ippValueGenericSymbol != null && SymbolEqualityComparer.Default.Equals(ippValType.OriginalDefinition, ippValueGenericSymbol)) ||
                     ippValType.Name == "IppValue");
                ITypeSymbol? ippValueInnerType = isIppValue ? ((INamedTypeSymbol)unwrapped).TypeArguments[0] : null;
                var (isIppValueArray, ippValueElementType) = GetIppValueArrayInfo(isIppValue, ippValueInnerType);

                if (isIppValue)
                {
                    isCol = false;
                    isColArr = false;
                    isArray = false;
                    isStructuredString = false;
                    isStructuredStringArr = false;
                    isKeywordOrNameEnum = false;
                    isKeywordOrNameEnumArr = false;
                    isKeywordEnum = false;
                    isKeywordEnumArr = false;
                    isEnum = false;
                    isEnumArr = false;
                    isString = false;
                    isStringArr = false;
                    isInt = false;
                    isIntArr = false;
                    isBool = false;
                    isBoolArr = false;
                    isDateTimeOffset = false;
                    isDateTimeOffsetArr = false;
                    isUri = false;
                    isUriArr = false;
                    isRange = false;
                    isRangeArr = false;
                    isResolution = false;
                    isResolutionArr = false;
                    isOctetString = false;
                    isOctetStringArr = false;
                    isStringWithLanguage = false;
                    isStringWithLanguageArr = false;
                }

                list.Add(new ModelPropertyInfo
                {
                    Property = prop,
                    UnwrappedType = unwrapped,
                    ElementType = elemType,
                    AttributeName = attrName,
                    Tag = tag ?? inferredTag,
                    Order = order,
                    SourceIndex = sourceIndex,
                    DefaultValue = defaultValue,
                    IsNullable = isNullable,
                    IsArray = isArray,
                    IsIppDictArray = isIppDictArray,
                    IsCollection = isCol,
                    IsCollectionArray = isColArr,
                    IsStructuredString = isStructuredString,
                    IsStructuredStringArray = isStructuredStringArr,
                    IsKeywordOrNameEnum = isKeywordOrNameEnum,
                    IsKeywordOrNameEnumArray = isKeywordOrNameEnumArr,
                    IsKeywordEnum = isKeywordEnum,
                    IsKeywordEnumArray = isKeywordEnumArr,
                    IsEnum = isEnum,
                    IsEnumArray = isEnumArr,
                    IsString = isString,
                    IsStringArray = isStringArr,
                    IsInt = isInt,
                    IsIntArray = isIntArr,
                    IsBool = isBool,
                    IsBoolArray = isBoolArr,
                    IsDateTimeOffset = isDateTimeOffset,
                    IsDateTimeOffsetArray = isDateTimeOffsetArr,
                    IsUri = isUri,
                    IsUriArray = isUriArr,
                    IsRange = isRange,
                    IsRangeArray = isRangeArr,
                    IsResolution = isResolution,
                    IsResolutionArray = isResolutionArr,
                    IsOctetString = isOctetString,
                    IsOctetStringArray = isOctetStringArr,
                    IsStringWithLanguage = isStringWithLanguage,
                    IsStringWithLanguageArray = isStringWithLanguageArr,
                    IsIppValue = isIppValue,
                    IsIppValueArray = isIppValueArray,
                    IppValueInnerType = ippValueInnerType,
                    IppValueElementType = ippValueElementType,
                    HasExplicitTag = tag != null
                });
            }
        }

        return list.OrderBy(p => p.Order).ThenBy(p => p.SourceIndex).ToList();
    }

    private static (string Tag, bool IsCollection, bool IsCollectionArray) InferTag(
        ITypeSymbol type,
        INamedTypeSymbol? iIppCollectionSymbol,
        INamedTypeSymbol? iIppStructuredStringSymbol,
        INamedTypeSymbol? ippValueGenericSymbol)
    {
        var underlying = type;
        if (type is INamedTypeSymbol named && named.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T)
        {
            underlying = named.TypeArguments[0];
        }

        if (underlying is INamedTypeSymbol ippValNamed &&
            ippValNamed.TypeArguments.Length == 1 &&
            ((ippValueGenericSymbol != null && SymbolEqualityComparer.Default.Equals(ippValNamed.OriginalDefinition, ippValueGenericSymbol)) ||
             ippValNamed.Name == "IppValue"))
        {
            underlying = ippValNamed.TypeArguments[0];
        }

        if (underlying is IArrayTypeSymbol arrType)
        {
            if (ImplementsOrInherits(arrType.ElementType, iIppCollectionSymbol))
                return ("BegCollection", false, true);
            if (ImplementsOrInherits(arrType.ElementType, iIppStructuredStringSymbol))
                return ("OctetStringWithAnUnspecifiedFormat", false, false);

            underlying = UnwrapNullable(arrType.ElementType);
        }
        else if (underlying is INamedTypeSymbol collType && (collType.Name == "IReadOnlyCollection" || collType.Name == "IEnumerable" || collType.Name == "List" || collType.Name == "IList") && collType.TypeArguments.Length == 1)
        {
            var elemType = collType.TypeArguments[0];
            if (ImplementsOrInherits(elemType, iIppCollectionSymbol))
                return ("BegCollection", false, true);
            if (ImplementsOrInherits(elemType, iIppStructuredStringSymbol))
                return ("OctetStringWithAnUnspecifiedFormat", false, false);

            underlying = UnwrapNullable(elemType);
        }

        if (ImplementsOrInherits(underlying, iIppCollectionSymbol))
            return ("BegCollection", true, false);

        if (ImplementsOrInherits(underlying, iIppStructuredStringSymbol))
            return ("OctetStringWithAnUnspecifiedFormat", false, false);

        if (underlying.SpecialType == SpecialType.System_Int32)
            return ("Integer", false, false);

        if (underlying.SpecialType == SpecialType.System_Boolean)
            return ("Boolean", false, false);

        if (underlying.SpecialType == SpecialType.System_String)
            return ("NameWithoutLanguage", false, false);

        if (underlying.Name == "StringWithLanguage")
            return ("NameWithoutLanguage", false, false);

        if (underlying.Name == "DateTimeOffset")
            return ("DateTime", false, false);

        if (underlying.Name == "Uri")
            return ("Uri", false, false);

        if (underlying.Name == "Resolution")
            return ("Resolution", false, false);

        if (underlying.Name == "Range")
            return ("RangeOfInteger", false, false);

        if (underlying.Name == "OctetString")
            return ("OctetStringWithAnUnspecifiedFormat", false, false);

        if (underlying.TypeKind == TypeKind.Enum)
            return ("Enum", false, false);

        return ("Keyword", false, false);
    }

    private static string GetAttributeNameExpression(string attrName, Dictionary<string, string> nameToConst)
    {
        if (nameToConst.TryGetValue(attrName, out var constName))
        {
            return $"global::SharpIpp.Protocol.Models.IppAttributeNames.{constName}";
        }
        return $"\"{attrName}\"";
    }

    internal static string ToKebabCase(string name)
    {
        if (string.IsNullOrEmpty(name))
            return name;

        var sb = new StringBuilder();
        for (int i = 0; i < name.Length; i++)
        {
            char c = name[i];
            if (char.IsUpper(c))
            {
                if (i > 0)
                    sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    internal static bool ImplementsOrInherits(ITypeSymbol? symbol, ITypeSymbol? targetType)
    {
        if (symbol == null || targetType == null)
            return false;

        if (SymbolEqualityComparer.Default.Equals(symbol, targetType))
            return true;

        if (targetType.TypeKind == TypeKind.Interface)
        {
            foreach (var iface in symbol.AllInterfaces)
            {
                if (SymbolEqualityComparer.Default.Equals(iface, targetType))
                    return true;
            }
        }
        else
        {
            var curr = symbol.BaseType;
            while (curr != null)
            {
                if (SymbolEqualityComparer.Default.Equals(curr, targetType))
                    return true;
                curr = curr.BaseType;
            }
        }

        return false;
    }

    internal static string? GetSectionPropertyName(byte sectionTag)
    {
        return sectionTag switch
        {
            1 => "OperationAttributes",
            2 => "JobAttributes",
            4 => "PrinterAttributes",
            5 => "UnsupportedAttributes",
            6 => "SubscriptionAttributes",
            7 => "EventNotificationAttributes",
            8 => "ResourceAttributes",
            9 => "DocumentAttributes",
            10 => "SystemAttributes",
            _ => null
        };
    }
}

