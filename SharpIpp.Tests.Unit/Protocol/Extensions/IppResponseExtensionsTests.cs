using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;
[TestClass]
public class IppResponseMessageExtensionsTests
{
    [TestMethod]
    [DataRow((IppStatusCode)(-1), false)]
    [DataRow(IppStatusCode.SuccessfulOk, true)]
    [DataRow(IppStatusCode.SuccessfulOkIgnoredOrSubstitutedAttributes, true)]
    [DataRow(IppStatusCode.SuccessfulOkConflictingAttributes, true)]
    [DataRow(IppStatusCode.SuccessfulOkIgnoredSubscriptions, true)]
    [DataRow(IppStatusCode.SuccessfulOkTooManyEvents, true)]
    [DataRow(IppStatusCode.SuccessfulOkEventsComplete, true)]
    [DataRow((IppStatusCode)0x0008, false)]
    [DataRow(IppStatusCode.ClientErrorBadRequest, false)]
    [DataRow(IppStatusCode.ServerErrorInternalError, false)]
    public void IsSuccessfulStatusCode_ShouldIdentifySuccessCorrectly(IppStatusCode statusCode, bool expected)
    {
        var mock = new Mock<IIppResponseMessage>();
        mock.Setup(x => x.StatusCode).Returns(statusCode);

        mock.Object.IsSuccessfulStatusCode().Should().Be(expected);
    }
    [TestMethod]
    public void GetSectionList_ShouldReturnCorrectSection()
    {
        var mock = new Mock<IIppResponseMessage>();
        var operationAttributes = new List<List<IppAttribute>>();
        var jobAttributes = new List<List<IppAttribute>>();
        var printerAttributes = new List<List<IppAttribute>>();
        var unsupportedAttributes = new List<List<IppAttribute>>();
        var subscriptionAttributes = new List<List<IppAttribute>>();
        var eventNotificationAttributes = new List<List<IppAttribute>>();
        var resourceAttributes = new List<List<IppAttribute>>();
        var documentAttributes = new List<List<IppAttribute>>();
        var systemAttributes = new List<List<IppAttribute>>();

        mock.Setup(x => x.OperationAttributes).Returns(operationAttributes);
        mock.Setup(x => x.JobAttributes).Returns(jobAttributes);
        mock.Setup(x => x.PrinterAttributes).Returns(printerAttributes);
        mock.Setup(x => x.UnsupportedAttributes).Returns(unsupportedAttributes);
        mock.Setup(x => x.SubscriptionAttributes).Returns(subscriptionAttributes);
        mock.Setup(x => x.EventNotificationAttributes).Returns(eventNotificationAttributes);
        mock.Setup(x => x.ResourceAttributes).Returns(resourceAttributes);
        mock.Setup(x => x.DocumentAttributes).Returns(documentAttributes);
        mock.Setup(x => x.SystemAttributes).Returns(systemAttributes);

        mock.Object.GetSectionList(SectionTag.OperationAttributesTag).Should().BeSameAs(operationAttributes);
        mock.Object.GetSectionList(SectionTag.JobAttributesTag).Should().BeSameAs(jobAttributes);
        mock.Object.GetSectionList(SectionTag.PrinterAttributesTag).Should().BeSameAs(printerAttributes);
        mock.Object.GetSectionList(SectionTag.UnsupportedAttributesTag).Should().BeSameAs(unsupportedAttributes);
        mock.Object.GetSectionList(SectionTag.SubscriptionAttributesTag).Should().BeSameAs(subscriptionAttributes);
        mock.Object.GetSectionList(SectionTag.EventNotificationAttributesTag).Should().BeSameAs(eventNotificationAttributes);
        mock.Object.GetSectionList(SectionTag.ResourceAttributesTag).Should().BeSameAs(resourceAttributes);
        mock.Object.GetSectionList(SectionTag.DocumentAttributesTag).Should().BeSameAs(documentAttributes);
        mock.Object.GetSectionList(SectionTag.SystemAttributesTag).Should().BeSameAs(systemAttributes);
    }

