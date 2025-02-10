using FluentValidation;
using URLS.University.Features.Specialties.DataAccess;

namespace URLS.University.Features.Specialties.Dtos.Validators;

public class CreateSpecialtyRequestValidator : AbstractValidator<CreateSpecialtyRequest>
{
    public CreateSpecialtyRequestValidator(ISpecialtiesQuery query)
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Назва є обов'язковою")
            .MinimumLength(1).WithMessage("Назва повинна містити хоча б 1 символ")
            .MaximumLength(150).WithMessage("Назва не повинна перевищувати 150 символів");

        RuleFor(s=>s.NameEng)
            .NotEmpty().WithMessage("Назва (англійською) є обов'язковою")
            .MinimumLength(1).WithMessage("Назва повинна містити хоча б 1 символ")
            .MaximumLength(150).WithMessage("Назва не повинна перевищувати 150 символів");

        RuleFor(s => s).CustomAsync(async (request, context, _) => await ValidateAsync(request, context, query));
    }

    private async Task ValidateAsync(CreateSpecialtyRequest request, ValidationContext<CreateSpecialtyRequest> validationContext, ISpecialtiesQuery query)
    {
        if (!request.FacultyId.HasValue)
        {
            validationContext.AddFailure("facultyId", "Факультет/Інститут є обов'язковим");
            return;
        }

        var faculty = await query.GetFacultyNullabeAsync(request.FacultyId.Value);
        if (faculty == null)
        {
            validationContext.AddFailure("facultyId", "Факультет/Інститут не знайдено");
            return;
        }
    }
}
