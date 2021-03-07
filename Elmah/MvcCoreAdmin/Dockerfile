#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Elmah/MvcCoreAdmin/Elmah.MvcCoreAdmin.csproj", "Elmah/Elmah.MvcCoreAdmin/"]
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