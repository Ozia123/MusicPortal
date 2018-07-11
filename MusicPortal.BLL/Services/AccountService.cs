using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services {
    public class AccountService : IAccountService {
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public AccountService(IMapper mapper, UserManager<AppUser> userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Create(RegistrationViewModel item) {
            var userIdentity = mapper.Map<AppUser>(item);
            return await userManager.CreateAsync(userIdentity, item.Password);
        }
    }
}
