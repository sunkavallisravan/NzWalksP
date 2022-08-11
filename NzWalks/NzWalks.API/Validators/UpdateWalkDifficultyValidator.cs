using FluentValidation;

namespace NzWalks.API.Validators
{
    public class UpdateWalkDifficultyValidator : AbstractValidator<Models.DTO.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
