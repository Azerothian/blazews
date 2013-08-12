using BlazeWS.Client.Managers;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client
{
    public class Service : IDisposable
    {

        JsonServiceClient _client;
        public ApplicationService Application { get; set; }
        public AuthService Auth { get; set; }
        public ItemService Item { get; set; }
        public UserService User { get; set; }
        public Service(string username, string password, string uri)
        {
            AutoMapManager.Initialise();
            _client = new JsonServiceClient(uri);
            Application = new ApplicationService(_client);
            Item = new ItemService(_client);
            User = new UserService(_client);
            Auth = new AuthService(_client);
            Auth.Login(username, password);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
