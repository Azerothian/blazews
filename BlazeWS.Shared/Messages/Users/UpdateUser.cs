using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Users
{
    [Route("/users/{Id}", Verbs="PUT")]
    public class UpdateUser : DtoUser, IReturn<UpdateUserResponse>
    {
    }
    public class UpdateUserResponse
    {
        public bool Success { get; set; }
    }
}
