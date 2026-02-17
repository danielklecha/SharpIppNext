#nullable disable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Mapping
{
    public class SimpleMapper : IMapper
    {
        private readonly ConcurrentDictionary<(Type src, Type dst), Func<object, object, SimpleMapper, object>> _dictionary =
            new ConcurrentDictionary<(Type src, Type dst), Func<object, object, SimpleMapper, object>>();

        private readonly ConcurrentDictionary<(Type src, Type dst), List<((Type src, Type dst) map, MapType type)>> _pairsCache =
            new ConcurrentDictionary<(Type src, Type dst), List<((Type src, Type dst) map, MapType type)>>();

        public void CreateMap<TSource, TDest>(Func<TSource, IMapperApplier, TDest> mapFunc)
        {
            CreateMap(typeof(TSource), typeof(TDest), (src, mapper) => mapFunc((TSource)src, mapper));
        }

        public void CreateMap<TSource, TDest>(Func<TSource, TDest, IMapperApplier, TDest> mapFunc)
        {
            CreateMap(typeof(TSource),
                typeof(TDest),
                (src, dst, mapper) => mapFunc((TSource)src, (TDest)dst, mapper));
        }

        public void CreateMap(Type sourceType, Type destType, Func<object, IMapperApplier, object> mapFunc)
        {
            CreateMap(sourceType, destType, (src, dst, mapper) => mapFunc(src, mapper));
        }

        public void CreateMap(Type sourceType, Type destType, Func<object, object, IMapperApplier, object> mapFunc)
        {
            var key = (sourceType, destType);
            _dictionary[key] = (src, dst, mapper) => mapFunc(src, dst, mapper);
        }

        public TDest Map<TDest>(object source)
        {
            return Map<TDest>(source, default);
        }

        public TDest Map<TDest>(object source, TDest dest)
        {
            if (source == null)
            {
                return dest;
            }

            return Map(source, source.GetType(), dest);
        }

        public TDest Map<TSource, TDest>(TSource source)
        {
            return Map<TSource, TDest>(source, default);
        }

        public TDest Map<TSource, TDest>(TSource source, TDest dest)
        {
            if (source == null)
            {
                return dest;
            }

            return Map(source, typeof(TSource), dest);
        }

        private TDest Map<TDest>(object source, Type sourceType, TDest dest)
        {
            var destType = typeof(TDest);

            if (sourceType == destType)
            {
                return (TDest)source;
            }
            
            var pairs = _pairsCache.GetOrAdd((sourceType, destType), key => PossiblePairs(key.src, key.dst).ToList());

            foreach (var (map, type) in pairs)
            {
                switch (type)
                {
                    case MapType.Cast: return (TDest)source;
                    case MapType.Simple:
                        if (_dictionary.TryGetValue(map, out var mapFunc))
                        {
                            return (TDest)mapFunc(source, dest, this);
                        }
                        break;
                }
            }

            throw new ArgumentException($"No mapping found for types {sourceType.FullName} -> {destType.FullName}. Source: {source}");
        }

        private static IEnumerable<((Type src, Type dst) map, MapType type)> PossiblePairs(Type sourceType, Type destType)
        {
            var underlying = Nullable.GetUnderlyingType(destType);

            if (underlying != null && underlying == sourceType)
            {
                yield return ((sourceType, destType), MapType.Cast);
            }

            yield return ((sourceType, destType), MapType.Simple);

            foreach (var ifc in sourceType.GetInterfaces())
            {
                yield return ((ifc, destType), MapType.Simple);
            }
        }

        private enum MapType
        {
            Simple,
            Cast,
        }
    }
}
