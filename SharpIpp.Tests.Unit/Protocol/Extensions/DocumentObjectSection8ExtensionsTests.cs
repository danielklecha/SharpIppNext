using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

[TestClass]
public class IppJobResponseExtensionsTests
{
    [TestMethod]
    public void Validate_SendDocumentWithoutDocumentAttributes_ShouldThrow()
    {
        var response = new SendDocumentResponse();

        Action act = () => response.Validate();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*must include document attributes*");
    }

    [TestMethod]
    public void Validate_SendUriWithoutRequiredDocumentStateReasons_ShouldThrow()
    {
        var response = new SendUriResponse
        {
            DocumentAttributes = new DocumentAttributes
            {
                DocumentNumber = 1,
                DocumentState = DocumentState.Pending,
                DocumentStateReasons = []
            }
        };

        Action act = () => response.Validate();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*document-state-reasons*");
    }

    [TestMethod]
    public void Validate_PrintJobWithoutDocumentAttributes_ShouldSucceed()
    {
        var response = new PrintJobResponse();

        Action act = () => response.Validate();

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_PrintUriWithInvalidDocumentNumber_ShouldThrow()
    {
        var response = new PrintUriResponse
        {
            DocumentAttributes = new DocumentAttributes
            {
                DocumentNumber = 0
            }
        };

        Action act = () => response.Validate();

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*invalid document-number*");
    }

    [TestMethod]
    public void Validate_SendDocumentWithRequiredAttributes_ShouldSucceed()
    {
        var response = new SendDocumentResponse
        {
            DocumentAttributes = new DocumentAttributes
            {
                DocumentNumber = 1,
                DocumentState = DocumentState.Pending,
                DocumentStateReasons = [DocumentStateReason.None]
            }
        };

        Action act = () => response.Validate();

        act.Should().NotThrow();
    }

    [TestMethod]
    public void Validate_CreateJobResponse_ShouldSucceed()
    {
        var response = new CreateJobResponse();

        Action act = () => response.Validate();

        act.Should().NotThrow();
    }
}