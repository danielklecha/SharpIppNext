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
            dst.JobMandatoryAttributes = map.MapFromDicSetNullable<string[]?>(src, JobAttribute.JobMandatoryAttributes);
            dst.ResourceIds = map.MapFromDicSetNullable<int[]?>(src, SystemAttribute.ResourceIds);
            if (src.TryGetValue(JobAttribute.ClientInfo, out var clientInfo) && clientInfo.GroupBegCollection().Any())
                dst.ClientInfo = clientInfo.GroupBegCollection().Select(x => map.Map<ClientInfo>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(JobAttribute.DocumentFormatDetails, out var documentFormatDetails) && documentFormatDetails.GroupBegCollection().Any())
                dst.DocumentFormatDetails = map.Map<DocumentFormatDetails>(documentFormatDetails.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            dst.JobName = map.MapFromDicNullable<string?>(src, JobAttribute.JobName);
            dst.JobMediaSheets = map.MapFromDicNullable<int?>(src, JobAttribute.JobMediaSheets);
            dst.JobKOctets = map.MapFromDicNullable<int?>(src, JobAttribute.JobKOctets);
            dst.IppAttributeFidelity = map.MapFromDicNullable<bool?>(src, JobAttribute.IppAttributeFidelity);
            dst.JobImpressions = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressions);
            dst.JobPassword = map.MapFromDicNullable<OctetString?>(src, JobAttribute.JobPassword);
            dst.JobPasswordEncryption = map.MapFromDicNullable<JobPasswordEncryption?>(src, JobAttribute.JobPasswordEncryption);
            dst.JobReleaseAction = map.MapFromDicNullable<JobReleaseAction?>(src, JobAttribute.JobReleaseAction);
            dst.JobAuthorizationUri = map.MapFromDicNullable<Uri?>(src, JobAttribute.JobAuthorizationUri);
            dst.JobImpressionsEstimated = map.MapFromDicNullable<int?>(src, JobAttribute.JobImpressionsEstimated);
            dst.ChargeInfoMessage = map.MapFromDicNullable<string?>(src, JobAttribute.ChargeInfoMessage);
            dst.ProofCopies = map.MapFromDicNullable<int?>(src, JobAttribute.ProofCopies);
            if (src.TryGetValue(JobAttribute.ProofPrint, out var proofPrint))
                dst.ProofPrint = map.Map<ProofPrint>(proofPrint.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(JobAttribute.JobStorage, out var jobStorage))
                dst.JobStorage = map.Map<JobStorage>(jobStorage.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(JobAttribute.CoverSheetInfo, out var coverSheetInfo))
                dst.CoverSheetInfo = map.Map<CoverSheetInfo>(coverSheetInfo.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            if (src.TryGetValue(JobAttribute.DestinationUris, out var destinationUris))
                dst.DestinationUris = destinationUris.GroupBegCollection().Select(x => map.Map<DestinationUri>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(JobAttribute.DestinationAccesses, out var destinationAccesses))
                dst.DestinationAccesses = destinationAccesses.GroupBegCollection().Select(x => map.Map<DocumentAccess>(x.FromBegCollection().ToIppDictionary())).ToArray();
            if (src.TryGetValue(JobAttribute.OutputAttributes, out var outputAttributes))
                dst.OutputAttributes = map.Map<OutputAttributes>(outputAttributes.GroupBegCollection().First().FromBegCollection().ToIppDictionary());
            return dst;
        });

        mapper.CreateMap<CreateJobOperationAttributes, List<IppAttribute>>((src, dst, map) =>
        {
            dst ??= new List<IppAttribute>();
            map.Map<OperationAttributes, List<IppAttribute>>(src, dst);
            if (src.JobMandatoryAttributes != null)
                dst.AddRange(src.JobMandatoryAttributes.Select(x => new IppAttribute(Tag.Keyword, JobAttribute.JobMandatoryAttributes, x)));
            if (src.ResourceIds != null)
                dst.AddRange(src.ResourceIds.Select(x => new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, x)));
            if (src.DocumentFormatDetails != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.DocumentFormatDetails).ToBegCollection(JobAttribute.DocumentFormatDetails));
            if (src.JobName != null)
                dst.Add(new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.JobName, src.JobName));
            if (src.IppAttributeFidelity.HasValue)
                dst.Add(new IppAttribute(Tag.Boolean, JobAttribute.IppAttributeFidelity, src.IppAttributeFidelity.Value));
            if (src.JobImpressions.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobImpressions, src.JobImpressions.Value));
            if (src.JobMediaSheets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobMediaSheets, src.JobMediaSheets.Value));
            if (src.JobKOctets.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobKOctets, src.JobKOctets.Value));
            if (src.JobPassword != null)
                dst.Add(new IppAttribute(Tag.OctetStringWithAnUnspecifiedFormat, JobAttribute.JobPassword, src.JobPassword.Value));
            if (src.JobPasswordEncryption != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobPasswordEncryption, map.Map<string>(src.JobPasswordEncryption.Value)));
            if (src.JobReleaseAction != null)
                dst.Add(new IppAttribute(Tag.Keyword, JobAttribute.JobReleaseAction, map.Map<string>(src.JobReleaseAction.Value)));
            if (src.JobAuthorizationUri != null)
                dst.Add(new IppAttribute(Tag.Uri, JobAttribute.JobAuthorizationUri, src.JobAuthorizationUri.ToString()));
            if (src.JobImpressionsEstimated.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.JobImpressionsEstimated, src.JobImpressionsEstimated.Value));
            if (src.ChargeInfoMessage != null)
                dst.Add(new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.ChargeInfoMessage, src.ChargeInfoMessage));
            if (src.ProofCopies.HasValue)
                dst.Add(new IppAttribute(Tag.Integer, JobAttribute.ProofCopies, src.ProofCopies.Value));
            if (src.ProofPrint != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.ProofPrint).ToBegCollection(JobAttribute.ProofPrint));
            if (src.JobStorage != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.JobStorage).ToBegCollection(JobAttribute.JobStorage));
            if (src.CoverSheetInfo != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.CoverSheetInfo).ToBegCollection(JobAttribute.CoverSheetInfo));
            if (src.DestinationUris != null)
                dst.AddRange(src.DestinationUris.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.DestinationUris)));
            if (src.DestinationAccesses != null)
                dst.AddRange(src.DestinationAccesses.SelectMany(x => map.Map<IEnumerable<IppAttribute>>(x).ToBegCollection(JobAttribute.DestinationAccesses)));
            if (src.OutputAttributes != null)
                dst.AddRange(map.Map<IEnumerable<IppAttribute>>(src.OutputAttributes).ToBegCollection(JobAttribute.OutputAttributes));
            return dst;
        });
    }
}
