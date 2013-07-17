using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Users
{

    [Route("/users", Verbs="GET")]
    public class ListUsers : IReturn<ListUsersResponse>
    {

        public Guid Application { get; set; }
    }
    public class ListUsersResponse
    {
        public IEnumerable<DtoUser> Users { get; set; }
    }


}
