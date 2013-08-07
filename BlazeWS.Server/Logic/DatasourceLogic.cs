// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="DataSourceLogic.cs" company="">
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
    /// Class DataSourceLogic
    /// </summary>
    public class DataSourceLogic : LogicAbstract<DataSource>
    {
        /// <summary>
        /// Creates from dto.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="dtoDataSource">The dto DataSource.</param>
        /// <returns>DataSource.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static DataSource CreateFromDto(ISession session, ITransaction tx, Shared.Dto.DtoDataSource dtoDataSource)
        {
            if (dtoDataSource.Id != Guid.Empty)
                dtoDataSource.Id = Guid.Empty;

            var existing = LoadBy(session, new Func<DataSource, bool>(a=>a.Name == dtoDataSource.Name && a.Application.Id  == dtoDataSource.Application));

            if(existing != null)
            {
                throw new ArgumentException(string.Format("DataSource with the name '{0}' already exists", dtoDataSource.Name));
            }

            var DataSource = AutoMapper.Mapper.Map<DataSource>(dtoDataSource);

            DataSource.Active = true;
            DataSource.DateCreated = DateTime.Now;
            DataSource.DateModified = DateTime.Now;
            DataSource.Save(session, tx);

            //DataSource.BaseItem = item.Id;
            DataSource.Save(session, tx);

            return DataSource;
        }

    }
}