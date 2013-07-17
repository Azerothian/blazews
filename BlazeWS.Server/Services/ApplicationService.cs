// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="ApplicationService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Logic;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Applications;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Services
{
    /// <summary>
    /// Class ApplicationService
    /// </summary>
    [Authenticate]
    public class ApplicationService : Service
    {
        /// <summary>
        /// Create a New Application
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>CreateApplicationResponse.</returns>
        public CreateApplicationResponse Any(CreateApplication i)
        {
            //CREATE NEW APPLICATION
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            Application application = null;
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    application = ApplicationLogic.CreateFromDto(session, tx, i as DtoApplication);
                    tx.Commit();

                }
                catch
                {
                    tx.Rollback();

                    throw;
                }
            }
            return AutoMapper.Mapper.Map<CreateApplicationResponse>(application);
        }
        /// <summary>
        /// Gets a application
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>GetApplicationResponse.</returns>
        public GetApplicationResponse Any(GetApplication app)
        {
            Application a = null;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            if (app.Id != Guid.Empty)
            {
                a = ApplicationLogic.LoadBy(session, p => p.Id == app.Id);
            }
            else if(!string.IsNullOrEmpty(app.ApplicationName))
            {
                a = ApplicationLogic.LoadBy(session, p => p.Name == app.ApplicationName);

            }
            if (a != null)
            {
                return AutoMapper.Mapper.Map<GetApplicationResponse>(a);
            }

            return null;
        }


        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DtoApplication[][].</returns>
        public ListApplicationsResponse Any(ListApplications app)
        {
            IEnumerable<Application> _apps;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            _apps = ApplicationLogic.LoadAllBy(session).ToArray();

            ListApplicationsResponse response = new ListApplicationsResponse()
            {
                Applications = AutoMapper.Mapper.Map<IEnumerable<DtoApplication>>(_apps)

            };

            return response;

        }

        /// <summary>
        /// Update Application
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>UpdateApplicationResponse.</returns>
        public UpdateApplicationResponse Any(UpdateApplication app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            var application = AutoMapper.Mapper.Map<Application>(app);

            if (application != null && application.Id != Guid.Empty)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        ApplicationLogic.Save(application, session, tx);
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
            return new UpdateApplicationResponse() { Success = result };
        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DeleteApplicationResponse.</returns>
        public DeleteApplicationResponse Any(DeleteApplication app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            var a = ApplicationLogic.LoadBy(session, p => p.Id == app.Id);
            if (a != null)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        ApplicationLogic.Delete(a, session, tx);
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
            return new DeleteApplicationResponse() { Success = result };

        }
    }
}