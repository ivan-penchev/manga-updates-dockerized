using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MU.Common.Extensions;

namespace MU.Common.Services.Identity
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }

            this.UserId = this.user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }

        public bool IsAdministrator => this.user.IsAdministrator();

        public bool isTranslator => this.user.IsTranslator();
    }
}
