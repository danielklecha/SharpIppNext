using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class ProfileNullDestTests
{
    private SimpleMapper CreateMapper()
    {
        var mapper = new SimpleMapper();
        mapper.RegisterGeneratedProfiles();
        return mapper;
    }

    public static IEnumerable<object[]> NullDest_ShouldCreateNew_Data
    {
        get
        {
            // IppJobProfile: IDictionary<string, IppAttribute[]> -> JobAttributes (dst ??= new JobAttributes())
            {
                var src = new Dictionary<string, IppAttribute[]>
                {
                    { IppAttributeNames.JobUri, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, "ipp://localhost/jobs/1") } },
                    { IppAttributeNames.JobId, new[] { new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 1) } },
                    { IppAttributeNames.JobState, new[] { new IppAttribute(Tag.Enum, IppAttributeNames.JobState, (int)JobState.Pending) } }
                };
                yield return [(object)src, typeof(IDictionary<string, IppAttribute[]>), typeof(JobAttributes), "IDictionary -> JobAttributes"];
            }

            // IppProfile: IIppResponse -> IppResponseMessage (dst ??= new IppResponseMessage())
            {
                var src = new Mock<IIppResponse>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                src.SetupGet(x => x.RequestId).Returns(42);
                src.SetupGet(x => x.StatusCode).Returns(IppStatusCode.SuccessfulOk);
                yield return [src.Object, typeof(IIppResponse), typeof(IppResponseMessage), "IIppResponse -> IppResponseMessage"];
            }

            // JobTemplateAttributesProfile: JobTemplateAttributes -> IppRequestMessage (dst ??= new IppRequestMessage())
            {
                var src = new JobTemplateAttributes();
                yield return [src, typeof(JobTemplateAttributes), typeof(IppRequestMessage), "JobTemplateAttributes -> IppRequestMessage"];
            }

            // JobTemplateAttributesProfile: IIppRequestMessage -> JobTemplateAttributes (dst ??= new JobTemplateAttributes())
            {
                var src = new Mock<IIppRequestMessage>();
                src.SetupGet(x => x.JobAttributes).Returns(new List<IppAttribute>());
                yield return [src.Object, typeof(IIppRequestMessage), typeof(JobTemplateAttributes), "IIppRequestMessage -> JobTemplateAttributes"];
            }

            // DocumentAttributesProfile: IDictionary<string, IppAttribute[]> -> DocumentAttributes (dst ??= new DocumentAttributes())
            {
                var src = new Dictionary<string, IppAttribute[]>
                {
                    { IppAttributeNames.DocumentNumber, [new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, 1)] },
                    { IppAttributeNames.DocumentState, [new IppAttribute(Tag.Enum, IppAttributeNames.DocumentState, (int)DocumentState.Pending)] }
                };
                yield return [(object)src, typeof(IDictionary<string, IppAttribute[]>), typeof(DocumentAttributes), "IDictionary -> DocumentAttributes"];
            }
        }
    }

    [TestMethod]
    [DynamicData(nameof(NullDest_ShouldCreateNew_Data))]
    public void MapNullable_NullDest_ShouldCreateNew(object source, Type sourceType, Type destType, string description)
    {
        // Arrange
        var mapper = CreateMapper();

        // Act
        var result = mapper.MapNullable(source, sourceType, destType, null);

        // Assert
        result.Should().NotBeNull($"mapping {description} with null dest should create a new instance");
        result.Should().BeAssignableTo(destType);
    }

    public static IEnumerable<object[]> NullDest_ShouldThrow_Data
    {
        get
        {
            // IppProfile: IppResponseMessage -> IIppResponse (dst ?? throw)
            {
                var src = new IppResponseMessage();
                yield return [(object)src, typeof(IppResponseMessage), typeof(IIppResponse), "IppResponseMessage -> IIppResponse"];
            }
        }
    }

    [TestMethod]
    [DynamicData(nameof(NullDest_ShouldThrow_Data))]
    public void MapNullable_NullDest_ShouldThrowArgumentNullException(object source, Type sourceType, Type destType, string description)
    {
        // Arrange
        var mapper = CreateMapper();

        // Act
        Action act = () => mapper.MapNullable(source, sourceType, destType, null);

        // Assert
        act.Should().Throw<ArgumentNullException>($"mapping {description} with null dest should throw");
    }
}

