# .NET Native & UWP Compilation

> [!NOTE]
> **Legacy Technology:** .NET Native is a whole-program native compilation toolchain used specifically for legacy Universal Windows Platform (UWP) applications. For modern Windows applications, consider modern .NET (such as .NET 8/9 with Native AOT or WinUI 3 / Windows App SDK).

When building a UWP application referencing `SharpIpp` in **Release** mode with the .NET Native toolchain enabled (`UseDotNetNativeToolchain=true`), the native compiler backend (`nutc_driver.exe`) performs whole-program optimization and native code generation across all referenced types, generic instantiations, and source-generated mapper delegates.

## Recommended Project Configuration

Even though peak memory is now approximately **1.28 GB**, the default 32-bit compiler host can become constrained near the 2 GB virtual address limit on fragmented heaps.

Always enable the 64-bit compiler host in your UWP application `.csproj`:

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release'">
  <Use64BitCompiler>true</Use64BitCompiler>
</PropertyGroup>
```

## Runtime Directives (`Default.rd.xml`)

Include the following directive in your application's `Properties/Default.rd.xml`:

```xml
<Directives xmlns="http://schemas.microsoft.com/netfx/2013/01/metadata">
  <Application>
    <Assembly Name="*Application*" Dynamic="Required All" />
    <Assembly Name="SharpIpp" Dynamic="Required All" />
  </Application>
</Directives>
```

> [!TIP]
> **Why specify `Dynamic="Required All"` for SharpIpp?**
> Empirical benchmarks show that specifying `<Assembly Name="SharpIpp" Dynamic="Required All" />` significantly reduces build time (by ~2.5 minutes) with consistent peak memory consumption.
> Because SharpIpp's mapper registry statically references all operation and model mappings, nothing substantial can be trimmed by `ilc.exe`. Adding this directive instructs the IL compiler to skip exhaustive whole-program call-graph reachability traversal for `SharpIpp`, passing the assembly directly to the native code generator (`nutc_driver.exe`).
