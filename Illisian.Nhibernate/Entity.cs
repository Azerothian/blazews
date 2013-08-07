using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Nhibernate.Membership;
using NHibernate;

namespace Illisian.Nhibernate
{
	[Serializable]
	public abstract class Entity<T> : IEntity
	{
		public virtual Guid Id { get; set; }
		public virtual bool Active { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual DateTime DateModified { get; set; }

		public virtual void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
		{
			model.Override<Entity<T>>(map =>
				map.Id(x => x.Id).GeneratedBy.Guid()
			);
		}
		public virtual void PreSave()
		{

		}
		public virtual void Save(ISession session, ITransaction tx)
		{
			this.PreSave();
			if (this.Id == Guid.Empty)
			{
				this.DateCreated = DateTime.Now;
				this.DateModified = DateTime.Now;

                //session.Merge(this);
				session.Save(this);
			}
			else
			{
				this.DateModified = DateTime.Now;

                var o = session.Merge(this);

				session.Update(o);
			}
		}

		public virtual void Refresh(ISession session)
		{
			session.Refresh(this);
		}

		public virtual T Load(Guid g, ISession session)
		{


			T d = default(T);
			try
			{
				d = session.Load<T>(g);
			}
			catch (Exception ex)
			{
				throw new Exception("Error trying to load entity object", ex);
			}
			return (T)d;

		}
		public virtual void Delete(ISession session, ITransaction tx)
		{
			try
			{
				session.Delete(this);
			}
			catch (Exception ex)
			{
				throw new Exception("Error trying to delete entity object", ex);
			}

		}
	}
}

