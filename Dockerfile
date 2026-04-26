# See https://docs.microsoft.com/dotnet/core/docker/build-container for .NET container guidance
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["PipelinePractice/PipelinePractice.csproj", "PipelinePractice/"]
RUN dotnet restore "PipelinePractice/PipelinePractice.csproj"
COPY . .
WORKDIR "/src/PipelinePractice"
RUN dotnet build "PipelinePractice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PipelinePractice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PipelinePractice.dll"]
