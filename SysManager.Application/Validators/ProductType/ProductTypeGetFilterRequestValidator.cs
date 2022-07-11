using FluentValidation;
using SysManager.Application.Contracts.ProductsType.Request;
using SysManager.Application.Data.MySql.Repositories;
using SysManager.Application.Errors;
using SysManager.Application.Helpers;

namespace SysManager.Application.Validators.ProductType
{
    public class ProductTypeGetFilterRequestValidator : AbstractValidator<ProductTypeGetFilterRequest>
    {
        public ProductTypeGetFilterRequestValidator(ProductTypeRepository repository)
        {
            RuleFor(contract => contract.Active)
               .Must(active => active == "todos" || active == "ativos" || active == "inativos")
               .WithMessage(SysManagerErrors.ProductType_Get_BadRequest_Active_Cannot_Be_Empty.Description());

            RuleFor(contract => contract.page)
                .Must(active => active > 0)
                .WithMessage(SysManagerErrors.ProductType_Get_BadRequest_Page_More_Than_Zero.Description());

            RuleFor(contract => contract.pageSize)
              .Must(active => active > 0)
              .WithMessage(SysManagerErrors.ProductType_Get_BadRequest_pageSize_More_Than_Zero.Description());

        }
    }
}
