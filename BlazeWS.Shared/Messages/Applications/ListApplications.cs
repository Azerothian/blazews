using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Applications
{

    [Route("/applications", Verbs="GET")]
    public class ListApplications : IReturn<ListApplicationsResponse> { }

    public class ListApplicationsResponse
    {
        public IEnumerable<DtoApplication> Applications { get; set; }
    }
}
