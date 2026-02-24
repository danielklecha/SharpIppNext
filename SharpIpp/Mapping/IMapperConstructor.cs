#nullable disable
using System;

namespace SharpIpp.Mapping
{
    public interface IMapperConstructor
    {
        void CreateMap<TSource, TDest>(Func<TSource, IMapperApplier, TDest> mapFunc);
        void CreateMap<TSource, TDest>(Func<TSource, TDest, IMapperApplier, TDest> mapFunc);
        void CreateMap(Type sourceType, Type destType, Func<object, IMapperApplier, object> mapFunc);
        void CreateMap(Type sourceType, Type destType, Func<object, object, IMapperApplier, object> mapFunc);
    }
}
