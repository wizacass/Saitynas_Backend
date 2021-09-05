FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Saitynas_API/Saitynas_API.csproj", "Saitynas_API/"]
RUN dotnet restore "Saitynas_API/Saitynas_API.csproj"
COPY . .
WORKDIR "/src/Saitynas_API"
RUN dotnet build "Saitynas_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Saitynas_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Saitynas_API.dll"]
