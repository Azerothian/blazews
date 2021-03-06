﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Automapping;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Web;
using NHibernate.Cfg.Loquacious;
using Illisian.Nhibernate.Membership.Entities;

namespace Illisian.Nhibernate
{
    public class Database
    {
       // private List<Type> _types;
       // private List<Assembly> _assemblies;
        private NHibernate.Cfg.Configuration _cfg;
        private FluentConfiguration _config = null;
        private static Database _context = null;
        private ISessionFactory _sessionFactory;
        const string VarSession = "nhsession";


        //public string HBMOutputDirectory { get; set; }

        /// <summary>
        /// Gets the context.
        /// </summary>


        public Database()
        {
           // _types = new List<Type>();
        }
        public static Database Context
        {
            get
            {
                return _context ?? (_context = new Database());
            }
        }
        public void Configure(params Assembly[] asm)
        {
            _cfg = new NHibernate.Cfg.Configuration();
            _cfg.Configure();
            _cfg.Properties[NHibernate.Cfg.Environment.CollectionTypeFactoryClass] = typeof(Net4CollectionTypeFactory).AssemblyQualifiedName;
            _config = Fluently.Configure(_cfg);
            SetupConnection();
            SetupMappings(asm);
            UpdateDbSchema();
			//ExportDbSchema();
            _sessionFactory = _config.BuildSessionFactory();

        }

        private bool CallModelOverride(Type x, AutoPersistenceModel model)
        {
            ((IEntity)Activator.CreateInstance(x)).ModelOverride(model);
            return true;
        }

        protected void SetupMappings(Assembly[] asm)
        {
            AutoPersistenceModel model = AutoMap.Assemblies(asm);
            model = model.Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEntity).IsAssignableFrom(x) &&  CallModelOverride(x, model));
            _config.Mappings(m => m.AutoMappings.Add(model));
        }
        protected void SetupConnection()
        {
            switch (_cfg.Properties["dialect"])
            {
                case "NHibernate.Dialect.MsSql2008Dialect":
                    _config.Database(MsSqlConfiguration.MsSql2008.ConnectionString(_cfg.Properties["connection.connection_string"]));
                    return;
                case "NHibernate.Dialect.MsSql2005Dialect":
                    _config.Database(MsSqlConfiguration.MsSql2005.ConnectionString(_cfg.Properties["connection.connection_string"]));
                    return;
                case "NHibernate.Dialect.MySQLDialect":
                case "NHibernate.Dialect.MySQL5Dialect":
                    _config.Database(MySQLConfiguration.Standard.ConnectionString(_cfg.Properties["connection.connection_string"]));
                    return;
            }

            throw new Exception("Dialect not found or Supported");
        }


        /// <summary>
        /// Updates the db schema.
        /// </summary>
        protected void UpdateDbSchema()
        {
            Action<string> updateExport = x =>
            {
                string s = String.Format("[NHibernate.UpdateDbSchema] {0}", x);
                s = s + "";
            };

            Action<Configuration> schemaUpdate = x =>
            {
                new SchemaUpdate(x).Execute(updateExport, true);
            };
            _config.ExposeConfiguration(schemaUpdate);
        }
        /// <summary>
        /// Exports the db schema.
        /// </summary>
        protected void ExportDbSchema()
        {
            Action<Configuration> schemaUpdate = x =>
            {
                var export = new SchemaExport(x);
                export.Execute(true, true, true);
            };
            _config.ExposeConfiguration(schemaUpdate);
        }

        private bool CheckValidSession(ISession _currentSession)
        {
            if (_currentSession == null || !_currentSession.IsConnected || !_currentSession.IsOpen)
                return false;
            return true;

        }
        public ISession GetSession()
        {
            return CreateSession();
        }


        private ISession CreateSession()
        {
            ISession session = _sessionFactory.OpenSession();
            if (session == null)
            {
                throw new InvalidOperationException("Call to SessionFactory.OpenSession() returned null.");
            }
            return session;
        }

        private ISession _session;

        public ISession CurrentSession
        {
            get
            {
                if (_session == null || !_session.IsConnected || !_session.IsOpen)
                {
                    _session = CreateSession();
                }
                return _session;

            }

        }


        //public void AddTypes(params Type[] types)
        //{
            
        //}

        //public void Initialise()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
