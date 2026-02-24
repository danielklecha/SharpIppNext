using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;
[TestClass]
[ExcludeFromCodeCoverage]
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
}
