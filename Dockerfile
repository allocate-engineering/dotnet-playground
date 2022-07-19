# FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS base
WORKDIR /app
EXPOSE 5015

ENV ASPNETCORE_URLS=http://+:5015

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["UserApi.Web.csproj", "./"]
RUN dotnet restore "UserApi.Web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "UserApi.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserApi.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN dotnet dev-certs https
ENTRYPOINT ["dotnet", "UserApi.Web.dll"]