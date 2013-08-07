// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="LogicAbstract.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using Illisian.Nhibernate;
namespace BlazeWS.Server.Logic
{
    /// <summary>
    /// Class LogicAbstract
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public abstract class LogicAbstract<T> where T : Entity<T>
	{
        /// <summary>
        /// Saves the specified l.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
		public static void Save(T l, ISession session, ITransaction tx)
		{
			l.DateModified = DateTime.Now;
			l.Save(session, tx);
		}
        /// <summary>
        /// Loads the by id.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="id">The id.</param>
        /// <returns>`0.</returns>
        public static T LoadById(ISession session, Guid id)
        {
            return LoadBy(session, p => p.Id == id);
        }
        /// <summary>
        /// Loads the by.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="paramArray">The param array.</param>
        /// <returns>`0.</returns>
		public static T LoadBy(ISession session, params Func<T, bool>[] paramArray)
		{
			return LoadAllBy(session, paramArray).FirstOrDefault();
		}
        /// <summary>
        /// Loads all by.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="paramArray">The param array.</param>
        /// <returns>IEnumerable{`0}.</returns>
		public static IEnumerable<T> LoadAllBy(ISession session, params Func<T, bool>[] paramArray)
		{
			IEnumerable<T> query = from v in session.Query<T>() where v.Active select v;
            
			foreach (var v in paramArray)
			{
				query = query.Where(v);
			}
			return query;

		}
        /// <summary>
        /// Deletes the specified l.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
		public static void Delete(T l, ISession session, ITransaction tx)
		{
			l.Delete(session, tx);
		}
        /// <summary>
        /// Deletes all.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="paramArray">The param array.</param>
		public static void DeleteAll(ISession session, ITransaction tx, params Func<T, bool>[] paramArray)
		{
			var profiles = LoadAllBy(session, paramArray);
			if (profiles != null)
			{
				foreach (var v in profiles)
				{
					v.Delete(session, tx);
				}
			}
		}
        /// <summary>
        /// Softs the delete.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="session">The session.</param>
        /// <param name="tx">The tx.</param>
		public static void SoftDelete(T l, ISession session, ITransaction tx)
		{
			l.Active = false;
			Save(l, session, tx);
		}
	}
}
