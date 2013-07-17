using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Collection.Generic;

namespace Illisian.Nhibernate.Membership.Entities
{
	/// <summary>
	/// 
	/// </summary>
    /// 
    
	public class Application : Entity<Application>
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets or sets the name of the lowered.
		/// </summary>
		/// <value>
		/// The name of the lowered.
		/// </value>
		public virtual string LoweredName { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public virtual string Description { get; set; }

		/// <summary>
		/// Gets or sets the roles.
		/// </summary>
		/// <value>
		/// The roles.
		/// </value>

        public virtual ISet<Role> Roles { get; set; }

		/// <summary>
		/// Gets or sets the users.
		/// </summary>
		/// <value>
		/// The users.
		/// </value>

        public virtual ISet<User> Users { get; set; }

        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<Application>(map =>
            {
                map.Table("Membership_Application");
            });
            base.ModelOverride(model);
        }

	}
}
