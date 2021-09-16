# NuGet restore
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY Saitynas_API_Tests/*.csproj Saitynas_API_Tests/
COPY Saitynas_API/*.csproj Saitynas_API/
RUN dotnet restore
COPY . .

# testing
FROM build AS testing
WORKDIR /src/Saitynas_API
RUN dotnet build
WORKDIR /src/Saitynas_API_Tests
RUN dotnet test

# publish
FROM build AS publish
WORKDIR /src/Saitynas_API
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet Saitynas_API.dll
