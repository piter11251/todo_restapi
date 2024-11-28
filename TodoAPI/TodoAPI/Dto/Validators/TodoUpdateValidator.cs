using FluentValidation;

namespace TodoAPI.Dto.Validators
{
    public class TodoUpdateValidator: AbstractValidator<TodoUpdateDto>
    {
        public TodoUpdateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title cannot be empty");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Title cannot be empty");
        }
    }
}
