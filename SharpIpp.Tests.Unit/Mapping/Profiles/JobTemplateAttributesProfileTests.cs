using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class JobTemplateAttributesProfileTests
{
    private readonly IMapper _mapper;

    public JobTemplateAttributesProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_JobTemplateAttributes_WithMediaSource_ShouldMapToIppRequestMessage()
    {
        var src = new JobTemplateAttributes
        {
            MediaSource = MediaSource.Main
        };

        var result = _mapper.Map<IppRequestMessage>(src);
        var mediaSourceAttr = result.JobAttributes.Single(a => a.Name == JobAttribute.MediaSource);

        mediaSourceAttr.Tag.Should().Be(Tag.Keyword);
        mediaSourceAttr.Value.Should().Be("main");
    }

    [TestMethod]
    public void Map_IppRequestMessage_WithMediaSource_ShouldMapToJobTemplateAttributes()
    {
        var src = new Mock<IIppRequestMessage>();
        src.SetupGet(x => x.JobAttributes).Returns(
        [
            new IppAttribute(Tag.Keyword, JobAttribute.MediaSource, "main")
        ]);

        var result = _mapper.Map<JobTemplateAttributes>(src.Object);

        result.MediaSource.Should().Be(MediaSource.Main);
    }
}
