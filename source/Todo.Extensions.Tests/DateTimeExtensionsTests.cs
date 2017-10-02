using System;
using NUnit.Framework;

namespace Todo.Extensions.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ConvertTo24HourFormatWithSeconds_WhenDateTimePmValue_ShouldReturnStringWithSeconds()
        {
            //---------------Arrange-------------------
            var dateTime = new DateTime(2017,2,1,22,00,01);
            //---------------Act----------------------
            var result = dateTime.ConvertTo24HourFormatWithSeconds();
            //---------------Assert-----------------------
            Assert.AreEqual("2017-02-01 22:00:01",result);
        }
    }
}
