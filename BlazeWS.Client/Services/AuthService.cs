using ServiceStack.Common.ServiceClient.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client
{
    public class AuthService
    {
        JsonServiceClient _client;
        public AuthService(JsonServiceClient client)
        {
            _client = client;
        }
        public void Login(string username, string password)
        {
            var authResponse = _client.Send<AuthResponse>(new Auth
            {
                UserName = username,
                Password = password,
                RememberMe = true,  //important tell client to retain permanent cookies
            });
            if (authResponse.ResponseStatus.ErrorCode != null)
            {
                throw new Exception("[BlazeWS.Client.AuthService.Login] - Login Failed");
            }
        }
    }
}
