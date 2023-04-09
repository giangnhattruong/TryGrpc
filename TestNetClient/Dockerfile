FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TestNetClient/TestNetClient.csproj", "TestNetClient/"]
RUN dotnet restore "TestNetClient.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TestNetClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestNetClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestNetClient.dll"]
