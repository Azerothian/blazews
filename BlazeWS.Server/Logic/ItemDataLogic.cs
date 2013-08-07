using BlazeWS.Server.Enums;
// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="ItemDataLogic.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using Illisian.Nhibernate;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Logic
{
    /// <summary>
    /// Class ItemDataLogic
    /// </summary>
    public class ItemDataLogic : LogicAbstract<ItemData>
    {
        /// <summary>
        /// Creates from dto.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="dtoItemData">The dto ItemData.</param>
        /// <returns>ItemData.</returns>
        internal static ItemData CreateFromDto(ISession session, ITransaction tx, Shared.Dto.DtoItemData dtoItemData)
        {
            if (dtoItemData.Id != Guid.Empty)
                dtoItemData.Id = Guid.Empty;
            var ItemData = AutoMapper.Mapper.Map<ItemData>(dtoItemData);
            ItemData.Active = true;
            ItemData.DateCreated = DateTime.Now;
            ItemData.DateModified = DateTime.Now;
            ItemData.Save(session, tx);
            return ItemData;
        }
        /// <summary>
        /// Saves the specified ItemData.
        /// </summary>
        /// <param name="ItemData">The ItemData.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        internal new static void Save(ItemData ItemData, ISession session, ITransaction tx)
        {
                ItemData.DateModified = DateTime.Now;
                ItemData.Save(session, tx);
            
        }
        /// <summary>
        /// Deletes the specified ItemData.
        /// </summary>
        /// <param name="ItemData">The ItemData.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        internal new static void Delete(ItemData ItemData, ISession session, ITransaction tx)
        {
            ItemData.Delete(session, tx);

        }
    }
}