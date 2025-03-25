using Blog.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Presentation.Web.Resources.Attributes;

public class AuthorizeRoleAttribute: AuthorizeAttribute
{
    public AuthorizeRoleAttribute(params Role[] roles)
    {
        Roles = string.Join(",", roles.Select(r => r.ToString()));
    }
}