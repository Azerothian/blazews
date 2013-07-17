// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="StatisticsLogic.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using BlazeWS.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Logic
{
    /// <summary>
    /// Class StatisticsLogic
    /// </summary>
    public class StatisticsLogic
    {
        /// <summary>
        /// Gets or sets the total access.
        /// </summary>
        /// <value>The total access.</value>
        public static Dictionary<StatisticsTrackType, int> TotalAccess { get; set; }
        /// <summary>
        /// Gets or sets the statistics.
        /// </summary>
        /// <value>The statistics.</value>
        public static List<DtoStatistics> Statistics { get; set; }
        /// <summary>
        /// Gets or sets the time started.
        /// </summary>
        /// <value>The time started.</value>
        public static DateTime TimeStarted { get; set; }

        /// <summary>
        /// The history create
        /// </summary>
        public static HashSet<int> HistoryCreate;

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        public static void Initialise()
        {
            //TotalAccess = new Dictionary<StatisticsTrackType, int>();
            //Statistics = new List<DtoStatistics>();
            //TimeStarted = DateTime.Now;
        }

        /// <summary>
        /// Tracks the start.
        /// </summary>
        /// <param name="statsItem">The stats item.</param>
        public static void TrackStart(DtoStatistics statsItem)
        {
            //if (Statistics != null)
            //{
            //    statsItem.Started = DateTime.Now;
            //}

        }
        /// <summary>
        /// Tracks the end.
        /// </summary>
        /// <param name="statsItem">The stats item.</param>
        public static void TrackEnd(DtoStatistics statsItem)
        {
            //if (Statistics != null)
            //{
            //    statsItem.Ended = DateTime.Now;

            //    if (Statistics.Count() > 500)
            //    {
            //        Statistics.RemoveRange(0, 1);
            //    }

            //    Statistics.Add(statsItem);
            //}
        }

    }
}