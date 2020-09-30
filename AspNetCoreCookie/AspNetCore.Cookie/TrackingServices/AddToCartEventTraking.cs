using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Cookie.TrackingServices
{
    public class AddToCartEventTraking: IAddToCartEventTraking
    {
        //private members
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;  

        public AddToCartEventTraking(IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }


        public string GetCartIdentifier()
        {
            var cartIdentifier = "";
            try
            {
                //read cookie value from appsettings.js
                var trackingCookieName = configuration["AspNetCore3.1.Cookie.AddToCartTraking"];

                var cartIdentifierCookie = httpContextAccessor.HttpContext.Request.Cookies[trackingCookieName];

                cartIdentifier = cartIdentifierCookie != null ? cartIdentifierCookie : string.Empty;
            }
            catch (Exception ex)
            {
                return "";
            }
            return cartIdentifier;
        }

        public void TrackToAddCartIdentifier(string cartIdenfier)
        {
            var cookieExpireDays = 30;

            try
            {
                //read cookie value from appsettings.js
                var trackingCookieName = configuration["AspNetCore3.1.Cookie.AddToCartTraking"];

                //lets remove first this identifier
                RemoveCookie(trackingCookieName);

                //let's set cookie info
                SetCookie(trackingCookieName, cartIdenfier, cookieExpireDays);
               
            }
            catch (Exception ex)
            {
                
            }
        }


        #region Private Methods


        private void SetCookie(string cookieKey,string cookieValue, int? expiretime)
        {
            CookieOptions options = new CookieOptions();

            options.Expires = DateTime.Now.AddDays(7);

            if (expiretime.HasValue)
                options.Expires = DateTime.Now.AddDays(expiretime.Value);

            httpContextAccessor.HttpContext.Response.Cookies.Append
            (
                cookieKey,cookieValue,options
            );            
        }

        private void RemoveCookie(string cookieKey)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieKey);
        }


        #endregion

    }
}