    [TestMethod]
    public void GetSectionList_UnknownTag_ShouldThrowArgumentException()
    {
        var mock = new Mock<IIppResponseMessage>();
        Action action = () => mock.Object.GetSectionList(SectionTag.Reserved);
        action.Should().Throw<ArgumentException>().WithMessage($"Unknown section tag: {SectionTag.Reserved}");
    }

    [TestMethod]
    public void GetAllAttributes_WhenResponseIsNull_ShouldThrowArgumentNullException()
    {
        IIppResponseMessage message = null!;
        Action act = () => message.GetAllAttributes().ToList();
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void GetAllAttributes_WithGroupsAndAttributes_ShouldEnumerateAllAttributes()
    {
        var response = new IppResponseMessage();
        var attr1 = new IppAttribute(Tag.Charset, "attributes-charset", "utf-8");
        var attr2 = new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en-us");
        var attr3 = new IppAttribute(Tag.Integer, "job-id", 123);
        var attr4 = new IppAttribute(Tag.Uri, "printer-uri-supported", "ipp://localhost/printer");

        response.OperationAttributes.Add([attr1, attr2]);
        response.JobAttributes.Add([attr3]);
        response.PrinterAttributes.Add([attr4]);

        var all = response.GetAllAttributes().ToList();

        all.Should().HaveCount(4);
        all.Should().ContainInOrder(attr1, attr2, attr3, attr4);
    }

    [TestMethod]
    public void GetAllAttributes_WithNullGroupsOrInnerLists_ShouldIgnoreNulls()
    {
        var mock = new Mock<IIppResponseMessage>();
        var attr = new IppAttribute(Tag.Integer, "job-id", 456);

        mock.Setup(x => x.OperationAttributes).Returns((List<List<IppAttribute>>)null!);
        mock.Setup(x => x.JobAttributes).Returns([null!, [attr]]);
        mock.Setup(x => x.PrinterAttributes).Returns([]);

        var all = mock.Object.GetAllAttributes().ToList();

        all.Should().ContainSingle().Which.Should().Be(attr);
    }
}

[TestClass]
public class IppRequestMessageExtensionsTests_GetAllAttributes
{
    [TestMethod]
    public void GetAllAttributes_WhenRequestIsNull_ShouldThrowArgumentNullException()
    {
        IIppRequestMessage message = null!;
        Action act = () => message.GetAllAttributes().ToList();
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void GetAllAttributes_WithGroupsAndAttributes_ShouldEnumerateAllAttributes()
    {
        var request = new IppRequestMessage();
        var attr1 = new IppAttribute(Tag.Charset, "attributes-charset", "utf-8");
        var attr2 = new IppAttribute(Tag.NaturalLanguage, "attributes-natural-language", "en-us");
        var attr3 = new IppAttribute(Tag.Integer, "job-id", 123);

        request.OperationAttributes.AddRange([attr1, attr2]);
        request.JobAttributes.Add(attr3);

        var all = request.GetAllAttributes().ToList();

        all.Should().HaveCount(3);
        all.Should().ContainInOrder(attr1, attr2, attr3);
    }

    [TestMethod]
    public void GetAllAttributes_WithNullGroups_ShouldIgnoreNulls()
    {
        var mock = new Mock<IIppRequestMessage>();
        var attr = new IppAttribute(Tag.Integer, "job-id", 456);

        mock.Setup(x => x.OperationAttributes).Returns((List<IppAttribute>)null!);
        mock.Setup(x => x.JobAttributes).Returns([attr]);

        var all = mock.Object.GetAllAttributes().ToList();

        all.Should().ContainSingle().Which.Should().Be(attr);
    }
}

[TestClass]
public class IppResponseExtensionsTests
{
    [TestMethod]
    public void GetOperation_WhenResponseIsNull_ShouldReturnNull()
    {
        IIppResponse? response = null;
        response.GetOperation().Should().BeNull();
    }

    [TestMethod]
    public void GetOperation_WhenResponseIsUnrecognized_ShouldReturnNull()
    {
        var response = new Mock<IIppResponse>().Object;
        response.GetOperation().Should().BeNull();
    }

    [TestMethod]
    public void GetOperation_WhenResponseIsRecognized_ShouldReturnOperation()
    {
        var response = new PrintJobResponse();
        response.GetOperation().Should().Be(IppOperation.PrintJob);
    }
}
