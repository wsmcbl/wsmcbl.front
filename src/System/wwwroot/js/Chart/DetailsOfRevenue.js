function RevenueChart(detailsDto) {
    const ctx = document.getElementById('financialBarChart').getContext('2d');

    // Calcular el valor mínimo para establecer suggestedMin
    const allValues = [
        detailsDto.preschool.regularStudent.amount,
        detailsDto.preschool.discountedStudent.amount,
        detailsDto.elementary.regularStudent.amount,
        detailsDto.elementary.discountedStudent.amount,
        detailsDto.secondary.regularStudent.amount,
        detailsDto.secondary.discountedStudent.amount
    ];
    const minValue = Math.min(...allValues);

    // Extrae datos del DTO con los nuevos colores
    const chartData = {
        labels: ['Preescolar', 'Primaria', 'Secundaria'],
        datasets: [
            {
                label: 'Estudiantes Regulares',
                backgroundColor: '#4e73df', // Azul
                borderColor: '#2e59d9',
                borderWidth: 1,
                data: [
                    detailsDto.preschool.regularStudent.amount,
                    detailsDto.elementary.regularStudent.amount,
                    detailsDto.secondary.regularStudent.amount
                ]
            },
            {
                label: 'Estudiantes con Descuento',
                backgroundColor: '#f6c23e', // Amarillo
                borderColor: '#f4b619',
                borderWidth: 1,
                data: [
                    detailsDto.preschool.discountedStudent.amount,
                    detailsDto.elementary.discountedStudent.amount,
                    detailsDto.secondary.discountedStudent.amount
                ]
            }
        ]
    };

    new Chart(ctx, {
        type: 'bar',
        data: chartData,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            layout: {
                padding: {
                    left: 30,
                    right: 30,
                    top: 20,
                    bottom: 20
                }
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        display: false
                    },
                    barPercentage: 0.5
                }],
                yAxes: [{
                    ticks: {
                        suggestedMin: minValue > 0 ? minValue * 0.9 : 0, // No menor que cero
                        beginAtZero: false,
                        callback: function(value) {
                            return 'C$' + value.toLocaleString('es-ES');
                        }
                    },
                    gridLines: {
                        color: 'rgba(0, 0, 0, 0.05)'
                    }
                }]
            },
            legend: {
                display: true,
                position: 'bottom',
                labels: {
                    boxWidth: 12,
                    padding: 20,
                    usePointStyle: true
                }
            },
            tooltips: {
                callbacks: {
                    label: function(tooltipItem, data) {
                        return data.datasets[tooltipItem.datasetIndex].label + ': C$' + tooltipItem.yLabel.toLocaleString('es-ES');
                    }
                }
            }
        }
    });
}