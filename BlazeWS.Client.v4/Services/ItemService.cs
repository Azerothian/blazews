﻿using BlazeWS.Client.Managers;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Items;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Client.Service
{
    public class ItemService
    {
        JsonServiceClient _client;
        public ItemService(string url = "http://blazews.localhost.com/")
        {
            AutoMapManager.Initialise();
            _client = new JsonServiceClient(url);
        }
        public DtoItem Create(DtoItem dto)
        {
            var msg = AutoMapper.Mapper.Map<DtoItem, CreateItem>(dto);

            var response = _client.Send<CreateItemResponse>(msg);


           return AutoMapper.Mapper.Map<DtoItem>(response);
        }
        
        public DtoItem Get(Guid ApplicationId, Guid ItemId)
        {
            var response = _client.Send<GetItemResponse>(new GetItem() { Id = ItemId, Application = ApplicationId });
            return AutoMapper.Mapper.Map<DtoItem>(response);
        }

        public IEnumerable<DtoItem> GetAll(Guid application, Guid? parentId = null)
        {
            return _client.Send<IEnumerable<DtoItem>>(new ListItemChildren() { ParentItem = parentId ?? Guid.Empty, Application = application });
        }

        public bool Update(DtoItem dto)
        {

            var msg = AutoMapper.Mapper.Map<DtoItem, UpdateItem>(dto);

            return _client.Send<UpdateItemResponse>(msg).Success;
        }
        public bool Delete(Guid ApplicationId, Guid id)
        {
            return _client.Send<DeleteItemResponse>(new DeleteItem() { Id = id, Application = ApplicationId }).Success;
        }

    }
}
