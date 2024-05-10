# Plantilla arquitectura hexagonal web api

[jairgrcTemplate2.Utility.Templates](https://www.nuget.org/packages/jairgrcTemplate2.Utility.Templates/1.2.3)


## Install the template

```
dotnet new install jairgrcTemplate2.Utility.Templates::1.2.1
dotnet new JGRCV2 -o <tagPlantilla>
```
### Project structure

```
└── 📁Plantilla
    └── Generador.rar
    └── LICENSE
    └── README.md
    └── TemplateBase.sln
    └── 📁TemplateBaseMicroservice.Api
        └── appsettings.Development.json
        └── appsettings.json
        └── Program.cs
        └── 📁Properties
            └── launchSettings.json
        └── Startup.cs
        └── TemplateBaseMicroservice.Api.csproj
        └── TemplateBaseMicroservice.Api.csproj.user
        └── TemplateBaseMicroservice.Api.http
    └── 📁TemplateBaseMicroservice.Domain
    
    └── 📁TemplateBaseMicroservice.Entities
        └── 📁Enums
        └── 📁Filter
        └── 📁Model
        └── 📁Request
            └── BaseRequest.cs
        └── 📁Response
            └── BaseResponse.cs
        └── TemplateBaseMicroservice.Entities.csproj
    └── 📁TemplateBaseMicroservice.Exceptions
        └── CustomException.cs
        └── TemplateBaseMicroservice.Exceptions.csproj
    └── 📁TemplateBaseMicroservice.Infraestructure
        └── BaseRepository.cs
        └── ConnectionFactory.cs
        └── TemplateBaseMicroservice.Infraestructure.csproj
    └── 📁TemplateBaseMicroservice.Repository
        └── IConnectionFactory.cs
        └── IGenericRepository.cs
        └── TemplateBaseMicroservice.Repository.csproj
    └── 📁TemplateBaseMicroservice.Service
        └── TemplateBaseMicroservice.Service.csproj
    └── 📁Util
        └── 📁AutoMapper
            └── AutoMapper.cs
        └── TrackerConfig.cs
        └── Util.csproj
```
# Package Versions Used

## Core Packages

### AutoMapper
- Version: 13.0.1

### AutoMapper.Extensions.Microsoft.DependencyInjection
- Version: 12.0.0

### Microsoft.AspNetCore.OpenApi
- Version: 8.0.4

### Microsoft.Data.SqlClient
- Version: 5.2.0

### Microsoft.VisualStudio.Web.CodeGeneration.Design
- Version: 8.0.2

### Swashbuckle.AspNetCore
- Version: 6.5.0

## Other Packages

### Dapper
- Version: 2.1.35

### Microsoft.Extensions.Configuration
- Version: 8.0.0

### System.Composition
- Version: 8.0.0

## Additional Information

### Target Framework
- Version: net8.0

### Implicit Usings
- Configuration: enable

### Nullable Reference Types
- Configuration: enable
