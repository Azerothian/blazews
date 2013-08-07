using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Applications
{

    [Route("/applications", Verbs="GET")]
    public class ListApplications : IReturn<ListApplicationsResponse> { }

    public class ListApplicationsResponse : IHasResponseStatus
    {
        public IEnumerable<DtoApplication> Applications { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
