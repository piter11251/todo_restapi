using FluentValidation;

namespace TodoAPI.Dto.Validators
{
    public class TodoCreateValidator : AbstractValidator<TodoCreateDto>
    {
        public TodoCreateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty");

            RuleFor(x => x.Expiry)
                .Custom((expiry, context) =>
                {
                    if (expiry < DateTime.UtcNow)
                    {
                        context.AddFailure("Expiry", "You cannot set date from past");
                    }
                });

            RuleFor(x => x.Percentage)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Set percentage between 0 and 100");

        }
    }
}
