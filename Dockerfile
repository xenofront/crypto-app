FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["CryptoApi.csproj", "./"]
RUN dotnet restore "CryptoApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CryptoApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptoApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoApi.dll"]
