using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class JobStatusAttributesProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_ToDict_WithAllProperties_EmitsAllAttributes()
    {
        var src = new JobStatusAttributes
        {
            JobId = 42,
            JobUri = "ipp://127.0.0.1:631/jobs/42",
            JobPrinterUri = new Uri("ipp://127.0.0.1:631/"),
            JobName = "Test Job",
            JobOriginatingUserName = "test-user",
            JobState = JobState.Processing,
            JobStateReasons = [JobStateReason.JobPrinting],
            JobStateMessage = "printing",
            JobImpressionsCompleted = 5,
            JobMediaSheetsCompleted = 2,
            JobKOctetsProcessed = 100,
            JobPagesCompleted = 10,
            JobPagesCompletedCol = new JobCounter { Monochrome = 10 },
            JobImpressionsCompletedCol = new JobCounter { FullColor = 5 },
            JobMediaSheetsCompletedCol = new JobCounter { Blank = 2 },
            JobProcessingTime = 30,
            ErrorsCount = 0,
            WarningsCount = 1,
            NumberOfInterveningJobs = 3,
            OutputDeviceAssigned = "printer-1",
            JobPrinterUpTime = 9999,
            TimeAtCreation = 100,
            TimeAtProcessing = 110,
            TimeAtCompleted = 120,
            DateTimeAtCreation = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            DateTimeAtProcessing = new DateTimeOffset(2024, 1, 1, 0, 1, 0, TimeSpan.Zero),
            DateTimeAtCompleted = new DateTimeOffset(2024, 1, 1, 0, 2, 0, TimeSpan.Zero),
        };

        var dic = _mapper.Map<IDictionary<string, IppAttribute[]>>(src);

        dic.Should().ContainKey(IppAttributeNames.JobId);
        dic.Should().ContainKey(IppAttributeNames.JobUri);
        dic.Should().ContainKey(IppAttributeNames.JobPrinterUri);
        dic.Should().ContainKey(IppAttributeNames.JobName);
        dic.Should().ContainKey(IppAttributeNames.JobOriginatingUserName);
        dic.Should().ContainKey(IppAttributeNames.JobState);
        dic.Should().ContainKey(IppAttributeNames.JobStateReasons);
        dic.Should().ContainKey(IppAttributeNames.JobStateMessage);
        dic.Should().ContainKey(IppAttributeNames.JobImpressionsCompleted);
        dic.Should().ContainKey(IppAttributeNames.JobMediaSheetsCompleted);
        dic.Should().ContainKey(IppAttributeNames.JobKOctetsProcessed);
        dic.Should().ContainKey(IppAttributeNames.JobPagesCompleted);
        dic.Should().ContainKey(IppAttributeNames.JobPagesCompletedCol);
        dic.Should().ContainKey(IppAttributeNames.JobImpressionsCompletedCol);
        dic.Should().ContainKey(IppAttributeNames.JobMediaSheetsCompletedCol);
        dic.Should().ContainKey(IppAttributeNames.JobProcessingTime);
        dic.Should().ContainKey(IppAttributeNames.ErrorsCount);
        dic.Should().ContainKey(IppAttributeNames.WarningsCount);
        dic.Should().ContainKey(IppAttributeNames.NumberOfInterveningJobs);
        dic.Should().ContainKey(IppAttributeNames.OutputDeviceAssigned);
        dic.Should().ContainKey(IppAttributeNames.JobPrinterUpTime);
        dic.Should().ContainKey(IppAttributeNames.TimeAtCreation);
        dic.Should().ContainKey(IppAttributeNames.TimeAtProcessing);
        dic.Should().ContainKey(IppAttributeNames.TimeAtCompleted);
        dic.Should().ContainKey(IppAttributeNames.DateTimeAtCreation);
        dic.Should().ContainKey(IppAttributeNames.DateTimeAtProcessing);
        dic.Should().ContainKey(IppAttributeNames.DateTimeAtCompleted);
    }

    [TestMethod]
    public void Map_FromDict_WithAllAttributes_PopulatesAllProperties()
    {
        var dic = new Dictionary<string, IppAttribute[]>
        {
            [IppAttributeNames.JobId] = [new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 42)],
            [IppAttributeNames.JobUri] = [new IppAttribute(Tag.Uri, IppAttributeNames.JobUri, "ipp://127.0.0.1:631/jobs/42")],
            [IppAttributeNames.JobPrinterUri] = [new IppAttribute(Tag.Uri, IppAttributeNames.JobPrinterUri, "ipp://127.0.0.1:631/")],
            [IppAttributeNames.JobName] = [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobName, "Test Job")],
            [IppAttributeNames.JobOriginatingUserName] = [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobOriginatingUserName, "test-user")],
            [IppAttributeNames.JobState] = [new IppAttribute(Tag.Enum, IppAttributeNames.JobState, (int)JobState.Processing)],
            [IppAttributeNames.JobStateReasons] = [new IppAttribute(Tag.Keyword, IppAttributeNames.JobStateReasons, "job-printing")],
            [IppAttributeNames.JobStateMessage] = [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.JobStateMessage, "printing")],
            [IppAttributeNames.JobImpressionsCompleted] = [new IppAttribute(Tag.Integer, IppAttributeNames.JobImpressionsCompleted, 5)],
            [IppAttributeNames.JobMediaSheetsCompleted] = [new IppAttribute(Tag.Integer, IppAttributeNames.JobMediaSheetsCompleted, 2)],
            [IppAttributeNames.JobKOctetsProcessed] = [new IppAttribute(Tag.Integer, IppAttributeNames.JobKOctetsProcessed, 100)],
            [IppAttributeNames.JobPagesCompleted] = [new IppAttribute(Tag.Integer, IppAttributeNames.JobPagesCompleted, 10)],
            [IppAttributeNames.JobProcessingTime] = [new IppAttribute(Tag.Integer, IppAttributeNames.JobProcessingTime, 30)],
            [IppAttributeNames.ErrorsCount] = [new IppAttribute(Tag.Integer, IppAttributeNames.ErrorsCount, 0)],
            [IppAttributeNames.WarningsCount] = [new IppAttribute(Tag.Integer, IppAttributeNames.WarningsCount, 1)],
            [IppAttributeNames.NumberOfInterveningJobs] = [new IppAttribute(Tag.Integer, IppAttributeNames.NumberOfInterveningJobs, 3)],
            [IppAttributeNames.OutputDeviceAssigned] = [new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.OutputDeviceAssigned, "printer-1")],
            [IppAttributeNames.JobPrinterUpTime] = [new IppAttribute(Tag.Integer, IppAttributeNames.JobPrinterUpTime, 9999)],
            [IppAttributeNames.TimeAtCreation] = [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCreation, 100)],
            [IppAttributeNames.TimeAtProcessing] = [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtProcessing, 110)],
            [IppAttributeNames.TimeAtCompleted] = [new IppAttribute(Tag.Integer, IppAttributeNames.TimeAtCompleted, 120)],
            [IppAttributeNames.DateTimeAtCreation] = [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCreation, new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero))],
            [IppAttributeNames.DateTimeAtProcessing] = [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtProcessing, new DateTimeOffset(2024, 1, 1, 0, 1, 0, TimeSpan.Zero))],
            [IppAttributeNames.DateTimeAtCompleted] = [new IppAttribute(Tag.DateTime, IppAttributeNames.DateTimeAtCompleted, new DateTimeOffset(2024, 1, 1, 0, 2, 0, TimeSpan.Zero))],
        };
        // Add collection attributes
        dic[IppAttributeNames.JobPagesCompletedCol] = _mapper.Map<JobCounter, IEnumerable<IppAttribute>>(new JobCounter { Monochrome = 10 }).ToBegCollection(IppAttributeNames.JobPagesCompletedCol).ToArray();
        dic[IppAttributeNames.JobImpressionsCompletedCol] = _mapper.Map<JobCounter, IEnumerable<IppAttribute>>(new JobCounter { FullColor = 5 }).ToBegCollection(IppAttributeNames.JobImpressionsCompletedCol).ToArray();
        dic[IppAttributeNames.JobMediaSheetsCompletedCol] = _mapper.Map<JobCounter, IEnumerable<IppAttribute>>(new JobCounter { Blank = 2 }).ToBegCollection(IppAttributeNames.JobMediaSheetsCompletedCol).ToArray();

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, JobStatusAttributes>(dic);

        dst.JobId.Should().Be(42);
        dst.JobUri.Should().Be("ipp://127.0.0.1:631/jobs/42");
        dst.JobPrinterUri.Should().Be(new Uri("ipp://127.0.0.1:631/"));
        dst.JobName.Should().Be("Test Job");
        dst.JobOriginatingUserName.Should().Be("test-user");
        dst.JobState.Should().Be(JobState.Processing);
        dst.JobStateReasons.Should().NotBeNull();
        dst.JobStateMessage.Should().Be("printing");
        dst.JobImpressionsCompleted.Should().Be(5);
        dst.JobMediaSheetsCompleted.Should().Be(2);
        dst.JobKOctetsProcessed.Should().Be(100);
        dst.JobPagesCompleted.Should().Be(10);
        dst.JobPagesCompletedCol.Should().NotBeNull();
        dst.JobImpressionsCompletedCol.Should().NotBeNull();
        dst.JobMediaSheetsCompletedCol.Should().NotBeNull();
        dst.JobProcessingTime.Should().Be(30);
        dst.ErrorsCount.Should().Be(0);
        dst.WarningsCount.Should().Be(1);
        dst.NumberOfInterveningJobs.Should().Be(3);
        dst.OutputDeviceAssigned.Should().Be("printer-1");
        dst.JobPrinterUpTime.Should().Be(9999);
        dst.TimeAtCreation.Should().Be(100);
        dst.TimeAtProcessing.Should().Be(110);
        dst.TimeAtCompleted.Should().Be(120);
        dst.DateTimeAtCreation.Should().Be(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero));
        dst.DateTimeAtProcessing.Should().Be(new DateTimeOffset(2024, 1, 1, 0, 1, 0, TimeSpan.Zero));
        dst.DateTimeAtCompleted.Should().Be(new DateTimeOffset(2024, 1, 1, 0, 2, 0, TimeSpan.Zero));
    }

    [TestMethod]
    public void Map_RoundTrip_PreservesAllFields()
    {
        var original = new JobStatusAttributes
        {
            JobId = 7,
            JobUri = "ipp://127.0.0.1:631/jobs/7",
            JobState = JobState.Completed,
            JobStateReasons = [JobStateReason.JobCompletedSuccessfully],
            JobStateMessage = "completed",
            JobImpressionsCompleted = 3,
            JobMediaSheetsCompleted = 1,
            JobKOctetsProcessed = 50,
            JobPagesCompleted = 6,
            JobPagesCompletedCol = new JobCounter { Monochrome = 6 },
            JobImpressionsCompletedCol = new JobCounter { Monochrome = 3 },
            JobMediaSheetsCompletedCol = new JobCounter { Monochrome = 1 },
            JobProcessingTime = 15,
            ErrorsCount = 0,
            WarningsCount = 0,
            TimeAtCreation = 200,
            TimeAtProcessing = 210,
            TimeAtCompleted = 225,
        };

        var dic = _mapper.Map<IDictionary<string, IppAttribute[]>>(original);
        var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, JobStatusAttributes>(dic);

        roundTripped.JobId.Should().Be(original.JobId);
        roundTripped.JobUri.Should().Be(original.JobUri);
        roundTripped.JobState.Should().Be(original.JobState);
        roundTripped.JobStateMessage.Should().Be(original.JobStateMessage);
        roundTripped.JobImpressionsCompleted.Should().Be(original.JobImpressionsCompleted);
        roundTripped.JobMediaSheetsCompleted.Should().Be(original.JobMediaSheetsCompleted);
        roundTripped.JobKOctetsProcessed.Should().Be(original.JobKOctetsProcessed);
        roundTripped.JobPagesCompleted.Should().Be(original.JobPagesCompleted);
        roundTripped.JobPagesCompletedCol.Should().NotBeNull();
        roundTripped.JobImpressionsCompletedCol.Should().NotBeNull();
        roundTripped.JobMediaSheetsCompletedCol.Should().NotBeNull();
        roundTripped.JobProcessingTime.Should().Be(original.JobProcessingTime);
        roundTripped.ErrorsCount.Should().Be(original.ErrorsCount);
        roundTripped.WarningsCount.Should().Be(original.WarningsCount);
        roundTripped.TimeAtCreation.Should().Be(original.TimeAtCreation);
        roundTripped.TimeAtProcessing.Should().Be(original.TimeAtProcessing);
        roundTripped.TimeAtCompleted.Should().Be(original.TimeAtCompleted);
    }
}
