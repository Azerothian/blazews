using BlazeWS.Server.Managers;
using BlazeWS.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazeWS.Server
{
    public class Core
    {
        public static void Initialise(string path = null)
        {


            Illisian.Nhibernate.Database.Context.Configure(typeof(Application).Assembly);
            new AppHostManager(path).Init();
            AutoMapper.Mapper.Reset();
            new AutoMapManager().Initialise();
        
        }
    }
}
