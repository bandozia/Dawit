FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Gateway/Dawit.API/Dawit.API.csproj", "Gateway/Dawit.API/"]
RUN dotnet restore "Gateway/Dawit.API/Dawit.API.csproj"
COPY . .
WORKDIR "/src/Gateway/Dawit.API"
RUN dotnet build "Dawit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dawit.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dawit.API.dll"]
