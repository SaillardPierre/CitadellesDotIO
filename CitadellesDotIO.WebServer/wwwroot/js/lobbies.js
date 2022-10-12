const connection = new signalR.HubConnectionBuilder()
    .withUrl("/lobbieshub")    
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Définition du corportement
connection.on("PullLobbies", (lobbies) => {
    console.log(lobbies);
    $("#lobbiesDiv").empty();
    lobbies.forEach((lobby) => {
        $("#lobbiesDiv").append("<p>" + lobby.name + "<p>");
    });
});

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

connection.onclose(async () => {
    await start();
});

// Start the connection.
start().then(async () => {
    getLobbies();
    $("#createLobbyButton").click(() => {
        const newLobby = {
            Name: "NewRandomLobbyName"
        };
        connection.invoke("CreateLobby", newLobby);
    });
});

function getLobbies() {
    connection.invoke("GetLobbies");
}
