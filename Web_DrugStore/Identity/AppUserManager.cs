using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Web_DrugStore.Identity;
using Web_DrugStore.Models;
using Microsoft.Owin.Security.DataProtection;
using System;

public class AppUserManager : UserManager<AppUser>
{
    public AppUserManager(IUserStore<AppUser> store)
        : base(store)
    {
        // Cấu hình UserTokenProvider
        var provider = new DpapiDataProtectionProvider("Web_DrugStore");
        var tokenProvider = new DataProtectorTokenProvider<AppUser>(provider.Create("ResetPasswordPurpose")) // mặc định token tạo ra là one-time use
        {
            TokenLifespan = TimeSpan.FromHours(1) // tuổi thọ token
        };
        // khi bấm vào token 1 lần nhưng chưa đổi mật khẩu thì vẫn xài dc lần sau, chỉ bị ảnh hưởng bởi lifespan
        this.UserTokenProvider = tokenProvider;
    }

    public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
    {
        var manager = new AppUserManager(new UserStore<AppUser>(context.Get<DS_DBContext>()));

        manager.UserTokenProvider = new DataProtectorTokenProvider<AppUser>(options.DataProtectionProvider.Create("ResetPasswordPurpose"));

        return manager;
    }
}