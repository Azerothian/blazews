using Illisian.Nhibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Models
{
    public class DataSource : Entity<DataSource>
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
        public virtual Guid ParentItem { get; set; }

        /// <summary>
        /// Gets or sets the object data.
        /// </summary>
        /// <value>The object data.</value>
        public virtual string ObjectData { get; set; }

        public virtual string Type { get; set; }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>The application.</value>
        public virtual Application Application { get; set; }

    }
}
