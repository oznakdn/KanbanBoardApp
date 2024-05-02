# KANBAN BOARD

#### This project is a kanban board application created with the Clean architecture structure. In this application created with Asp.Net Core Mvc, JQuery and Ajax were used for the drag and drop feature. You can easily follow up by creating a new project with the application and uploading the stories, tasks and bugs of the project to the project board. ####
<hr>

<img src="https://github.com/oznakdn/KanbanBoardApp/blob/master/docs/Board.png"/>

<a href="https://github.com/oznakdn/KanbanBoardApp/tree/master/docs">Other pages :point_left:</a>




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
   "PostgresConnection": "Server=127.0.0.1;Port=5432;Database=KanbanBoardDB;User Id=[your username];Password=[your password];"
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
