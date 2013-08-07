using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
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
        public Guid Application { get; set; }
    }
    public class DeleteUserResponse : IHasResponseStatus
    {
        public bool Success { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}