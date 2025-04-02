// Check if SignalR connection is already established
if (!window.signalRConnection) {
    window.signalRConnection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

    // Start the connection once when the user logs in
    window.signalRConnection.start()
        .then(() => console.log("Connected to SignalR"))
        .catch(err => console.error("SignalR Connection Error:", err));
} else {
    // Ensure the connection is started if already initialized
    if (window.signalRConnection.state === "Disconnected") {
        window.signalRConnection.start()
            .then(() => console.log("Reconnected to SignalR"))
            .catch(err => console.error("SignalR Reconnection Error:", err));
    }
}