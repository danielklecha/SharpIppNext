using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Models.Requests;
/// <summary>
/// Purge-Jobs operation attributes.
/// <br/>
/// Deprecated/Obsolete Support: The library intentionally implements operations and attributes that the latest standards have deprecated or obsoleted for the sake of backward compatibility, such as the Purge-Jobs operation (Deprecated in RFC 8011).
/// See: RFC 2911 Section 3.2.9
/// </summary>
[Obsolete("See RFC 8011 Section 4.2.9.")]
public class PurgeJobsOperationAttributes : PausePrinterOperationAttributes
{

}
