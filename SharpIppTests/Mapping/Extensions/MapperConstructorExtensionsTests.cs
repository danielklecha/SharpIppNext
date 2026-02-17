using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Mapping.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class MapperConstructorExtensionsTests
{
    [TestMethod]
    public void CreateIppMap_Should_Register_Identity_Mapping()
    {
        // Arrange
        var mapperMock = new Mock<IMapperConstructor>();
        Func<int, IMapperApplier, int>? registeredMapFunc = null;

        mapperMock.Setup(x => x.CreateMap(It.IsAny<Func<int, IMapperApplier, int>>()))
            .Callback<Func<int, IMapperApplier, int>>(func => registeredMapFunc = func);

        // Act
        mapperMock.Object.CreateIppMap<int>();

        // Assert
        Assert.IsNotNull(registeredMapFunc);
        var result = registeredMapFunc!(42, Mock.Of<IMapperApplier>());
        Assert.AreEqual(42, result);
    }
}
