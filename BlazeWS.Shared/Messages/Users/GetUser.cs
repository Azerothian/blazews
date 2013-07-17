
using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Users
{
    [Route("/users/{Id}", Verbs="GET")]
    public class GetUser : IReturn<GetUserResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ApplicationId { get; set; }
    }

    public class GetUserResponse : DtoUser
    {
    }
}