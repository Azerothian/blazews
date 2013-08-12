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

        public IDataSource PrimaryDataSource { get; set; }
        public DataManager()
        {
        }



        public CreateItemResponse Create(CreateItem action)
        {
            var response = PrimaryDataSource.Create(action);
            if (response == null)
            { 
                //TODO: Process Alternatives
            
            }
            return response;
        }

        public DeleteItemResponse Delete(DeleteItem action)
        {
            var response = PrimaryDataSource.Delete(action);
            if (response == null)
            {
                //TODO: Process Alternatives

            }
            return response;
        }

        public GetItemResponse Get(GetItem action)
        {
            var response = PrimaryDataSource.Get(action);
            if (response == null)
            {
                //TODO: Process Alternatives

            }
            return response;
        }

        public ListItemsResponse List(ListItemChildren action)
        {
            var response = PrimaryDataSource.List(action);
            if (response == null)
            {
                //TODO: Process Alternatives

            }
            return response;
        }

        public UpdateItemResponse Update(UpdateItem action)
        {
            var response = PrimaryDataSource.Update(action);
            if (response == null)
            {
                //TODO: Process Alternatives

            }
            return response;
        }
    }
}
