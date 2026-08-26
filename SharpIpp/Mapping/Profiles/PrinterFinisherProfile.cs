using System;
using System.Text;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class PrinterFinisherProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<string, PrinterFinisher>((src, _) => PrinterFinisher.Parse(src));
        mapper.CreateIppMap<PrinterFinisher, string>((src, _) => src.ToString());
        mapper.CreateIppMap<byte[], PrinterFinisher>((src, map) => map.Map<PrinterFinisher>(Encoding.UTF8.GetString(src)));
        mapper.CreateIppMap<PrinterFinisher, byte[]>((src, map) => Encoding.UTF8.GetBytes(map.Map<string>(src)));
        mapper.CreateIppMap<OctetString, PrinterFinisher>((src, map) => map.Map<PrinterFinisher>(src.Value));
        mapper.CreateIppMap<PrinterFinisher, OctetString>((src, map) => new OctetString(map.Map<byte[]>(src)));
    }
}
