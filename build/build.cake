//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var mygetFeed = Argument("mygetFeed", "");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var absoluteRootDir = MakeAbsolute(Directory("../"));
var rootDir = Directory(absoluteRootDir.FullPath);
var artifactsDir = rootDir + Directory("artifacts");
var buildDir = rootDir + Directory("build");
var toolsDir = artifactsDir + Directory("tools");
var buildToolsDir = buildDir + Directory("tools");
var srcDir = rootDir + Directory("src");
var nugetFile = buildToolsDir + File("nuget.exe");
var xunitRunnerFile = toolsDir + Directory("xunit.runner.console") + Directory("tools") + File("xunit.console.exe");
var gitVersionFile = toolsDir + Directory("GitVersion.CommandLine") + Directory("tools") + File("GitVersion.exe");
var gitLinkFile = toolsDir + Directory("GitLink") + Directory("lib") + Directory("net45") + File("GitLink.exe");
var outputBinariesDir = artifactsDir + Directory("bin");
var outputPackagesDir = artifactsDir + Directory("packages");
var packagesDir = rootDir + Directory("packages/");
var solutionFile = rootDir + File("MAF.FeaturesFlipping.sln");
var unitTestsProjectFile = rootDir + Directory("tests") + Directory("MAF.FeaturesFlipping.UnitTests") + File("MAF.FeaturesFlipping.UnitTests.csproj");
var versionAssemblyFile = srcDir + Directory("CommonFiles") + File("VersionAssemblyInfo.cs");

var nugetPackagePublicationFeed = "https://api.nuget.org/v3/index.json";
var informationalVersion = "0.0.0";
var sha1Hash = "";
var publishPackage = true;
var packagePublishingApiKeyName = "NUGET_API_KEY";
var isBuildingPR = HasEnvironmentVariable("APPVEYOR_PULL_REQUEST_NUMBER");
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Setup(context => {
    context.Tools.RegisterFile(nugetFile);
});

Task("Clean")
    .Does(() =>
{
    CleanDirectory(outputBinariesDir);
});

Task("Install-Tools-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var defaultInstallSettings = new NuGetInstallSettings {
        OutputDirectory = toolsDir,
        ExcludeVersion = true,
        ToolPath = nugetFile
    };
    NuGetInstall("GitVersion.CommandLine", defaultInstallSettings);
    var gitLinkInstallSettings =  new NuGetInstallSettings {
        OutputDirectory = toolsDir,
        ExcludeVersion = true,
        Version = "2.4.0",
        ToolPath = nugetFile
    };
NuGetInstall("gitlink", gitLinkInstallSettings);
});

public string GetBranchName(GitVersion gitVersion)
{
	var branchName = EnvironmentVariable("APPVEYOR_REPO_BRANCH") ?? gitVersion.BranchName ?? "<Not Set>";
	return branchName;
}

public string GetPreReleaseNumber(string branchName, GitVersion gitVersion)
{
	if(branchName != gitVersion.BranchName)
	{
		return "1";
	}
	return gitVersion.PreReleaseNumber.ToString();
}

Task("Prepare-Build")
    .IsDependentOn("Install-Tools-Packages")
    .Does(() =>
{
	var assemblyInfoSettings = new AssemblyInfoSettings {
        Version = informationalVersion,
        FileVersion = informationalVersion,
        InformationalVersion = informationalVersion
    };
	if(!isBuildingPR)
    {
		var gitVersionSettings = new GitVersionSettings
		{
			ToolPath = gitVersionFile
		};
		var gitVersion = GitVersion(gitVersionSettings);
		var subVersion = "";
		var version = gitVersion.MajorMinorPatch;
		var branchName = GetBranchName(gitVersion);
		var buildNumber = GetPreReleaseNumber(branchName, gitVersion);
		sha1Hash = gitVersion.Sha;
		if (branchName == "dev")
		{
			subVersion = "-alpha" + buildNumber;
			nugetPackagePublicationFeed = mygetFeed;
			packagePublishingApiKeyName = "MYGET_API_KEY";
			publishPackage = !string.IsNullOrEmpty(nugetPackagePublicationFeed);
		}
		else if (branchName.StartsWith("release-"))
		{
			subVersion = "-beta" + buildNumber;
		}
		else if (branchName != "master")
		{
			publishPackage = false;
		}
		informationalVersion = version + subVersion;
		assemblyInfoSettings = new AssemblyInfoSettings {
			Version = version,
			FileVersion = version,
			InformationalVersion = informationalVersion
		};
	}
	else
	{
		publishPackage = false;
	}
	publishPackage &= HasEnvironmentVariable(packagePublishingApiKeyName);
	CreateAssemblyInfo(versionAssemblyFile, assemblyInfoSettings);
});

Task("Build")
    .IsDependentOn("Prepare-Build")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
         Configuration = configuration,
         OutputDirectory = outputBinariesDir
    };

    DotNetCoreBuild(solutionFile, settings);
});
Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {
         Configuration = configuration
    };

    DotNetCoreTest(unitTestsProjectFile, settings);
});
Task("Run-Tests")
    .IsDependentOn("Run-Unit-Tests")
	;

Task("Create-Packages")
    .IsDependentOn("Build")//Run-Tests")
    .WithCriteria(() => !isBuildingPR)
    .Does(() =>
{
    var gitLinkSettings = new GitLinkSettings {
        PdbDirectoryPath = outputBinariesDir,
        ToolPath = gitLinkFile,
        ShaHash = sha1Hash
    };
    GitLink(rootDir, gitLinkSettings);
	var settings = new DotNetCorePackSettings
     {
         Configuration = configuration,
         OutputDirectory = outputPackagesDir
     };
	 var projectFiles = GetFiles(MakeAbsolute(srcDir) + "/**/*.csproj");
	 foreach(var projectFile in projectFiles)
	 {
	 	 DotNetCorePack(projectFile.ToString(), settings);   
	 }
     //DotNetCorePack(MakeAbsolute(srcDir) + "/MAF.FeaturesFlipping.Abstractions/MAF.FeaturesFlipping.Abstractions.csproj", settings);   
});

Task("Publish-Packages")
    .IsDependentOn("Create-Packages")
    .WithCriteria(() => publishPackage)
    .Does(() =>
{
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("Create-Packages")
    ;

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);
