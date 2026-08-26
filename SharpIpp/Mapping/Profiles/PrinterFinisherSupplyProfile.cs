using System;
using System.Text;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping.Profiles;

// ReSharper disable once UnusedMember.Global
internal class PrinterFinisherSupplyProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateIppMap<string, PrinterFinisherSupply>((src, _) => PrinterFinisherSupply.Parse(src));
        mapper.CreateIppMap<PrinterFinisherSupply, string>((src, _) => src.ToString());
        mapper.CreateIppMap<byte[], PrinterFinisherSupply>((src, map) => map.Map<PrinterFinisherSupply>(Encoding.UTF8.GetString(src)));
        mapper.CreateIppMap<PrinterFinisherSupply, byte[]>((src, map) => Encoding.UTF8.GetBytes(map.Map<string>(src)));
        mapper.CreateIppMap<OctetString, PrinterFinisherSupply>((src, map) => map.Map<PrinterFinisherSupply>(src.Value));
        mapper.CreateIppMap<PrinterFinisherSupply, OctetString>((src, map) => new OctetString(map.Map<byte[]>(src)));
    }
}
