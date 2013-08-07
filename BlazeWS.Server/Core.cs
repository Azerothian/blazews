using BlazeWS.Server.Logic;
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
        private static DataManager _dataManager;
        public static DataManager DataManager
        {
            get
            {
                if (_dataManager == null)
                    _dataManager = new DataManager();
                return _dataManager;

            }
        }


        public static void Initialise(string path = null)
        {


            Illisian.Nhibernate.Database.Context.Configure(typeof(Application).Assembly);
            new AppHostManager(path).Init();
            AutoMapper.Mapper.Reset();
            new AutoMapManager().Initialise();
            _dataManager = new DataManager();
        
        }
    }
}
