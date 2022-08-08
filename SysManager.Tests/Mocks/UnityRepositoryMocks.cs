using Moq;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Tests.Mocks
{
    public static class UnityRepositoryMocks
    {
        public static void MockGetByNameAsync(this Mock<UnityRepository> mockRepository, UnityEntity response)
        {
            mockRepository.Setup(repository => repository.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(response);
        }
    }
}
