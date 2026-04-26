FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

RUN groupadd -r appgroup && useradd -r -g appgroup appuser

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["PipelinePractice/PipelinePractice.csproj", "PipelinePractice/"]
COPY ["PipelinePractice.Models/PipelinePractice.Models.csproj", "PipelinePractice.Models/"]
COPY ["PipelinePractice.Services/PipelinePractice.Services.csproj", "PipelinePractice.Services/"]

RUN dotnet restore "PipelinePractice/PipelinePractice.csproj"

COPY . .
WORKDIR "/src/PipelinePractice"
RUN dotnet build "PipelinePractice.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "PipelinePractice.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --chown=appuser:appgroup --from=publish /app/publish .

USER appuser

ENTRYPOINT ["dotnet", "PipelinePractice.dll"]
