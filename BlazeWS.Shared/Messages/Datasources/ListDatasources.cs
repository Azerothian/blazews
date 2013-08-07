using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.DataSources
{

    [Route("/datasources", Verbs="GET")]
    public class ListDataSources : IReturn<ListDataSourcesResponse> { 
        public Guid ApplicationId { get; set; } 
    }

    public class ListDataSourcesResponse : IHasResponseStatus
    {
        public IEnumerable<DtoDataSource> Data { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
