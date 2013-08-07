using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Applications
{
    [Route("/applications/{Id}", Verbs="DELETE")]
    public class DeleteApplication: IReturn<DeleteApplicationResponse>
    {
        public Guid Id { get; set; }
    }
    public class DeleteApplicationResponse : IHasResponseStatus
    {
        public bool Success { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}