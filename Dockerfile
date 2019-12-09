FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 8005

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["survey/survey.csproj", "survey/"]
COPY ["survey.services/survey.services.csproj", "survey.services/"]
COPY ["survey.data/survey.data.csproj", "survey.data/"]
RUN dotnet restore "survey/survey.csproj"
COPY . .
WORKDIR "/src/survey"
RUN dotnet build "survey.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "survey.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "survey.dll"]