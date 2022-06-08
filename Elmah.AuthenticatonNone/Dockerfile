#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Elmah.AuthenticatonNone/Elmah.AuthenticatonNone.csproj", "Elmah.AuthenticatonNone/"]
RUN dotnet restore "Elmah.AuthenticatonNone/Elmah.AuthenticatonNone.csproj"
COPY . .
WORKDIR "/src/Elmah.AuthenticatonNone"
RUN dotnet build "Elmah.AuthenticatonNone.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elmah.AuthenticatonNone.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elmah.AuthenticatonNone.dll"]