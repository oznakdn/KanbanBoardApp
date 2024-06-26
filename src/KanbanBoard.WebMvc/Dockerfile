#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/KanbanBoard.WebMvc/KanbanBoard.WebMvc.csproj", "src/KanbanBoard.WebMvc/"]
COPY ["src/KanbanBoard.Application/KanbanBoard.Application.csproj", "src/KanbanBoard.Application/"]
COPY ["src/KanbanBoard.Infrastructure/KanbanBoard.Infrastructure.csproj", "src/KanbanBoard.Infrastructure/"]
COPY ["src/KanbanBoard.Core/KanbanBoard.Core.csproj", "src/KanbanBoard.Core/"]
RUN dotnet restore "./src/KanbanBoard.WebMvc/KanbanBoard.WebMvc.csproj"
COPY . .
WORKDIR "/src/src/KanbanBoard.WebMvc"
RUN dotnet build "./KanbanBoard.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KanbanBoard.WebMvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KanbanBoard.WebMvc.dll"]