using FluentValidation;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.ViewModels.Validators
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel> {
        public RegistrationViewModelValidator() {
            RuleFor(vm => vm.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress()
                .WithMessage("You should enter a valid email address");

            RuleFor(vm => vm.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty");

            RuleFor(vm => vm.FirstName)
                .NotEmpty()
                .WithMessage("FirstName cannot be empty");

            RuleFor(vm => vm.LastName)
                .NotEmpty()
                .WithMessage("LastName cannot be empty");

            RuleFor(vm => vm.Location)
                .NotEmpty()
                .WithMessage("Location cannot be empty");
        }
    }
}
