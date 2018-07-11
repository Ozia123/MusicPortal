using System.Threading.Tasks;
using MusicPortal.ViewModels.ViewModels;

namespace MusicPortal.Auth.Interfaces {
    public interface IJwtService {
        Task<string> GetJwtForCredentials(CredentialsViewModel credentials);
    }
}
