using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Applications
{
    [Route("/applications", Verbs="POST")]
    public class CreateApplication : DtoApplication, IReturn<CreateApplicationResponse>
    {
    }
    public class CreateApplicationResponse : DtoApplication , IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }

}