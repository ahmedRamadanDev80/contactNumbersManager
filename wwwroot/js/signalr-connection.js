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

// Function to send a message
function sendMessage() {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;

    if (user && message) {
        // Use window.signalRConnection instead of connection
        window.signalRConnection.invoke("SendMessage", user, message)
            .catch(err => console.error("Error sending message: ", err));
    } else {
        alert("Please enter both a name and a message.");
    }
}

// Listen for the ReceiveMessage event from the server
window.signalRConnection.on("ReceiveMessage", (user, message) => {
    const msg = document.createElement("div");
    msg.textContent = `${user}: ${message}`;
    document.getElementById("messagesList").appendChild(msg);
});