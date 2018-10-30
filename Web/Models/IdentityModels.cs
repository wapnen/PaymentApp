using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ElCamino.AspNet.Identity.AzureTable;
using ElCamino.AspNet.Identity.AzureTable.Model;

namespace Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityCloudContext
    {
       

        public ApplicationDbContext(IdentityConfiguration config) : base(config) { }

        public static ApplicationDbContext Create()
        {
            var config = new IdentityConfiguration();
            config.StorageConnectionString = System.Configuration.ConfigurationManager.AppSettings["StorageConnectionString"].ToString();
            config.TablePrefix = System.Configuration.ConfigurationManager.AppSettings["TablePrefix"].ToString();
            return new ApplicationDbContext(config);
        }
    }
}