using ServiceStack.Configuration;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Utillities
{
    public class AppSettingsAuthProvider : CredentialsAuthProvider
    {
        public class AppSettings
        {
            public static string Username
            {
                get
                {
                    return ConfigurationManager.AppSettings["Username"];
                }
            }

            public static string Password
            {
                get
                {
                    return ConfigurationManager.AppSettings["Password"];
                }
            }
        }


        public override bool TryAuthenticate(IServiceBase authService, string userName, string password)
        {
            //Add here your custom auth logic (database calls etc)
            //Return true if credentials are valid, otherwise false

            return (AppSettings.Username == userName && AppSettings.Password == password);
        }

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            session.IsAuthenticated = true;
            authService.SaveSession(session, SessionExpiry);
        }
    }
}