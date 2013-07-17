// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="UserService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Logic;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Users;
using ServiceStack.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Services
{
    /// <summary>
    /// Class UserService
    /// </summary>
    [Authenticate]
    public class UserService : Service
    {


        /// <summary>
        /// Create a New User
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>CreateUserResponse.</returns>
        public CreateUserResponse Any(CreateUser request)
        {
            //CREATE NEW User
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            User user = null;
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    user = UserLogic.CreateFromDto(session, tx, request as DtoUser);
                    tx.Commit();

                }
                catch
                {
                    tx.Rollback();

                    throw;
                }
            }
            return AutoMapper.Mapper.Map<CreateUserResponse>(user);
        }
        /// <summary>
        /// Gets a User
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>GetUserResponse.</returns>
        /// <exception cref="System.ArgumentException">You need to provide a application id to request a user</exception>
        public GetUserResponse Any(GetUser request)
        {
            User a = null;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            if (request.ApplicationId == Guid.Empty)
            {
                throw new ArgumentException("You need to provide a application id to request a user");
            }

            if (request.Id != Guid.Empty)
            {
                a = UserLogic.LoadBy(session, p => p.Id == request.Id && p.Application == request.ApplicationId);
            }
            else if (!string.IsNullOrEmpty(request.Name))
            {
                a = UserLogic.LoadBy(session, p => p.Name == request.Name && p.Application == request.ApplicationId);

            }
            if (a != null)
            {
                return AutoMapper.Mapper.Map<GetUserResponse>(a);
            }

            return null;
        }


        /// <summary>
        /// Anies the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>DtoUser[][].</returns>
        /// <exception cref="System.ArgumentException">You need to provide a application id to get a list of users</exception>
        public ListUsersResponse Any(ListUsers request)
        {
            if (request.Application == Guid.Empty)
            {
                throw new ArgumentException("You need to provide a application id to get a list of users");
            }

            IEnumerable<User> _apps;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            _apps = UserLogic.LoadAllBy(session, u => u.Application == request.Application);

            return new ListUsersResponse()
            {
                Users = AutoMapper.Mapper.Map<IEnumerable<DtoUser>>(_apps)
            };

        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>UpdateUserResponse.</returns>
        public UpdateUserResponse Any(UpdateUser request)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();

            var user = AutoMapper.Mapper.Map<User>(request);

            if (user != null && user.Id != Guid.Empty)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        UserLogic.Save(user, session, tx);
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
            return new UpdateUserResponse() { Success = result };
        }
        /// <summary>
        /// Anies the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <returns>DeleteUserResponse.</returns>
        public DeleteUserResponse Any(DeleteUser app)
        {
            var result = false;
            var session = Illisian.Nhibernate.Database.Context.GetSession();
            var a = UserLogic.LoadBy(session, p => p.Id == app.Id);
            if (a != null)
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        UserLogic.Delete(a, session, tx);
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
            return new DeleteUserResponse() { Success = result };

        }
    }
}