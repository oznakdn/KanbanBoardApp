let hubConnection;

hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/commenthub")
    .configureLogging(signalR.LogLevel.Information)
    .build();






