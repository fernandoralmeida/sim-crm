using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Sim.Identity.Context
{
    using Entity;
    using Config;
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext() { }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {

        }

        public DbSet<ApplicationUser> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=127.0.0.1,1433;Database=sim-accountcenter;User Id=sa;Password=sql@1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfiguration(new Config.UserMap());
            base.OnModelCreating(modelbuilder);

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();

            //Seeding the User to AspNetUsers table
            modelbuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    Name = "Admin",
                    LastName = "Global",
                    Gender = "Masculino",
                    Email = "sim@sim.com",
                    NormalizedEmail = "sim@sim.com".ToUpper(),
                    UserName = "Admin",
                    NormalizedUserName = "Admin".ToUpper(),
                    PhoneNumber = "+111111111111",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = hasher.HashPassword(null, "$im1234"),
                    Theme = "dark"
                }
            );

            // Aqui você pode adicionar claims diretamente no OnModelCreating
            modelbuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9", // Substitua pelo ID do usuário
                    ClaimType = "Permission",
                    ClaimValue = "Adm_Global"
                }
            );

        }
    }
}
