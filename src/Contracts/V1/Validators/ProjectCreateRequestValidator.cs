using FluentValidation;
using ToDoOrganizer.Contracts.V1.Requests;

namespace ToDoOrganizer.Contracts.V1.Validators;

public class ProjectCreateRequestValidator : AbstractValidator<ProjectCreateRequest>
{
    public ProjectCreateRequestValidator()
    {
        RuleFor(projectCreateRequest => projectCreateRequest.Description)
            .MaximumLength(100);

        RuleFor(projectCreateRequest=> projectCreateRequest.Name)
            .NotEmpty()
            .MinimumLength(3);
    }
}
