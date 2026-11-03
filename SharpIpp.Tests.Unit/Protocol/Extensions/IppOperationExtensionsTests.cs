using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

[TestClass]
public class IppOperationExtensionsTests
{
    [TestMethod]
    public void IsSystemServiceOperation_ShouldReturnExpectedValues()
    {
        IppOperation[] expected =
        [
            IppOperation.AllocatePrinterResources,
            IppOperation.CreatePrinter,
            IppOperation.DeallocatePrinterResources,
            IppOperation.DeletePrinter,
            IppOperation.GetPrinters,
            IppOperation.GetPrinterResources,
            IppOperation.ShutdownOnePrinter,
            IppOperation.StartupOnePrinter,
            IppOperation.RestartOnePrinter,
            IppOperation.CancelResource,
            IppOperation.CreateResource,
            IppOperation.InstallResource,
            IppOperation.SendResourceData,
            IppOperation.SetResourceAttributes,
            IppOperation.CreateResourceSubscriptions,
            IppOperation.CreateSystemSubscriptions,
            IppOperation.DisableAllPrinters,
            IppOperation.EnableAllPrinters,
            IppOperation.GetResources,
            IppOperation.GetSystemAttributes,
            IppOperation.GetSystemSupportedValues,
            IppOperation.PauseAllPrinters,
            IppOperation.PauseAllPrintersAfterCurrentJob,
            IppOperation.RegisterOutputDevice,
            IppOperation.RestartSystem,
            IppOperation.ResumeAllPrinters,
            IppOperation.SetSystemAttributes,
            IppOperation.ShutdownAllPrinters,
            IppOperation.StartupAllPrinters,
            IppOperation.CancelSubscription,
            IppOperation.GetNotifications,
            IppOperation.GetSubscriptionAttributes,
            IppOperation.GetSubscriptions,
            IppOperation.RenewSubscription,
        ];

        foreach (var op in expected)
        {
            op.IsSystemServiceOperation().Should().BeTrue($"{op} should be system service operation");
        }

        IppOperation.CreateJob.IsSystemServiceOperation().Should().BeFalse();
        IppOperation.PrintJob.IsSystemServiceOperation().Should().BeFalse();
        ((IppOperation)(-1)).IsSystemServiceOperation().Should().BeFalse();
    }

    [TestMethod]
    public void IsStrictSystemServiceOperation_ShouldReturnExpectedValues()
    {
        IppOperation[] expected =
        [
            IppOperation.AllocatePrinterResources,
            IppOperation.CreatePrinter,
            IppOperation.DeallocatePrinterResources,
            IppOperation.DeletePrinter,
            IppOperation.GetPrinters,
            IppOperation.GetPrinterResources,
            IppOperation.ShutdownOnePrinter,
            IppOperation.StartupOnePrinter,
            IppOperation.RestartOnePrinter,
            IppOperation.CancelResource,
            IppOperation.CreateResource,
            IppOperation.InstallResource,
            IppOperation.SendResourceData,
            IppOperation.SetResourceAttributes,
            IppOperation.CreateResourceSubscriptions,
            IppOperation.CreateSystemSubscriptions,
            IppOperation.DisableAllPrinters,
            IppOperation.EnableAllPrinters,
            IppOperation.GetResources,
            IppOperation.GetSystemAttributes,
            IppOperation.GetSystemSupportedValues,
            IppOperation.PauseAllPrinters,
            IppOperation.PauseAllPrintersAfterCurrentJob,
            IppOperation.RegisterOutputDevice,
            IppOperation.RestartSystem,
            IppOperation.ResumeAllPrinters,
            IppOperation.SetSystemAttributes,
            IppOperation.ShutdownAllPrinters,
            IppOperation.StartupAllPrinters,
        ];

        foreach (var op in expected)
        {
            op.IsStrictSystemServiceOperation().Should().BeTrue($"{op} should be strict system service operation");
        }

        IppOperation.CancelSubscription.IsStrictSystemServiceOperation().Should().BeFalse();
        IppOperation.PrintJob.IsStrictSystemServiceOperation().Should().BeFalse();
        ((IppOperation)(-1)).IsStrictSystemServiceOperation().Should().BeFalse();
    }

    [TestMethod]
    public void IsSubscriptionOperation_ShouldReturnExpectedValues()
    {
        IppOperation[] expected =
        [
            IppOperation.CancelSubscription,
            IppOperation.GetNotifications,
            IppOperation.GetSubscriptionAttributes,
            IppOperation.GetSubscriptions,
            IppOperation.RenewSubscription,
        ];

        foreach (var op in expected)
        {
            op.IsSubscriptionOperation().Should().BeTrue($"{op} should be subscription operation");
        }

        IppOperation.CreateJob.IsSubscriptionOperation().Should().BeFalse();
        IppOperation.PrintJob.IsSubscriptionOperation().Should().BeFalse();
        ((IppOperation)(-1)).IsSubscriptionOperation().Should().BeFalse();
    }

