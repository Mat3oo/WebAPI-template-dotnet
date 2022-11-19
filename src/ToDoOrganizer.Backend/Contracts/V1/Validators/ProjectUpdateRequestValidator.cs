using FluentValidation;
using ToDoOrganizer.Backend.Contracts.V1.Requests;

namespace ToDoOrganizer.Backend.Contracts.V1.Validators;

public class ProjectUpdateRequestValidator : AbstractValidator<ProjectUpdateRequest>
{
    public ProjectUpdateRequestValidator()
    {
        RuleFor(projectCreateRequest => projectCreateRequest.Description)
            .MaximumLength(100);

        RuleFor(projectCreateRequest=> projectCreateRequest.Name)
            .NotEmpty()
            .MinimumLength(3);
    }
}
