using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MU.Admin.Models.Identity;
using MU.Admin.Services;
using MU.Common.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

using static MU.Common.Infrastructure.InfrastructureConstants;
namespace MU.Admin.Controllers
{
    public class IdentityController : BaseControllerClass
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public IdentityController(
           IIdentityService identityService,
           IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginFormModel model)
        => await this.Handle(
            async () =>
            {
                var result = await this.identityService
                    .Login(this.mapper.Map<UserInputModel>(model));

                this.Response.Cookies.Append(
                    AuthenticationCookieName,
                    result.Token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        MaxAge = TimeSpan.FromDays(1)
                    });
            },
            success: RedirectToAction(nameof(StatisticsController.Index), "Statistics"),
            failure: View("../Home/Index", model));

        [AuthorizeAdministrator]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
