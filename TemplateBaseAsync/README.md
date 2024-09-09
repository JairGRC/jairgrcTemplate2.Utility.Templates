# Plantilla arquitectura hexagonal web api

[jairgrcTemplate2.Utility.Templates](https://www.nuget.org/packages/jairgrcTemplate2.Utility.Templates/1.2.3)



## Install the template

```
dotnet new install jairgrcTemplate2.Utility.Templates::1.2.7
dotnet new JGRCV2 -o <tagPlantilla>
```

### Project structure
```
    └── TemplateBase.sln
    └── 📁TemplateBaseMicroservice.Api
        └── appsettings.Development.json
        └── appsettings.json
        └── 📁Controllers
            └── EjemploController.cs
        └── 📁Extensions
            └── ApplicationBuilderExtensions.cs
            └── ServiceCollectionExtensions.cs
        └── Program.cs
        └── Startup.cs
        └── Startup.cs
        └── TemplateBaseMicroservice.Api.csproj
        └── TemplateBaseMicroservice.Api.csproj.user
        └── EjemploDomain.cs
        └── TemplateBaseMicroservice.Domain.csproj
        └── EjemploDomain.cs
        └── TemplateBaseMicroservice.Domain.csproj
    └── 📁TemplateBaseMicroservice.Entities
        └── 📁Enums
            └── .empty
        └── 📁Filter
            └── .empty
            └── EjemploFilter.cs
            └── EjemploFilterType.cs
        └── 📁FilterValidator
            └── EjemploFilterValidator.cs
        └── 📁Model
            └── .empty
            └── EjemploEntity.cs
        └── 📁Request
            └── BaseRequest.cs
            └── EjemploRequest.cs
        └── 📁Response
            └── BaseResponse.cs
            └── EjemploResponse.cs
        └── TemplateBaseMicroservice.Entities.csproj
    └── 📁TemplateBaseMicroservice.Exceptions
        └── CustomException.cs
        └── EjemploHeaderException.cs
        └── FluentValidatorExceptions.cs
        └── TemplateBaseMicroservice.Exceptions.csproj
    └── 📁TemplateBaseMicroservice.Infraestructure
        └── BaseRepository.cs
        └── ConnectionFactory.cs
        └── EjemploRepository.cs
        └── TemplateBaseMicroservice.Infraestructure.csproj
    └── 📁TemplateBaseMicroservice.Repository
        └── IConnectionFactory.cs
        └── IEjemploRepository.cs
        └── IGenericRepository.cs
        └── TemplateBaseMicroservice.Repository.csproj
    └── 📁TemplateBaseTest
        └── EjemploDomainTest.cs
        └── TemplateBaseTest.csproj
    └── 📁TemplateBaseTest
        └── EjemploDomainTest.cs
        └── TemplateBaseTest.csproj
    └── 📁Util
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
