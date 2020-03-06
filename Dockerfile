FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src

COPY ["MetalsPrices.Abstraction.MetalPricesDataProviders/MetalsPrices.Abstraction.MetalPricesDataProviders.csproj", "MetalsPrices.Abstraction.MetalPricesDataProviders/"]
COPY ["MetalsPrices.Abstraction.MeralPricesServices/MetalsPrices.Abstraction.MeralPricesServices.csproj", "MetalsPrices.Abstraction.MeralPricesServices/"]
COPY ["MetalsPrices.Model/MetalsPrices.Model.csproj", "MetalsPrices.Model/"]
COPY ["MetalsPrices.ExternalApi.Services/MetalsPrices.ExternalApi.Services.csproj", "MetalsPrices.ExternalApi.Services/"]
COPY ["MetalsPrices.Api/MetalsPrices.Api.csproj", "MetalsPrices.Api/"]
RUN dotnet restore "MetalsPrices.Api/MetalsPrices.Api.csproj"
COPY . .
WORKDIR "/src/MetalsPrices.Api"
RUN dotnet build "MetalsPrices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MetalsPrices.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MetalsPrices.Api.dll"]
#