# HCI.Core.Helper
![](https://img.shields.io/nuget/v/HCI.Core.Helper.svg)

## Features
HCI.Core.Helper is a [NuGet library](https://www.nuget.org/api/v2/package/HCI.Core.Helper) that you can automatically register all services by following the name pattern by assembly.

The following platforms are supported:
- .NET Core

## Getting started
The easiest way to get started is by [installing the available NuGet packages](https://www.nuget.org/packages/HCI.Core.Helper) and if you're not a NuGet fan then follow these steps:

Download the latest runtime library from: https://www.nuget.org/api/v2/package/HCI.Core.Helper;
 Or install the latest package:
PM> **Install-Package HCI.Core.Helper**

## A Quick Example

### Services Register
Automatically register all services by following the name pattern by assembly.

1. In ```Startup.cs``` enter the code  according to line 4 at the end of the ```ConfigureServices``` method;
1. Use the ```ServicesRegister``` class to trigger the desired method;
1. Enter the service container;
1. Enter the assembly of a project implementation target of the dependency injection;
1. Enter the suffix pattern for the services to be registered;
1. Optionally, enter a suffix pattern for the used contract.

> Add services as scope
```csharp
public void ConfigureServices(IServiceCollection services)
{
	// TODO code here.
	ServicesRegister.AddServicesScope(services, typeof(FooService).Assembly, "Service");
}
```

------------

> Add services as singleton
```csharp
public void ConfigureServices(IServiceCollection services)
{
	// TODO code here.
	ServicesRegister.AddServicesSingleton(services, typeof(FooService).Assembly, "Service");
}
```

------------

> Add services as transient
```csharp
public void AddServicesTransient(IServiceCollection services)
{
	// TODO code here.
	ServicesRegister.AddServicesScope(services, typeof(FooService).Assembly, "Service");
}
```

## Report Support
To report errors, questions and suggestions go to the [link](https://www.nuget.org/packages/HCI.Core.Helper/1.0.0/ReportMyPackage)
