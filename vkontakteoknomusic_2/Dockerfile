#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["vkontakteoknomusic_2/vkontakteoknomusic_2.csproj", "vkontakteoknomusic_2/"]
RUN dotnet restore "vkontakteoknomusic_2/vkontakteoknomusic_2.csproj"
COPY . .
WORKDIR "/src/vkontakteoknomusic_2"
RUN dotnet build "vkontakteoknomusic_2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vkontakteoknomusic_2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vkontakteoknomusic_2.dll"]