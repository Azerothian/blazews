using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Datasources
{
    [Route("/datasources/{Id}", Verbs="PUT")]
    public class UpdateDatasource : DtoDatasource,IReturn<UpdateDatasourceResponse>
    {
    }
    public class UpdateDatasourceResponse
    {
        public bool Success { get; set; }
    }
}
