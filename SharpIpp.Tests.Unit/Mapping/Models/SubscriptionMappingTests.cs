using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SubscriptionMappingTests : MapperTestBase
    {
        [TestMethod]
        public void SubscriptionDescriptionAttributes_EmptyList_ReturnsEmptyArray()
        {
            var src = new List<List<IppAttribute>>();
            var result = _mapper.Map<List<List<IppAttribute>>, SubscriptionDescriptionAttributes[]>(src);
            result.Should().BeEmpty();
        }

        [TestMethod]
        public void SubscriptionDescriptionAttributes_RoundTrip()
        {
            var original = new SubscriptionDescriptionAttributes
            {
                NotifySubscriptionId = 1234,
                NotifyPullMethod = NotifyPullMethod.IppGet,
                NotifyEvents = new[] { NotifyEvent.JobCreated, NotifyEvent.JobCompleted },
                NotifyLeaseDuration = 3600,
                NotifyRecipientUri = new Uri("ipp://localhost/test"),
                NotifyUserData = new OctetString(new byte[] { 0xDE, 0xAD, 0xBE, 0xEF }),
                NotifySubscriberUserName = "Daniel",
                NotifyCharset = "utf-8",
                NotifyNaturalLanguage = "en-us"
            };

            var dict = _mapper.Map<SubscriptionDescriptionAttributes, IDictionary<string, IppAttribute[]>>(original);
            var roundTripped = _mapper.Map<IDictionary<string, IppAttribute[]>, SubscriptionDescriptionAttributes>(dict);

            roundTripped.NotifySubscriptionId.Should().Be(original.NotifySubscriptionId);
            roundTripped.NotifyPullMethod.Should().Be(original.NotifyPullMethod);
            roundTripped.NotifyEvents.Should().BeEquivalentTo(original.NotifyEvents);
            roundTripped.NotifyLeaseDuration.Should().Be(original.NotifyLeaseDuration);
            roundTripped.NotifyRecipientUri.Should().Be(original.NotifyRecipientUri);
            roundTripped.NotifyUserData.Should().Be(original.NotifyUserData);
            roundTripped.NotifySubscriberUserName.Should().Be(original.NotifySubscriberUserName);
            roundTripped.NotifyCharset.Should().Be(original.NotifyCharset);
            roundTripped.NotifyNaturalLanguage.Should().Be(original.NotifyNaturalLanguage);
        }

        [TestMethod]
        public void CreatePrinterSubscriptionsResponse_RoundTrip()
        {
            var original = new CreatePrinterSubscriptionsResponse
            {
                Version = new IppVersion(2, 0),
                StatusCode = IppStatusCode.SuccessfulOk,
                RequestId = 42,
                SubscriptionsAttributes = new[]
                {
                    new SubscriptionDescriptionAttributes
                    {
                        NotifySubscriptionId = 1,
                        NotifyEvents = new[] { NotifyEvent.PrinterStateChanged }
                    },
                    new SubscriptionDescriptionAttributes
                    {
                        NotifySubscriptionId = 2,
                        NotifyEvents = new[] { NotifyEvent.JobStateChanged }
                    }
                }
            };

            var raw = _mapper.Map<IppResponseMessage>(original);
            var roundTripped = _mapper.Map<CreatePrinterSubscriptionsResponse>(raw);

            roundTripped.Version.Should().Be(original.Version);
            roundTripped.StatusCode.Should().Be(original.StatusCode);
            roundTripped.RequestId.Should().Be(original.RequestId);
            roundTripped.SubscriptionsAttributes.Should().BeEquivalentTo(original.SubscriptionsAttributes);
        }

        [TestMethod]
        public void CreateJobSubscriptionsResponse_RoundTrip()
        {
            var original = new CreateJobSubscriptionsResponse
            {
                Version = new IppVersion(2, 0),
                StatusCode = IppStatusCode.SuccessfulOk,
                RequestId = 43,
                SubscriptionsAttributes = new[]
                {
                    new SubscriptionDescriptionAttributes
                    {
                        NotifySubscriptionId = 10,
                        NotifyEvents = new[] { NotifyEvent.JobCompleted }
                    }
                }
            };

            var raw = _mapper.Map<IppResponseMessage>(original);
            var roundTripped = _mapper.Map<CreateJobSubscriptionsResponse>(raw);

            roundTripped.SubscriptionsAttributes.Should().BeEquivalentTo(original.SubscriptionsAttributes);
        }

        [TestMethod]
        public void GetSubscriptionAttributesResponse_RoundTrip()
        {
            var original = new GetSubscriptionAttributesResponse
            {
                Version = new IppVersion(2, 0),
                StatusCode = IppStatusCode.SuccessfulOk,
                RequestId = 44,
                SubscriptionAttributes = new SubscriptionDescriptionAttributes
                {
                    NotifySubscriptionId = 99,
                    NotifyPullMethod = NotifyPullMethod.IppGet,
                    NotifyEvents = new[] { NotifyEvent.SystemStateChanged }
                }
            };

            var raw = _mapper.Map<IppResponseMessage>(original);
            var roundTripped = _mapper.Map<GetSubscriptionAttributesResponse>(raw);

            roundTripped.SubscriptionAttributes.Should().BeEquivalentTo(original.SubscriptionAttributes);
        }

        [TestMethod]
        public void SystemServiceResponses_RoundTrip()
        {
            var createSystem = new CreateSystemSubscriptionsResponse
            {
                SubscriptionsAttributes = new[] { new SubscriptionDescriptionAttributes { NotifySubscriptionId = 111 } }
            };
            var createResource = new CreateResourceSubscriptionsResponse
            {
                SubscriptionsAttributes = new[] { new SubscriptionDescriptionAttributes { NotifySubscriptionId = 222 } }
            };
            var getSubscriptions = new GetSubscriptionsResponse
            {
                SubscriptionsAttributes = new[] { new SubscriptionDescriptionAttributes { NotifySubscriptionId = 333 } }
            };

            var raw1 = _mapper.Map<IppResponseMessage>(createSystem);
            var roundTripped1 = _mapper.Map<CreateSystemSubscriptionsResponse>(raw1);
            roundTripped1.SubscriptionsAttributes.Should().BeEquivalentTo(createSystem.SubscriptionsAttributes);

            var raw2 = _mapper.Map<IppResponseMessage>(createResource);
            var roundTripped2 = _mapper.Map<CreateResourceSubscriptionsResponse>(raw2);
            roundTripped2.SubscriptionsAttributes.Should().BeEquivalentTo(createResource.SubscriptionsAttributes);

            var raw3 = _mapper.Map<IppResponseMessage>(getSubscriptions);
            var roundTripped3 = _mapper.Map<GetSubscriptionsResponse>(raw3);
            roundTripped3.SubscriptionsAttributes.Should().BeEquivalentTo(getSubscriptions.SubscriptionsAttributes);
        }
    }
}
