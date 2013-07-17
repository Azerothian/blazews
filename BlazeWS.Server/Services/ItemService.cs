// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="ItemService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Logic;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Items;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Services
{
    /// <summary>
    /// Class ItemService
    /// </summary>
    [Authenticate]
    public class ItemService : Service
    {
        /// <summary>
        /// Create a New Item
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>CreateItemResponse.</returns>
        public CreateItemResponse Any(CreateItem i)
        {

            var session = Illisian.Nhibernate.Database.Context.GetSession();
            Item item = null;

            if (i.Application == Guid.Empty)
            {
                throw new ArgumentException("Unable to create an item without an Application id");
            }

            if (i.Parent == Guid.Empty)
            {
                var application = ApplicationLogic.LoadById(session, i.Application);
                i.Parent = application.BaseItem;
            }

            using (var tx = session.BeginTransaction())
            {
                try
                {

                    item = ItemLogic.CreateFromDto(session, tx, i as DtoItem);
                    tx.Commit();

                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw;
                }
            }

            return AutoMapper.Mapper.Map<CreateItemResponse>(item);

        }
        /// <summary>
        /// Gets a Item
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>GetItemResponse.</returns>
        public GetItemResponse Any(GetItem app)
        {
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            Item a = ItemLogic.LoadBy(session, p => p.Id == app.Id
                && (p.Application != null) ?
                        p.Application.Id == app.Application
                        : false);

            if (a != null)
            {
                var response = AutoMapper.Mapper.Map<GetItemResponse>(a);

                return response;
            }

            return null;

        }


        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DtoItem[][].</returns>
        /// <exception cref="System.ArgumentException">Application id has not been supplied</exception>
        public ListItemsResponse Any(ListItemChildren app)
        {
            if (app.Application == Guid.Empty)
            {
                throw new ArgumentException("Application id has not been supplied");
            }

            


            IEnumerable<Item> _items;
            using (var session = Illisian.Nhibernate.Database.Context.GetSession())
            {
                Guid parentID = Guid.Empty;
                if (app.ParentItem == Guid.Empty)
                {
                    Application a = ApplicationLogic.LoadById(session, app.Application);
                    if (a != null && a.BaseItem != Guid.Empty)
                    {
                        parentID = a.BaseItem;
                    }
                    else
                    {
                        throw new ArgumentException("Unable to get item with any Id");
                    }
                }
                else
                {
                    parentID = app.ParentItem;
                }


                _items = ItemLogic.LoadAllBy(session, p => (p.Application.Id == app.Application)
                    && p.Parent == parentID).ToArray();
                
                return  new ListItemsResponse()
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
        public UpdateItemResponse Any(UpdateItem app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            var item = AutoMapper.Mapper.Map<Item>(app);
            if (item != null && item.Id != Guid.Empty)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        ItemLogic.Save(item, session, tx);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
                result = true;
            }
            return new UpdateItemResponse() { Success = result };

        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DeleteItemResponse.</returns>
        public DeleteItemResponse Any(DeleteItem app)
        {

            if (app.Application == Guid.Empty)
            {
                throw new ArgumentException("Application Id was not supplied. Unable to delete Item");
            }
            if (app.Id == Guid.Empty)
            {
                throw new ArgumentException("Item Id was not supplied. Unable to delete Item");
            }
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            

            var a = ItemLogic.LoadBy(session, p => p.Id == app.Id && p.Application == null ? false : p.Application.Id == app.Application);
            if (a != null)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        ItemLogic.Delete(a, session, tx);
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
                result = true;
            }
            return new DeleteItemResponse() { Success = result };

        }
    }
}