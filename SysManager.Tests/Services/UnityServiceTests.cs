using Moq;
using SysManager.Application.Contracts.Unity.Request;
using SysManager.Application.Data.MySql;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Services;
using SysManager.Tests.Mocks;
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
            _unityRepository = new Mock<UnityRepository>(new MySqlContext());
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
            _unityService = new UnityService(_unityRepository.Object);

            //Act
            var result = await _unityService.PostAsync(request);

            //Assert
            Assert.False(result.Success);
        }
    }
}
