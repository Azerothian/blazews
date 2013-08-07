using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.DataSources
{
    [Route("/datasources/{Id}", Verbs="PUT")]
    public class UpdateDataSource : DtoDataSource,IReturn<UpdateDataSourceResponse>
    {
    }
    public class UpdateDataSourceResponse : IHasResponseStatus
    {
        public bool Success { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
