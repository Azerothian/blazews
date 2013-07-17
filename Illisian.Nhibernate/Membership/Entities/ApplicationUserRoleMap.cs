using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Nhibernate.Membership.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationUserRoleMap : Entity<ApplicationUserRoleMap>
	{
		/// <summary>
		/// Gets or sets the application.
		/// </summary>
		/// <value>
		/// The application.
		/// </value>
		public virtual Application Application { get; set; }

		/// <summary>
		/// Gets or sets the user.
		/// </summary>
		/// <value>
		/// The user.
		/// </value>
		public virtual User User { get; set; }

		/// <summary>
		/// Gets or sets the role.
		/// </summary>
		/// <value>
		/// The role.
		/// </value>
		public virtual Role Role { get; set; }

        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<Application>(map =>
            {
                map.Table("Membership_ApplicationUserRoleMap");
            });
            base.ModelOverride(model);
        }

	}
}
