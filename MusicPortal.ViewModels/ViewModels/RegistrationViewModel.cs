using FluentValidation.Attributes;
using MusicPortal.ViewModels.Base;
using MusicPortal.ViewModels.Validators;

namespace MusicPortal.ViewModels.ViewModels {
    [Validator(typeof(RegistrationViewModelValidator))]
    public class RegistrationViewModel : IViewModel {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Location { get; set; }
    }
}
