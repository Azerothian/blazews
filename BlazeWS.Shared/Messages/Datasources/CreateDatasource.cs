using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Datasources
{
    [Route("/Datasources", Verbs="POST")]
    public class CreateDatasource : DtoDatasource, IReturn<CreateDatasourceResponse>
    {
    }
    public class CreateDatasourceResponse : DtoDatasource
    {
    }

}