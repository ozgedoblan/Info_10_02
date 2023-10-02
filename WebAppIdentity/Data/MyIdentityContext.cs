using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppIdentity.Data
{
    public class AppUser : IdentityUser<int>
    {
        public string Fullname { get; set; }       
    }
    public class AppRole :IdentityRole<int>
    {
        public bool IsBuiltin { get; set; } = false;
    }
    public class MyIdentityContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public MyIdentityContext(DbContextOptions options) : base(options){ }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .Property(x => x.Fullname)
                .HasMaxLength(50);

            builder.Entity<AppRole>().HasData(

                new AppRole { Id = 1, Name = "Admin",NormalizedName="ADMIN", IsBuiltin = true },
                new AppRole { Id = 2, Name = "User", NormalizedName = "USER", IsBuiltin = true }
                );
        }
    }
   

}
