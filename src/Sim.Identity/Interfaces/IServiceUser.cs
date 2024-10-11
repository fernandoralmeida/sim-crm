
namespace Sim.Identity.Interfaces
{
    using System.Linq.Expressions;
    using Entity;
    using Microsoft.AspNetCore.Identity;
    using Sim.Identity.Context;

    public interface IServiceUser
    {
        Task<ApplicationUser?> GetIdAsync(string id);
        Task<IEnumerable<ApplicationUser>?> ListAllAsync();
        Task<IEnumerable<AppUserExtended>?> GetUsersGroupedByRolesAndClaimsAsync(string? role = null, string? claim = null);
        Task<IEnumerable<AppUserExtended>?> GetUsersExtendedAsync(Expression<Func<AppUserExtended, bool>>? filter = null);
        Task<bool> lockUnlockAsync(string id, bool lockUnlock);
        Task<bool> SetThemeAsync(string id, string theme);
    }
}
