using Course.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.IdentityServer.Services
{
    public class IdentityResourceOwner : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwner(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var exist = await _userManager.FindByEmailAsync(context.UserName);

            if(exist==null)
            {
                var errors = new Dictionary<string, object>
                {
                    {
                        "errors",
                        new List<string> { "Email veya Şifre yanlıştır." }
                    }
                };
                context.Result.CustomResponse = errors;
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(exist, context.Password);

            if(passwordCheck == false)
            {
                var errors = new Dictionary<string, object>
                {
                    {
                        "errors",
                        new List<string> { "Email veya Şifre yanlıştır." }
                    }
                };
                context.Result.CustomResponse = errors;
                return;
            }

            context.Result = new GrantValidationResult(exist.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
