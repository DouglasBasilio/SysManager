using SysManager.Application.Contracts.Users.Request;
using SysManager.Application.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SysManager.Application.Services
{
    public class UserService
    {
        public UserService()
        {
        }

        public async Task<ResultData> PostAsync(UserPostRequest request)
        {
            var erros = new List<string>();

            if (request.UserName == "" || request.UserName == null)
                erros.Add("Precisa informar a propriedade (UserName)");
            if (request.Email == "" || request.Email == null)
                erros.Add("Precisa informar a propriedade (Email)");
            if (request.Password == "" || request.Password == null)
                erros.Add("Precisa informar a propriedade (Password)");

            if (erros.Count > 0)
                return Utils.ErrorData(erros);

            return Utils.SuccessData(request);
        }
    }
}
