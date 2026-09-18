using SharpIpp.Mapping;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Enable-Printer Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse]
public class EnablePrinterResponse : IppResponse<OperationAttributes> { }
