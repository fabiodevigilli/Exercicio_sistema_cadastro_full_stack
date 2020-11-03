using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace FirstOne.Cadastros.Api.Config
{
    public class CustomAuthorize
    {
        public static bool ValidateUsuarioClaims(HttpContext httpContext, string claimName, string claimValue)
        {
            var cargosNecessario = claimValue.Trim().Split(",");
            var cargosUsuario = httpContext.User.Claims.FirstOrDefault(x => x.Type == claimName).Value.Split(",");
            var temCargo = cargosUsuario.Any(c => cargosNecessario.Select(s => s).Contains(c));
            return httpContext.User.Identity.IsAuthenticated && temCargo;

            //return httpContext.User.Identity.IsAuthenticated &&
            //    httpContext.User.Claims.Any(x => x.Type == claimName && x.Value.Contains(claimValue));
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequirementClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class RequirementClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequirementClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!CustomAuthorize.ValidateUsuarioClaims(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new JsonResult("Usuário não tem permissão para acessar esta rotina") { StatusCode = 403 };
                return;
            }
           
        }
    }
}
