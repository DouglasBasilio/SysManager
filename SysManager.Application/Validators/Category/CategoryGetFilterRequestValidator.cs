using FluentValidation;
using SysManager.Application.Contracts.Category.Request;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SysManager.Application.Validators.Category
{
    public class CategoryGetFilterRequestValidator : AbstractValidator<CategoryGetFilterRequest>
    {
        public CategoryGetFilterRequestValidator()
        {
            RuleFor(contract => contract.Active)
               .Must(active => active == "todos" || active == "ativos" || active == "inativos")
               .WithMessage(SysManagerErrors.Category_Get_BadRequest_Active_Cannot_Be_Empty.Description());

            RuleFor(contract => contract.page)
                .Must(active => active > 0)
                .WithMessage(SysManagerErrors.Category_Get_BadRequest_Page_More_Than_Zero.Description());

            RuleFor(contract => contract.pageSize)
              .Must(active => active > 0)
              .WithMessage(SysManagerErrors.Category_Get_BadRequest_pageSize_More_Than_Zero.Description());

        }
    }
}
