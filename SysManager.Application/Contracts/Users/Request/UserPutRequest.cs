using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Contracts.Users.Request
{
    /// <summary>
    /// Contrato para atualizar a senha
    /// </summary>
    public class UserPutRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}