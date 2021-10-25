#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["EcommerceProject.API/EcommerceProject.API.csproj", "EcommerceProject.API/"]
COPY ["EcommerceProject.Service/EcommerceProject.Service.csproj", "EcommerceProject.Service/"]
COPY ["EcommerceProject.Core/EcommerceProject.Core.csproj", "EcommerceProject.Core/"]
COPY ["EcommerceProject.Data/EcommerceProject.Data.csproj", "EcommerceProject.Data/"]
RUN dotnet restore "EcommerceProject.API/EcommerceProject.API.csproj"
COPY . .
WORKDIR "/src/EcommerceProject.API"
RUN dotnet build "EcommerceProject.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcommerceProject.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceProject.API.dll"]