using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace WEBPresentationLayer.Profile
{
    public static class Util
    {
        public static void IsLogin()
        {
            bool isLogin = User.Identity.IsAuthenticated;
            ViewBag.isLogin = isLogin;
        }
    }
}
