// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="DatasourceLogic.cs" company="">
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
    /// Class DatasourceLogic
    /// </summary>
    public class DatasourceLogic : LogicAbstract<Datasource>
    {
        /// <summary>
        /// Creates from dto.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="dtoDatasource">The dto Datasource.</param>
        /// <returns>Datasource.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Datasource CreateFromDto(ISession session, ITransaction tx, Shared.Dto.DtoDatasource dtoDatasource)
        {
            if (dtoDatasource.Id != Guid.Empty)
                dtoDatasource.Id = Guid.Empty;

            var existing = LoadBy(session, new Func<Datasource, bool>(a=>a.Name == dtoDatasource.Name));

            if(existing != null)
            {
                throw new ArgumentException(string.Format("Datasource with the name '{0}' already exists", dtoDatasource.Name));
            }

            var neu = AutoMapper.Mapper.Map<Datasource>(dtoDatasource);

            neu.Active = true;
            neu.DateCreated = DateTime.Now;
            neu.DateModified = DateTime.Now;
            neu.Save(session, tx);

            return neu;
        }

    }
}