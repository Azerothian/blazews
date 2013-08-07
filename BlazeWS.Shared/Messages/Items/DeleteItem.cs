using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Items
{
    [Route("/items", Verbs="DELETE")]
    public class DeleteItem : IReturn<DeleteItemResponse>
    {
       // public Guid Id { get; set; }
        public string Path { get; set; }
        public Guid Application { get; set; }
    }
    public class DeleteItemResponse : IHasResponseStatus
    {
        public bool Success { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}