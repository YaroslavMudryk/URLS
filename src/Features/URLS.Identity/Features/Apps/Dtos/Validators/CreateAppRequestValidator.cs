using FluentValidation;
using URLS.Identity.Features.Apps.DataAccess;

namespace URLS.Identity.Features.Apps.Dtos.Validators;

public class CreateAppRequestValidator : AbstractValidator<CreateAppRequest>
{
    public static string CantBeBlank(string label) => $"{label} can't be blank";
    public static string NameIsNotUniqueness(string label) => $"{label} is not uniqueness";
    public static string SomeClaimIdsAreInvalide(string label, IReadOnlyList<int> diff) => $"{label} with {string.Join(',', diff)} are invalide";

    public CreateAppRequestValidator(IAppsQuery query)
    {
        RuleFor(x => x.Name).NotNull().WithMessage(CantBeBlank("Name"));
        RuleFor(x => x.IsActive).NotEmpty().WithMessage(CantBeBlank("IsActive"));
        RuleFor(x => x.ClaimIds).NotEmpty().WithMessage(CantBeBlank("ClaimIds"));
        
        RuleFor(x => x.ClaimIds)
            .CustomAsync(async (claimIds, validationContext, _) => await ValidateClaimIdsAsync(claimIds, query, validationContext));
        
        RuleFor(x => x.Name)
            .CustomAsync(async (appName, validationContext, _) => await ValidateNameUniquenessAsync(appName, query, validationContext));
    }

    private static async Task<bool> ValidateNameUniquenessAsync(string appName, IAppsQuery query, ValidationContext<CreateAppRequest> context)
    {
        var isUniqName = await query.IsAppNameUniquenessAsync(appName);
        if (!isUniqName)
        {
            context.AddFailure(nameof(CreateAppRequest.Name), NameIsNotUniqueness("Name"));
            return false;
        }

        return true;
    }

    private static async Task<bool> ValidateClaimIdsAsync(IReadOnlyList<int> claimIds, IAppsQuery query, ValidationContext<CreateAppRequest> context)
    {
        var diff = await query.GetDiffClaimIdsAsync(claimIds);

        if (diff.Any())
        {
            context.AddFailure(nameof(CreateAppRequest.ClaimIds), SomeClaimIdsAreInvalide("ClaimIds", diff));
            return false;
        }

        return true;
    }
}
