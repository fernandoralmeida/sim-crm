
namespace Sim.Identity.Interfaces
{
    using Entity;
    using Sim.Identity.Context;

    public interface IServiceUser
    {
        Task<ApplicationUser> GetIdAsync(string id);
        Task<IEnumerable<ApplicationUser>> ListAllAsync();
        Task<IEnumerable<KeyValuePair<string, IEnumerable<ApplicationUser>>>> GetUsersGroupedByRolesAndClaimsAsync();
        Task<bool> lockUnlockAsync(string id, bool lockUnlock);
        Task<bool> SetThemeAsync(string id, string theme);
    }
}
