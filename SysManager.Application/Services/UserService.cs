using SysManager.Application.Contracts.Users.Request;
using SysManager.Application.Data.MySql.Entities;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Helpers;
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
            var errors = new List<string>();

            if (request.UserName == "" || request.UserName == null)
                errors.Add("Precisa informar a propriedade (UserName)");
            if (request.Email == "" || request.Email == null)
                errors.Add("Precisa informar a propriedade (Email)");
            if (request.Password == "" || request.Password == null)
                errors.Add("Precisa informar a propriedade (Password)");

            var userExists = await _userRepository.GetUserByEmail(request.Email);

            if (userExists != null)
                errors.Add($"Já existe um usuário cadastrado com esse e-mail ({request.Email})");

            if (errors.Count > 0)
            {
                Console.WriteLine("Erros encontrados" + DateTime.Now + "\r\n");
                return Utils.ErrorData(errors);
            }

            var entity = new UserEntity(request);

            var response = await _userRepository.CreateUser(entity);
            Console.WriteLine("Sucesso" + DateTime.Now + "\r\n");

            return Utils.SuccessData(response);

            //return Utils.SuccessData(await _userRepository.CreateUser());
        }
    }
}
