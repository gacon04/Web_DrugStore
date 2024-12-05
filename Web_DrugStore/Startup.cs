using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;  // Sử dụng cookies để lưu thông tin đăng nhập ng` dùng
using Microsoft.AspNet.Identity.EntityFramework;
using Web_DrugStore.Identity;
[assembly: OwinStartup(typeof(Web_DrugStore.Startup))]

namespace Web_DrugStore
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppDBContext.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
        // cấu hình role trong đây
        public void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AppDBContext()));
            var appDbContext = new AppDBContext();
            var appUserStore = new AppUserStore(appDbContext);
            var userManager = new AppUserManager(appUserStore);
            // nếu role đã tạo rồi thì sau này nó sẽ ko tạo lại nữa
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }  
            if (userManager.FindByName("admin") == null)
            {
                var user = new AppUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                user.HoTen = "Trần Át Min";
                string userPwd = "Admin123.";
                var checkUser = userManager.Create(user, userPwd);
               
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }    
            }
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }

        }
    }
}
