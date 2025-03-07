function downloadFile(fileName, content) {
    const a = document.createElement("a");
    a.href = content;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}
