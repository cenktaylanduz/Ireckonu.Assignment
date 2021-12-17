#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Ireckonu.Assignment.Api/Ireckonu.Assignment.Api.csproj", "Ireckonu.Assignment.Api/"]
RUN dotnet restore "Ireckonu.Assignment.Api/Ireckonu.Assignment.Api.csproj"
COPY . .
WORKDIR "/src/Ireckonu.Assignment.Api"
RUN dotnet build "Ireckonu.Assignment.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ireckonu.Assignment.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ireckonu.Assignment.Api.dll"]