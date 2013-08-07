using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Enums
{
    public enum ItemPermission
    {
        /// <summary>
        /// The user none
        /// </summary>
        UserNone = 0,
        /// <summary>
        /// The user read
        /// </summary>
        UserRead = 1,
        /// <summary>
        /// The user write
        /// </summary>
        UserWrite = 2,
        /// <summary>
        /// The user modify
        /// </summary>
        UserModify = 4
    }
}
