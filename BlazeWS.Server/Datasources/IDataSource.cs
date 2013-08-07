using BlazeWS.Shared.Messages.Items;
using System;
namespace BlazeWS.Server.Datasources
{
    public interface IDataSource
    {
        CreateItemResponse Create(CreateItem action);
        DeleteItemResponse Delete(DeleteItem action);
        GetItemResponse Get(GetItem action);
        ListItemsResponse List(ListItemChildren action);
        UpdateItemResponse Update(UpdateItem action);
    }
}
