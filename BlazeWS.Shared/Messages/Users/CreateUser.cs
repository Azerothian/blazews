using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Users
{
    [Route("/users", Verbs="POST")]
    public class CreateUser : DtoUser, IReturn<CreateUserResponse>
    {
    }
    public class CreateUserResponse : DtoUser
    {
    }

}