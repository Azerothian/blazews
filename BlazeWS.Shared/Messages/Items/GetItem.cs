
using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Items
{
    [Route("/items",Verbs="GET")]
    public class GetItem : IReturn<GetItemResponse>
    {
        public Guid Id { get; set; }
        public Guid Application { get; set; }
      //  public string Path { get; set; }
    }

    public class GetItemResponse : DtoItem , IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}