using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
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
    public class ListUsersResponse : IHasResponseStatus
    {
        public IEnumerable<DtoUser> Users { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }


}
