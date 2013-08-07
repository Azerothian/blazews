using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.DataSources
{
    [Route("/datasources/{Id}", Verbs="DELETE")]
    public class DeleteDataSource: IReturn<DeleteDataSourceResponse>
    {
        public Guid Id { get; set; }

        public Guid ApplicationId { get; set; }
    }
    public class DeleteDataSourceResponse : IHasResponseStatus
    {
        public bool Success { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}