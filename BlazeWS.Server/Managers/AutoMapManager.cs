// ***********************************************************************
// Assembly         : BlazeWS.Server
// Author           : Matthew Mckenzie
// Created          : 07-01-2013
//
// Last Modified By : Matthew Mckenzie
// Last Modified On : 07-01-2013
// ***********************************************************************
// <copyright file="AutoMapManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoMapper;
using BlazeWS.Server.Logic;
using BlazeWS.Server.Models;
using BlazeWS.Shared.Dto;
using BlazeWS.Shared.Messages.Applications;
using BlazeWS.Shared.Messages.Items;
using BlazeWS.Shared.Messages.Users;
using Illisian.Nhibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlazeWS.Server.Managers
{
    /// <summary>
    /// Class AutoMapManager
    /// </summary>
    public class AutoMapManager
    {
        /// <summary>
        /// Initialises this instance.
        /// </summary>
        public void Initialise()
        {
            //Item Mappings            
            IdMappings<Item>();
            DtoMappings<Item, DtoItem>();
            DtoMappings<Item, CreateItem>();
            DtoMappings<Item, UpdateItem>();
            DtoMappings<Item, CreateItemResponse>();
            DtoMappings<Item, GetItemResponse>();


            //Application Mappings
            IdMappings<Application>();
            DtoMappings<Application, DtoApplication>();
            DtoMappings<Application, CreateApplication>();
            DtoMappings<Application, UpdateApplication>();
            DtoMappings<Application, CreateApplicationResponse>();
            DtoMappings<Application, GetApplicationResponse>();

            IdMappings<User>();
            DtoMappings<User, DtoUser>();
            DtoMappings<User, CreateUser>();
            DtoMappings<User, UpdateUser>();
            DtoMappings<User, CreateUserResponse>();
            DtoMappings<User, GetUserResponse>();

        }


        /// <summary>
        /// Ids the mappings.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        public void IdMappings<E>()
            where E : Entity<E>
           
        {
            AutoMapper.Mapper.CreateMap<E, Guid>()
                .ConvertUsing<EntityToIdConverter<E>>();
            AutoMapper.Mapper.CreateMap<Guid, E>()//.AfterMap((p, e) => ScanObjectForIEnumerables<E>(e))
                .ConvertUsing<IdToEntityConverter<E>>();
        }
        /// <summary>
        /// Dtoes the mappings.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="D"></typeparam>
        public void DtoMappings<E, D>()
            where E : Entity<E>, new()
            where D : DtoBase
        {

            AutoMapper.Mapper.CreateMap<E, D>();
            AutoMapper.Mapper.CreateMap<D, E>()
                .ConstructUsing(p => LoadEntityFromDto<E>(((DtoBase)p.SourceValue)));

        }

        //private E ScanObjectForIEnumerables<E>(E e)
        //    where E : Entity<E>
        //{
        //    if (e.Id != Guid.Empty)
        //    {
        //        var session = Illisian.Nhibernate.Database.Context.GetSession();
        //        var t = typeof(E);
        //        Dictionary<string, Array> data = new Dictionary<string, Array>();
        //        foreach (var p in t.GetProperties())
        //        {
        //            if (p.PropertyType.IsEnumerableType())
        //            {
        //                var value = t.GetProperty(p.Name).GetValue(e, null) as Array;
        //                data.Add(p.Name, value);
        //            }
        //        }
        //        e.Refresh(session);
        //        foreach (var key in data.Keys)
        //        {
        //            foreach(var v in 
        //        }


        //    }
        //    return e;

        //}



        /// <summary>
        /// Loads the entity from dto.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="db">The db.</param>
        /// <returns>``0.</returns>
        private E LoadEntityFromDto<E>(DtoBase db) where E : Entity<E>, new()
        {
            if (db == null)
            {
                return null;
            }
            if (db.Id == Guid.Empty)
            {
                return new E();
            }

            var session = Illisian.Nhibernate.Database.Context.GetSession();
            return NHLogicAbstract<E>.LoadBy(session, p => p.Id == db.Id);
        }

        /// <summary>
        /// Class EntityToIdConverter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class EntityToIdConverter<T> : ITypeConverter<Entity<T>, Guid> where T : Entity<T>
        {
            /// <summary>
            /// Converts the specified context.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>Guid.</returns>
            public Guid Convert(ResolutionContext context)
            {
                if (context.SourceValue == null)
                {
                    return Guid.Empty;
                }

                return ((T)context.SourceValue).Id;
            }

        }

        /// <summary>
        /// Class IdToEntityConverter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class IdToEntityConverter<T> : ITypeConverter<Guid, T> where T : Entity<T>
        {
            /// <summary>
            /// Converts the specified context.
            /// </summary>
            /// <param name="context">The context.</param>
            /// <returns>`0.</returns>
            public T Convert(ResolutionContext context)
            {

                var id = (Guid)context.SourceValue;
                if (id == Guid.Empty)
                {
                    return null;
                }
                T returnVal = null;
                var session = Illisian.Nhibernate.Database.Context.GetSession();
                returnVal = LogicAbstract<T>.LoadBy(session, p => p.Id == id);
                return returnVal;

            }

        }
    }
}