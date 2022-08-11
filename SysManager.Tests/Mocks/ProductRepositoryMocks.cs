using Moq;
using SysManager.Application.Data.MySql.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Tests.Mocks
{
    public static class ProductRepositoryMocks
    {
        public static void MockGetCountProductByDependenceAsync(this Mock<ProductRepository> mockRepository, int response)
        {
            mockRepository.Setup(x => x.GetCountProductByDependenceAsync(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync(response);
        }
    }
}
