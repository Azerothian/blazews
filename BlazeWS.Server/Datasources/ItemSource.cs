using BlazeWS.Server.Logic;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Datasources
{
    public class ItemSource : IDataSource
    {
        /// <summary>
        /// Create a New Item
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>CreateItemResponse.</returns>
        public CreateItemResponse Create(CreateItem dtoItem)
        {
            using (var session = Illisian.Nhibernate.Database.Context.GetSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        var item = ItemLogic.Create(session, tx, dtoItem as DtoItem);
                        tx.Commit();
                        return AutoMapper.Mapper.Map<CreateItemResponse>(item);
                    }
                    catch
                    {
                        tx.Rollback();

                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a Item
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>GetItemResponse.</returns>
        public GetItemResponse Get(GetItem action)
        {
            using (var session = Illisian.Nhibernate.Database.Context.GetSession())
            {
                var result = ItemLogic.Get(session, action);
                if (result != null)
                {
                    return AutoMapper.Mapper.Map<GetItemResponse>(result);
                }
            }
            return null;
        }

        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DtoItem[][].</returns>
        /// <exception cref="System.ArgumentException">Application id has not been supplied</exception>
        public ListItemsResponse List(ListItemChildren action)
        {
            using (var session = Illisian.Nhibernate.Database.Context.GetSession())
            {
                var _items = ItemLogic.List(session, action);
                return new ListItemsResponse()
                {
                    Items = AutoMapper.Mapper.Map<DtoItem[]>(_items)
                };

            }
        }

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>UpdateItemResponse.</returns>
        public UpdateItemResponse Update(UpdateItem action)
        {
            var result = false;
            using (var session = Illisian.Nhibernate.Database.Context.GetSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        result = ItemLogic.Update(session, tx, action);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        result = false;
                        throw;
                    }
                }
                
            }
            return new UpdateItemResponse() { Success = result };
        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="action">The app.</param>
        /// <returns>DeleteItemResponse.</returns>
        public DeleteItemResponse Delete(DeleteItem action)
        {
            var result = false;
            using (var session = Illisian.Nhibernate.Database.Context.GetSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        result = ItemLogic.Delete(session, tx, action);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        result = false;
                        throw;
                    }
                }

            }
            return new DeleteItemResponse() { Success = result };
        }
    }
}