    [TestMethod]
    public void IsJobTargetOperation_ShouldReturnExpectedValues()
    {
        IppOperation[] expected =
        [
            IppOperation.CancelJob,
            IppOperation.GetJobAttributes,
            IppOperation.HoldJob,
            IppOperation.ReleaseJob,
            IppOperation.RestartJob,
            IppOperation.SetJobAttributes,
            IppOperation.SendDocument,
            IppOperation.SendUri,
            IppOperation.CloseJob,
            IppOperation.ResubmitJob,
            IppOperation.ReprocessJob,
            IppOperation.ResumeJob,
            IppOperation.PromoteJob,
            IppOperation.ScheduleJobAfter,
            IppOperation.GetDocuments,
        ];

        foreach (var op in expected)
        {
            op.IsJobTargetOperation().Should().BeTrue($"{op} should be job target operation");
        }

        IppOperation.PrintJob.IsJobTargetOperation().Should().BeFalse();
        IppOperation.CreateJob.IsJobTargetOperation().Should().BeFalse();
        ((IppOperation)(-1)).IsJobTargetOperation().Should().BeFalse();
    }

    [TestMethod]
    public void IsPwg51005DocumentTargetOperation_ShouldReturnExpectedValues()
    {
        IppOperation[] expected =
        [
            IppOperation.CancelDocument,
            IppOperation.GetDocumentAttributes,
            IppOperation.GetDocuments,
            IppOperation.SetDocumentAttributes,
            IppOperation.DeleteDocument,
            IppOperation.AddDocumentImages,
            IppOperation.AcknowledgeDocument,
            IppOperation.FetchDocument,
            IppOperation.UpdateDocumentStatus,
        ];

        foreach (var op in expected)
        {
            op.IsPwg51005DocumentTargetOperation().Should().BeTrue($"{op} should be PWG 5100.5 document target operation");
        }

        IppOperation.PrintJob.IsPwg51005DocumentTargetOperation().Should().BeFalse();
        IppOperation.CreateJob.IsPwg51005DocumentTargetOperation().Should().BeFalse();
        ((IppOperation)(-1)).IsPwg51005DocumentTargetOperation().Should().BeFalse();
    }

    [TestMethod]
    public void SupportsJobUriTarget_WhenJobTargetOperation_ShouldReturnTrue()
    {
        IppOperation[] jobTargetOps =
        [
            IppOperation.CancelJob,
            IppOperation.GetJobAttributes,
            IppOperation.HoldJob,
            IppOperation.ReleaseJob,
            IppOperation.RestartJob,
            IppOperation.SetJobAttributes,
            IppOperation.SendDocument,
            IppOperation.SendUri,
            IppOperation.CloseJob,
            IppOperation.ResubmitJob,
            IppOperation.ReprocessJob,
            IppOperation.ResumeJob,
            IppOperation.PromoteJob,
            IppOperation.ScheduleJobAfter,
            IppOperation.GetDocuments,
        ];

        foreach (var op in jobTargetOps)
        {
            op.SupportsJobUriTarget().Should().BeTrue($"{op} should support job URI target via IsJobTargetOperation");
        }
    }

    [TestMethod]
    public void SupportsJobUriTarget_WhenDocumentTargetOperation_ShouldReturnTrue()
    {
        IppOperation[] docTargetOps =
        [
            IppOperation.CancelDocument,
            IppOperation.GetDocumentAttributes,
            IppOperation.SetDocumentAttributes,
            IppOperation.DeleteDocument,
            IppOperation.AddDocumentImages,
            IppOperation.AcknowledgeDocument,
            IppOperation.FetchDocument,
            IppOperation.UpdateDocumentStatus,
        ];

        foreach (var op in docTargetOps)
        {
            op.SupportsJobUriTarget().Should().BeTrue($"{op} should support job URI target via IsPwg51005DocumentTargetOperation");
        }
    }

    [TestMethod]
    public void SupportsJobUriTarget_WhenCreateJobSubscriptions_ShouldReturnTrue()
    {
        IppOperation.CreateJobSubscriptions.SupportsJobUriTarget().Should().BeTrue();
    }

    [TestMethod]
    public void SupportsJobUriTarget_WhenValidateDocument_ShouldReturnTrue()
    {
        IppOperation.ValidateDocument.SupportsJobUriTarget().Should().BeTrue();
    }

    [TestMethod]
    public void SupportsJobUriTarget_WhenNonJobUriTargetOperation_ShouldReturnFalse()
    {
        IppOperation[] nonSupportingOps =
        [
            IppOperation.PrintJob,
            IppOperation.PrintUri,
            IppOperation.ValidateJob,
            IppOperation.CreateJob,
            IppOperation.GetPrinterAttributes,
            IppOperation.GetJobs,
            IppOperation.PausePrinter,
            IppOperation.ResumePrinter,
            IppOperation.PurgeJobs,
            (IppOperation)(-1),
        ];

        foreach (var op in nonSupportingOps)
        {
            op.SupportsJobUriTarget().Should().BeFalse($"{op} should not support job URI target");
        }
    }
}

