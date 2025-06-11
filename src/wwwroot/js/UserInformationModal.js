function imprimirContenido() {
    const modal = document.querySelector('#InfoUserModal .modal-body');
    const ventanaImpresion = window.open('', '_blank', "width: 85%; height: 30%; max-width: 90%; max-height: 50%");
    const fechaHora = new Date().toLocaleString('es-NI', {
        dateStyle: 'long',
        timeStyle: 'short',
        timeZone: 'America/Managua',
    });
    // Extraer datos dinámicamente
    const titulo = modal.querySelector('.card-title').innerText || 'Sin dato2';
    const roles = modal.querySelector('p:nth-of-type(1)').innerText || 'Sin dato2';
    const nombre = modal.querySelector('p:nth-of-type(2)').innerHTML.replace(/<strong>.*?<\/strong>\s*/g, '').trim() || 'Sin dato';
    const email = modal.querySelector('p:nth-of-type(3)').innerHTML.replace(/<strong>.*?<\/strong>\s*/g, '').trim() || 'Sin dato';
    const contrasena = modal.querySelector('p:nth-of-type(4)').innerHTML.replace(/<strong>.*?<\/strong>\s*/g, '').trim() || 'Sin dato';
    ventanaImpresion.document.open();
    ventanaImpresion.document.write(`

    <html>
      <head>
        <title>Impresión de Credenciales</title>
        <link href="https://fonts.googleapis.com/css2?family=Fira+Code:wght@400;700&display=swap" rel="stylesheet">
        <style>
          /* Estilo global */
          body {
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 20px;
            background: #fff;
            color: #333;
          }

          /* Contenedor principal */
          .print-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
          }

          /* Encabezado */
          .header {
            text-align: center;
            border-bottom: 2px solid #007bff;
            padding-bottom: 15px;
            margin-bottom: 20px;
          }

          .header img {
            max-width: 100px;
            margin-bottom: 10px;
          }

          .header h1 {
            font-size: 24px;
            color: #007bff;
            margin: 0;
          }

          /* Fecha y hora */
          .date {
            text-align: right;
            font-size: 14px;
            color: #555;
            margin-bottom: 20px;
          }

          /* Contenido */
          .content {
            line-height: 1.6;
            font-size: 16px;
            margin-bottom: 20px;
          }

          /* Sección de credenciales */
          .credentials {
            padding: 15px;
            background: #f9f9f9;
            border: 1px dashed #007bff;
            border-radius: 5px;
            font-size: 14px;
          }

          .credentials h3 {
            margin-top: 0;
            color: #007bff;
          }
          
          .password-field {
              font-family: 'Fira Code', monospace;
              font-size: 14px;
              letter-spacing: 1px;
              background-color: #f5f5f5;
              padding: 2px 5px;
              border-radius: 3px;
              display: inline-block;
              margin-left: 5px;
           }

          /* Pie de página */
          .footer {
            text-align: center;
            margin-top: 20px;
            font-size: 12px;
            color: #999;
          }
        </style>
      </head>
      <body>
        <div class="print-container">
          <!-- Encabezado -->
          <div class="header">
            <img src="https://cbl-edu.com/assets/material-kit-3/img/logos/CBL_COLOR.png" alt="Logo de la Compañía">
            <h1>Colegio Bautista Libertad</h1>
          </div>

          <!-- Fecha y hora -->
          <div class="date">
            Impreso el: ${fechaHora}
          </div>

          <!-- Contenido -->
          <div class="content">
            <p>
              Estimado usuario, a continuación encontrará la información relacionada con sus credenciales
              y acceso. Por favor, guarde este documento en un lugar seguro y no comparta esta información
              con nadie más.
            </p>
            <p>
              Si tiene algún problema o pregunta, no dude en contactarnos a través de nuestro correo de soporte:
              <strong>soporte@cbl-edu.com</strong>.
            </p>
          </div>

          <!-- Sección de credenciales -->
          <div class="credentials">
            <h3>Credenciales de Acceso</h3>
            <p><strong>Usuario:</strong> ${nombre}</p>
            <p><strong>Correo:</strong> ${email}</p>
            <p><strong>Contraseña:</strong><span class="password-field">${contrasena}</span></p>
            <p><strong>URL de Acceso:</strong> <a href="https://wsm.cbl-edu.com" target="_blank">https://wsm.cbl-edu.com</a></p>
          </div>

          <!-- Pie de página -->
          <div class="footer">
            Documento generado automáticamente por WSM-CBL. No lo comparta con terceros.
          </div>
        </div>
      </body>
    </html>
     
  `);
    ventanaImpresion.document.close();

    // Inicia la impresión
    ventanaImpresion.print();

    // Cierra la ventana después de imprimir
    ventanaImpresion.onafterprint = () => ventanaImpresion.close();
}
