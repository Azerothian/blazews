using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Items
{

    [Route("/items/children", Verbs="GET")]
    public class ListItemChildren : IReturn<ListItemsResponse> {
        public Guid Application { get; set; }
        public Guid Id { get; set; }
       // public string Path { get; set; }
    }
    public class ListItemsResponse : IHasResponseStatus
    {
        public DtoItem[] Items { get; set; }


        public ResponseStatus ResponseStatus { get; set; }
    }


}
