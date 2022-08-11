using FluentValidation;

namespace NzWalks.API.Validators
{
    public class AddWalkDifiicultyRequestValidator : AbstractValidator<Models.DTO.AddWalkDifficultyRequest>
    {
        public AddWalkDifiicultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }

    }
}
