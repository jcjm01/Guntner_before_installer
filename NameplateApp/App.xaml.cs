using System;
using System.Windows;

namespace NameplateApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Iniciar servidor OWIN
            string baseAddress = "http://localhost:8080/";

            try
            {
                // Iniciar el host de OWIN
                Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: baseAddress);
                System.Diagnostics.Debug.WriteLine($"Servidor escuchando en {baseAddress}");

                // Mostrar ventana de bienvenida
                WelcomeWindow welcomeWindow = new WelcomeWindow();
                welcomeWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar el servidor OWIN: {ex.Message}");
                Shutdown();
            }
        }
    }
}
