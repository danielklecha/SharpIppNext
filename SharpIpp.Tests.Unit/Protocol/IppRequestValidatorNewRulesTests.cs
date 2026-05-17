using System;
using System.Diagnostics.CodeAnalysis;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestValidatorNewRulesTests
{
    // ─── Task 10.1: attributes-charset must be utf-8 ───────────────────────────

    [TestMethod]
    public void Validate_AttributesCharset_NonUtf8Value_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        // Replace the charset attribute with a non-utf-8 value
        request.OperationAttributes[0] = new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "iso-8859-1");
        request.Document = new System.IO.MemoryStream();

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_AttributesCharset_UppercaseUtf8_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        // Replace the charset attribute with uppercase UTF-8 (case-insensitive match)
        request.OperationAttributes[0] = new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "UTF-8");
        request.Document = new System.IO.MemoryStream();

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_AttributesCharset_LowercaseUtf8_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.2: notify-pull-method must be ippget ──────────────────────────

    [TestMethod]
    public void Validate_GetNotifications_AbsentNotifyPullMethod_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.GetNotifications);
        // notify-pull-method is absent

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_GetNotifications_WrongNotifyPullMethod_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.GetNotifications);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, SystemAttribute.NotifyPullMethod, "other"));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_GetNotifications_IppgetNotifyPullMethod_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.GetNotifications);
        request.OperationAttributes.Add(new IppAttribute(Tag.Keyword, SystemAttribute.NotifyPullMethod, "ippget"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.3: notify-subscription-id required ────────────────────────────

    [TestMethod]
    public void Validate_CancelSubscription_AbsentNotifySubscriptionId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.CancelSubscription);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_GetSubscriptionAttributes_AbsentNotifySubscriptionId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.GetSubscriptionAttributes);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_RenewSubscription_AbsentNotifySubscriptionId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.RenewSubscription);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_CancelSubscription_WithNotifySubscriptionId_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.CancelSubscription);
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, SystemAttribute.NotifySubscriptionId, 1));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.4: job-password / job-password-encryption co-presence ─────────

    [TestMethod]
    public void Validate_PrintJob_OnlyJobPassword_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.JobPassword, "secret"));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_PrintJob_OnlyJobPasswordEncryption_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobPasswordEncryption, "none"));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_PrintJob_BothJobPasswordAndEncryption_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.JobPassword, "secret"));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobPasswordEncryption, "none"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_PrintJob_NeitherJobPasswordNorEncryption_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.5: media / media-col mutual exclusion at job level ────────────

    [TestMethod]
    public void Validate_CreateJob_BothMediaAndMediaCol_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4"));
        // Add a minimal media-col collection
        request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, JobAttribute.MediaCol, NoValue.Instance));
        request.JobAttributes.Add(new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_CreateJob_OnlyMedia_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_CreateJob_OnlyMediaCol_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.CreateJob);
        request.JobAttributes.Add(new IppAttribute(Tag.BegCollection, JobAttribute.MediaCol, NoValue.Instance));
        request.JobAttributes.Add(new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.6: page-ranges ascending non-overlapping ──────────────────────

    [TestMethod]
    public void Validate_PageRanges_OverlappingRanges_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(1, 5)));
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(3, 8)));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_PageRanges_DescendingRanges_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(5, 10)));
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(1, 4)));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_PageRanges_ValidAscendingRanges_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(1, 5)));
        request.JobAttributes.Add(new IppAttribute(Tag.RangeOfInteger, JobAttribute.PageRanges, new SharpIpp.Protocol.Models.Range(7, 10)));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.7: copies >= 1 ────────────────────────────────────────────────

    [TestMethod]
    public void Validate_Copies_Zero_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, 0));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_Copies_Negative_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, -1));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_Copies_One_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.Copies, 1));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_Copies_Absent_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // no copies attribute — FirstOrDefault returns default struct, guard should skip validation

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow(JobAttribute.Copies)]
    [DataRow(JobAttribute.JobPriority)]
    [DataRow(JobAttribute.NumberUp)]
    public void Validate_IntegerJobAttribute_NonIntegerValue_ShouldNotThrow(string attributeName)
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // attribute is present but carries a non-integer value — the `is int` guard should skip validation
        request.JobAttributes.Add(new IppAttribute(Tag.NoValue, attributeName, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.8: job-priority range 1-100 ───────────────────────────────────

    [TestMethod]
    public void Validate_JobPriority_Zero_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobPriority, 0));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_JobPriority_OneHundredAndOne_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobPriority, 101));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_JobPriority_Fifty_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobPriority, 50));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.9: number-up >= 1 ─────────────────────────────────────────────

    [TestMethod]
    public void Validate_NumberUp_Zero_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.NumberUp, 0));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_NumberUp_Two_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.NumberUp, 2));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.10: resource-id required ──────────────────────────────────────

    [TestMethod]
    public void Validate_CancelResource_AbsentResourceId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.CancelResource);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_GetResourceAttributes_AbsentResourceId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.GetResourceAttributes);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_InstallResource_AbsentResourceId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.InstallResource);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_SendResourceData_AbsentResourceId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.SendResourceData);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_SetResourceAttributes_AbsentResourceId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.SetResourceAttributes);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_CancelResource_WithResourceId_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.CancelResource);
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, 1));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.11: printer-id required ───────────────────────────────────────

    [TestMethod]
    public void Validate_AllocatePrinterResources_AbsentPrinterId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.AllocatePrinterResources);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_DeallocatePrinterResources_AbsentPrinterId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.DeallocatePrinterResources);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_DeletePrinter_AbsentPrinterId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.DeletePrinter);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_GetPrinterResources_AbsentPrinterId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.GetPrinterResources);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_ShutdownOnePrinter_AbsentPrinterId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.ShutdownOnePrinter);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_StartupOnePrinter_AbsentPrinterId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.StartupOnePrinter);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_RestartOnePrinter_AbsentPrinterId_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.RestartOnePrinter);

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_AllocatePrinterResources_WithPrinterId_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.AllocatePrinterResources);
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.PrinterId, 1));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.12: fidelity-based validation ─────────────────────────────────

    [TestMethod]
    public void Validate_FidelityTrue_UnsupportedMedia_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestValidator();
        validator.Context.MediaSupported = [new Media("iso_a4", true)];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, "na_letter"));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_FidelityTrue_SupportedMedia_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.MediaSupported = [new Media("iso_a4", true)];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityFalse_UnsupportedMedia_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.MediaSupported = [new Media("iso_a4", true)];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // ipp-attribute-fidelity is not set (defaults to false)
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Media, "na_letter"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityFinishings_UnsupportedValue_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestValidator();
        validator.Context.FinishingsSupported = [Finishings.None];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_FidelityFinishings_SupportedValue_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.FinishingsSupported = [Finishings.Staple];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityFinishings_FidelityFalse_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.FinishingsSupported = [Finishings.None];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // ipp-attribute-fidelity is not set (defaults to false)
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityFinishings_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        // validator.Context.FinishingsSupported is null by default

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityFinishings_MultipleValuesSecondUnsupported_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestValidator();
        validator.Context.FinishingsSupported = [Finishings.None];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.None));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, (int)Finishings.Staple));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_FidelitySides_UnsupportedValue_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestValidator();
        validator.Context.SidesSupported = [Sides.OneSided];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, Sides.TwoSidedLongEdge.Value));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_FidelitySides_SupportedValue_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.SidesSupported = [Sides.OneSided];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, Sides.OneSided.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelitySides_FidelityFalse_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.SidesSupported = [Sides.OneSided];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // ipp-attribute-fidelity is not set (defaults to false)
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, Sides.TwoSidedLongEdge.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelitySides_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        // validator.Context.SidesSupported is null by default

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.Sides, Sides.TwoSidedLongEdge.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityPrintQuality_UnsupportedValue_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestValidator();
        validator.Context.PrintQualitySupported = [PrintQuality.Normal];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)PrintQuality.Draft));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_FidelityPrintQuality_SupportedValue_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.PrintQualitySupported = [PrintQuality.Normal];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)PrintQuality.Normal));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityPrintQuality_FidelityFalse_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.PrintQualitySupported = [PrintQuality.Normal];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // ipp-attribute-fidelity is not set (defaults to false)
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)PrintQuality.Draft));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityPrintQuality_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        // validator.Context.PrintQualitySupported is null by default

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.PrintQuality, (int)PrintQuality.Draft));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityOrientationRequested_UnsupportedValue_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestValidator();
        validator.Context.OrientationRequestedSupported = [Orientation.Portrait];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)Orientation.Landscape));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_FidelityOrientationRequested_SupportedValue_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.OrientationRequestedSupported = [Orientation.Portrait];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)Orientation.Portrait));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityOrientationRequested_FidelityFalse_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.OrientationRequestedSupported = [Orientation.Portrait];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // ipp-attribute-fidelity is not set (defaults to false)
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)Orientation.Landscape));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityOrientationRequested_NullSupportedList_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        // validator.Context.OrientationRequestedSupported is null by default

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OrientationRequested, (int)Orientation.Landscape));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityPrintColorMode_UnsupportedValue_ShouldThrowClientErrorAttributesOrValuesNotSupported()
    {
        var validator = new IppRequestValidator();
        validator.Context.PrintColorModeSupported = [PrintColorMode.Color];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintColorMode, PrintColorMode.Monochrome.Value));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
    }

    [TestMethod]
    public void Validate_FidelityPrintColorMode_SupportedValue_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.PrintColorModeSupported = [PrintColorMode.Color];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintColorMode, PrintColorMode.Color.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityPrintColorMode_FidelityFalse_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.PrintColorModeSupported = [PrintColorMode.Color];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        // ipp-attribute-fidelity is not set (defaults to false)
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintColorMode, PrintColorMode.Monochrome.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    [DataRow("null-list")]
    [DataRow("empty-list")]
    public void Validate_FidelityPrintColorMode_NoEffectiveSupportedList_ShouldNotThrow(string scenario)
    {
        var validator = new IppRequestValidator();
        if (scenario == "empty-list")
            validator.Context.PrintColorModeSupported = [];
        // else null by default — both skip the validation block

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintColorMode, PrintColorMode.Monochrome.Value));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_FidelityPrintColorMode_NoValue_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        validator.Context.PrintColorModeSupported = [PrintColorMode.Monochrome];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        // NoValue signals "use printer default" — should be skipped, not validated against supported list
        request.JobAttributes.Add(new IppAttribute(Tag.NoValue, JobAttribute.PrintColorMode, NoValue.Instance));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Task 10.13: subscription-attributes-group required ───────────────────

    [TestMethod]
    public void Validate_CreatePrinterSubscriptions_EmptySubscriptionAttributes_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.CreatePrinterSubscriptions);
        // subscription attributes are empty (not added)

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_CreateSystemSubscriptions_EmptySubscriptionAttributes_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.CreateSystemSubscriptions);
        // subscription attributes are empty (not added)

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_CreatePrinterSubscriptions_WithSubscriptionAttributes_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicSystemRequest(IppOperation.CreatePrinterSubscriptions);
        request.SubscriptionAttributes.Add(new IppAttribute(Tag.Keyword, "notify-events", "job-completed"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Line 349: print-quality non-int value skips throw ────────────────────

    [TestMethod]
    public void ValidateFidelityBasedJobAttributes_PrintQuality_NonIntValue_ShouldNotThrow()
    {
        // Line 349: pqAttr.Value is int pqInt — pattern fails for non-int → no throw
        var validator = new IppRequestValidator();
        validator.Context.PrintQualitySupported = [PrintQuality.Normal];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        // Use keyword tag with string value — Value is not an int, so pattern match fails
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.PrintQuality, "draft"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Line 359: orientation-requested non-int value skips throw ────────────

    [TestMethod]
    public void ValidateFidelityBasedJobAttributes_OrientationRequested_NonIntValue_ShouldNotThrow()
    {
        // Line 359: orientAttr.Value is int orientInt — pattern fails for non-int → no throw
        var validator = new IppRequestValidator();
        validator.Context.OrientationRequestedSupported = [Orientation.Portrait];

        var request = CreateBasicRequest(IppOperation.PrintJob);
        request.Document = new System.IO.MemoryStream();
        request.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        // Use keyword tag with string value — Value is not an int, so pattern match fails
        request.JobAttributes.Add(new IppAttribute(Tag.Keyword, JobAttribute.OrientationRequested, "landscape"));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Lines 668, 679, 692: ValidateOperationAttributesGroup=false skips required-attribute checks ───

    [TestMethod]
    public void ValidateOperationRules_CancelSubscription_GroupDisabled_AbsentSubscriptionId_ShouldNotThrow()
    {
        // Line 668: ValidateOperationAttributesGroup=false → condition short-circuits → no throw
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(IppOperation.CancelSubscription);
        // notify-subscription-id is absent — but group validation is disabled

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_GetSubscriptionAttributes_GroupDisabled_AbsentSubscriptionId_ShouldNotThrow()
    {
        // Line 668: ValidateOperationAttributesGroup=false → condition short-circuits → no throw
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(IppOperation.GetSubscriptionAttributes);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_RenewSubscription_GroupDisabled_AbsentSubscriptionId_ShouldNotThrow()
    {
        // Line 668: ValidateOperationAttributesGroup=false → condition short-circuits → no throw
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(IppOperation.RenewSubscription);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_CancelResource_GroupDisabled_AbsentResourceId_ShouldNotThrow()
    {
        // Line 679: ValidateOperationAttributesGroup=false → condition short-circuits → no throw
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(IppOperation.CancelResource);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_GetResourceAttributes_GroupDisabled_AbsentResourceId_ShouldNotThrow()
    {
        // Line 679: ValidateOperationAttributesGroup=false → condition short-circuits → no throw
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(IppOperation.GetResourceAttributes);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_AllocatePrinterResources_GroupDisabled_AbsentPrinterId_ShouldNotThrow()
    {
        // Line 692: ValidateOperationAttributesGroup=false → condition short-circuits → no throw
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(IppOperation.AllocatePrinterResources);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_DeletePrinter_GroupDisabled_AbsentPrinterId_ShouldNotThrow()
    {
        // Line 692: ValidateOperationAttributesGroup=false → condition short-circuits → no throw
        var validator = new IppRequestValidator { ValidateOperationAttributesGroup = false };
        var request = CreateBasicSystemRequest(IppOperation.DeletePrinter);

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    // ─── Helpers ───────────────────────────────────────────────────────────────

    /// <summary>
    /// Creates a basic IPP request with printer-uri (for printer-targeted operations).
    /// </summary>
    private static IppRequestMessage CreateBasicRequest(IppOperation operation)
    {
        var request = new IppRequestMessage
        {
            IppOperation = operation,
            RequestId = 123,
        };

        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/")
        ]);

        return request;
    }

    /// <summary>
    /// Creates a basic IPP request with system-uri (for system service operations).
    /// </summary>
    private static IppRequestMessage CreateBasicSystemRequest(IppOperation operation)
    {
        var request = new IppRequestMessage
        {
            IppOperation = operation,
            RequestId = 123,
        };

        request.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new IppAttribute(Tag.Uri, SystemAttribute.SystemUri, "ipp://127.0.0.1:8631/system")
        ]);

        return request;
    }
    // ─── Task 11: Update-Active-Jobs co-dependency ──────────────────────────────
    // See: PWG 5100.18-2025 Section 5.7.1

    [TestMethod]
    public void Validate_UpdateActiveJobs_OnlyJobIds_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.UpdateActiveJobs);
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, "ipp://127.0.0.1/output-device"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobIds, 1));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_UpdateActiveJobs_OnlyOutputDeviceJobStates_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.UpdateActiveJobs);
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, "ipp://127.0.0.1/output-device"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)JobState.Processing));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }

    [TestMethod]
    public void Validate_UpdateActiveJobs_BothPresent_ShouldNotThrow()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.UpdateActiveJobs);
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, "ipp://127.0.0.1/output-device"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobIds, 1));
        request.OperationAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)JobState.Processing));

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_UpdateActiveJobs_MismatchedCardinality_ShouldThrowClientErrorBadRequest()
    {
        var validator = new IppRequestValidator();
        var request = CreateBasicRequest(IppOperation.UpdateActiveJobs);
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, "ipp://127.0.0.1/output-device"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobIds, 1));
        request.OperationAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.JobIds, 2));
        request.OperationAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)JobState.Processing));

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .Which.StatusCode.Should().Be(IppStatusCode.ClientErrorBadRequest);
    }
}

