using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Messages.Applications
{
    [Route("/applications/{Id}", Verbs="PUT")]
    public class UpdateApplication : DtoApplication,IReturn<UpdateApplicationResponse>
    {
    }
    public class UpdateApplicationResponse
    {
        public bool Success { get; set; }
    }
}
