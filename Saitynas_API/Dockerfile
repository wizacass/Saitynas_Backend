# NuGet restore
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY BackendTests/*.csproj BackendTests/
COPY Backend/*.csproj Backend/
RUN dotnet restore
COPY . .

# testing
FROM build AS testing
WORKDIR /src/Backend
RUN dotnet build
WORKDIR /src/BackendTests
RUN dotnet test

# publish
FROM build AS publish
WORKDIR /src/Backend
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Backend.dll
