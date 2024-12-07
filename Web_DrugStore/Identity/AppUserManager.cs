using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Web_DrugStore.Identity;
using Web_DrugStore.Models;
using Microsoft.Owin.Security.DataProtection;

public class AppUserManager : UserManager<AppUser>
{
    public AppUserManager(IUserStore<AppUser> store)
        : base(store)
    {
        // Cấu hình UserTokenProvider
        var provider = new DpapiDataProtectionProvider("Web_DrugStore");
        this.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(provider.Create("ResetPasswordPurpose"));
    }

    public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
    {
        var manager = new AppUserManager(new UserStore<AppUser>(context.Get<DS_DBContext>()));

        manager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(options.DataProtectionProvider.Create("ResetPasswordPurpose"));

        return manager;
    }
}