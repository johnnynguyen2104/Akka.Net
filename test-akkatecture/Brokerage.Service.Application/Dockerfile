FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Brokerage.Service.Application/Brokerage.Service.Application.csproj", "Brokerage.Service.Application/"]
COPY ["Brokerage.Service.Infrastructure/Brokerage.Service.Infrastructure.csproj", "Brokerage.Service.Infrastructure/"]
COPY ["Brokerage.Service.Akka.Extensions/Brokerage.Service.Akka.Extensions.csproj", "Brokerage.Service.Akka.Extensions/"]
COPY ["Brokerage.Service.Domain/Brokerage.Service.Domain.csproj", "Brokerage.Service.Domain/"]
RUN dotnet restore "Brokerage.Service.Application/Brokerage.Service.Application.csproj"
COPY . .
WORKDIR "/src/Brokerage.Service.Application"
RUN dotnet build "Brokerage.Service.Application.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Brokerage.Service.Application.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Brokerage.Service.Application.dll"]
