using System.Security.Claims;
using static MU.Common.Constants;


namespace MU.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdministrator(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);

        public static bool IsTranslator(this ClaimsPrincipal user)
       => user.IsInRole(TranslatorRoleName);
    }
}
