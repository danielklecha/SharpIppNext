using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SharpIpp.Mapping.Profiles.Requests;

internal class CreateJobOperationAttributesProfile : IProfile
{
    public void CreateMaps(IMapperConstructor mapper)
    {
        mapper.CreateMap<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>((src, dst, map) =>
        {
            dst ??= new CreateJobOperationAttributes();
            map.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(src, dst);
            dst.JobMandatoryAttributes = map.MapFromDicSetNullable<string[]?>(src, IppAttributeNames.JobMandatoryAttributes);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, IppAttributeNames.ResourceIds);
            if (src.TryGetValue(IppAttributeNames.ClientInfo, out var clientInfo) && clientInfo.GroupBegCollection().Any())
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(IppAttributeNames.DocumentFormatDetails, out var documentFormatDetails) && documentFormatDetails.GroupBegCollection().Any())
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.JobName = map.MapFromDicNullable<string?>(src, IppAttributeNames.JobName);
            dst.JobMediaSheets = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobMediaSheets);
            dst.JobKOctets = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobKOctets);
            dst.IppAttributeFidelity = map.MapFromDicNullable<bool?>(src, IppAttributeNames.IppAttributeFidelity);
            dst.JobImpressions = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobImpressions);
            dst.JobPassword = map.MapFromDicNullable<OctetString?>(src, IppAttributeNames.JobPassword);
            dst.JobPasswordEncryption = map.MapFromDicNullable<JobPasswordEncryption?>(src, IppAttributeNames.JobPasswordEncryption);
            dst.JobReleaseAction = map.MapFromDicNullable<JobReleaseAction?>(src, IppAttributeNames.JobReleaseAction);
            dst.JobAuthorizationUri = map.MapFromDicNullable<Uri?>(src, IppAttributeNames.JobAuthorizationUri);
            dst.JobImpressionsEstimated = map.MapFromDicNullable<int?>(src, IppAttributeNames.JobImpressionsEstimated);
            dst.ChargeInfoMessage = map.MapFromDicNullable<string?>(src, IppAttributeNames.ChargeInfoMessage);
            dst.ProofCopies = map.MapFromDicNullable<int?>(src, IppAttributeNames.ProofCopies);
            if (src.TryGetValue(IppAttributeNames.ProofPrint, out var proofPrint))
                dst.ProofPrint = map.Map<ProofPrint>(proofPrint.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(IppAttributeNames.JobStorage, out var jobStorage))
                dst.JobStorage = map.Map<JobStorage>(jobStorage.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(IppAttributeNames.CoverSheetInfo, out var coverSheetInfo))
                dst.CoverSheetInfo = map.Map<CoverSheetInfo>(coverSheetInfo.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(IppAttributeNames.DestinationUris, out var destinationUris))
                dst.DestinationUris = destinationUris.GroupBegCollection().Select(x => map.Map<DestinationUri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(IppAttributeNames.DestinationAccesses, out var destinationAccesses))
                dst.DestinationAccesses = destinationAccesses.GroupBegCollection().Select(x => map.Map<DocumentAccess>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(IppAttributeNames.OutputAttributes, out var outputAttributes))
                dst.OutputAttributes = map.Map<OutputAttributes>(outputAttributes.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<CreateJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobMandatoryAttributes != null)
                dst.AddRange(src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, x)));
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, x)));
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(IppAttributeNames.DocumentFormatDetails));
            if (src.JobName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.JobName, src.JobName));
            if (src.IppAttributeFidelity.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, IppAttributeNames.IppAttributeFidelity, src.IppAttributeFidelity.Value));
            if (src.JobImpressions.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobImpressions, src.JobImpressions.Value));
            if (src.JobMediaSheets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobMediaSheets, src.JobMediaSheets.Value));
            if (src.JobKOctets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobKOctets, src.JobKOctets.Value));
            if (src.JobPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, IppAttributeNames.JobPassword, src.JobPassword.Value));
            if (src.JobPasswordEncryption != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobPasswordEncryption, map.Map<string>(src.JobPasswordEncryption.Value)));
            if (src.JobReleaseAction != null)
                dst.Add(new IppAttribute(Tag.Keyword, IppAttributeNames.JobReleaseAction, map.Map<string>(src.JobReleaseAction.Value)));
            if (src.JobAuthorizationUri != null)
                dst.Add(new IppAttribute(Tag.Uri, IppAttributeNames.JobAuthorizationUri, src.JobAuthorizationUri.ToString()));
            if (src.JobImpressionsEstimated.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.JobImpressionsEstimated, src.JobImpressionsEstimated.Value));
            if (src.ChargeInfoMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.ChargeInfoMessage, src.ChargeInfoMessage));
            if (src.ProofCopies.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, IppAttributeNames.ProofCopies, src.ProofCopies.Value));
            if (src.ProofPrint != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.ProofPrint).ToBegCollection(IppAttributeNames.ProofPrint));
            if (src.JobStorage != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobStorage).ToBegCollection(IppAttributeNames.JobStorage));
            if (src.CoverSheetInfo != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverSheetInfo).ToBegCollection(IppAttributeNames.CoverSheetInfo));
            if (src.DestinationUris != null)
                dst.AddRange(src.DestinationUris.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.DestinationUris)));
            if (src.DestinationAccesses != null)
                dst.AddRange(src.DestinationAccesses.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(IppAttributeNames.DestinationAccesses)));
            if (src.OutputAttributes != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.OutputAttributes).ToBegCollection(IppAttributeNames.OutputAttributes));
            return dst;
        });
    }
}
