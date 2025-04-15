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
            @page {
                size: 58mm;
                margin: 2mm;
            }
            body {
                font-family: Arial, sans-serif;
                font-size: 9px;
                width: 58mm;
                padding: 2mm;
                -webkit-print-color-adjust: exact !important;
                print-color-adjust: exact !important;
            }
            .header {
                text-align: center;
                font-weight: bold;
                margin-bottom: 2mm;
                font-size: 11px;
                border-bottom: 1px solid #000;
                padding-bottom: 2mm;
            }
            .section-title {
                font-weight: bold;
                margin: 2mm 0 1mm 0;
                font-size: 10px;
                color: #2c3e50;
            }
            .credentials-box {
                margin: 2mm 0;
                padding: 2mm;
                background-color: #f8f9fa;
                border: 1px dashed #6c757d;
            }
            .password {
                font-family: "Consolas", monospace;
                font-size: 12px;
                font-weight: bold;
                text-align: center;
                margin: 1mm 0;
                background-color: #e9ecef;
                padding: 1mm;
                word-break: break-all;
            }
            .warning-box {
                margin: 2mm 0;
                padding: 2mm;
                background-color: #fff3cd;
                border: 1px dashed #ffc107;
                font-size: 8px;
            }
            .contact-info {
                margin-top: 3mm;
                font-size: 8px;
                text-align: center;
                border-top: 1px solid #000;
                padding-top: 2mm;
            }
            ol, ul {
                padding-left: 4mm;
                margin: 1mm 0;
            }
            li {
                margin-bottom: 1mm;
            }
        </style>
    </head>
    <body>
        <div class="header">COLEGIO BAUTISTA LIBERTAD</div>
        
        <div class="section-title">INSTRUCCIONES DE ACCESO:</div>
        <ol>
            <li>Ingrese a: cbl-edu.com/online-grades</li>
            <li>ID de usuario: <strong>${studentId}</strong></li>
            <li>Contraseña temporal: 
                <div class="password">${tempPassword}</div>
            </li>
            <li>Haga clic en "Ver calificaciones"</li>
        </ol>
        
        <div class="section-title">PARA VER SUS NOTAS:</div>
        <ol>
            <li>Debe estar al dia con sus pagos"</li>
        </ol>
        
        <div class="warning-box">
            <div style="font-weight: bold; margin-bottom: 1mm;">RECOMENDACIONES DE SEGURIDAD:</div>
            <ul>
                <li>No comparta sus credenciales con terceros</li>
                <li>Guarde esta información en lugar seguro</li>
            </ul>
        </div>
        
        <div class="contact-info">
            <strong>¿Problemas con el acceso?</strong><br>
            Contacte a: soporte@cbl-edu.com<br>
            Teléfono: 2270-5587<br>
            Celular: 8270-4585<br>
            ${new Date().toLocaleDateString()}
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
    
}