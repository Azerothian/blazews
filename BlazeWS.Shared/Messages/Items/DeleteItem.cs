using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Items
{
    [Route("/items/{Id}", Verbs="DELETE")]
    public class DeleteItem : IReturn<DeleteItemResponse>
    {
        public Guid Id { get; set; }
        public Guid Application { get; set; }
    }
    public class DeleteItemResponse
    {
        public bool Success { get; set; }
    }
}