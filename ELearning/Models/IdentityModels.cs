using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ELearning.Models
{
    
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        

        [Display(Name ="Full Name"),Required]
        public string FullName { get; set; }
        [Required]
        public string Gender { get; set; }
        public int Phone { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //Role creation class
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName) : base(roleName) { }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
          //  this.Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
      
        public System.Data.Entity.DbSet<ELearning.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<ELearning.Models.Videos> Videos { get; set; }

        public System.Data.Entity.DbSet<ELearning.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<ELearning.Models.SubCategory> SubCategories { get; set; }

        public System.Data.Entity.DbSet<ELearning.Models.Posts> Posts { get; set; }

        public System.Data.Entity.DbSet<ELearning.Models.Replies> Replies { get; set; }

        public System.Data.Entity.DbSet<ELearning.Models.Forum> Forum { get; set; }

        // public System.Data.Entity.DbSet<ELearning.Models.RoleViewModel> RoleViewModels { get; set; }
    }
}