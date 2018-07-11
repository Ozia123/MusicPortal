using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MusicPortal.Auth.Helpers;
using MusicPortal.Auth.Interfaces;
using MusicPortal.Auth.Models;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicPortal.Auth.Services {
    public class JwtService : IJwtService {
        private readonly UserManager<AppUser> userManager;
        private readonly IJwtFactory jwtFactory;
        private readonly JwtIssuerOptions jwtOptions;

        public JwtService(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions) {
            this.userManager = userManager;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions.Value;
        }

        public async Task<string> GetJwtForCredentials(CredentialsViewModel credentials) {
            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);

            if (identity == null) {
                return null;
            }

            return await Tokens.GenerateJwt(identity, jwtFactory, credentials.Email, jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password) {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var userToVerify = await userManager.FindByNameAsync(userName);

            if (userToVerify == null) {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            if (await userManager.CheckPasswordAsync(userToVerify, password)) {
                return await Task.FromResult(jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }

    }
}
