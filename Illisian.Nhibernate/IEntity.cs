﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Illisian.Nhibernate
{
    public interface IEntity
    {
        void ModelOverride(FluentNHibernate.Automapping.AutoPersistenceModel model);
    }
}
