using FluentValidation;
using SysManager.Application.Contracts.Users.Request;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Validators
{
    /*
     Classe de validação que recebe um Generics, passando o nosso contrato

     */
    public class UserPutRequestValidator : AbstractValidator<UserPutRequest>
    {
        public UserPutRequestValidator(UserRepository repository)
        {
            RuleFor(contract => contract.UserName)
                .Must(_name => !string.IsNullOrEmpty(_name))
                .WithMessage(SysManagerErrors.User_Post_BadRequest_UserName_Cannot_Be_Null_Or_Empty.Description());

            RuleFor(contract => contract.UserName)
                .Must(_name => !string.IsNullOrEmpty(_name))
                .WithMessage(SysManagerErrors.User_Post_BadRequest_UserName_Cannot_Be_Null_Or_Empty.Description());


        }
    }
}
