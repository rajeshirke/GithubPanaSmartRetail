using System;
using Foundation;
using Retail.DependencyServices;
using Retail.iOS.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(IClearCookiesImplementation))]
namespace Retail.iOS.DependencyServices
{
    public class IClearCookiesImplementation : IClearCookies
    {
        public void Clear()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
                CookieStorage.DeleteCookie(cookie);
        }
    }
}
