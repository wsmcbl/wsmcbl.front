// Asegúrate de exponer la función correctamente al ámbito global
window.levelsDonutChart = function(distributionData) {
    try {
        // Validación de datos
        if (!distributionData) {
            console.error('Datos no proporcionados');
            return;
        }

        const ctx = document.getElementById('levelsDonutChart');
        if (!ctx) {
            console.error('Elemento canvas no encontrado');
            return;
        }

        // Destruir instancia anterior si existe
        if (window.currentDonutChart) {
            window.currentDonutChart.destroy();
        }

        // Configuración del gráfico
        const chartData = {
            labels: ['Preescolar', 'Primaria', 'Secundaria'],
            datasets: [{
                data: [
                    distributionData.preschool?.amount || 0,
                    distributionData.elementary?.amount || 0,
                    distributionData.secondary?.amount || 0
                ],
                backgroundColor: [
                    '#4e73df', // Azul
                    '#1cc88a', // Verde
                    '#36b9cc'  // Cyan
                ],
                borderWidth: 1
            }]
        };

        window.currentDonutChart = new Chart(ctx, {
            type: 'doughnut',
            data: chartData,
            options: {
                maintainAspectRatio: false,
                cutout: '70%',
                plugins: {
                    legend: { display: false }
                }
            }
        });

    } catch (error) {
        console.error('Error al crear gráfico:', error);
    }
};