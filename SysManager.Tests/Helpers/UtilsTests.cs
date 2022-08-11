using SysManager.Application.Contracts.Unity.Request;
using SysManager.Application.Helpers;
using System;
using Xunit;

namespace SysManager.Tests.Helpers
{
    public class UtilsTests
    {
     //[Fact]
     public void ToJsonSuccess()
        {
            //Arrange
            var request = new UnityPostRequest()
            {
                Active = true,
                Name = "test"
            };

            var expectedResponse = "{\"Name\":\"test\",\"Active\":true}";

            //Act
            var response = Utils.ToJson(request);

            //Assert
            Assert.Equal(expectedResponse, response);
        }
    }
}
