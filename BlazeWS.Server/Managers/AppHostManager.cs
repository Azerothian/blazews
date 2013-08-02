// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="AppHostManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Server.Services;
using BlazeWS.Server.Utillities;
using Enyim.Caching;
using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Memcached;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BlazeWS.Server.Managers
{
    /// <summary>
    /// Class AppHostManager
    /// </summary>
    public class AppHostManager : AppHostBase
    {
        private static AppHostManager _context;
        public static AppHostManager Context
        {
            get
            {
                return _context;
            }

        }
        public string ServiceStackHandlerFactoryPath { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AppHostManager"/> class.
        /// </summary>
        public AppHostManager(string path = null) : base("Blaze Web Services", typeof(AppHostManager).Assembly) {
            _context = this;
            ServiceStackHandlerFactoryPath = path;
        }
        public AuthFeature AuthFeature { get; set; }

        public ICacheClient AuthCacheClient { get; set; }

        /// <summary>
        /// Configures the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public override void Configure(Container container)
        {

            if (!string.IsNullOrEmpty(ServiceStackHandlerFactoryPath))
            {
                SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = this.ServiceStackHandlerFactoryPath });
            }
            AuthCacheClient = new MemoryCacheClient();
           // AuthCacheClient = new MemcachedClientCache(new[] { new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11211) });

            container.Register<ICacheClient>(AuthCacheClient);
            //() => new AuthUserSession(), new IAuthProvider[] {
            //    new AppSettingsAuthProvider()
            //}
            AuthFeature = new AuthFeature( new Func<IAuthSession>( () => new AuthUserSession()), new IAuthProvider[] {
               new AppSettingsAuthProvider()
            } );

            AuthFeature.ServiceRoutes.Remove(typeof(AssignRolesService));
            AuthFeature.ServiceRoutes.Remove(typeof(UnAssignRolesService));
            AuthFeature.ServiceRoutes.Add(typeof(AuthCheckService), new[] { "/authcheck" });
            AuthFeature.ServiceRoutes.Add(typeof(AuthLogoutService), new[] { "/authlogout" });
            Plugins.Add(AuthFeature);

           // Plugins.Add(new CorsFeature());
            
            //	var userRep = new RedisAuthRepository();
            //	container.Register<IUserAuthRepository>(userRep);

            ////Add a user for testing purposes
            //string hash;
            //string salt;
            //new SaltedHash().GetHashAndSaltString(Password, out hash, out salt);
            //userRep.CreateUserAuth(new UserAuth
            //{
            //	Id = 1,
            //	DisplayName = "DisplayName",
            //	Email = "as@if.com",
            //	UserName = UserName,
            //	FirstName = "FirstName",
            //	LastName = "LastName",
            //	PasswordHash = hash,
            //	Salt = salt,
            //}, Password);

            //register user-defined REST-ful urls Routes .Add<Hello>("/hello") .Add<Hello>("/hello/{Name}"); 
        }
    }
}