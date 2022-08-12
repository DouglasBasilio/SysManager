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

        private void SetupService()
        {
            _unityService = new UnityService(_unityRepository.Object, _productRepository.Object);
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
            SetupService();

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
            SetupService();

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

            SetupService();

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
            SetupService();

            //Act
            var result = await _unityService.PutAsync(request);

            //Assert
            Assert.False(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test GetById Success")]
        public async Task Unity_GetById_Success()
        {
            //Arrange
            var id = Guid.NewGuid();
            var response = new UnityEntity()
            {
                Id = id,
                Name = "test",
                Active = true
            };

            _unityRepository.MockGetByIdAsync(response);
            SetupService();

            //Act
            var result = await _unityService.GetByIdAsync(id);

            //Assert
            Assert.True(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test GetById Error")]
        public async Task Unity_GetById_Error()
        {
            //Arrange
            var id = Guid.NewGuid();
            var response = new
            {
                Errors = new List<string>() 
                {
                    SysManagerErrors.Unity_Get_BadRequest_Id_Is_Invalid_Or_Inexistent.Description()
                }
            };

            SetupService();

            //Act
            var result = await _unityService.GetByIdAsync(id);

            //Assert
            Assert.False(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test GetByFilter Success")]
        public async Task Unity_GetByFilter_Success()
        {
            //Arrange
            
            var request = new UnityGetFilterRequest()
            {
                Active = "todos",
                Name = "test",
                page = 1,
                pageSize = 10
            };

            var listResponse = new List<UnityEntity>()
                {
                    new UnityEntity() { Name = "teste" },
                    new UnityEntity() { Name = "teste 2" }
                };

            var response = new PaginationResponse<UnityEntity>()
            {
                Items = listResponse.ToArray(),
                _page = 1,
                _pageSize = 2,
                _total = 10
            };

            _unityRepository.MockGetByFilterAsync(response);
            SetupService();

            //Act
            var result = await _unityService.GetByFilterAsync(request);

            //Assert
            Assert.True(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test DeleteById Success")]
        public async Task Unity_DeleteById_Success()
        {
            //Arrange
            var id = Guid.NewGuid();

            var response = new UnityEntity()
            {
                Id = id,
                Name = "test",
                Active = false
            };

            var defaultResponse = new DefaultResponse(id.ToString(), "Unidade de medida excluída com sucesso", false);

            _unityRepository.MockGetByIdAsync(response);
            _productRepository.MockGetCountProductByDependenceAsync(0);
            _unityRepository.MockDeleteByIdAsync(defaultResponse);

            SetupService();

            //Act
            var result = await _unityService.DeleteByIdAsync(id);

            //Assert
            Assert.True(result.Success);

            var jsonResponse = Utils.ToJson(defaultResponse);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test DeleteById Dependence Error")]
        public async Task Unity_DeleteById_Dependence_Error()
        {
            //Arrange
            var id = Guid.NewGuid();
            var response = new
            {
                Errors = new List<string>()
                {
                    string.Format(SysManagerErrors.Unity_Delete_BadRequest_Id_Gave_Dependence.Description(), 1)
                }
            };

            var unityResponse = new UnityEntity()
            {
                Id = id,
                Name = "test",
                Active = false
            };

            _unityRepository.MockGetByIdAsync(unityResponse);
            _productRepository.MockGetCountProductByDependenceAsync(1);

            SetupService();

            //Act
            var result = await _unityService.DeleteByIdAsync(id);

            //Assert
            Assert.False(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }

        [Fact(DisplayName = "Test DeleteById Error")]
        public async Task Unity_DeleteById_Error()
        {
            //Arrange
            var id = Guid.NewGuid();
            var response = new
            {
                Errors = new List<string>()
                {
                    SysManagerErrors.Unity_Delete_BadRequest_Id_Is_Invalid_Or_Inexistent.Description()
                }
            };

            SetupService();

            //Act
            var result = await _unityService.DeleteByIdAsync(id);

            //Assert
            Assert.False(result.Success);

            var jsonResponse = Utils.ToJson(response);
            var jsonData = Utils.ToJson(result.Data);

            Assert.Equal(jsonResponse, jsonData);
        }
    }
}