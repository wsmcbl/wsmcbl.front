
document.getElementById("amoun").addEventListener("input", (event) => {
    // Obtener los valores de los inputs
    var montoTotal = parseFloat(document.getElementById("modalAmount").value) || 0;
    var pagaCon = parseFloat(event.target.value) || 0;

    // Calcular el cambio
    var cambio = pagaCon - montoTotal;

    // Asignar el cambio al input correspondiente
    document.getElementById("chamge").value = cambio.toFixed(2);
});

    function openPrint(url, target) {
        window.open(url, target);
    }

    window.showModal = function(modalId) {
        $('#' + modalId).modal('show');
    };


    