# SharpGrip FileSystem [![NuGet](https://img.shields.io/nuget/v/SharpGrip.FileSystem)](https://www.nuget.org/packages/SharpGrip.FileSystem)

## Introduction
SharpGrip FileSystem is a file system abstraction supporting multiple adapters.

## Installation
Reference NuGet package `SharpGrip.FileSystem` (https://www.nuget.org/packages/SharpGrip.FileSystem).

For adapters other than the local file system (included in the `SharpGrip.FileSystem` package) please see the [Supported adapters](#supported-adapters) section.

## Usage

### Instantiation
```
var adapters = new List<IAdapter>
{
    new LocalAdapter("adapterPrefix", "adapterRootPath")
};

// Instantiation option 1.
var fileSystem = new FileSystem(adapters);

// Instantiation option 2.
var fileSystem = new FileSystem();
fileSystem.Adapters = adapters;
```

### Local adapter
```
var adapters = new List<IAdapter>
{
    new LocalAdapter("local1", "/var/files"),
    new LocalAdapter("local2", "D:\\Files")
};

var fileSystem = new FileSystem(adapters);
```

### AzureFileStorage adapter
```
// Azure connection.
var azureClient = new ShareClient("connectionString", "shareName");

var adapters = new List<IAdapter>
{
    new LocalAdapter("local", "/var/files"),
    new AzureFileStorageAdapter("azure", "/Files", azureClient)
};

var fileSystem = new FileSystem(adapters);
```

### Dropbox adapter
```
// Dropbox connection.
var dropboxClient = new DropboxClient("oAuth2AccessToken");

var adapters = new List<IAdapter>
{
    new LocalAdapter("local", "/var/files"),
    new DropboxAdapter("dropbox", "/Files", dropboxClient)
};

var fileSystem = new FileSystem(adapters);
```

### SFTP adapter
```
// SFTP connection.
var privateKeyFile = new PrivateKeyFile("/home/userName/.ssh/id_rsa");
var privateKeyAuthenticationMethod = new PrivateKeyAuthenticationMethod("userName", privateKeyFile);
var sftpConnectionInfo = new ConnectionInfo("hostName", "userName", privateKeyAuthenticationMethod);
var sftpClient = new SftpClient(sftpConnectionInfo);

var adapters = new List<IAdapter>
{
    new LocalAdapter("local", "/var/files"),
    new SftpAdapter("sftp", "/var/files", sftpClient)
};

var fileSystem = new FileSystem(adapters);
```

### Example operations
```
// Azure connection.
var azureClient = new ShareClient("connectionString", "shareName");

// Dropbox connection.
var dropboxClient = new DropboxClient("oAuth2AccessToken");

var adapters = new List<IAdapter>
{
    new LocalAdapter("local", "/var/files"),
    new AzureFileStorageAdapter("azure", "/Files", azureClient),
    new DropboxAdapter("dropbox", "/Files", dropboxClient)
};

// Copies a file from the `local1` adapter to the `local2` adapter.
await fileSystem.CopyFileAsync("local1://foo/bar.txt", "local2://bar/foo.txt");

// Moves a file from the `azure` adapter to the `dropbox` adapter.
await fileSystem.MoveFileAsync("azure://Foo/Bar.txt", "dropbox://Bar/Foo.txt");

// Writes string contents to the `azure` adapter.
await fileSystem.WriteFileAsync("azure://Foo.txt", "Bar!");

// Read a text file from the `dropbox` adapter.
var contents = fileSystem.ReadTextFileAsync("dropbox://Foo.txt");
```

## Supported operations
For a full list of the supported operations please see the [IFileSystem](../master/FileSystem/src/IFileSystem.cs) interface.

## Supported adapters
- Local (included in the `SharpGrip.FileSystem` package) [![NuGet](https://img.shields.io/nuget/v/SharpGrip.FileSystem)](https://www.nuget.org/packages/SharpGrip.FileSystem)
- AzureFileStorage (`SharpGrip.FileSystem.Adapters.AzureFileStorage`) [![NuGet](https://img.shields.io/nuget/v/SharpGrip.FileSystem.Adapters.AzureFileStorage)](https://www.nuget.org/packages/SharpGrip.FileSystem.Adapters.AzureFileStorage)
- Dropbox (`SharpGrip.FileSystem.Adapters.Dropbox`) [![NuGet](https://img.shields.io/nuget/v/SharpGrip.FileSystem.Adapters.Dropbox)](https://www.nuget.org/packages/SharpGrip.FileSystem.Adapters.Dropbox)
- SFTP (`SharpGrip.FileSystem.Adapters.Sftp`) [![NuGet](https://img.shields.io/nuget/v/SharpGrip.FileSystem.Adapters.Sftp)](https://www.nuget.org/packages/SharpGrip.FileSystem.Adapters.Sftp)