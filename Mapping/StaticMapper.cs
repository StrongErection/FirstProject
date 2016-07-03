using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace Mapping
{
    public static class StaticMapper
    {
        public static ICollection<TDest> MapCollections<TDest, TSrc>(ICollection<TSrc> src)
            where TDest : class
            where TSrc : class
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TSrc, TDest>());
            ICollection<TDest> destCollection = new List<TDest>();
            foreach (TSrc srcElement in src)
            {
                destCollection.Add(Mapper.DynamicMap<TSrc, TDest>(srcElement));
                Mapper.AssertConfigurationIsValid();
            }
            Mapper.AssertConfigurationIsValid();
            Mapper.Reset();
            return destCollection;
        }

        public static TDest Map<TDest, TSrc>(TSrc src)
            where TDest : class
            where TSrc : class
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TSrc, TDest>());
            TDest dest = Mapper.DynamicMap<TSrc, TDest>(src);
            Mapper.AssertConfigurationIsValid();
            Mapper.Reset();
            return dest;
        }
    }
}
