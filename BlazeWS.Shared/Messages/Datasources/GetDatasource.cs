
using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Datasources
{
    [Route("/datasources/{Id}", Verbs = "GET")]
    public class GetDatasource: IReturn<GetDatasourceResponse>
    {
        public Guid Id { get; set; }
        public string DatasourceName { get; set; }
    }

    public class GetDatasourceResponse : DtoDatasource
    {
    }
}