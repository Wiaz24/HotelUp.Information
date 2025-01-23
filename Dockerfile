FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/HotelUp.Information.API/HotelUp.Information.API.csproj", "Src/HotelUp.Information.API/"]
COPY ["Src/HotelUp.Information.Services/HotelUp.Information.Services.csproj", "Src/HotelUp.Information.Services/"]
COPY ["Src/HotelUp.Information.Persistence/HotelUp.Information.Persistence.csproj", "Src/HotelUp.Information.Persistence/"]
COPY ["Shared/HotelUp.Information.Shared/HotelUp.Information.Shared.csproj", "Shared/HotelUp.Information.Shared/"]
RUN dotnet restore "Src/HotelUp.Information.API/HotelUp.Information.API.csproj"
COPY . .
WORKDIR "/src/Src/HotelUp.Information.API"
RUN dotnet build "HotelUp.Information.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HotelUp.Information.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelUp.Information.API.dll"]
