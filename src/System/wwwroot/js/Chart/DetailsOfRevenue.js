function RevenueChart(detailsDto) {
    const ctx = document.getElementById('financialBarChart');
    if (!ctx) return;

    // Destruir gráfico anterior si existe
    if (ctx.chart) {
        ctx.chart.destroy();
    }

    // Calcular valores mínimos y máximos
    const allValues = [
        detailsDto.preschool.regularStudent.amount,
        detailsDto.preschool.discountedStudent.amount,
        detailsDto.elementary.regularStudent.amount,
        detailsDto.elementary.discountedStudent.amount,
        detailsDto.secondary.regularStudent.amount,
        detailsDto.secondary.discountedStudent.amount
    ];
    const minValue = Math.min(...allValues);
    const maxValue = Math.max(...allValues);

    // Configuración responsive
    const isMobile = window.innerWidth <= 768;
    const barThickness = isMobile ? 20 : 30;

    const chartData = {
        labels: ['Preescolar', 'Primaria', 'Secundaria'],
        datasets: [
            {
                label: 'Estudiantes Regulares',
                backgroundColor: '#4e73df',
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
                backgroundColor: '#f6c23e',
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

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        layout: {
            padding: {
                left: isMobile ? 10 : 30,
                right: isMobile ? 10 : 30,
                top: 10,
                bottom: isMobile ? 30 : 20
            }
        },
        scales: {
            x: {
                grid: {
                    display: false
                },
                ticks: {
                    font: {
                        size: isMobile ? 10 : 12
                    }
                },
                barThickness: barThickness
            },
            y: {
                min: minValue > 0 ? minValue * 0.9 : 0,
                ticks: {
                    font: {
                        size: isMobile ? 10 : 12
                    },
                    callback: function(value) {
                        // Formato corto para móviles
                        if (isMobile && value >= 1000) {
                            return 'C$' + (value/1000).toFixed(1) + 'k';
                        }
                        return 'C$' + value.toLocaleString('es-ES');
                    }
                },
                grid: {
                    color: 'rgba(0, 0, 0, 0.05)'
                }
            }
        },
        plugins: {
            legend: {
                position: isMobile ? 'top' : 'bottom',
                labels: {
                    boxWidth: 12,
                    padding: 20,
                    usePointStyle: true,
                    font: {
                        size: isMobile ? 12 : 13
                    }
                }
            },
            tooltip: {
                callbacks: {
                    label: function(context) {
                        return `${context.dataset.label}: C$${context.parsed.y.toLocaleString('es-ES')}`;
                    }
                },
                titleFont: {
                    size: isMobile ? 12 : 14
                },
                bodyFont: {
                    size: isMobile ? 12 : 13
                }
            }
        },
        animation: {
            duration: isMobile ? 1000 : 1500
        }
    };

    // Crear nuevo gráfico
    ctx.chart = new Chart(ctx, {
        type: 'bar',
        data: chartData,
        options: options
    });

    // Manejar redimensionamiento
    window.addEventListener('resize', function() {
        if (ctx.chart) {
            ctx.chart.resize();
        }
    });
}