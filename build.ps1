Param(
    [string]$Script = "build/build.cake",
    [string]$Target = "Default",
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    [ValidateSet("Quiet", "Minimal", "Normal", "Verbose", "Diagnostic")]
    [string]$Verbosity = "Verbose",
    [switch]$UseNetCoreVersion
)

$BUILD_DIR = Join-Path $PSScriptRoot "build"
$ARTIFACTS_DIR = Join-Path $PSScriptRoot "artifacts"
$TOOLS_DIR = Join-Path $ARTIFACTS_DIR "tools"
$BUILD_TOOLS_DIR = Join-Path $BUILD_DIR "tools"
$NUGET_EXE = Join-Path $BUILD_TOOLS_DIR "nuget.exe"


if($UseNetCoreVersion){
    $CAKE = Join-Path $TOOLS_DIR "Cake.CoreCLR/Cake.dll"
    $CAKE_NAME = "Cake.CoreCLR"
    $CAKE_EXECUTION = "dotnet $CAKE"
} else {
    $CAKE = Join-Path $TOOLS_DIR "Cake/Cake.exe"
    $CAKE_NAME = "Cake"
    $CAKE_EXECUTION = "$CAKE"
}
if(!(Test-Path $TOOLS_DIR)){
    Write-Host "Create tools directory"
    New-Item -ItemType Directory $TOOLS_DIR
}
if(!(Test-Path $BUILD_TOOLS_DIR)){
    Write-Host "Create build\tools directory"
    New-Item -ItemType Directory $BUILD_TOOLS_DIR
}

# Try download NuGet.exe if do not exist.
if (!(Test-Path $NUGET_EXE)) {
    Write-Host "Download Nuget.exe"
    Invoke-WebRequest -Uri https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile $NUGET_EXE
}

# Make sure NuGet exists where we expect it.
if (!(Test-Path $NUGET_EXE)) {
    Throw "Could not find NuGet.exe"
}

# Restore tools from NuGet.
Push-Location
Set-Location $TOOLS_DIR
Invoke-Expression "$NUGET_EXE install $CAKE_NAME -ExcludeVersion"
Pop-Location
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

# Make sure that Cake has been installed.
if (!(Test-Path $CAKE)) {
    Throw "Could not find $CAKE_NAME ($CAKE)"
}

# Start Cake
$CakeInvokeExpression = "$CAKE_EXECUTION `"$Script`" -target=`"$Target`" -configuration=`"$Configuration`" -verbosity=`"$Verbosity`""
Invoke-Expression $CakeInvokeExpression 
Write-Host
exit $LASTEXITCODE