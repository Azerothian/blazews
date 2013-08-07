using BlazeWS.Server.Datasources;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Messages.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Managers
{
    public class DataManager : IDataSource
    {
        public DataManager()
        {
        }



        public CreateItemResponse Create(CreateItem action)
        {
            // Verify Path if datasource

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
