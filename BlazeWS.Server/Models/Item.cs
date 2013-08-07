using BlazeWS.Server.Enums;
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
    public class Item : Entity<Item>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            //Children = new HashSet<Item>();
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public virtual int Type { get; set; } // enum ItemType

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public virtual string Path { get; set; }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public virtual string JsonDataType { get; set; }

        /// <summary>
        /// Gets or sets the object data. 
        /// </summary>
        /// <value>The object data.</value>
        public virtual string JsonData { get; set; }
        /// <summary>
        /// Gets or sets the system permissions
        /// </summary>
        /// <value>The object data.</value>
        public virtual int Permissions { get; set; } // enum ItemPermissions
       
        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>The application.</value>
        public virtual Application Application { get; set; }
        
        /// <summary>
        /// Gets or sets the last user to modify the item.
        /// </summary>
        /// <value>The last user to modify the item.</value>
        public virtual User ModifiedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the datasource.
        /// </summary>
        /// <value>The datasource.</value>
        public virtual ItemData ItemData { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public virtual Guid Parent { get; set; }
        //public virtual ISet<Item> Children
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Models the override.
        /// </summary>
        /// <param name="model">The model.</param>
        public override void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model)
        {
            model.Override<Item>(map =>
            {
                map.Map(p=> p.Parent).CustomType(typeof(GuidTypeConverter));
          
                //map.HasMany<Item>(p => p.Children).KeyColumn("Parent").LazyLoad();
                map.Map(p => p.JsonData).Length(10000);
         //       map.Map(p => p.ModifiedBy).Nullable();
               // map.Map(p => p.Datasource).Column("DatasourceId").Nullable();

            });
            base.ModelOverride(model);
        }

    }
}