
using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Applications
{
    [Route("/applications/{Id}", Verbs="GET")]
    public class GetApplication: IReturn<GetApplicationResponse>
    {
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
    }

    public class GetApplicationResponse : DtoApplication
    {
    }
}