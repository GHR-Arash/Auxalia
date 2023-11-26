using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuxaliaRevitToolkit.Utilities;
using System.Globalization;

namespace AuxaliaRevitToolkit.Tests
{
    [TestClass]
    public class NullToBoolConverterTests
    {
        [TestMethod]
        public void Convert_ShouldReturnTrue_WhenValueIsNotNull()
        {
            // Arrange
            var converter = new NullToBoolConverter();
            var notNullValue = new object();

            // Act
            var result = converter.Convert(notNullValue, null, null, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsTrue((bool)result);
        }

        [TestMethod]
        public void Convert_ShouldReturnFalse_WhenValueIsNull()
        {
            // Arrange
            var converter = new NullToBoolConverter();

            // Act
            var result = converter.Convert(null, null, null, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse((bool)result);
        }
    }
}
