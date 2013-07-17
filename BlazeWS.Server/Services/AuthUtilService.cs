using System;
using System.Configuration;
using System.Runtime.Serialization;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Text;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using BlazeWS.Server.Managers;

namespace BlazeWS.Server.Services
{
    public class AuthCheck : IReturn<AuthCheckResponse>
    {
        public AuthCheck()
        {
        }
    }

    public class AuthCheckResponse : IHasResponseStatus
    {
        public AuthCheckResponse()
        {

        }
        public bool IsValid { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
    [DefaultRequest(typeof(AuthCheck))]
    public class AuthCheckService : Service
    {
        public IUserAuthRepository UserAuthRepo { get; set; }


        public object Post(AuthCheck request)
        {
            var userSession = SessionFeature.GetOrCreateSession<AuthUserSession>(AppHostManager.Context.AuthCacheClient);
            return new AuthCheckResponse()
            {
                IsValid = userSession.IsAuthenticated

            };
        }
        public object Get(AuthCheck request)
        {
            return Post(request);
        }
    }

    public class AuthLogout : IReturn<AuthLogoutResponse>
    {
        public AuthLogout()
        {
        }
    }

    public class AuthLogoutResponse : IHasResponseStatus
    {
        public AuthLogoutResponse()
        {

        }
        public bool IsValid { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
    [DefaultRequest(typeof(AuthLogout))]
    public class AuthLogoutService : Service
    {
        public IUserAuthRepository UserAuthRepo { get; set; }


        public object Post(AuthLogout request)
        {
            var userSession = SessionFeature.GetOrCreateSession<AuthUserSession>(AppHostManager.Context.AuthCacheClient);
            userSession.IsAuthenticated = false;
            return new AuthLogoutResponse()
            {
                IsValid = userSession.IsAuthenticated

            };
        }
        public object Get(AuthLogout request)
        {
            return Post(request);
        }
    }

}