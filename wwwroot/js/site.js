// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showMessage(messageType, message) {
    var messageContainer = document.getElementById("message-container");

    if (messageContainer) {
        // Create a message element
        var messageElement = document.createElement("div");
        messageElement.className = "message " + messageType;
        messageElement.innerText = message;

        // Append the message element to the message container
        messageContainer.appendChild(messageElement);

        // Display the message container
        messageContainer.style.display = "flex";

        // Automatically remove the message after 5 seconds
        setTimeout(function () {
            messageElement.remove();

            // Hide the message container if it's empty
            if (messageContainer.childElementCount === 0) {
                messageContainer.style.display = "none";
            }
        }, 2000); // Show for 5 seconds
    }
}