using System;
using System.Windows;

namespace NameplateApp
{
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
            // Obtener el nombre de usuario de Windows
            string userName = Environment.UserName;
            txtWelcome.Text = $"Bienvenido, {userName}";
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Abrir la ventana principal al presionar el botón "Comenzar"
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Cierra la ventana de bienvenida
        }
    }
}
