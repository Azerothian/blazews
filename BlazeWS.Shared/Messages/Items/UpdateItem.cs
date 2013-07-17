using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
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
    public class UpdateItemResponse
    {
        public bool Success { get; set; }
    }
}
