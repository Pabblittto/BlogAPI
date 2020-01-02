FROM mcr.microsoft.com/dotnet/core/aspnet:latest
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR ./app

COPY *.csproj ./

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

ENTRYPOINT [ "dotnet","./out/BlogAPI.dll" ]