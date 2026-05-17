using System.Text;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class PrinterAlertProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<string, PrinterAlert>((src, _) => PrinterAlert.Parse(src, PrinterAlertParseOptions.Relaxed));
        mapper.CreateIppMap<byte[], PrinterAlert>((src, map) => map.Map<PrinterAlert>(Encoding.UTF8.GetString(src)));
        mapper.CreateIppMap<PrinterAlert, string>((src, _) => PrinterAlert.Serialize(src));
        mapper.CreateIppMap<PrinterAlert, byte[]>((src, map) => Encoding.UTF8.GetBytes(map.Map<string>(src)));
        mapper.CreateIppMap<OctetString, PrinterAlert>((src, map) => map.Map<PrinterAlert>(src.Value));
        mapper.CreateIppMap<PrinterAlert, OctetString>((src, map) => new OctetString(map.Map<byte[]>(src)));
    }
}
