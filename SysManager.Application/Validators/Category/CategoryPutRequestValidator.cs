using FluentValidation;
using SysManager.Application.Contracts.Category.Request;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Validators.Category
{
    public class CategoryPutRequestValidator : AbstractValidator<CategoryPutRequest>
    {
        public CategoryPutRequestValidator(CategoryRepository repository)
        {
            RuleFor(contract => contract.Id)
                .Must(id => !string.IsNullOrEmpty(id.ToString()))
                .WithMessage(SysManagerErrors.Category_Put_BadRequest_Id_Cannot_Be_Null_Or_Empty.Description());

            RuleFor(contract => contract.Name)
                .Must(name => !string.IsNullOrEmpty(name))
                .WithMessage(SysManagerErrors.Category_Put_BadRequest_Name_Cannot_Be_Null_Or_Empty.Description());

            RuleFor(contract => contract.Active)
                .Must(active => active == true || active == false)
                .WithMessage(SysManagerErrors.Category_Put_BadRequest_Name_Cannot_Be_Null_Or_Empty.Description());

            RuleFor(contract => contract)
               .Must(contract =>
               {
                   var exists = repository.GetByNameAsync(contract.Name).Result;

                   if (exists != null)
                       if (exists.Id != contract.Id)
                           return false;
                   return true;
               })
               .WithMessage(SysManagerErrors.Category_Put_BadRequest_Name_Cannot_Be_Duplicated.Description());

            RuleFor(contract => contract.Id)
                .Must(id =>
                {
                    var exists = repository.GetByIdAsync(id).Result;
                    return exists != null;
                })
                .WithMessage(SysManagerErrors.Category_Put_BadRequest_Id_Is_Invalid_Or_Inexistent.Description());
        }
    }
}
