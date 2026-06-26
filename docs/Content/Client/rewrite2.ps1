$ops = @(
    @{ Name = "Activate-Printer"; Type = "ActivatePrinter"; IsJob = $false },
    @{ Name = "Cancel-Current-Job"; Type = "CancelCurrentJob"; IsJob = $true },
    @{ Name = "Create-Job-Subscriptions"; Type = "CreateJobSubscriptions"; IsJob = $true },
    @{ Name = "Create-Printer-Subscriptions"; Type = "CreatePrinterSubscriptions"; IsJob = $false },
    @{ Name = "Deactivate-Printer"; Type = "DeactivatePrinter"; IsJob = $false },
    @{ Name = "Disable-Printer"; Type = "DisablePrinter"; IsJob = $false },
    @{ Name = "Enable-Printer"; Type = "EnablePrinter"; IsJob = $false },
    @{ Name = "Get-Printer-Supported-Values"; Type = "GetPrinterSupportedValues"; IsJob = $false },
    @{ Name = "Hold-New-Jobs"; Type = "HoldNewJobs"; IsJob = $false },
    @{ Name = "Pause-Printer-After-Current-Job"; Type = "PausePrinterAfterCurrentJob"; IsJob = $false },
    @{ Name = "Promote-Job"; Type = "PromoteJob"; IsJob = $true },
    @{ Name = "Release-Held-New-Jobs"; Type = "ReleaseHeldNewJobs"; IsJob = $false },
    @{ Name = "Restart-Printer"; Type = "RestartPrinter"; IsJob = $false },
    @{ Name = "Resume-Job"; Type = "ResumeJob"; IsJob = $true },
    @{ Name = "Schedule-Job-After"; Type = "ScheduleJobAfter"; IsJob = $true; Extra = $true },
    @{ Name = "Shutdown-Printer"; Type = "ShutdownPrinter"; IsJob = $false },
    @{ Name = "Startup-Printer"; Type = "StartupPrinter"; IsJob = $false },
    @{ Name = "Suspend-Current-Job"; Type = "SuspendCurrentJob"; IsJob = $true }
)

foreach ($op in $ops) {
    $opType = $op.Type
    $opName = $op.Name
    $isJob = $op.IsJob
    $extra = $op.Extra
    $path = "c:\Projects\GitHub\SharpIppNext\docs\Content\Client\$opType.md"

    $jobLine = ""
    if ($isJob) {
        $jobLine = ",`r`n        JobId = 123"
    }

    $extraLine = ""
    if ($extra) {
        $extraLine = ",`r`n        PredecessorJobId = 456"
    }

    $content = @"
# $opName Example

Here is a basic example of how to initialize a `${opType}Request` and send it using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new ${opType}Request
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")$jobLine$extraLine
    }
};

var response = await client.${opType}Async(request);
```
"@

    Set-Content -Path $path -Value $content -Encoding UTF8
}

Write-Host "Done rewriting 18 examples"
