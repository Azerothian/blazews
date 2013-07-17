using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
namespace Illisian.Nhibernate
{
	public abstract class NHLogicAbstract<T> where T : Entity<T>
	{
		public static void Save(T l, ISession session, ITransaction tx)
		{
			l.DateModified = DateTime.Now;
			l.Save(session, tx);
		}
        public static T LoadById(ISession session, Guid id)
        {
            return LoadBy(session, p => p.Id == id);
        }
		public static T LoadBy(ISession session, params Func<T, bool>[] paramArray)
		{
			return LoadAllBy(session, paramArray).FirstOrDefault();
		}
		public static IEnumerable<T> LoadAllBy(ISession session, params Func<T, bool>[] paramArray)
		{
			IEnumerable<T> query = from v in session.Query<T>() where v.Active select v;

			foreach (var v in paramArray)
			{
				query = query.Where(v);
			}
			return query;

		}
		public static void Delete(T l, ISession session, ITransaction tx)
		{
			l.Delete(session, tx);
		}
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
		public static void SoftDelete(T l, ISession session, ITransaction tx)
		{
			l.Active = false;
			Save(l, session, tx);
		}
	}
}
