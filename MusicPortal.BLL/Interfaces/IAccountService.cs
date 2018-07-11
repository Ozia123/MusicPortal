using Microsoft.AspNetCore.Identity;
using MusicPortal.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces {
    public interface IAccountService {
        Task<IdentityResult> Create(RegistrationViewModel item);
    }
}
