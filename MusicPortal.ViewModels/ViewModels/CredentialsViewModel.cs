using FluentValidation.Attributes;
using MusicPortal.ViewModels.Base;
using MusicPortal.ViewModels.Validators;

namespace MusicPortal.ViewModels.ViewModels {
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel : IViewModel {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
