// Formato de moneda nicaragüense
function formatNicaraguanCurrency(value) {
    return new Intl.NumberFormat('es-NI', {
        style: 'currency',
        currency: 'NIO',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    }).format(value);
}

window.RevenueChart = function(data) {
    // Opciones comunes para ambos gráficos
    const commonOptions = {
        responsive: true,
        maintainAspectRatio: false,
        layout: {
            padding: {
                top: 10,
                bottom: 10,
                left: 10,
                right: 10
            }
        }
    };
    
    // Gráfico de Barras
    var barCtx = document.getElementById('incomeBarChart');
    new Chart(barCtx, {
        type: 'bar',
        data: {
            labels: data.barLabels,
            datasets: [
                {
                    label: 'Esperado C$',
                    data: [data.expected],
                    backgroundColor: '#4e73df',
                    borderColor: '#4e73df'
                },
                {
                    label: 'Recibido (Esperado)',
                    data: [data.receivedExpected],
                    backgroundColor: '#1cc88a',
                    borderColor: '#1cc88a'
                },
                {
                    label: 'Recibido (Total)',
                    data: [data.additional],
                    backgroundColor: '#36b9cc',
                    borderColor: '#36b9cc'
                }
            ]
        },
        options: {
            commonOptions,
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        callback: function(value) {
                            return formatNicaraguanCurrency(value);
                        }
                    }
                }
            },
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            return context.dataset.label + ': ' +
                                formatNicaraguanCurrency(context.raw);
                        }
                    }
                }
            }
        }
    });

    // Gráfico Circular
    var pieCtx = document.getElementById('incomePieChart');
    new Chart(pieCtx, {
        type: 'doughnut',
        data: {
            labels: ['Recibido (Esperado)', 'Faltante', 'Recibido (Total)'],
            datasets: [{
                data: [data.pieData.receivedExpected, data.pieData.pending, data.pieData.additional],
                backgroundColor: ['#1cc88a', '#e74a3b', '#36b9cc'],
                borderWidth: 1
            }]
        },
        options: {
            commonOptions,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            const total = context.dataset.data.reduce((a, b) => a + b, 0);
                            const percentage = Math.round((context.raw / total) * 100);
                            return `${context.label}: ${formatNicaraguanCurrency(context.raw)} (${percentage}%)`;
                        }
                    }
                },
                legend: {
                    display: false
                }
            },
            cutout: '70%'
        }
    });
};