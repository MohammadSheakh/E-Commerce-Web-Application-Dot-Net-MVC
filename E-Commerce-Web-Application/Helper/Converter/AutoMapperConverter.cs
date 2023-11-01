using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce_Web_Application.Helper.Converter
{
    public class AutoMapperConverter
    {
        public AutoMapperConverter() { }

        // 🔰🔗 i dont know constructor er moddhe kono kaj korte hobe kina 
        //for List
        public List<TDestination> ConvertForList<TSource, TDestination>(List<TSource> sourceListFromDB)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<TDestination>>(sourceListFromDB);
        }

        //for Single Instance
        public TDestination ConvertForSingleInstance<TSource, TDestination>(TSource sourceFromDB)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<TDestination>(sourceFromDB);
        }

        //for Array
        public TDestination[] ConvertForMap<TSource, TDestination>(TSource[] sourceFromDB)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<TDestination[]>(sourceFromDB);
        }
    }
}