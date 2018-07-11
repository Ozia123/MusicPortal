using FluentValidation;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.ViewModels.Validators {
    public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel> {
        public CredentialsViewModelValidator() {
            RuleFor(vm => vm.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty");

            RuleFor(vm => vm.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty");

            RuleFor(vm => vm.Password)
                .Length(6, 12)
                .WithMessage("Password length must be between 6 and 12 characters");
        }
    }
}
