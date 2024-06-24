window.onload = function() {
    Imprimir();
};

function Imprimir() {
    setTimeout(function() {
        window.print();
    }, 100);
    console.log("TESTING PRINTIN")
}