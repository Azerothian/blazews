using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
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
    public class UpdateUserResponse : IHasResponseStatus
    {
        public bool Success { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
