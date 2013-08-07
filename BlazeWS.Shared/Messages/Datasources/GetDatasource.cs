
using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.DataSources
{
    [Route("/datasources/{Id}", Verbs="GET")]
    public class GetDataSource: IReturn<GetDataSourceResponse>
    {
        public Guid ApplicationId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }

    public class GetDataSourceResponse : DtoDataSource , IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}