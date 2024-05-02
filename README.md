# KANBAN BOARD
<img src="https://github.com/oznakdn/KanbanBoardApp/blob/master/docs/Board.png"/>

# Local
```
git clone https://github.com/oznakdn/KanbanBoardApp.git
cd KanbanBoard.WebMvc
dotnet restore
dotnet run
```

## appsettings.json 
```csharp
 "ConnectionStrings": {
   "SqliteConnection": "Data Source = it should be enter own directory path for connection string"
 }
```

## Database migration
```
cd KanbanBoard.Infrastructure
```

```csharp
dotnet ef migrations add [migration name] --startup-project [KanbanBoard.WebMvc directory path]
dotnet ef migrations database update --startup-project [KanbanBoard.WebMvc directory path]
```


# Dockerizing

```
cd KanbanBoardApp
docker-compose up -d
```
