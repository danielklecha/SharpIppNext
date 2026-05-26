using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class UpdateActiveJobsOperationAttributesProfileTests
{
    private static SimpleMapper CreateMapper()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        return mapper;
    }

    [TestMethod]
    public void Map_UpdateActiveJobsOperationAttributes_To_Attributes_ShouldIncludeAllFields()
    {
        var mapper = CreateMapper();
        var src = new UpdateActiveJobsOperationAttributes
        {
            PrinterUri = new Uri("ipp://127.0.0.1/printer"),
            OutputDeviceUuid = new Uri("uuid:123"),
            OutputDeviceJobStates = [JobState.Processing, JobState.Pending],
            JobIds = [1, 2]
        };

        var result = mapper.Map<UpdateActiveJobsOperationAttributes, List<IppAttribute>>(src);

        result.Should().Contain(x => x.Name == IppAttributeNames.PrinterUri && x.Value.ToString() == src.PrinterUri.ToString());
        result.Should().Contain(x => x.Name == IppAttributeNames.OutputDeviceUuid && x.Value.ToString() == src.OutputDeviceUuid.ToString());
        result.Where(x => x.Name == IppAttributeNames.OutputDeviceJobStates).Select(x => (JobState)x.Value).Should().BeEquivalentTo(src.OutputDeviceJobStates);
        result.Where(x => x.Name == IppAttributeNames.JobIds).Select(x => (int)x.Value).Should().BeEquivalentTo(src.JobIds);
    }

    [TestMethod]
    public void Map_Dictionary_To_UpdateActiveJobsOperationAttributes_ShouldIncludeAllFields()
    {
        var mapper = CreateMapper();
        var dic = new Dictionary<string, IppAttribute[]>
        {
            [IppAttributeNames.PrinterUri] = [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1/printer")],
            [IppAttributeNames.OutputDeviceUuid] = [new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, "uuid:123")],
            [IppAttributeNames.OutputDeviceJobStates] = 
            [
                new IppAttribute(Tag.Enum, IppAttributeNames.OutputDeviceJobStates, (int)JobState.Processing),
                new IppAttribute(Tag.Enum, IppAttributeNames.OutputDeviceJobStates, (int)JobState.Pending)
            ],
            [IppAttributeNames.JobIds] = 
            [
                new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, 1),
                new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, 2)
            ]
        };

        var result = mapper.Map<IDictionary<string, IppAttribute[]>, UpdateActiveJobsOperationAttributes>(dic);

        result.PrinterUri!.ToString().Should().Be("ipp://127.0.0.1/printer");
        result.OutputDeviceUuid!.ToString().Should().Be("uuid:123");
        result.OutputDeviceJobStates.Should().BeEquivalentTo(new[] { JobState.Processing, JobState.Pending });
        result.JobIds.Should().BeEquivalentTo(new[] { 1, 2 });
    }
}
