window.printStudentCredentials = function(studentId, tempPassword) {
    if (!studentId || !tempPassword) {
        console.error('Datos de estudiante no disponibles');
        alert('No se pudo obtener la información del estudiante');
        return;
    }

    const printContent = `
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="UTF-8">
        <title>Credenciales de Acceso</title>
        <style>
            @page { margin: 0; size: auto; }
            body {
                font-family: "Courier New", monospace;
                font-size: 12px;
                margin: 2mm;
                padding: 0;
                white-space: pre-line;
            }
            .header {
                text-align: center;
                font-weight: bold;
                margin-bottom: 3mm;
                text-decoration: underline;
            }
            .password {
                font-family: "Courier New", monospace;
                font-size: 14px;
                text-align: center;
                margin: 2mm 0;
                letter-spacing: 1px;
                font-weight: bold;
            }
            ol, ul {
                padding-left: 5mm;
                margin: 2mm 0;
            }
            li {
                margin-bottom: 1mm;
            }
        </style>
    </head>
    <body>
        <div class="header">COLEGIO BAUTISTA LIBERTAD</div>
        
        <strong>INSTRUCCIONES DE ACCESO:</strong>
        <ol>
            <li>Ingrese a: cbl-edu.com/online-grades</li>
            <li>Código del estudiante: ${studentId}</li>
            <li>Contraseña: ${tempPassword}</li>
            <li>Haga clic en "Ver calificaciones"</li>
        </ol>
        <strong>PARA VER SUS NOTAS:</strong>
        <ol>
            <li>Debe estar al dia con sus pagos</li>
        </ol>
        <strong>RECOMENDACIONES:</strong>
        <ul>
            <li>No comparta sus credenciales</li>
            <li>Guarde esta información</li>
        </ul>
        <div style="text-align: center; font-size: 10px;">
            ${new Date().toLocaleDateString()} | soporte@cbl-edu.com
        </div>
    </body>
    </html>
    `;

    const printWindow = window.open('', '_blank');
    if (!printWindow) {
        alert('Por favor permite ventanas emergentes para imprimir');
        return;
    }

    printWindow.document.open();
    printWindow.document.write(printContent);
    printWindow.document.close();

    // Esperar breve momento para que cargue el contenido antes de imprimir
    setTimeout(() => {
        printWindow.print();
        // No cerramos automáticamente para permitir reimpresión si falla
    }, 200);
};