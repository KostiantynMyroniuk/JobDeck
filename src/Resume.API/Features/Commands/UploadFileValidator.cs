using FluentValidation;

namespace Resume.API.Features.Commands
{
    public class UploadFileValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileValidator()
        {
            RuleFor(x => x.File)
                .NotNull().WithMessage("File is required.")
                .Must(f => f.ContentType == "application/pdf").WithMessage("Only PDF files are allowed.");
        }
    }
}
