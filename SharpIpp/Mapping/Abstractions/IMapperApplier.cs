#nullable enable
namespace SharpIpp.Mapping
{
    public interface IMapperApplier
    {
        TDest Map<TDest>(object? source);

        TDest Map<TDest>(object? source, TDest dest);

        TDest Map<TSource, TDest>(TSource? source);

        TDest Map<TSource, TDest>(TSource? source, TDest dest);

        TDest? MapNullable<TDest>(object? source);

        TDest? MapNullable<TDest>(object? source, TDest? dest);

        TDest? MapNullable<TSource, TDest>(TSource? source);

        TDest? MapNullable<TSource, TDest>(TSource? source, TDest? dest);
    }
}
