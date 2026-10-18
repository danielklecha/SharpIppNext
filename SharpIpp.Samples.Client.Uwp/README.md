# SharpIpp.Samples.Client.Uwp

This sample project is designed to test and benchmark the **.NET Native** toolchain backend compiler (`nutc_driver.exe`) with `SharpIpp`.

## Background

When compiling a UWP application referencing `SharpIpp` in **Release** mode with `.NET Native` (`UseDotNetNativeToolchain=true`), the backend optimizer (`nutc_driver.exe`) compiles and specializes all referenced types, generic instances (e.g. `IppValue<T>`), and source-generated mappers.

In version 5.0.0, whole-program optimization in `nutc_driver.exe` peaked at **~10.2 GB (x64 Release)** and **~13.2 GB (ARM64 Release)**. The optimizations introduced in this repository reduce this memory footprint by de-duplicating non-generic helper routines, splitting generated mapper registries into batches, and chunking large model mappers (e.g., `PrinterDescriptionAttributes`).

## Prerequisites

To build this project with .NET Native, the machine must have:
1. **Visual Studio 2019 / 2022 / 2026** with the **Universal Windows Platform development** workload installed.
2. **Windows 10 SDK (10.0.17763.0 or later)**.
3. At least **16 GB RAM** recommended for Release builds.

## How to Test and Measure Memory

### Option 1: Automated Profiling Script (Recommended)

Run the included PowerShell script from this directory:

```powershell
# Profile x64 Release build (default)
.\Test-NutcDriverMemory.ps1 -Platform x64

# Profile ARM64 Release build
.\Test-NutcDriverMemory.ps1 -Platform ARM64
```

The script will:
1. Locate Visual Studio MSBuild via `vswhere`.
2. Restore NuGet packages (`Microsoft.NETCore.UniversalWindowsPlatform`).
3. Start the Release build with `UseDotNetNativeToolchain=true` and `Use64BitCompiler=true`.
4. Continuously sample `nutc_driver.exe` process memory every 150 ms.
5. Print a summary of peak WorkingSet and PagedMemory in GB.

### Option 2: Manual MSBuild Command

```powershell
# 1. Restore packages
msbuild SharpIpp.Samples.Client.Uwp.csproj -t:Restore /p:Platform=x64 /p:Configuration=Release

# 2. Build Release with .NET Native and 64-bit compiler
msbuild SharpIpp.Samples.Client.Uwp.csproj /p:Configuration=Release /p:Platform=x64 /p:UseDotNetNativeToolchain=true /p:Use64BitCompiler=true /v:normal
```

To monitor `nutc_driver.exe` memory manually in another PowerShell window while the build is running:

```powershell
while ($true) {
    Get-Process nutc_driver -ErrorAction SilentlyContinue |
        Select-Object ProcessName, Id, @{N='WorkingSet (GB)'; E={[Math]::Round($_.WorkingSet64 / 1GB, 2)}} |
        Format-Table
    Start-Sleep -Seconds 1
}
```

## Key Configuration Directives

- **`Properties/Default.rd.xml`**: Specifies `<Assembly Name="SharpIpp" Dynamic="Required All" />` to force full instantiation and optimization of SharpIpp's types during .NET Native compilation.
- **`<Use64BitCompiler>true</Use64BitCompiler>`**: Uses the 64-bit compiler host to prevent 32-bit address space exhaustion.

## Latest Benchmark Results

- **Build Result:** SUCCESS
- **Peak WorkingSet (`nutc_driver.exe`):** 1.28 GB (1310 MB)
- **Peak Paged Memory:** 1.28 GB (1314 MB)
- **Total Duration:** 394.7 seconds (~6.5 minutes)

For full architectural details, see the [.NET Native & UWP Compilation Guide](../docs/Content/Client/DotNetNative.md).
