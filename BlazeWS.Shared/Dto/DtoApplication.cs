using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Dto
{
    public class DtoApplication : DtoBase
    {
        public string Name { get; set; }
        public Guid BaseItem { get; set; }
    }
}
