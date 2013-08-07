// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="Application.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Illisian.Nhibernate;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazeWS.Server.Models
{

    /// <summary>
    /// Class Application
    /// </summary>
    public class Application : Entity<Application>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }
    }


}
