using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Shared.Dto
{
    public class DtoUser : DtoBase
    {
        public Guid Application { get; set; }
        public string Name { get; set; }
        public string ObjectData { get; set; }
    }
}
