using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using NbCrossPost.Web.Models;

namespace NbCrossPost.Web
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        internal class NopIdentityValidator<TUser> : IIdentityValidator<TUser>
        {
            public async Task<IdentityResult> ValidateAsync(TUser item)
            {
                return IdentityResult.Success;
            }
        }

        internal class NopPasswordValidator : IIdentityValidator<string>
        {
            public async Task<IdentityResult> ValidateAsync(string item)
            {
                // TODO: could check password against web.config
                return IdentityResult.Success;
            }
        }

        internal class NopUserStore : IUserStore<ApplicationUser>
        {
            public void Dispose() {}

            public async Task CreateAsync(ApplicationUser user) {}

            public async Task UpdateAsync(ApplicationUser user) {}

            public async Task DeleteAsync(ApplicationUser user) {}

            public async Task<ApplicationUser> FindByIdAsync(string userId)
            {
                return new ApplicationUser();
            }

            public async Task<ApplicationUser> FindByNameAsync(string userName)
            {
                return new ApplicationUser();
            }
        }

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new NopUserStore());
            
            manager.UserValidator = new NopIdentityValidator<ApplicationUser>();

            manager.PasswordValidator = new NopPasswordValidator();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
              if ((object) user == null)
                throw new ArgumentNullException("user");
              return this.ClaimsIdentityFactory.CreateAsync(this, user, authenticationType);
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
