// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="Item.cs" company="">
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
    /// Class Item
    /// </summary>
    public class ItemData : Entity<ItemData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public ItemData()
        {
            //Children = new HashSet<Item>();
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public virtual string Type { get; set; }

        
        /// <summary>
        /// Gets or sets the object data. 
        /// </summary>
        /// <value>The object data.</value>
        public virtual byte[] Data { get; set; }

        public virtual Item Item { get; set; } 

        /// <summary>
        /// Models the override.
        /// </summary>
        /// <param name="model">The model.</param>
        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<ItemData>(map =>
            {
                map.Map(p => p.Data).Length(10000);

            });
            base.ModelOverride(model);
        }

    }
}