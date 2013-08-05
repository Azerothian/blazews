using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Datasources
{
    [Route("/datasources/{Id}", Verbs="DELETE")]
    public class DeleteDatasource: IReturn<DeleteDatasourceResponse>
    {
        public Guid Id { get; set; }
    }
    public class DeleteDatasourceResponse
    {
        public bool Success { get; set; }
    }
}