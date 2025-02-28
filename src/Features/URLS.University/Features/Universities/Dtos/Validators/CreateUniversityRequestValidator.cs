using FluentValidation;

namespace URLS.University.Features.Universities.Dtos.Validators;

public class CreateUniversityRequestValidator : AbstractValidator<CreateUniversityRequest>
{
    public CreateUniversityRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва є обов'язковою")
            .MaximumLength(150).WithMessage("Назва не повинна перевищувати 150 символів");

        RuleFor(x => x.ShortName)
            .MaximumLength(15).WithMessage("Коротка назва не повинна перевищувати 15 символів");

        RuleFor(x => x.NameEng)
            .MaximumLength(150).WithMessage("Англійська назва не повинна перевищувати 150 символів");

        RuleFor(x => x.ShortNameEng)
            .MaximumLength(15).WithMessage("Англійська коротка назва не повинна перевищувати 15 символів");
    }
}
