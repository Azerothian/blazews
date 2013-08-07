using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Items
{
    [Route("/items/{Id}", Verbs="PUT")]
    public class UpdateItem : DtoItem
    {
    }
    public class UpdateItemResponse : IHasResponseStatus
    {
        public bool Success { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
