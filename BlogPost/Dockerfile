FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlogPost/BlogPost.csproj", "BlogPost/"]
COPY ["BlogPost.Persistence/BlogPost.Persistence.csproj", "BlogPost.Persistence/"]
COPY ["BlogPost.Application/BlogPost.Application.csproj", "BlogPost.Application/"]
COPY ["BlogPost.Domain/BlogPost.Domain.csproj", "BlogPost.Domain/"]
RUN dotnet restore "BlogPost/BlogPost.csproj"
COPY . .
WORKDIR "/src/BlogPost"
RUN dotnet build "BlogPost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlogPost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogPost.dll"]
