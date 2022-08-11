using Moq;
using SysManager.Application.Contracts;
using SysManager.Application.Contracts.Unity.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Helpers;
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

        public static void MockGetByIdAsync(this Mock<UnityRepository> mockRepository, UnityEntity response)
        {
            mockRepository.Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(response);
        }

        public static void MockCreateAsync(this Mock<UnityRepository> mockRepository, DefaultResponse response)
        {
            mockRepository.Setup(x => x.CreateAsync(It.IsAny<UnityEntity>())).ReturnsAsync(response);
        }

        public static void MockUpdateAsync(this Mock<UnityRepository> mockRepository, DefaultResponse response)
        {
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<UnityEntity>())).ReturnsAsync(response);
        }

        public static void MockGetByFilterAsync(this Mock<UnityRepository> mockRepository, PaginationResponse<UnityEntity> response)
        {
            mockRepository.Setup(x => x.GetByFilterAsync(It.IsAny<UnityGetFilterRequest>())).ReturnsAsync(response);
        }

        public static void MockDeleteByIdAsync(this Mock<UnityRepository> mockRepository, DefaultResponse response)
        {
            mockRepository.Setup(repository => repository.DeleteByIdAsync(It.IsAny<Guid>())).ReturnsAsync(response);
        }
    }
}
