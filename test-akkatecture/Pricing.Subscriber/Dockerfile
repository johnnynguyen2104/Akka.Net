FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY /Pricing.Subscriber ./Pricing.Subscriber

RUN dotnet restore
COPY . .

WORKDIR /src/Pricing.Subscriber
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

CMD ["dotnet", "Pricing.Subscriber.dll"]
