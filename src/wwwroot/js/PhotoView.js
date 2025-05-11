window.openFileDialog = function() {
    document.getElementById("fileInput").click();
}

let mediaStream = null; // Variable para almacenar el flujo de medios

function startCamera() {
    navigator.mediaDevices.getUserMedia({ video: true })
        .then(function(stream) {
            mediaStream = stream; // Guardar el flujo de medios
            const videoElement = document.getElementById("videoElement");
            videoElement.srcObject = stream;
            videoElement.style.display = "";
        })
        .catch(function(error) {
            console.error("Error al acceder a la cámara: ", error);
            alert("No se pudo acceder a la cámara.");
        });
}

function stopCamera() {
    if (mediaStream) {
        const tracks = mediaStream.getTracks();
        tracks.forEach(function(track) {
            track.stop(); // Detener cada pista de video
        });
        mediaStream = null; // Limpiar el flujo de medios

        // Ocultar el video
        const videoElement = document.getElementById("videoElement");
        videoElement.style.display = "none"; // Ocultar el video
    }
}

async function captureAndSendImage(dotNetRef) {
    try {
        const videoElement = document.querySelector("#videoElement");
        const canvas = document.createElement("canvas");
        canvas.width = 320; // Reducir resolución
        canvas.height = 240;
        const context = canvas.getContext("2d");
        context.drawImage(videoElement, 0, 0, canvas.width, canvas.height);

        const base64Image = canvas.toDataURL("image/jpeg", 0.8);

        const chunkSize = 1024 * 8;
        for (let i = 0; i < base64Image.length; i += chunkSize) {
            const chunk = base64Image.slice(i, i + chunkSize);
            await dotNetRef.invokeMethodAsync("ReceiveImageChunk", chunk);
        }

        await dotNetRef.invokeMethodAsync("ImageTransferComplete");

        // Detener la cámara después de capturar la imagen
        stopCamera(); // Aquí se llama a la función stopCamera
    } catch (error) {
        console.error("Error al capturar o enviar la imagen: ", error);
    }
}