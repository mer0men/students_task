FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RogueLIKE/RogueLIKE.csproj", "RogueLIKE/"]
RUN dotnet restore "RogueLIKE/RogueLIKE.csproj"
COPY . .
WORKDIR "/src/RogueLIKE"
RUN dotnet build "RogueLIKE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RogueLIKE.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RogueLIKE.dll"]
