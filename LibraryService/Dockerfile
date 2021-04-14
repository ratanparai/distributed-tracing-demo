#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["LibraryService/LibraryService.csproj", "LibraryService/"]
RUN dotnet restore "LibraryService/LibraryService.csproj"
COPY . .
WORKDIR "/src/LibraryService"
RUN dotnet build "LibraryService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LibraryService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryService.dll"]
