// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="ApplicationLogic.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Models;
using Illisian.Nhibernate;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Logic
{
    /// <summary>
    /// Class ApplicationLogic
    /// </summary>
    public class ApplicationLogic : LogicAbstract<Application>
    {
        /// <summary>
        /// Creates from dto.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="dtoApplication">The dto application.</param>
        /// <returns>Application.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Application CreateFromDto(ISession session, ITransaction tx, Shared.Dto.DtoApplication dtoApplication)
        {
            if (dtoApplication.Id != Guid.Empty)
                dtoApplication.Id = Guid.Empty;

            var existing = LoadBy(session, new Func<Application, bool>(a=>a.Name == dtoApplication.Name));

            if(existing != null)
            {
                throw new ArgumentException(string.Format("Application with the name '{0}' already exists", dtoApplication.Name));
            }

            var application = AutoMapper.Mapper.Map<Application>(dtoApplication);

            application.Active = true;
            application.DateCreated = DateTime.Now;
            application.DateModified = DateTime.Now;
            application.Save(session, tx);

            var item = ItemLogic.CreateFromDto(session, tx, new Shared.Dto.DtoItem()
            {
                Name = "root"
            });
           // item.Parent = null;
            item.Application = application;
          //  item.SystemPermissions = ItemPermissionsFlag.UserNone;
            item.Save(session, tx);

            application.BaseItem = item.Id;
            application.Save(session, tx);

            return application;
        }
        /// <summary>
        /// Deletes the specified l.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        public static new void Delete(Application l, ISession session, ITransaction tx)
        {
            ItemLogic.DeleteAll(session, tx, a => a.Application != null && a.Application.Id == l.Id);
            UserLogic.DeleteAll(session, tx, a => a.Application == l.Id);
            l.Delete(session, tx);
        }
    }
}