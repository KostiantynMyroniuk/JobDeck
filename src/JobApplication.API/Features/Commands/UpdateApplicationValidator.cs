using FluentValidation;
using JobApplication.API.Models;

namespace JobApplication.API.Features.Commands
{
    public class UpdateApplicationValidator : AbstractValidator<UpdateApplicationCommand>
    {
        public UpdateApplicationValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(200).WithMessage("Company name must not exceed 200 characters.");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(200).WithMessage("Position must not exceed 200 characters.");

            RuleFor(x => x.JobUrl)
                .MaximumLength(500).WithMessage("Job URL must not exceed 500 characters.")
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Job URL must be a valid URL.");

            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .IsInEnum().WithMessage("Status must be a valid application status.");

            RuleFor(x => x.InterviewDate)
                .NotNull().WithMessage("Interview date is required when status is Interview.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Interview date must be in the future.")
                .When(x => x.Status == ApplicationStatus.Interview);

        }
    }
}
