using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Datasources
{

    [Route("/Datasources", Verbs="GET")]
    public class ListDatasources : IReturn<ListDatasourcesResponse> { }

    public class ListDatasourcesResponse
    {
        public IEnumerable<DtoDatasource> Datasources { get; set; }
    }
}
