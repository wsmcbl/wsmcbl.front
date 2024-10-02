
document.getElementById("amoun").addEventListener("input", (event) => {
    var montoTotal = parseFloat(document.getElementById("modalAmount").value) || 0;
    var pagaCon = parseFloat(event.target.value) || 0;
    
    var cambio = pagaCon - montoTotal;
    
    if (cambio < 0)
    {
        document.getElementById("chamge").value = "Faltante";
    }
    else
    {
        document.getElementById("chamge").value = cambio.toFixed(2);
    }
});

    function openPrint(url, target) {
        window.open(url, target);
    }

    window.showModal = function(modalId) {
        $('#' + modalId).modal('show');
    };


    
