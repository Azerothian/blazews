// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="ItemLogic.cs" company="">
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
    /// Class ItemLogic
    /// </summary>
    public class ItemLogic : LogicAbstract<Item>
    {
        /// <summary>
        /// Creates from dto.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="dtoItem">The dto item.</param>
        /// <returns>Item.</returns>
        internal static Item CreateFromDto(ISession session, ITransaction tx, Shared.Dto.DtoItem dtoItem)
        {
            if (dtoItem.Id != Guid.Empty)
                dtoItem.Id = Guid.Empty;


            var item = AutoMapper.Mapper.Map<Item>(dtoItem);


            item.SystemPermissions = ItemPermissionsFlag.UserModify | ItemPermissionsFlag.UserRead | ItemPermissionsFlag.UserWrite;
            item.Active = true;
            item.DateCreated = DateTime.Now;
            item.DateModified = DateTime.Now;
            item.Save(session, tx);
            return item;
        }
        /// <summary>
        /// Saves the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        internal new static void Save(Item item, ISession session, ITransaction tx)
        {
            //if (item.Parent == null)
            //{
            //    item.Parent = null;
            //}
            //if (item.Parent && item.Parent == Guid.Empty)
            //{
            //    item.Parent = null;
            //}
            //if(item.SystemPermissions.HasFlag(ItemPermissionsFlag.UserModify | ItemPermissionsFlag.UserWrite))
            //{
                item.DateModified = DateTime.Now;
                item.Save(session, tx);
            //}
            
        }
        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        internal new static void Delete(Item item, ISession session, ITransaction tx)
        {

            var children = GetChildrenByParent(item, session, tx);
            foreach (var c in children)
            {
                Delete(c, session, tx);
            }
            item.Delete(session, tx);

        }
        /// <summary>
        /// Gets the children by parent.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="filters">The filters.</param>
        /// <returns>IEnumerable{Item}.</returns>
        internal static IEnumerable<Item> GetChildrenByParent(Item item, ISession session, ITransaction tx, params Func<Item, bool>[] filters)
        {

            ///TODO : find a cleaner way..
            var param = new Func<Item, bool>[] { i => i.Parent == item.Id }.Concat(filters.AsEnumerable()).ToArray();

            return LoadAllBy(session, param);
        }

        internal static string GetFullPath(Item item, ISession session, ITransaction tx, string output = "")
        {
            return item.Name + "/" + output;

        }




        //private static void RefreshCollection(Item item, DtoItem ditem, ISession session)
        //{
        //     var children = item.Children;
        //    item.Children.Clear();
        //    foreach (Guid c in ditem.Children)
        //    {
        //        item.Children.Add(session.Load<Item>(c));
        //    }
        //}

    }
}