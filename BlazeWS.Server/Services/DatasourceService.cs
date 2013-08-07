// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="DataSourceService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Logic;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.DataSources;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Services
{
    /// <summary>
    /// Class DataSourceService
    /// </summary>
    [Authenticate]
    public class DataSourceService : Service
    {
        /// <summary>
        /// Create a New DataSource
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>CreateDataSourceResponse.</returns>
        public CreateDataSourceResponse Any(CreateDataSource i)
        {
            //CREATE NEW DataSource
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            DataSource DataSource = null;
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    DataSource = DataSourceLogic.CreateFromDto(session, tx, i as DtoDataSource);
                    tx.Commit();

                }
                catch
                {
                    tx.Rollback();

                    throw;
                }
            }
            return AutoMapper.Mapper.Map<CreateDataSourceResponse>(DataSource);
        }
        /// <summary>
        /// Gets a DataSource
        /// </summary>
        /// <param name="action">The app.</param>
        /// <returns>GetDataSourceResponse.</returns>
        public GetDataSourceResponse Any(GetDataSource action)
        {
            DataSource a = null;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            if (action.Id != Guid.Empty)
            {
                a = DataSourceLogic.LoadBy(session, p => p.Id == action.Id && p.Application.Id == action.ApplicationId);
            }
            else if(!string.IsNullOrEmpty(action.Name))
            {
                a = DataSourceLogic.LoadBy(session, p => p.Name == action.Name && p.Application.Id == action.ApplicationId);

            }
            if (a != null)
            {
                return AutoMapper.Mapper.Map<GetDataSourceResponse>(a);
            }

            return null;
        }


        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="action">The app.</param>
        /// <returns>DtoDataSource[][].</returns>
        public ListDataSourcesResponse Any(ListDataSources action)
        {
            IEnumerable<DataSource> _apps;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            _apps = DataSourceLogic.LoadAllBy(session, p=>p.Application.Id == action.ApplicationId).ToArray();

            ListDataSourcesResponse response = new ListDataSourcesResponse()
            {
                Data = AutoMapper.Mapper.Map<IEnumerable<DtoDataSource>>(_apps)
            };

            return response;

        }

        /// <summary>
        /// Update DataSource
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>UpdateDataSourceResponse.</returns>
        public UpdateDataSourceResponse Any(UpdateDataSource app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            var DataSource = AutoMapper.Mapper.Map<DataSource>(app);

            if (DataSource != null && DataSource.Id != Guid.Empty)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        DataSourceLogic.Save(DataSource, session, tx);
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
            return new UpdateDataSourceResponse() { Success = result };
        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DeleteDataSourceResponse.</returns>
        public DeleteDataSourceResponse Any(DeleteDataSource app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            var a = DataSourceLogic.LoadBy(session, p => p.Id == app.Id && app.ApplicationId == p.Application.Id);
            if (a != null)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        DataSourceLogic.Delete(a, session, tx);
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
            return new DeleteDataSourceResponse() { Success = result };

        }
    }
}