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
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        return mapper;
    }

    public static IEnumerable<object[]> NullDest_ShouldCreateNew_Data
    {
        get
        {
            // IppJobProfile: IIppJobRequest -> IppRequestMessage (dst ??= new IppRequestMessage())
            {
                var src = new Mock<IIppJobRequest>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                src.SetupGet(x => x.RequestId).Returns(42);
                yield return [src.Object, typeof(IIppJobRequest), typeof(IppRequestMessage), "IIppJobRequest -> IppRequestMessage"];
            }

            // IppJobProfile: IIppJobResponse -> IppResponseMessage (dst ??= new IppResponseMessage())
            {
                var src = new Mock<IIppJobResponse>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                src.SetupGet(x => x.RequestId).Returns(42);
                src.SetupGet(x => x.StatusCode).Returns(IppStatusCode.SuccessfulOk);
                src.SetupGet(x => x.JobAttributes).Returns(new JobAttributes
                {
                    JobUri = "ipp://localhost/jobs/1",
                    JobId = 1,
                    JobState = JobState.Pending
                });
                yield return [src.Object, typeof(IIppJobResponse), typeof(IppResponseMessage), "IIppJobResponse -> IppResponseMessage"];
            }

            // IppJobProfile: IDictionary<string, IppAttribute[]> -> JobAttributes (dst ??= new JobAttributes())
            {
                var src = new Dictionary<string, IppAttribute[]>
                {
                    { JobAttribute.JobUri, new[] { new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://localhost/jobs/1") } },
                    { JobAttribute.JobId, new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 1) } },
                    { JobAttribute.JobState, new[] { new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)JobState.Pending) } }
                };
                yield return [(object)src, typeof(IDictionary<string, IppAttribute[]>), typeof(JobAttributes), "IDictionary -> JobAttributes"];
            }

            // IppProfile: IIppRequest -> IppRequestMessage (dst ??= new IppRequestMessage())
            {
                var src = new Mock<IIppRequest>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                src.SetupGet(x => x.RequestId).Returns(42);
                yield return [src.Object, typeof(IIppRequest), typeof(IppRequestMessage), "IIppRequest -> IppRequestMessage"];
            }

            // IppProfile: IIppResponse -> IppResponseMessage (dst ??= new IppResponseMessage())
            {
                var src = new Mock<IIppResponse>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                src.SetupGet(x => x.RequestId).Returns(42);
                src.SetupGet(x => x.StatusCode).Returns(IppStatusCode.SuccessfulOk);
                yield return [src.Object, typeof(IIppResponse), typeof(IppResponseMessage), "IIppResponse -> IppResponseMessage"];
            }

            // IppProfile: IIppPrinterRequest -> IppRequestMessage (dst ??= new IppRequestMessage())
            {
                var src = new Mock<IIppPrinterRequest>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                src.SetupGet(x => x.RequestId).Returns(42);
                yield return [src.Object, typeof(IIppPrinterRequest), typeof(IppRequestMessage), "IIppPrinterRequest -> IppRequestMessage"];
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
            // IppJobProfile: IIppRequestMessage -> IIppJobRequest (dst ?? throw)
            {
                var src = new Mock<IIppRequestMessage>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                yield return [src.Object, typeof(IIppRequestMessage), typeof(IIppJobRequest), "IIppRequestMessage -> IIppJobRequest"];
            }

            // IppJobProfile: IppResponseMessage -> IIppJobResponse (dst ?? throw)
            {
                var src = new IppResponseMessage
                {
                    JobAttributes = { new List<IppAttribute>
                    {
                        new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://localhost/jobs/1"),
                        new IppAttribute(Tag.Integer, JobAttribute.JobId, 1),
                        new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)JobState.Pending)
                    }}
                };
                yield return [(object)src, typeof(IppResponseMessage), typeof(IIppJobResponse), "IppResponseMessage -> IIppJobResponse"];
            }

            // IppProfile: IIppRequestMessage -> IIppRequest (dst ?? throw)
            {
                var src = new Mock<IIppRequestMessage>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                yield return [src.Object, typeof(IIppRequestMessage), typeof(IIppRequest), "IIppRequestMessage -> IIppRequest"];
            }

            // IppProfile: IppResponseMessage -> IIppResponse (dst ?? throw)
            {
                var src = new IppResponseMessage();
                yield return [(object)src, typeof(IppResponseMessage), typeof(IIppResponse), "IppResponseMessage -> IIppResponse"];
            }

            // IppProfile: IIppRequestMessage -> IIppPrinterRequest (dst ?? throw)
            {
                var src = new Mock<IIppRequestMessage>();
                src.SetupGet(x => x.Version).Returns(new IppVersion(1, 1));
                yield return [src.Object, typeof(IIppRequestMessage), typeof(IIppPrinterRequest), "IIppRequestMessage -> IIppPrinterRequest"];
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
