using Microsoft.AspNetCore.Authorization;

namespace Shared.Identity;

public class ScopeRequirement : IAuthorizationRequirement
{
    public ScopeRequirement()
    {
    }
}
