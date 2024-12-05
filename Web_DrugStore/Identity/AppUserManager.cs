using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Web_DrugStore.Identity;
using Web_DrugStore.Models;

public class AppUserManager : UserManager<AppUser>
{
    public AppUserManager(IUserStore<AppUser> store)
        : base(store)
    {
    }

    public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
    {
        var manager = new AppUserManager(new UserStore<AppUser>(context.Get<DS_DBContext>()));
        // Cấu hình các settings cho manager nếu cần
        return manager;
    }
}