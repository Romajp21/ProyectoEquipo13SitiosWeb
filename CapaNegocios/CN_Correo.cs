using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
namespace CapaNegocios
{
    public class CN_Correo
    {
        public bool EnviarCorreo(string destinatario, string asunto, string mensaje, out string error)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("romariojimenez2@gmail.com", "ihld kqsh fcuw gxsr"),
                    EnableSsl = true
                };

                // Construir el mensaje HTML
                var mensajeHtml = $@"
            <html>
                <head>
                    <style>
                        body {{
                            font-family: 'Arial', sans-serif;
                            background-color: #f7f9fc;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 20px auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            overflow: hidden;
                        }}
                        .header {{
                            background-color: #4CAF50;
                            color: #ffffff;
                            text-align: center;
                            padding: 20px;
                            font-size: 24px;
                            font-weight: bold;
                        }}
                        .content {{
                            padding: 20px;
                            text-align: center;
                            color: #333333;
                            font-size: 16px;
                            line-height: 1.6;
                        }}
                        .content p {{
                            margin: 20px 0;
                        }}
                        .button {{
                            display: inline-block;
                            background-color: #4CAF50;
                            color: #ffffff;
                            text-decoration: none;
                            padding: 10px 20px;
                            border-radius: 4px;
                            font-weight: bold;
                            margin-top: 20px;
                        }}
                        .footer {{
                            background-color: #f4f4f4;
                            text-align: center;
                            padding: 10px;
                            font-size: 12px;
                            color: #777777;
                        }}
                        .footer a {{
                            color: #4CAF50;
                            text-decoration: none;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <div class=""header"">
                            Nueva Membresía Creada
                        </div>
                        <div class=""content"">
                            <p>Se ha creado un nuevo contenido:</p>
                            <p style=""font-size: 18px; font-weight: bold;"">'1 Mes' para la membresía '17'.</p>
                            <a href=""#"" class=""button"">Ver detalles</a>
                        </div>
                        <div class=""footer"">
                            Este correo fue enviado automáticamente por el sistema. <br>
                            Por favor, no respondas a este mensaje. <br>
                            <a href=""#"">Política de privacidad</a> | <a href=""#"">Soporte técnico</a>
                        </div>
                    </div>
                </body>
                </html>";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("romariojimenez2@gmail.com"),
                    Subject = asunto,
                    Body = mensajeHtml,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(destinatario);

                smtpClient.Send(mailMessage);

                error = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
