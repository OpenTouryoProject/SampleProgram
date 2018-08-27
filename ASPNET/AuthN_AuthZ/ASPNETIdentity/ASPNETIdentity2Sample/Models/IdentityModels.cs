using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASPNETIdentity2Sample.Models
{
    // ApplicationUser クラスにさらにプロパティを追加すると、ユーザーのプロファイル データを追加できます。詳細については、https://go.microsoft.com/fwlink/?LinkID=317594 を参照してください。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // authenticationType が CookieAuthenticationOptions.AuthenticationType で定義されているものと一致している必要があります
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // ここにカスタム ユーザー クレームを追加します
            return userIdentity;
        }
    }

    //public class ApplicationUser : IdentityUser
    //{
    //    public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
    //    {
    //        return Task.FromResult(GenerateUserIdentity(manager));
    //    }

    //    public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
    //    {
    //        // authenticationType は、CookieAuthenticationOptions.AuthenticationType に定義されている種類と一致する必要があります
    //        var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // カスタム ユーザー要求をここに追加します
    //        return userIdentity;
    //    }

    //    /// <summary>GenerateUserIdentityAsync</summary>
    //    /// <param name="manager">UserManager</param>
    //    /// <returns>ClaimsIdentityを非同期に返す</returns>
    //    /// <remarks>
    //    /// 以下から利用されている。
    //    /// - ApplicationSignInManager.CreateUserIdentityAsync()から呼び出される。
    //    /// - SecurityStampValidator.OnValidateIdentityでdelegateとして設定された後、呼び出される。
    //    /// </remarks>
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // サインインの際（SignInManager.PasswordSignInAsync）、
    //        // ApplicationSignInManager.CreateUserIdentityAsync()経由で呼び出されている。

    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        // 注：authenticationTypeはCookieAuthenticationOptions.AuthenticationTypeで定義されたものと一致する必要があります。
    //        ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

    //        // Add custom user claims here
    //        // カスタム　ユーザのClaimsをここで追加する。
    //        return userIdentity;
    //    }
    //}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            // : base("DefaultConnection")
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    static ApplicationDbContext()
    //    {
    //        // Set the database intializer which is run once during application start
    //        // This seeds the database with admin user credentials and admin role
    //        Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}