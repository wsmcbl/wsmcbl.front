window.renderincomePieChart = function(data) {
    const pieCtx = document.getElementById('incomePieChart');
    if (!pieCtx) return;

    // Destruir gráfico anterior si existe
    if (pieCtx.chart) {
        pieCtx.chart.destroy();
    }

    // Función de formato mejorada
    const formatCurrency = (value) => {
        value = Number(value);
        if (isNaN(value)) return 'C$0.00';
        return 'C$' + value.toFixed(2).replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    };

    // Calcular porcentajes
    const total = Number(data.expectedIncomeReceived) + Number(data.other);
    const receivedPercentage = ((data.expectedIncomeReceived / total) * 100).toFixed(1);
    const otherPercentage = ((data.other / total) * 100).toFixed(1);

    // Configuración del gráfico
    const pieConfig = {
        type: 'doughnut',
        data: {
            labels: [ // Etiquetas con porcentajes
                `Ingresos recíbidos  (${receivedPercentage}%)`,
                `Otros Ingresos (${otherPercentage}%)`
            ],
            datasets: [{
                data: [data.expectedIncomeReceived, data.other],
                backgroundColor: ['#f6c23e', '#36b9cc'],
                borderColor: ['#f4b619', '#2c9faf'],
                borderWidth: 2
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            tooltips: {
                callbacks: {
                    label: function(tooltipItem, data) {
                        // Solo muestra el valor formateado
                        const value = data.datasets[0].data[tooltipItem.index];
                        return formatCurrency(value);
                    }
                }
            },
            legend: {
                display: true,
                position: 'top', // Leyenda arriba
                labels: {
                    usePointStyle: true,
                    padding: 20,
                    fontSize: 13,
                    generateLabels: function(chart) {
                        return chart.data.labels.map(function(label, index) {
                            return {
                                text: label, // Mantiene los porcentajes en la leyenda
                                fillStyle: chart.data.datasets[0].backgroundColor[index],
                                hidden: false,
                                index: index
                            };
                        });
                    }
                }
            },
            cutout: '70%'
        }
    };

    // Crear nuevo gráfico
    pieCtx.chart = new Chart(pieCtx, pieConfig);
};