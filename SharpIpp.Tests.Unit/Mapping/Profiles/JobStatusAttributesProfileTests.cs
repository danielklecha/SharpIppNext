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

        dic.Should().ContainKey(JobAttribute.JobId);
        dic.Should().ContainKey(JobAttribute.JobUri);
        dic.Should().ContainKey(JobAttribute.JobPrinterUri);
        dic.Should().ContainKey(JobAttribute.JobName);
        dic.Should().ContainKey(JobAttribute.JobOriginatingUserName);
        dic.Should().ContainKey(JobAttribute.JobState);
        dic.Should().ContainKey(JobAttribute.JobStateReasons);
        dic.Should().ContainKey(JobAttribute.JobStateMessage);
        dic.Should().ContainKey(JobAttribute.JobImpressionsCompleted);
        dic.Should().ContainKey(JobAttribute.JobMediaSheetsCompleted);
        dic.Should().ContainKey(JobAttribute.JobKOctetsProcessed);
        dic.Should().ContainKey(JobAttribute.JobPagesCompleted);
        dic.Should().ContainKey(JobAttribute.JobPagesCompletedCol);
        dic.Should().ContainKey(JobAttribute.JobImpressionsCompletedCol);
        dic.Should().ContainKey(JobAttribute.JobMediaSheetsCompletedCol);
        dic.Should().ContainKey(JobAttribute.JobProcessingTime);
        dic.Should().ContainKey(JobAttribute.ErrorsCount);
        dic.Should().ContainKey(JobAttribute.WarningsCount);
        dic.Should().ContainKey(JobAttribute.NumberOfInterveningJobs);
        dic.Should().ContainKey(JobAttribute.OutputDeviceAssigned);
        dic.Should().ContainKey(JobAttribute.JobPrinterUpTime);
        dic.Should().ContainKey(JobAttribute.TimeAtCreation);
        dic.Should().ContainKey(JobAttribute.TimeAtProcessing);
        dic.Should().ContainKey(JobAttribute.TimeAtCompleted);
        dic.Should().ContainKey(JobAttribute.DateTimeAtCreation);
        dic.Should().ContainKey(JobAttribute.DateTimeAtProcessing);
        dic.Should().ContainKey(JobAttribute.DateTimeAtCompleted);
    }

    [TestMethod]
    public void Map_FromDict_WithAllAttributes_PopulatesAllProperties()
    {
        var dic = new Dictionary<string, IppAttribute[]>
        {
            [JobAttribute.JobId] = [new IppAttribute(Tag.Integer, JobAttribute.JobId, 42)],
            [JobAttribute.JobUri] = [new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://127.0.0.1:631/jobs/42")],
            [JobAttribute.JobPrinterUri] = [new IppAttribute(Tag.Uri, JobAttribute.JobPrinterUri, "ipp://127.0.0.1:631/")],
            [JobAttribute.JobName] = [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobName, "Test Job")],
            [JobAttribute.JobOriginatingUserName] = [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobOriginatingUserName, "test-user")],
            [JobAttribute.JobState] = [new IppAttribute(Tag.Enum, JobAttribute.JobState, (int)JobState.Processing)],
            [JobAttribute.JobStateReasons] = [new IppAttribute(Tag.Keyword, JobAttribute.JobStateReasons, "job-printing")],
            [JobAttribute.JobStateMessage] = [new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.JobStateMessage, "printing")],
            [JobAttribute.JobImpressionsCompleted] = [new IppAttribute(Tag.Integer, JobAttribute.JobImpressionsCompleted, 5)],
            [JobAttribute.JobMediaSheetsCompleted] = [new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheetsCompleted, 2)],
            [JobAttribute.JobKOctetsProcessed] = [new IppAttribute(Tag.Integer, JobAttribute.JobKOctetsProcessed, 100)],
            [JobAttribute.JobPagesCompleted] = [new IppAttribute(Tag.Integer, JobAttribute.JobPagesCompleted, 10)],
            [JobAttribute.JobProcessingTime] = [new IppAttribute(Tag.Integer, JobAttribute.JobProcessingTime, 30)],
            [JobAttribute.ErrorsCount] = [new IppAttribute(Tag.Integer, JobAttribute.ErrorsCount, 0)],
            [JobAttribute.WarningsCount] = [new IppAttribute(Tag.Integer, JobAttribute.WarningsCount, 1)],
            [JobAttribute.NumberOfInterveningJobs] = [new IppAttribute(Tag.Integer, JobAttribute.NumberOfInterveningJobs, 3)],
            [JobAttribute.OutputDeviceAssigned] = [new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.OutputDeviceAssigned, "printer-1")],
            [JobAttribute.JobPrinterUpTime] = [new IppAttribute(Tag.Integer, JobAttribute.JobPrinterUpTime, 9999)],
            [JobAttribute.TimeAtCreation] = [new IppAttribute(Tag.Integer, JobAttribute.TimeAtCreation, 100)],
            [JobAttribute.TimeAtProcessing] = [new IppAttribute(Tag.Integer, JobAttribute.TimeAtProcessing, 110)],
            [JobAttribute.TimeAtCompleted] = [new IppAttribute(Tag.Integer, JobAttribute.TimeAtCompleted, 120)],
            [JobAttribute.DateTimeAtCreation] = [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCreation, new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero))],
            [JobAttribute.DateTimeAtProcessing] = [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtProcessing, new DateTimeOffset(2024, 1, 1, 0, 1, 0, TimeSpan.Zero))],
            [JobAttribute.DateTimeAtCompleted] = [new IppAttribute(Tag.DateTime, JobAttribute.DateTimeAtCompleted, new DateTimeOffset(2024, 1, 1, 0, 2, 0, TimeSpan.Zero))],
        };
        // Add collection attributes
        dic[JobAttribute.JobPagesCompletedCol] = _mapper.Map<JobCounter, IEnumerable<IppAttribute>>(new JobCounter { Monochrome = 10 }).ToBegCollection(JobAttribute.JobPagesCompletedCol).ToArray();
        dic[JobAttribute.JobImpressionsCompletedCol] = _mapper.Map<JobCounter, IEnumerable<IppAttribute>>(new JobCounter { FullColor = 5 }).ToBegCollection(JobAttribute.JobImpressionsCompletedCol).ToArray();
        dic[JobAttribute.JobMediaSheetsCompletedCol] = _mapper.Map<JobCounter, IEnumerable<IppAttribute>>(new JobCounter { Blank = 2 }).ToBegCollection(JobAttribute.JobMediaSheetsCompletedCol).ToArray();

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
