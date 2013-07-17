// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="UserLogic.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Logic
{
    /// <summary>
    /// Class UserLogic
    /// </summary>
    public class UserLogic : LogicAbstract<User>
    {
        /// <summary>
        /// Creates from dto.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="dtoApplicationUser">The dto application user.</param>
        /// <returns>User.</returns>
        /// <exception cref="System.ArgumentException">You cannot create a new user without a application id</exception>
        public static User CreateFromDto(ISession session, ITransaction tx, Shared.Dto.DtoUser dtoApplicationUser)
        {

            if (dtoApplicationUser.Application == Guid.Empty)
            {
                throw new ArgumentException("You cannot create a new user without a application id");
            }

            var users = UserLogic.LoadAllBy(session, 
                u => u.Name == dtoApplicationUser.Name 
                    && u.Application == dtoApplicationUser.Application);
            if (users.Count() > 0)
            {
                throw new ArgumentException("A user with this name already exists");
            }


            var applicationUser = AutoMapper.Mapper.Map<User>(dtoApplicationUser);

            applicationUser.Active = true;
            applicationUser.DateCreated = DateTime.Now;
            applicationUser.DateModified = DateTime.Now;
            applicationUser.Save(session, tx);
            return applicationUser;
        }

    }
}