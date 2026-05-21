FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["PRN232.LAB1.API/PRN232.LAB1.API.csproj", "PRN232.LAB1.API/"]
COPY ["PRN232.LAB1.Services/PRN232.LAB1.Services.csproj", "PRN232.LAB1.Services/"]
COPY ["PRN232.LAB1.Repositories/PRN232.LAB1.Repositories.csproj", "PRN232.LAB1.Repositories/"]
RUN dotnet restore "PRN232.LAB1.API/PRN232.LAB1.API.csproj"
COPY . .
WORKDIR "/src/PRN232.LAB1.API"
RUN dotnet build "PRN232.LAB1.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PRN232.LAB1.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT ["dotnet", "PRN232.LAB1.API.dll"]
