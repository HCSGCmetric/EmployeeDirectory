using Microsoft.Owin.Security.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Helper
{
    public class GoogleAuthProvider : GoogleOAuth2AuthenticationProvider
    {
        public override void ApplyRedirect(GoogleOAuth2ApplyRedirectContext context)
        {
            var newRedirectUri = context.RedirectUri;
            newRedirectUri += string.Format("&hd={0}", "hcsgops.com");


            context.Response.Redirect(newRedirectUri);
        }
    }
}