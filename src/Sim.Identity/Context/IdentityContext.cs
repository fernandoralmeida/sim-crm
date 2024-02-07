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

            //Seeding a  'Administrators' role to AspNetRoles table
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = AccountType.Adm_Global, NormalizedName = AccountType.Adm_Global.ToUpper() });
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "90ef8df5-ab72-457d-8cc7-8da2872ce404", Name = AccountType.Adm_Account, NormalizedName = AccountType.Adm_Account.ToUpper() });
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "deaf7dfe-db2c-4d9c-aa11-a0943983e247", Name = AccountType.Adm_Settings, NormalizedName = AccountType.Adm_Settings.ToUpper() });

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


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelbuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );

        }
    }
}
