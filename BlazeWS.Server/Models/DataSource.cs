using Illisian.Nhibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Models
{
    public class Datasource : Entity<Datasource>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public virtual Item Item { get; set; }

        /// <summary>
        /// Gets or sets the object data.
        /// </summary>
        /// <value>The object data.</value>
        public virtual string Data { get; set; }

        /// <summary>
        /// Gets or sets the datasource type.
        /// </summary>
        /// <value>The datasource type.</value>
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>The application.</value>
        public virtual Application Application { get; set; }
        /// <summary>
        /// Gets or sets the last user to modify this item.
        /// </summary>
        /// <value>The user.</value>
       // public virtual User ModifiedBy { get; set; }

        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<Datasource>(map =>
            {
        //        map.Map(p => p.ModifiedBy).Nullable();

            });
            base.ModelOverride(model);
        }

    }
}
