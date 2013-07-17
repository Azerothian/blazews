using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Items
{

    [Route("/items/children", Verbs="GET")]
    public class ListItemChildren : IReturn<ListItemsResponse> {
        public Guid Application { get; set; }
        public Guid ParentItem { get; set; }
    }
    public class ListItemsResponse
    {
        public DtoItem[] Items { get; set; }

    }


}
