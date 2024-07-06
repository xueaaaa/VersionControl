VersionControl is designed to monitor users' app versions, and update them if a new one is released on GitHub. 

## To begin with
Make sure you have published your app on GitHub, its repository is publicly accessible, and new versions of the app are released using GitHub Releases, with the mandatory version indication in the Release Tag.

**Please note that VersionControl only works with 0.0.0.0 format versions. If you are using a different application versioning system, this package will not work for you!**

The screenshot below shows how a properly designed release should look like for VersionControl to work correctly.

![](https://i.imgur.com/XHNtWWA.png)

## Install
You can install VersionControl directly into your project using [NuGet](https://www.nuget.org/packages/xueaaaa.VersionControl/).
```
dotnet add package xueaaaa.VersionControl
```

## Usage

**Before using the library, be sure to initialize the parameters required for operation.**
```csharp
using VersionControl;

Parameters.Set(repoName: "VersionControl",
               repoOwner: "xueaaaa",
               updateFileName: "upd.zip");
```

To get the version of the app installed on the user's device, use this:
```csharp
using VersionControl.Models.Versions;

var local = Version.Local;
```

To get the latest version of the app on GitHub you just need to use static method Create(). You cannot get a non-latest version from GitHub.
```csharp
using VersionControl.Models.Versions;

var gitVersion = await GitVersion.Create();
```

To install the latest version, use the VersionInstaller class, for example, as shown in the following code snippet. It is recommended to check if it is really required before installing.
```csharp
using VersionControl.Models.Installers;
using VersionControl.Models.Versions;

var local = Version.Local;
var gitVersion = await GitVersion.Create();
var installer = new VersionInstaller(local, gitVersion);

if (installer.Check())
  installer.Install();
// OR
if (gitVersion > local)
  installer.Install();
```

VersionControl only downloads the file, it does not install/unpack it.
