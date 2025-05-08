using System;
using Android.Webkit;
using Retail.DependencyServices;
using Retail.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(IClearCookiesImplementation))]
namespace Retail.Droid.DependencyServices
{
    public class IClearCookiesImplementation : IClearCookies
    {
        public void Clear()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }
}
