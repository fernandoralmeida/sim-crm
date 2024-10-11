using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Sim.Identity.Repository
{
    using Entity;
    using Interfaces;
    using Context;
    using Microsoft.AspNetCore.Identity;

    public class RepositoryUser : IServiceUser
    {
        protected readonly IdentityContext _db;
        public RepositoryUser(IdentityContext identity)
        {
            _db = identity;
        }
        public async Task<ApplicationUser?> GetIdAsync(string id)
        {
            return await _db.AppUsers!.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<AppUserExtended>?> GetUsersExtendedAsync(Expression<Func<AppUserExtended, bool>>? filter = null)
        {
            // Obtenha todos os usuários, suas roles e suas claims
            var usersWithRolesAndClaims = await (from user in _db.AppUsers!.Where(s => s.LockoutEnabled == false & s.UserName != "Admin")
                                                 join userRole in _db.UserRoles on user.Id equals userRole.UserId into userRolesGroup
                                                 from userRole in userRolesGroup.DefaultIfEmpty() // Simula o "left join" com userRoles
                                                 join role in _db.Roles on userRole.RoleId equals role.Id into rolesGroup
                                                 from role in rolesGroup.DefaultIfEmpty() // Simula o "left join" com roles
                                                 join userClaim in _db.UserClaims on user.Id equals userClaim.UserId into userClaimsGroup
                                                 from userClaim in userClaimsGroup.DefaultIfEmpty() // Simula o "left join" com claims
                                                 select new
                                                 {
                                                     User = user,
                                                     Role = role.Name,
                                                     Claim = new KeyValuePair<string, string>(
                                                        userClaim.ClaimType,
                                                        userClaim.ClaimValue)
                                                 }).ToListAsync();

            // Certifique-se de que o filtro é aplicado corretamente a uma lista materializada
            var users = new List<AppUserExtended>();

            foreach (var userGroup in usersWithRolesAndClaims.GroupBy(g => g.User))
            {
                users.Add(new AppUserExtended()
                {
                    Id = userGroup.Key.Id,
                    Name = userGroup.Key.Name,
                    UserName = userGroup.Key.UserName,
                    UserEmail = userGroup.Key.Email,
                    LockoutEnabled = userGroup.Key.LockoutEnabled,

                    Roles = userGroup
                                .Select(u => u.Role) // Agrupa as roles corretamente
                                .Distinct() // Remove duplicatas
                                .ToList(),

                    Claims = userGroup
                                .Select(c => new KeyValuePair<string, string>(c.Claim.Key, c.Claim.Value)) // Agrupa as claims corretamente
                                .Distinct() // Remove duplicatas
                                .ToList(),
                });
            }

            return users.AsQueryable<AppUserExtended>().Where(filter ?? (p => true));
        }

        public async Task<IEnumerable<AppUserExtended>?> GetUsersGroupedByRolesAndClaimsAsync(string? role = null, string? claim = null)
        {
            // // Carrega todos os usuários, suas roles e suas claims em uma única consulta
            // var usersWithRolesAndClaims = await _db.AppUsers
            //     .Where(user => user.LockoutEnabled == false && user.UserName != "Admin")
            //     .Select(user => new AppUserExtended
            //     {
            //         Id = user.Id,
            //         Name = user.Name,
            //         UserName = user.UserName,
            //         UserEmail = user.Email,
            //         LockoutEnabled = user.LockoutEnabled,

            //         // Carrega as roles relacionadas ao usuário
            //         Roles = _db.UserRoles
            //             .Where(ur => ur.UserId == user.Id)
            //             .Join(_db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            //             .Where(r => string.IsNullOrEmpty(role) || r == role)
            //             .Distinct()
            //             .ToList(),

            //         // Carrega as claims relacionadas ao usuário  
            //         Claims = _db.UserClaims
            //             .Where(uc => uc.UserId == user.Id)
            //             .Where(c => string.IsNullOrEmpty(claim) || c.ClaimValue == claim)
            //             .Select(uc => new KeyValuePair<string, string>(uc.ClaimType, uc.ClaimValue))
            //             .Distinct()
            //             .ToList()
            //     })
            //     .ToListAsync();

            // var _usersgroups = usersWithRolesAndClaims.Where(
            //             s => string.IsNullOrEmpty(role) || s.Roles!.Any(r => r == role) &&
            //             string.IsNullOrEmpty(claim) || s.Claims!.Any(c => c.Value == claim)
            //         );

            // var _returngroups = new List<AppUserExtended>();

            // if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(claim))
            // {
            //     foreach (var item in _usersgroups!.Where(s => s.Roles!.Any() && s.Claims!.Any()))
            //     {
            //         _returngroups.Add(item);
            //     }
            // }
            // else if (string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(claim))
            // {
            //     foreach (var item in _usersgroups!.Where(s => s.Claims!.Any()))
            //     {
            //         _returngroups.Add(item);
            //     }
            // }
            // else if (!string.IsNullOrEmpty(role) && string.IsNullOrEmpty(claim))
            // {
            //     foreach (var item in _usersgroups!.Where(s => s.Roles!.Any()))
            //     {
            //         _returngroups.Add(item);
            //     }
            // }
            // else
            // {
            //     _returngroups = _usersgroups!.ToList();
            // }

            // return _returngroups;
            // Carrega todos os usuários, suas roles e suas claims em uma única consulta
            var usersWithRolesAndClaims = await _db.AppUsers!
                .Where(user => user.LockoutEnabled == false && user.UserName != "Admin")
                .Select(user => new AppUserExtended
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    LockoutEnabled = user.LockoutEnabled,

                    Roles = _db.UserRoles
                        .Where(ur => ur.UserId == user.Id)
                        .Join(_db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                        .Where(r => string.IsNullOrEmpty(role) || r == role)
                        .Distinct()
                        .ToList(),

                    Claims = _db.UserClaims
                        .Where(uc => uc.UserId == user.Id)
                        .Where(c => string.IsNullOrEmpty(claim) || c.ClaimValue == claim)
                        .Select(uc => new KeyValuePair<string, string>(uc.ClaimType, uc.ClaimValue))
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            // Aplica os filtros de role e claim diretamente na consulta
            return usersWithRolesAndClaims
                .Where(user =>
                    (string.IsNullOrEmpty(role) || user.Roles!.Any(r => r == role)) &&
                    (string.IsNullOrEmpty(claim) || user.Claims!.Any(c => c.Value == claim))
                )
                .ToList();
        }

        public async Task<IEnumerable<ApplicationUser>?> ListAllAsync()
        {
            return await _db.AppUsers!
                                .ToListAsync();
        }

        public async Task<bool> lockUnlockAsync(string id, bool lockUnlock)
        {
            var t = await _db.AppUsers!.FirstOrDefaultAsync(s => s.UserName == id);
            t!.LockoutEnabled = lockUnlock;
            await _db.SaveChangesAsync();
            return lockUnlock;
        }

        public async Task<bool> SetThemeAsync(string id, string theme)
        {
            var t = await _db.AppUsers!.FirstOrDefaultAsync(s => s.Id == id);
            t!.Theme = theme;
            _db.Update(t);
            return await _db.SaveChangesAsync() == 1;
        }
    }
}
