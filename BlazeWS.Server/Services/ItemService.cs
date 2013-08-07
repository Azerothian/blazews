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
        public CreateItemResponse Any(CreateItem action)
        {
            return Core.DataManager.Create(action);
        }
        /// <summary>
        /// Gets a Item
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>GetItemResponse.</returns>
        public GetItemResponse Any(GetItem action)
        {
            return Core.DataManager.Get(action);
        }


        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DtoItem[][].</returns>
        /// <exception cref="System.ArgumentException">Application id has not been supplied</exception>
        public ListItemsResponse Any(ListItemChildren action)
        {
            return Core.DataManager.List(action);

        }

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>UpdateItemResponse.</returns>
        public UpdateItemResponse Any(UpdateItem action)
        {
            return Core.DataManager.Update(action);

        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DeleteItemResponse.</returns>
        public DeleteItemResponse Any(DeleteItem action)
        {
            return Core.DataManager.Delete(action);
        }
    }
}