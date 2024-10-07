using Microsoft.EntityFrameworkCore;

namespace Sim.Identity.Repository
{
    using Entity;
    using Interfaces;
    using Context;
    using System.Collections.Generic;

    public class RepositoryUser : IServiceUser
    {
        protected IdentityContext _db;
        public RepositoryUser(IdentityContext identity)
        {
            _db = identity;
        }
        public async Task<ApplicationUser> GetIdAsync(string id)
        {
            return await _db.AppUsers.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<KeyValuePair<string, IEnumerable<ApplicationUser>>>> GetUsersGroupedByRolesAndClaimsAsync()
        {
            // Obtenha todos os usuários, suas roles e suas claims
            var usersWithRolesAndClaims = await (from user in _db.Users.Where(s => s.LockoutEnabled == false & s.UserName != "Admin")
                                                 join userRole in _db.UserRoles on user.Id equals userRole.UserId into userRolesGroup
                                                 from userRole in userRolesGroup.DefaultIfEmpty() // Simula o "left join" com userRoles
                                                 join role in _db.Roles on userRole.RoleId equals role.Id into rolesGroup
                                                 from role in rolesGroup.DefaultIfEmpty() // Simula o "left join" com roles
                                                 join userClaim in _db.UserClaims on user.Id equals userClaim.UserId into userClaimsGroup
                                                 from userClaim in userClaimsGroup.DefaultIfEmpty() // Simula o "left join" com claims
                                                 select new
                                                 {
                                                     User = user,
                                                     Role = role,
                                                     Claims = userClaim
                                                 }).ToListAsync();

            // Agrupa por Role, ignorando o agrupamento por Claim
            var roleUsers = usersWithRolesAndClaims
                .GroupBy(x => x.Role)
                .Select(
                    g => new KeyValuePair<string, IEnumerable<ApplicationUser>>(
                        g.Key == null ? "" : g.Key.Name, g.Select(u => (ApplicationUser)u.User)));

            var claimUsers = usersWithRolesAndClaims
                .Where(x => x.Claims != null)
                .GroupBy(x => x.Claims)
                .Select(
                    g => new KeyValuePair<string, IEnumerable<ApplicationUser>>(
                        g.Key == null ? "" : g.Key.ClaimValue, g.Select(u => (ApplicationUser)u.User)));

            return roleUsers.Concat(claimUsers).OrderBy(o => o.Key);
        }

        public async Task<IEnumerable<ApplicationUser>> ListAllAsync()
        {
            return await _db.AppUsers
                                .ToListAsync();
        }

        public async Task<bool> lockUnlockAsync(string id, bool lockUnlock)
        {
            var t = await _db.AppUsers.FirstOrDefaultAsync(s => s.UserName == id);
            t.LockoutEnabled = lockUnlock;
            await _db.SaveChangesAsync();
            return lockUnlock;
        }

        public async Task<bool> SetThemeAsync(string id, string theme)
        {
            var t = await _db.AppUsers.FirstOrDefaultAsync(s => s.Id == id);
            t.Theme = theme;
            _db.Update(t);
            return await _db.SaveChangesAsync() == 1;
        }
    }
}
