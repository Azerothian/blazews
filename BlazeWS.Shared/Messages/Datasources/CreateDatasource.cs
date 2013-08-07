using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.DataSources
{
    [Route("/datasources", Verbs="POST")]
    public class CreateDataSource : DtoDataSource, IReturn<CreateDataSourceResponse>
    {
    }
    public class CreateDataSourceResponse : DtoDataSource , IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }

}