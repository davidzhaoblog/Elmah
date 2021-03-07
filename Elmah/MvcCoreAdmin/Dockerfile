#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1433

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Elmah/MvcCoreAdmin/Elmah.MvcCoreAdmin.csproj", "Elmah/MvcCoreAdmin/"]
COPY ["Elmah/UIWebCore/AspNetMvcCoreApiController/Elmah.AspNetMvcCoreApiController.csproj", "Elmah/UIWebCore/AspNetMvcCoreApiController/"]
COPY ["Elmah/UIWebCore/AspNetMvcCoreViewModel/Elmah.AspNetMvcCoreViewModel.csproj", "Elmah/UIWebCore/AspNetMvcCoreViewModel/"]
COPY ["Frameworks/Framework/Framework.csproj", "Frameworks/Framework/"]
COPY ["Frameworks/Framework.Resx/Framework.Resx.csproj", "Frameworks/Framework.Resx/"]
COPY ["Elmah/DAL/DALContracts/Elmah.DALContracts.csproj", "Elmah/DAL/DALContracts/"]
COPY ["Elmah/Contracts/EntityContracts/Elmah.EntityContracts.csproj", "Elmah/Contracts/EntityContracts/"]
COPY ["Elmah/Contracts/Resx/Elmah.Resx.csproj", "Elmah/Contracts/Resx/"]
COPY ["Elmah/Contracts/DataSourceEntities/Elmah.DataSourceEntities.csproj", "Elmah/Contracts/DataSourceEntities/"]
COPY ["Frameworks/FrameworkCore.Services/FrameworkCore.Services.csproj", "Frameworks/FrameworkCore.Services/"]
COPY ["Elmah/ServicesCore/CoreCommonBLL/Elmah.CoreCommonBLL.csproj", "Elmah/ServicesCore/CoreCommonBLL/"]
COPY ["Elmah/Services/WcfContracts/Elmah.WcfContracts.csproj", "Elmah/Services/WcfContracts/"]
COPY ["Elmah/Contracts/CommonBLLEntities/Elmah.CommonBLLEntities.csproj", "Elmah/Contracts/CommonBLLEntities/"]
COPY ["Elmah/Contracts/ViewModelData/Elmah.ViewModelData.csproj", "Elmah/Contracts/ViewModelData/"]
COPY ["Elmah/DALCore/EFCoreContext/Elmah.EFCoreContext.csproj", "Elmah/DALCore/EFCoreContext/"]
COPY ["Elmah/DALCore/EFCoreDAL/Elmah.EFCoreDAL.csproj", "Elmah/DALCore/EFCoreDAL/"]
RUN dotnet restore "Elmah/MvcCoreAdmin/Elmah.MvcCoreAdmin.csproj"
COPY . .
WORKDIR "/src/Elmah/MvcCoreAdmin"
RUN dotnet build "Elmah.MvcCoreAdmin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elmah.MvcCoreAdmin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elmah.MvcCoreAdmin.dll"]
