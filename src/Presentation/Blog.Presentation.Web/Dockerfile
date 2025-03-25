FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/Blog.Presentation.Web/Blog.Presentation.Web.csproj", "src/Presentation/Blog.Presentation.Web/"]
RUN dotnet restore "src/Presentation/Blog.Presentation.Web/Blog.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/src/Presentation/Blog.Presentation.Web"
RUN dotnet build "Blog.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Blog.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blog.Presentation.Web.dll"]
