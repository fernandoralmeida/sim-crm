using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RoleOrClaimAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly string _role;
    private readonly string _claimType;
    private readonly string _claimValue;

    public RoleOrClaimAuthorizeAttribute(string role, string claimType, string claimValue)
    {
        _role = role;
        _claimType = claimType;
        _claimValue = claimValue;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        // Verifica se o usuário tem a role ou a claim
        if (!user.IsInRole(_role) && !user.HasClaim(_claimType, _claimValue))
        {
            context.Result = new ForbidResult();
        }
    }
}