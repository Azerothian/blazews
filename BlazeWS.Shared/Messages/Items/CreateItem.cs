﻿using BlazeWS.Shared.Dto;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Shared.Messages.Items
{
    [Route("/items", Verbs="POST")]
    public class CreateItem : DtoItem, IReturn<CreateItemResponse>
    {
    }
    public class CreateItemResponse : DtoItem , IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }

}