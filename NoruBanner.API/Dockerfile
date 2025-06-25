FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["NoruBanner.API.csproj", "./"]
RUN dotnet restore "./NoruBanner.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NoruBanner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NoruBanner.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NoruBanner.API.dll"]