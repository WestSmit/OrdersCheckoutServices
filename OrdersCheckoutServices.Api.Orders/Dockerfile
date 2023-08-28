#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OrdersCheckoutServices.Api.Orders/OrdersCheckoutServices.Api.Orders.csproj", "OrdersCheckoutServices.Api.Orders/"]
COPY ["OrdersCheckoutServices.Shared/OrdersCheckoutServices.Shared.csproj", "OrdersCheckoutServices.Shared/"]
RUN dotnet restore "OrdersCheckoutServices.Api.Orders/OrdersCheckoutServices.Api.Orders.csproj"
COPY . .
WORKDIR "/src/OrdersCheckoutServices.Api.Orders"
RUN dotnet build "OrdersCheckoutServices.Api.Orders.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersCheckoutServices.Api.Orders.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersCheckoutServices.Api.Orders.dll"]