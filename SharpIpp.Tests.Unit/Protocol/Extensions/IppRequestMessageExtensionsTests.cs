using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestMessageExtensionsTests
{
    [TestMethod]
    public void Validate_MessageIsNull_ShouldThrowArgumentNullException()
    {
        Action act = () => ValidateCoreOnly(null);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Validate_WhenCoreRulesDisabled_ShouldSkipCoreValidation()
    {
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = -1,
            Version = new IppVersion(0, 9)
        };

        var validator = CreateValidator(v =>
        {
            v.ValidateCoreRules = false;
            v.ValidateOperationSpecificRules = false;
            v.ValidateJobAttributesGroup = false;
            v.ValidateDocumentAttributesGroup = false;
        });

        Action act = () => validator.Validate(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenOperationSpecificRulesDisabled_ShouldSkipOperationSpecificValidation()
    {
        var message = CreateMessage(IppOperation.PrintJob);

        Action act = () => ValidateWith(message, v => v.ValidateOperationSpecificRules = false);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenJobAttributesGroupDisabled_ShouldSkipJobValidation()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, 3));
        message.JobAttributes.Add(new IppAttribute(Tag.BegCollection, JobAttribute.FinishingsCol, NoValue.Instance));
        message.JobAttributes.Add(new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance));

        Action act = () => ValidateWith(message, v =>
        {
            v.ValidateJobAttributesGroup = false;
            v.ValidateOperationSpecificRules = false;
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_WhenDocumentAttributesGroupDisabled_ShouldSkipDocumentValidation()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.DocumentAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, 3));
        message.DocumentAttributes.Add(new IppAttribute(Tag.BegCollection, JobAttribute.FinishingsCol, NoValue.Instance));
        message.DocumentAttributes.Add(new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance));

        Action act = () => ValidateWith(message, v =>
        {
            v.ValidateDocumentAttributesGroup = false;
            v.ValidateOperationSpecificRules = false;
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateCore_RequestIdIsInvalid_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.RequestId = 0;

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Bad request-id value")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_IppVersionIsInvalid_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.Version = new IppVersion(0, 9);

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("Unsupported IPP version")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_WhenOperationAttributesGroupDisabled_ShouldSkipOperationAttributeChecks()
    {
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };

        Action act = () => ValidateCoreOnly(message, v => v.ValidateOperationAttributesGroup = false);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateCore_OperationAttributesMissing_ShouldThrowException()
    {
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.CreateJob,
            RequestId = 123,
        };

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("No Operation Attributes")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_FirstOperationAttributeInvalid_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.OperationAttributes[0] = new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en");

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("attributes-charset MUST be the first attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_SecondOperationAttributeInvalid_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.OperationAttributes[1] = new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/");

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("attributes-natural-language MUST be the second attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_MissingPrinterAndSystemUri_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob, includePrinterUri: false, includeSystemUri: false, includeJobUri: false);

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("No printer-uri or system-uri operation attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_NonDocumentTargetOperationWithOnlyJobUri_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob, includePrinterUri: false, includeSystemUri: false, includeJobUri: true);

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("No printer-uri or system-uri operation attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_DocumentTargetOperationWithOnlyJobUri_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.GetDocuments, includePrinterUri: false, includeSystemUri: false, includeJobUri: true);

        Action act = () => ValidateCoreOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateCore_SystemServiceOperationWithoutSystemUri_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CancelResource, includePrinterUri: true, includeSystemUri: false, includeJobUri: false);

        Action act = () => ValidateCoreOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("No system-uri operation attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateCore_SystemServiceOperationWithSystemUri_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.CancelResource, includePrinterUri: false, includeSystemUri: true, includeJobUri: false);

        Action act = () => ValidateCoreOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_ConflictingFinishings_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.JobAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, 3));
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Integer, "some-value", 1)
        }.ToBegCollection(JobAttribute.FinishingsCol));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateDocumentAttributes_ConflictingFinishings_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CreateJob);
        message.DocumentAttributes.Add(new IppAttribute(Tag.Enum, JobAttribute.Finishings, 3));
        message.DocumentAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Integer, "some-value", 1)
        }.ToBegCollection(JobAttribute.FinishingsCol));

        Action act = () => ValidateDocumentOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("'finishings' and 'finishings-col' are conflicting attributes and cannot be supplied together.")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_CancelDocumentMissingDocumentNumber_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.CancelDocument);

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing document-number")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_GetDocumentAttributesInvalidDocumentNumber_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.GetDocumentAttributes);
        message.OperationAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, DocumentAttribute.DocumentNumber, "x"));

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid document-number")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_SetDocumentAttributesValidDocumentNumber_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.SetDocumentAttributes);
        message.OperationAttributes.Add(new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 1));

        Action act = () => ValidateOperationOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_PrintJobWithoutDocument_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.PrintJob);

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("document stream required")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_PrintJobWithDocument_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.PrintJob);
        message.Document = new MemoryStream([1, 2, 3]);

        Action act = () => ValidateOperationOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_PrintUriMissingDocumentUri_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.PrintUri);

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing document-uri")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_PrintUriWithDocumentUri_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.PrintUri);
        message.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, "https://example.test/doc.pdf"));

        Action act = () => ValidateOperationOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_SendDocumentMissingLastDocument_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.SendDocument);

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing last-document")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_SendDocumentInvalidLastDocument_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.SendDocument);
        message.OperationAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.LastDocument, "false"));

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid last-document")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_SendDocumentWithoutDocumentWhenLastDocumentFalse_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.SendDocument);
        message.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, false));

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("document stream required when last-document=false")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_SendDocumentWithLastDocumentTrueAndNoStream_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.SendDocument);
        message.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, true));

        Action act = () => ValidateOperationOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_SendUriMissingLastDocument_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.SendUri);

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing last-document")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_SendUriInvalidLastDocument_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.SendUri);
        message.OperationAttributes.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.LastDocument, "false"));

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid last-document")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_SendUriLastDocumentFalseMissingDocumentUri_ShouldThrowException()
    {
        var message = CreateMessage(IppOperation.SendUri);
        message.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, false));

        Action act = () => ValidateOperationOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("missing document-uri")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateOperationRules_SendUriLastDocumentTrueWithoutDocumentUri_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.SendUri);
        message.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, true));

        Action act = () => ValidateOperationOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_SendUriLastDocumentFalseWithDocumentUri_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.SendUri);
        message.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, false));
        message.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, "https://example.test/doc.pdf"));

        Action act = () => ValidateOperationOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateOperationRules_WhenOperationAttributesGroupDisabled_ShouldSkipOperationAttributeChecks()
    {
        var message = CreateMessage(IppOperation.CancelDocument);

        Action act = () => ValidateOperationOnly(message, v => v.ValidateOperationAttributesGroup = false);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesOutOfBandCollection_ShouldBeIgnored()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.Add(new IppAttribute(Tag.NoValue, JobAttribute.Overrides, NoValue.Instance));

        Action act = () => ValidateJobOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesMalformedCollectionEncoding_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(
        [
            new IppAttribute(Tag.BegCollection, JobAttribute.Overrides, NoValue.Instance),
            new IppAttribute(Tag.MemberAttrName, string.Empty, "pages"),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance)
        ]);

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides collection encoding")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesMissingPagesMember_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: missing required 'pages' member")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesPagesWrongType_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.Integer, "pages", 1),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'pages' must be 1setOf rangeOfInteger")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesDocumentNumbersWrongType_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Integer, "document-numbers", 1),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'document-numbers' must be 1setOf rangeOfInteger")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesPagesNotFirst_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'pages' must be the first member attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesDocumentNumbersNotSecond_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-copies", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'document-numbers' must be second when present")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesSelectorAfterOverrideAttribute_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided"),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1))
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: selector members must precede overriding Job Template attributes")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesPagesLowerBoundGreaterThanUpper_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(3, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'pages' range lower bound cannot exceed upper bound")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesPagesOverlapping_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 3)),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(3, 4)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'pages' ranges must be ascending and non-overlapping")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesPagesAscendingNonOverlapping_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(2, 3)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.Sides];
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesDocumentNumbersOverlappingInCollection_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 3)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(3, 4)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'document-numbers' ranges must be ascending and non-overlapping")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesDocumentCopiesOverlappingInCollection_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-copies", new SharpIpp.Protocol.Models.Range(1, 3)),
            new IppAttribute(Tag.RangeOfInteger, "document-copies", new SharpIpp.Protocol.Models.Range(3, 4)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'document-copies' ranges must be ascending and non-overlapping")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesWithoutOverrideMembers_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1))
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: each collection must contain at least one overriding Job Template attribute")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesAcrossCollectionsOverlappingDocumentNumbers_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(2, 2)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 2)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "two-sided-long-edge")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: override collections must have ascending, non-overlapping document-numbers")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesWithTagNotBegCollection_ShouldBeIgnored()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.Add(new IppAttribute(Tag.Integer, JobAttribute.Overrides, 123));

        Action act = () => ValidateJobOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesUnsupportedMembersWithoutSupportContext_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, "x-custom-member", "value")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesSupportedMembersCollectionEnumeratesEmpty_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, "x-custom-member", "value")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.Context.OverridesSupported = new NonEnumeratingOverrideSupportedCollection();
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesAllMembersSupported_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.Sides];
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesUnsupportedMembers_ShouldThrowCapabilityException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.Sides];
        });

        act.Should().Throw<IppRequestException>()
            .Where(x => x.StatusCode == IppStatusCode.ClientErrorAttributesOrValuesNotSupported)
            .WithMessage("invalid overrides: member(s) not supported by target printer: media")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesUnsupportedMembersWithFidelityDisabled_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, false));
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.UseIppAttributeFidelityForCapabilityValidation = true;
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.Sides];
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesUnsupportedMembersWithFidelityEnabled_ShouldThrowCapabilityException()
    {
        var message = CreateValidateJobMessage();
        message.OperationAttributes.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, true));
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.UseIppAttributeFidelityForCapabilityValidation = true;
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.Sides];
        });

        act.Should().Throw<IppRequestException>()
            .Where(x => x.StatusCode == IppStatusCode.ClientErrorAttributesOrValuesNotSupported)
            .WithMessage("invalid overrides: member(s) not supported by target printer: media")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesValidCollections_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(2, 2)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(2, 2)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "two-sided-long-edge")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.DocumentNumbers, OverrideSupported.Sides];
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void EnumerateTopLevelOverrideMemberNames_WithNestedCollections_ShouldHandleDepthTransitions()
    {
        var method = typeof(IppRequestValidator)
            .GetMethod("EnumerateTopLevelOverrideMemberNames", BindingFlags.NonPublic | BindingFlags.Static);
        method.Should().NotBeNull();

        var members = new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance),
            new IppAttribute(Tag.Integer, "x-dimension", 21000),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        };

        var result = (IEnumerable<string>)method!.Invoke(null, [members])!;

        result.Should().ContainInOrder("pages", "media-col", JobAttribute.Sides);
    }

    [TestMethod]
    public void ValidateSelectorOrder_WhenNoSelectors_ShouldThrowPagesFirstException()
    {
        var method = typeof(IppRequestValidator)
            .GetMethod("ValidateSelectorOrder", BindingFlags.NonPublic | BindingFlags.Static);
        method.Should().NotBeNull();

        var message = CreateValidateJobMessage();
        var members = new[]
        {
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        };

        Action act = () => method!.Invoke(null, [members, false, false, message]);

        act.Should().Throw<TargetInvocationException>()
            .Where(x =>
                x.InnerException != null &&
                x.InnerException.GetType() == typeof(IppRequestException) &&
                x.InnerException.Message == "invalid overrides: 'pages' must be the first member attribute");
    }

    [TestMethod]
    public void ValidateSelectorOrder_DocumentCopiesInvalidPositionBranch_ShouldThrowException()
    {
        var method = typeof(IppRequestValidator)
            .GetMethod("ValidateSelectorOrder", BindingFlags.NonPublic | BindingFlags.Static);
        method.Should().NotBeNull();

        var message = CreateValidateJobMessage();
        var members = new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-copies", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        };

        Action act = () => method!.Invoke(null, [members, false, true, message]);

        act.Should().Throw<TargetInvocationException>()
            .Where(x =>
                x.InnerException != null &&
                x.InnerException.GetType() == typeof(IppRequestException) &&
                x.InnerException.Message == "invalid overrides: 'document-copies' is in an invalid position");
    }

    [TestMethod]
    public void IppOperationExtensions_IsSystemServiceOperation_ShouldReturnExpectedValues()
    {
        var method = GetIppOperationExtensionMethod("IsSystemServiceOperation");

        IppOperation[] trueOperations =
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

        foreach (var operation in trueOperations)
            InvokeOperationExtension(method, operation).Should().BeTrue();

        InvokeOperationExtension(method, IppOperation.CreateJob).Should().BeFalse();
    }

    [TestMethod]
    public void IppOperationExtensions_IsPwg51005DocumentTargetOperation_ShouldReturnExpectedValues()
    {
        var method = GetIppOperationExtensionMethod("IsPwg51005DocumentTargetOperation");

        IppOperation[] trueOperations =
        [
            IppOperation.CancelDocument,
            IppOperation.GetDocumentAttributes,
            IppOperation.GetDocuments,
            IppOperation.SetDocumentAttributes,
        ];

        foreach (var operation in trueOperations)
            InvokeOperationExtension(method, operation).Should().BeTrue();

        InvokeOperationExtension(method, IppOperation.CreateJob).Should().BeFalse();
    }

    [TestMethod]
    public void ValidateOperationRules_PrintUriWithOperationAttributesValidationDisabled_ShouldBeSuccess()
    {
        var message = CreateMessage(IppOperation.PrintUri);

        Action act = () => ValidateOperationOnly(message, v => v.ValidateOperationAttributesGroup = false);

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesRangeTagWithWrongValueType_ShouldThrowException()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", 1),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message);

        act.Should().Throw<IppRequestException>()
            .WithMessage("invalid overrides: 'pages' must be 1setOf rangeOfInteger")
            .Which.RequestMessage.Should().Be(message);
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesDocumentCopiesWithDocumentNumbers_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-numbers", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.RangeOfInteger, "document-copies", new SharpIpp.Protocol.Models.Range(1, 2)),
            new IppAttribute(Tag.Keyword, JobAttribute.Sides, "one-sided")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.DocumentNumbers, OverrideSupported.DocumentCopies, OverrideSupported.Sides];
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void ValidateJobAttributes_OverridesUnsupportedMembersWithFidelityValidationAndMissingFidelityAttribute_ShouldBeSuccess()
    {
        var message = CreateValidateJobMessage();
        message.JobAttributes.AddRange(new[]
        {
            new IppAttribute(Tag.RangeOfInteger, "pages", new SharpIpp.Protocol.Models.Range(1, 1)),
            new IppAttribute(Tag.Keyword, JobAttribute.Media, "iso_a4_210x297mm")
        }.ToBegCollection(JobAttribute.Overrides));

        Action act = () => ValidateJobOnly(message, v =>
        {
            v.UseIppAttributeFidelityForCapabilityValidation = true;
            v.Context.OverridesSupported = [OverrideSupported.Pages, OverrideSupported.Sides];
        });

        act.Should().NotThrow();
    }

    [TestMethod]
    public void EnumerateNamedCollections_WithNestedCollections_ShouldReturnExpectedCollections()
    {
        var method = typeof(IppRequestValidator)
            .GetMethod("EnumerateNamedCollections", BindingFlags.NonPublic | BindingFlags.Static);
        method.Should().NotBeNull();

        var attributes = new[]
        {
            new IppAttribute(Tag.Integer, "not-overrides", 1),
            new IppAttribute(Tag.BegCollection, JobAttribute.Overrides, NoValue.Instance),
            new IppAttribute(Tag.BegCollection, "nested", NoValue.Instance),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
            new IppAttribute(Tag.EndCollection, string.Empty, NoValue.Instance),
        };

        var result = (IEnumerable<IReadOnlyList<IppAttribute>>)method!.Invoke(null, [attributes, JobAttribute.Overrides])!;

        result.Should().ContainSingle();
    }

    [TestMethod]
    public void ValidateRangesAscendingNonOverlapping_WithOverlappingRanges_ShouldThrowException()
    {
        var method = typeof(IppRequestValidator)
            .GetMethod("ValidateRangesAscendingNonOverlapping", BindingFlags.NonPublic | BindingFlags.Static);
        method.Should().NotBeNull();

        var message = CreateValidateJobMessage();
        var ranges = new[]
        {
            new SharpIpp.Protocol.Models.Range(1, 3),
            new SharpIpp.Protocol.Models.Range(3, 5),
        };

        Action act = () => method!.Invoke(null, [ranges, "pages", message]);

        act.Should().Throw<TargetInvocationException>()
            .Where(x =>
                x.InnerException != null &&
                x.InnerException.GetType() == typeof(IppRequestException) &&
                x.InnerException.Message == "invalid overrides: 'pages' ranges must be ascending and non-overlapping");
    }

    [TestMethod]
    public void Validator_DefaultFlags_ShouldExposeExpectedDefaults()
    {
        var validator = new IppRequestValidator();

        validator.ValidatePrinterAttributesGroup.Should().BeTrue();
        validator.ValidateUnsupportedAttributesGroup.Should().BeTrue();
        validator.ValidateSubscriptionAttributesGroup.Should().BeTrue();
        validator.ValidateEventNotificationAttributesGroup.Should().BeTrue();
        validator.ValidateResourceAttributesGroup.Should().BeTrue();
        validator.ValidateSystemAttributesGroup.Should().BeTrue();
        validator.UseIppAttributeFidelityForCapabilityValidation.Should().BeFalse();
    }

    private static IppRequestMessage CreateMessage(
        IppOperation operation = IppOperation.CreateJob,
        bool includePrinterUri = true,
        bool includeSystemUri = false,
        bool includeJobUri = false)
    {
        var message = new IppRequestMessage
        {
            IppOperation = operation,
            RequestId = 123,
        };

        message.OperationAttributes.AddRange(
        [
            new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")
        ]);

        if (includePrinterUri)
            message.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1:631/"));

        if (includeSystemUri)
            message.OperationAttributes.Add(new IppAttribute(Tag.Uri, SystemAttribute.SystemUri, "ipp://127.0.0.1:8631/system"));

        if (includeJobUri)
            message.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.JobUri, "ipp://127.0.0.1:631/jobs/123"));

        return message;
    }

    private static IppRequestMessage CreateValidateJobMessage()
    {
        return CreateMessage(IppOperation.ValidateJob);
    }

    private static void ValidateWith(IIppRequestMessage? request, Action<IppRequestValidator>? configure = null)
    {
        var validator = CreateValidator(configure);
        validator.Validate(request);
    }

    private static void ValidateCoreOnly(IIppRequestMessage? request, Action<IppRequestValidator>? configure = null)
    {
        var validator = CreateValidator(v =>
        {
            v.ValidateOperationSpecificRules = false;
            v.ValidateJobAttributesGroup = false;
            v.ValidateDocumentAttributesGroup = false;
            configure?.Invoke(v);
        });

        validator.Validate(request);
    }

    private static void ValidateOperationOnly(IIppRequestMessage? request, Action<IppRequestValidator>? configure = null)
    {
        var validator = CreateValidator(v =>
        {
            v.ValidateCoreRules = false;
            v.ValidateJobAttributesGroup = false;
            v.ValidateDocumentAttributesGroup = false;
            configure?.Invoke(v);
        });

        validator.Validate(request);
    }

    private static void ValidateJobOnly(IIppRequestMessage? request, Action<IppRequestValidator>? configure = null)
    {
        var validator = CreateValidator(v =>
        {
            v.ValidateCoreRules = false;
            v.ValidateOperationSpecificRules = false;
            v.ValidateDocumentAttributesGroup = false;
            configure?.Invoke(v);
        });

        validator.Validate(request);
    }

    private static void ValidateDocumentOnly(IIppRequestMessage? request, Action<IppRequestValidator>? configure = null)
    {
        var validator = CreateValidator(v =>
        {
            v.ValidateCoreRules = false;
            v.ValidateOperationSpecificRules = false;
            v.ValidateJobAttributesGroup = false;
            configure?.Invoke(v);
        });

        validator.Validate(request);
    }

    private static IppRequestValidator CreateValidator(Action<IppRequestValidator>? configure = null)
    {
        var validator = new IppRequestValidator
        {
            ValidatePrinterAttributesGroup = false,
            ValidateUnsupportedAttributesGroup = false,
            ValidateSubscriptionAttributesGroup = false,
            ValidateEventNotificationAttributesGroup = false,
            ValidateResourceAttributesGroup = false,
            ValidateSystemAttributesGroup = false,
        };

        configure?.Invoke(validator);
        return validator;
    }

    private static MethodInfo GetIppOperationExtensionMethod(string methodName)
    {
        var extensionsType = typeof(IppRequestValidator).Assembly.GetType("SharpIpp.Protocol.IppOperationExtensions", throwOnError: true);
        extensionsType.Should().NotBeNull();

        var method = extensionsType!.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        method.Should().NotBeNull();

        return method!;
    }

    private static bool InvokeOperationExtension(MethodInfo method, IppOperation operation)
    {
        return (bool)method.Invoke(null, [operation])!;
    }

    private sealed class NonEnumeratingOverrideSupportedCollection : IReadOnlyCollection<OverrideSupported>
    {
        public int Count => 1;

        public IEnumerator<OverrideSupported> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
