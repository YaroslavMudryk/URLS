using FluentValidation;
using URLS.University.Features.Faculties.DataAccess;

namespace URLS.University.Features.Faculties.Dtos.Validators;

public class CreateFacultyRequestValidator : AbstractValidator<CreateFacultyRequest>
{
    public CreateFacultyRequestValidator(IFacultiesQuery query)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва є обов'язковою")
            .MinimumLength(1).WithMessage("Назва повинна містити хоча б 1 символ")
            .MaximumLength(150).WithMessage("Назва не повинна перевищувати 150 символів");
        RuleFor(x => x.NameEng)
            .MinimumLength(1).WithMessage("Назва повинна містити хоча б 1 символ")
            .MaximumLength(150).WithMessage("Назва не повинна перевищувати 150 символів");

        RuleFor(x => x.Order)
            .NotEmpty().WithMessage("Порядок є обов'язковим");

        RuleFor(x => x).CustomAsync(async (request, conxt, token) => await ValidateAsync(request, conxt, query));
    }

    private static async Task ValidateAsync(CreateFacultyRequest request, ValidationContext<CreateFacultyRequest> validationContext, IFacultiesQuery query)
    {
        if (!request.UniversityId.HasValue)
        {
            validationContext.AddFailure("universityId", "Університет є обов'язковим");
            return;
        }

        var university = await query.GetUniversityNullableAsync(request.UniversityId.Value);
        if (university == null)
        {
            validationContext.AddFailure("universityId", "Університет не знайдено");
            return;
        }

        var sameOrderedFaculty = await query.GetFacultyByOrderNullableAsync(request.UniversityId.Value, request.Order);
        if (sameOrderedFaculty != null)
        {
            validationContext.AddFailure(propertyName: "order", "Порядок факультету зайнятий");
            return;
        }
    }
}
