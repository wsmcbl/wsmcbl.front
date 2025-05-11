function renderIncomeBarChart(detailsDto) {
    const ctx = document.getElementById('incomeBarChart');
    if (!ctx) {
        console.error("No se encontró el elemento canvas");
        return;
    }

    // Destruir gráfico anterior si existe
    if (ctx.chart) {
        ctx.chart.destroy();
    }

    // Calcular valores
    const allValues = [
        detailsDto.expectedIncomeThisMonth,
        detailsDto.expectedIncomeReceived,
        detailsDto.other,
        detailsDto.totalIncomeThisMonth
    ];
    const maxValue = Math.max(...allValues);

    // Definir chartData
    const chartData = {
        labels: ['Comparación de Ingresos'],
        datasets: [
            {
                label: 'Esperado',
                backgroundColor: '#4e73df',
                borderColor: '#2e59d9',
                borderWidth: 1,
                data: [detailsDto.expectedIncomeThisMonth]
            },
            {
                label: 'Recibido',
                backgroundColor: '#1cc88a',
                borderColor: '#17a673',
                borderWidth: 1,
                data: [detailsDto.expectedIncomeReceived]
            },
            {
                label: 'Otros',
                backgroundColor: '#f6c23e',
                borderColor: '#f4b619',
                borderWidth: 1,
                data: [detailsDto.other]
            },
            {
                label: 'Total',
                backgroundColor: '#36b9cc',
                borderColor: '#2c9faf',
                borderWidth: 1,
                data: [detailsDto.totalIncomeThisMonth]
            }
        ]
    };

    // Configuración para Chart.js 2.9.4 con espaciado ajustado
    const options = {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            xAxes: [{
                categoryPercentage: 0.8,  // Controla el espacio entre grupos de barras (0.5 = 50% del espacio disponible)
                barPercentage: 0.8,       // Controla el ancho de cada barra individual (0.8 = 80% del espacio asignado)
                gridLines: {
                    display: false
                },
                ticks: {
                    fontSize: window.innerWidth <= 768 ? 10 : 12,
                    maxRotation: 0,       // Evita rotación de etiquetas
                    minRotation: 0
                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    min: 0,
                    max: Math.ceil(maxValue * 1.1),
                    callback: function(value) {
                        if (value === 0) return 'C$0';
                        if (value >= 1000000) return 'C$' + (value/1000000).toFixed(1) + 'M';
                        if (value >= 1000) return 'C$' + (value/1000).toFixed(0) + 'k';
                        return 'C$' + value;
                    },
                    fontSize: window.innerWidth <= 768 ? 10 : 12
                },
                gridLines: {
                    color: 'rgba(0, 0, 0, 0.05)'
                }
            }]
        },
        legend: {
            display: true,
            position: window.innerWidth <= 768 ? 'top' : 'bottom',
            labels: {
                boxWidth: 12,
                padding: 20,
                usePointStyle: true,
                fontSize: window.innerWidth <= 768 ? 12 : 13
            }
        },
        tooltips: {
            callbacks: {
                label: function(tooltipItem, data) {
                    return data.datasets[tooltipItem.datasetIndex].label + ': C$' +
                        tooltipItem.yLabel.toLocaleString('es-ES');
                }
            }
        },
        animation: {
            duration: 1000
        }
    };

    // Ajustes específicos para móviles
    if (window.innerWidth <= 768) {
        options.scales.xAxes[0].categoryPercentage = 0.6;
        options.scales.xAxes[0].barPercentage = 0.7;
    }

    // Crear gráfico
    ctx.chart = new Chart(ctx, {
        type: 'bar',
        data: chartData,
        options: options
    });

    // Manejar redimensionamiento
    window.addEventListener('resize', function() {
        if (ctx.chart) {
            ctx.chart.resize();
            // Reaplicar ajustes de móvil si es necesario
            if (window.innerWidth <= 768) {
                ctx.chart.options.scales.xAxes[0].categoryPercentage = 0.6;
                ctx.chart.options.scales.xAxes[0].barPercentage = 0.7;
            } else {
                ctx.chart.options.scales.xAxes[0].categoryPercentage = 0.5;
                ctx.chart.options.scales.xAxes[0].barPercentage = 0.8;
            }
            ctx.chart.update();
        }
    });
}