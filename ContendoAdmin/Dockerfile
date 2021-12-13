#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContendoAdmin/ContendoAdmin.csproj", "ContendoAdmin/"]
RUN dotnet restore "ContendoAdmin/ContendoAdmin.csproj"
COPY . .
WORKDIR "/src/ContendoAdmin"
RUN dotnet build "ContendoAdmin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContendoAdmin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContendoAdmin.dll"]