using Moq;
using SysManager.Application.Contracts;
using SysManager.Application.Contracts.Unity.Request;
using SysManager.Application.Data.MySql;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;
using SysManager.Application.Services;
using SysManager.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace SysManager.Tests.Services
{
    public class UnityServiceTests
    {
        private UnityService _unityService;
        private Mock<UnityRepository> _unityRepository;
        private Mock<ProductRepository> _productRepository;

        public UnityServiceTests()
        {
            _unityRepository = new Mock<UnityRepository>(new MySqlContext()) { CallBase = false };
            _productRepository = new Mock<ProductRepository>(new MySqlContext());
        }

        [Fact]
        public async Task Unity_Post_Error()
        {
            //Arrange
            var request = new UnityPostRequest()
            {
                Active = true,
                Name = "test"
            };

            var entity = new UnityEntity(request);
            _unityRepository.MockGetByNameAsync(entity);
            _unityService = new UnityService(_unityRepository.Object, _productRepository.Object);

            //Act
            var result = await _unityService.PostAsync(request);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        //[Display("1")]
        public async Task Unity_Post_Success()
        {
            //Arrange
            var request = new UnityPostRequest()
            {
                Active = true,
                Name = "test"
            };
            
            var response = new DefaultResponse(Guid.NewGuid().ToString(), "Unidade de medida criada com sucesso", false);

            _unityRepository.MockCreateAsync(response);
            _unityService = new UnityService(_unityRepository.Object, _productRepository.Object);

            //Act
            var result = await _unityService.PostAsync(request);

            //Assert
            Assert.True(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test Put Success")]
        public async Task Unity_Put_Success()
        {
            //Arrange
            var id = Guid.NewGuid();

            var request = new UnityPutRequest()
            {
                Id = id,
                Active = true,
                Name = "test"
            };

            var entity = new UnityEntity(request);
            var defaultResponse = new DefaultResponse(id.ToString(), "Unidade de medida alterada com sucesso", false);

            _unityRepository.MockGetByIdAsync(entity);
            _unityRepository.MockGetByNameAsync(entity);
            _unityRepository.MockUpdateAsync(defaultResponse);

            _unityService = new UnityService(_unityRepository.Object, _productRepository.Object);

            //Act
            var result = await _unityService.PutAsync(request);

            //Assert
            Assert.True(result.Success);

            var jsonResponse = Utils.ToJson(defaultResponse);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test Put Error")]
        public async Task Unity_Put_Error()
        {
            //Arrange
            var id = Guid.NewGuid();

            var request = new UnityPutRequest()
            {
                Id = id,
                Active = true,
                Name = "test"
            };

            var response = new
            {
                Errors = new List<string>()
                {
                    SysManagerErrors.Unity_Put_BadRequest_Name_Cannot_Be_Duplicated.Description(),
                    SysManagerErrors.Unity_Put_BadRequest_Id_Is_Invalid_Or_Inexistent.Description()
                }
            };

            var unityResponse = new UnityEntity() { Id = Guid.Empty };

            _unityRepository.MockGetByNameAsync(unityResponse);
            _unityService = new UnityService(_unityRepository.Object, _productRepository.Object);

            //Act
            var result = await _unityService.PutAsync(request);

            //Assert
            Assert.False(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

    }
}
