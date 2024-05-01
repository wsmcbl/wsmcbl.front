
    document.getElementById("amoun").addEventListener("input", (event)=>{

        var montoTotal = document.getElementById("modalAmount").innerText;

        var pagaCon = event.target.value;

        var cambio = document.getElementById("chamge");
        cambio.value = (pagaCon - montoTotal);
    })

    function openPrint(url, target) {
        window.open(url, target);
    }