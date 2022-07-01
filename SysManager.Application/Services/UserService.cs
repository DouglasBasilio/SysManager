using SysManager.Application.Contracts.Users.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Helpers;
using SysManager.Application.Validators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysManager.Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        //injeção de dependência
        public UserService(UserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<ResultData> PostAsync(UserPostRequest request)
        {
            var validator = new UserPostRequestValidator(_userRepository);
            var validatorResult = validator.Validate(request);

            if (!validatorResult.IsValid)
                return Utils.ErrorData(validatorResult.Errors.ToErrorList());

            var entity = new UserEntity(request);

            var response = await _userRepository.CreateUser(entity);
            Console.WriteLine("Sucesso" + DateTime.Now + "\r\n");

            return Utils.SuccessData(response);

            //return Utils.SuccessData(await _userRepository.CreateUser());
        }
    }
}