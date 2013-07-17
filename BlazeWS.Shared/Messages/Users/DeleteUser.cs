using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Users
{
    [Route("/users/{Id}", Verbs="DELETE")]
    public class DeleteUser : IReturn<DeleteUserResponse>
    {
        public Guid Id { get; set; }
    }
    public class DeleteUserResponse
    {
        public bool Success { get; set; }
    }
}