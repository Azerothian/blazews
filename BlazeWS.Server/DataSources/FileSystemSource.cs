using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server.Datasources
{
    public class FileSystemSource
    {

        public string BaseDirectory { get; set; }

        public bool Create(DtoItem data)
        {
            return false;
        }
        public bool Update(DtoItem data)
        {
            return false;
        }
        public DtoItem Get(string path)
        {
            return false;
        }
        public bool Delete(string path)
        {
            return false;
        }
    }
}
