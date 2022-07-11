using FluentValidation;
using SysManager.Application.Contracts.Unity.Request;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;

namespace SysManager.Application.Validators.Unity
{
    /*
     Classe de validação que recebe um Generics, passando o nosso contrato

     */
    public class UnityPostRequestValidator : AbstractValidator<UnityPostRequest>
    {
        public UnityPostRequestValidator(UnityRepository repository)
        {
            RuleFor(contract => contract.Name)
                .Must(name => !string.IsNullOrEmpty(name))
                .WithMessage(SysManagerErrors.Unity_Post_BadRequest_Name_Cannot_Be_Null_Or_Empty.Description());

            RuleFor(contract => contract.Active)
                .Must(active => active == true || active == false)
                .WithMessage(SysManagerErrors.Unity_Post_BadRequest_Active_Cannot_Be_Diferent_True_Or_False.Description());

            RuleFor(contract => contract.Name)
                .Must(name =>
                {
                    var exists = repository.GetByNameAsync(name).Result;
                    return exists == null;

                 })
                .WithMessage(SysManagerErrors.Unity_Post_BadRequest_Name_Cannot_Be_Duplicated.Description());
        }
    }
}
