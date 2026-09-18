using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class ValidateJobResponseProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_ValidateJobResponse_RoundTrip_WithPreferredAttributes_ShouldSucceed()
    {
        // Arrange
        var response = new ValidateJobResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = new ValidateOperationAttributes
            {
                StatusMessage = "Validated successfully",
                PreferredAttributes = new JobTemplateAttributes
                {
                    Copies = 5,
                    Sides = Sides.TwoSidedLongEdge
                }
            }
        };

        // Act - Map to IppResponseMessage
        var message = _mapper.Map<ValidateJobResponse, IppResponseMessage>(response);

        // Assert - Map to IppResponseMessage
        message.Version.Should().Be(new IppVersion(1, 1));
        message.RequestId.Should().Be(42);
        message.StatusCode.Should().Be(IppStatusCode.SuccessfulOk);

        // Act - Map back to ValidateJobResponse
        var roundTrip = _mapper.Map<IppResponseMessage, ValidateJobResponse>(message);

        // Assert - Round-trip
        roundTrip.Version.Should().Be(new IppVersion(1, 1));
        roundTrip.RequestId.Should().Be(42);
        roundTrip.StatusCode.Should().Be(IppStatusCode.SuccessfulOk);
        roundTrip.OperationAttributes.Should().NotBeNull();
        roundTrip.OperationAttributes.StatusMessage.Should().Be("Validated successfully");
        roundTrip.OperationAttributes.PreferredAttributes.Should().NotBeNull();
        roundTrip.OperationAttributes.PreferredAttributes!.Copies.Should().Be(5);
        roundTrip.OperationAttributes.PreferredAttributes!.Sides.Should().Be(Sides.TwoSidedLongEdge);
    }

    [TestMethod]
    public void Map_ValidateJobResponse_RoundTrip_WithNullOperationAttributes_ShouldSucceed()
    {
        // Arrange
        var response = new ValidateJobResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = null
        };

        // Act
        var message = _mapper.Map<ValidateJobResponse, IppResponseMessage>(response);
        var roundTrip = _mapper.Map<IppResponseMessage, ValidateJobResponse>(message);

        // Assert
        roundTrip.Version.Should().Be(new IppVersion(1, 1));
        roundTrip.RequestId.Should().Be(42);
        roundTrip.StatusCode.Should().Be(IppStatusCode.SuccessfulOk);
        roundTrip.OperationAttributes.Should().NotBeNull();
        roundTrip.OperationAttributes.PreferredAttributes.Should().BeNull();
    }

    [TestMethod]
    public void Map_ValidateJobResponse_RoundTrip_WithNullPreferredAttributes_ShouldSucceed()
    {
        // Arrange
        var response = new ValidateJobResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = new ValidateOperationAttributes
            {
                StatusMessage = "Validated successfully",
                PreferredAttributes = null
            }
        };

        // Act
        var message = _mapper.Map<ValidateJobResponse, IppResponseMessage>(response);
        var roundTrip = _mapper.Map<IppResponseMessage, ValidateJobResponse>(message);

        // Assert
        roundTrip.Version.Should().Be(new IppVersion(1, 1));
        roundTrip.RequestId.Should().Be(42);
        roundTrip.StatusCode.Should().Be(IppStatusCode.SuccessfulOk);
        roundTrip.OperationAttributes.Should().NotBeNull();
        roundTrip.OperationAttributes.StatusMessage.Should().Be("Validated successfully");
        roundTrip.OperationAttributes.PreferredAttributes.Should().BeNull();
    }
}
