using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Datasources
{
    public class FileSystemSource : IDataSource
    {
        public string BaseDirectory { get; set; }
        public CreateItemResponse Create(CreateItem action)
        {
            throw new NotImplementedException();
        }

        public DeleteItemResponse Delete(DeleteItem action)
        {
            throw new NotImplementedException();
        }

        public GetItemResponse Get(GetItem action)
        {
            throw new NotImplementedException();
        }

        public ListItemsResponse List(ListItemChildren action)
        {
            throw new NotImplementedException();
        }

        public UpdateItemResponse Update(UpdateItem action)
        {
            throw new NotImplementedException();
        }
    }
}
