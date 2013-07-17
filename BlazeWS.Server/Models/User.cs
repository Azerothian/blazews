// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="User.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Illisian.Nhibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Models
{
    /// <summary>
    /// Class User
    /// </summary>
    public class User: Entity<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            //Children = new HashSet<Item>();
        }
        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>The application.</value>
        public virtual Guid Application { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }
        /// <summary>
        /// JSON Object
        /// </summary>
        /// <value>The object data.</value>
        public virtual string ObjectData { get; set; }
        /// <summary>
        /// Models the override.
        /// </summary>
        /// <param name="model">The model.</param>
        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<User>(map =>
            {
                map.Map(p => p.ObjectData).Length(10000);
            });
            base.ModelOverride(model);
        }

    }
}