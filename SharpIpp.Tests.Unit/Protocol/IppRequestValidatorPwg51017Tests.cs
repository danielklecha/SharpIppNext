using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;

namespace SharpIpp.Tests.Unit.Protocol;

[TestClass]
public class IppRequestValidatorPwg51017Tests
{
    private static IppRequestMessage CreateMinimalCreateJobRequest(List<IppAttribute> operationAttributes)
    {
        var request = new IppRequestMessage
        {
            RequestId = 1,
            Version = new IppVersion(2, 0),
            IppOperation = IppOperation.CreateJob
        };

        request.OperationAttributes.AddRange(operationAttributes);
        return request;
    }

    private static IppRequestMessage CreateMinimalSetPrinterAttributesRequest(List<IppAttribute> printerAttributes)
    {
        var request = new IppRequestMessage
        {
            RequestId = 2,
            Version = new IppVersion(2, 0),
            IppOperation = IppOperation.SetPrinterAttributes
        };

        request.OperationAttributes.Add(new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"));
        request.OperationAttributes.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"));
        request.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"));
        request.PrinterAttributes.AddRange(printerAttributes);
        return request;
    }

    [TestMethod]
    public void Validate_CreateJob_WithDestinationAccessesCardinalityMismatch_Throws()
    {
        var attributes = new List<IppAttribute>
        {
            new(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/a"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/b"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),

            new(Tag.BegCollection, JobAttribute.DestinationAccesses, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "access-user-name"),
            new(Tag.NameWithoutLanguage, string.Empty, "scan-user"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalCreateJobRequest(attributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-accesses cardinality MUST match destination-uris*");
    }

    [TestMethod]
    public void Validate_CreateJob_WithForbiddenDestinationUriScheme_Throws()
    {
        var attributes = new List<IppAttribute>
        {
            new(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "tel:+12025550123"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalCreateJobRequest(attributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*invalid destination-uri scheme for Scan*");
    }

    [TestMethod]
    public void Validate_CreateJob_WithForbiddenDestinationUriMembers_Throws()
    {
        var attributes = new List<IppAttribute>
        {
            new(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"),
            new(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"),
            new(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer"),

            new(Tag.BegCollection, JobAttribute.DestinationUris, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-uri"),
            new(Tag.Uri, string.Empty, "https://example.test/upload"),
            new(Tag.MemberAttrName, string.Empty, "post-dial-string"),
            new(Tag.TextWithoutLanguage, string.Empty, "123"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalCreateJobRequest(attributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*reserved fax member attributes are not allowed for Scan*");
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithDestinationOAuthScopeWithoutToken_Throws()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-scope"),
            new(Tag.OctetStringWithAnUnspecifiedFormat, string.Empty, "scope1 scope2"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-oauth-scope requires destination-oauth-token*");
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithDestinationOAuthUriWithoutToken_Throws()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-uri"),
            new(Tag.Uri, string.Empty, "https://issuer.example/token"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-oauth-uri requires destination-oauth-token*");
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithForbiddenDestinationAttributesSupportedMember_Throws()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-attributes-supported"),
            new(Tag.Keyword, string.Empty, JobAttribute.JobPassword),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-attributes-supported MUST NOT include password attributes*");
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithForbiddenDestinationAttributesMember_Throws()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-attributes"),
            new(Tag.BegCollection, string.Empty, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, JobAttribute.DocumentPassword),
            new(Tag.OctetStringWithAnUnspecifiedFormat, string.Empty, "secret"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().Throw<IppRequestException>()
            .WithMessage("*destination-attributes MUST NOT include password attributes*");
    }

    [TestMethod]
    public void Validate_SetPrinterAttributes_WithDestinationOAuthTokenDependenciesSatisfied_DoesNotThrow()
    {
        var printerAttributes = new List<IppAttribute>
        {
            new(Tag.BegCollection, PrinterAttribute.DestinationUriReady, NoValue.Instance),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-token"),
            new(Tag.OctetStringWithAnUnspecifiedFormat, string.Empty, "token-part-1"),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-scope"),
            new(Tag.OctetStringWithAnUnspecifiedFormat, string.Empty, "scope1 scope2"),
            new(Tag.MemberAttrName, string.Empty, "destination-oauth-uri"),
            new(Tag.Uri, string.Empty, "https://issuer.example/token"),
            new(Tag.EndCollection, string.Empty, NoValue.Instance)
        };

        var request = CreateMinimalSetPrinterAttributesRequest(printerAttributes);
        var validator = IppRequestValidator.Default;

        Action act = () => validator.Validate(request);

        act.Should().NotThrow();
    }
}
