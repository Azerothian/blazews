// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="DatasourceService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Logic;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Datasources;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Services
{
    /// <summary>
    /// Class DatasourceService
    /// </summary>
    [Authenticate]
    public class DatasourceService : Service
    {
        /// <summary>
        /// Create a New Datasource
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>CreateDatasourceResponse.</returns>
        public CreateDatasourceResponse Any(CreateDatasource i)
        {
            //CREATE NEW Datasource
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            Datasource Datasource = null;
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    Datasource = DatasourceLogic.CreateFromDto(session, tx, i as DtoDatasource);
                    tx.Commit();

                }
                catch
                {
                    tx.Rollback();

                    throw;
                }
            }
            return AutoMapper.Mapper.Map<CreateDatasourceResponse>(Datasource);
        }
        /// <summary>
        /// Gets a Datasource
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>GetDatasourceResponse.</returns>
        public GetDatasourceResponse Any(GetDatasource app)
        {
            Datasource a = null;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            if (app.Id != Guid.Empty)
            {
                a = DatasourceLogic.LoadBy(session, p => p.Id == app.Id);
            }
            else if(!string.IsNullOrEmpty(app.DatasourceName))
            {
                a = DatasourceLogic.LoadBy(session, p => p.Name == app.DatasourceName);

            }
            if (a != null)
            {
                return AutoMapper.Mapper.Map<GetDatasourceResponse>(a);
            }

            return null;
        }


        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DtoDatasource[][].</returns>
        public ListDatasourcesResponse Any(ListDatasources app)
        {
            IEnumerable<Datasource> _apps;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            _apps = DatasourceLogic.LoadAllBy(session).ToArray();

            ListDatasourcesResponse response = new ListDatasourcesResponse()
            {
                Datasources = AutoMapper.Mapper.Map<IEnumerable<DtoDatasource>>(_apps)

            };

            return response;

        }

        /// <summary>
        /// Update Datasource
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>UpdateDatasourceResponse.</returns>
        public UpdateDatasourceResponse Any(UpdateDatasource app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            var Datasource = AutoMapper.Mapper.Map<Datasource>(app);

            if (Datasource != null && Datasource.Id != Guid.Empty)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        DatasourceLogic.Save(Datasource, session, tx);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }

                result = true;

            }
            return new UpdateDatasourceResponse() { Success = result };
        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DeleteDatasourceResponse.</returns>
        public DeleteDatasourceResponse Any(DeleteDatasource app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            var a = DatasourceLogic.LoadBy(session, p => p.Id == app.Id);
            if (a != null)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        DatasourceLogic.Delete(a, session, tx);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
                result = true;
            }
            return new DeleteDatasourceResponse() { Success = result };

        }
    }
}