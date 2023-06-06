using FluentValidation;
using WebApiAdvance.Entities.Dtos.Products;

namespace WebApiAdvance.Validation.Product
{
    public class CreateProductDtoValidator:AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Bos Gondermek Olmaz")
                .NotNull().WithMessage("Bosh Gondermek Olmaz")
                .MaximumLength(100)
                .MinimumLength(6)
                .Must(MustBeStartWithA).WithMessage("A ile Baslamalidi");  ////////////Yeni ashagdaki methodu cagirmaq istiyirsizse Must()Ist olunur

            RuleFor(p => p.Price)
                .NotNull().WithMessage("Qiymet Daxil Edin")
                .GreaterThanOrEqualTo(10) ////10 dan Boyuk olsun
                .LessThanOrEqualTo(100);   //////100den kicik olsun
        }
        public bool MustBeStartWithA(string name)
        {
            return name.StartsWith("A");
        }
    }
}
