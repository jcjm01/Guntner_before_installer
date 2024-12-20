using Microsoft.Win32; // Necesario para abrir el diálogo de archivos
using System.IO; // Necesario para trabajar con archivos
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Configuration;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Diagnostics;
using System.Windows.Controls;
using System.Text.Json;
using Newtonsoft.Json;

namespace NameplateApp
{
    public partial class MainWindow : Window
    {
        private string RutaArchivo; // Ruta configurada para b2merlin.txt
        private FileSystemWatcher fileWatcher; // Monitorea cambios en el archivo
        private string ContraseñaConfiguración = "admin";
        // Lista de plantillas
        private List<Plantilla> ListaDePlantillas = new List<Plantilla>();

        public MainWindow()
        {
            InitializeComponent();

            // Obtener la ruta configurada desde App.config
            RutaArchivo = ConfigurationManager.AppSettings["ArchivoPath"];

            // Verificar si la ruta está configurada
            if (string.IsNullOrEmpty(RutaArchivo))
            {
                MessageBox.Show(
                    "La ruta del archivo no está configurada. Configure la ruta antes de continuar.",
                    "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning
                );
                btnIniciarMarcado.IsEnabled = false; // Deshabilitar botones relacionados
            }
            else
            {
                // Solo configurar el monitoreo si el archivo existe
                if (File.Exists(RutaArchivo))
                {
                    ConfigurarFileWatcher(RutaArchivo);
                    ProcesarArchivo(RutaArchivo);
                }
                else
                {
                    MessageBox.Show(
                        "El archivo no existe en la ruta configurada. Configure la ruta correctamente.",
                        "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning
                    );
                    btnIniciarMarcado.IsEnabled = false; // Deshabilitar botones relacionados
                }
            }
        }




        private void ConfigurarFileWatcher(string rutaArchivo)
        {
            if (fileWatcher != null)
            {
                fileWatcher.Dispose();
            }

            fileWatcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(rutaArchivo),
                Filter = Path.GetFileName(rutaArchivo),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                EnableRaisingEvents = true // Activa el monitoreo
            };

            fileWatcher.Changed += OnArchivoModificado;
            fileWatcher.Created += OnArchivoModificado;
        }

        private void FormatearYGuardarPNG_Click(object sender, RoutedEventArgs e)
        {
            var lineas = txtContenidoArchivo.Text.Split('\n');

            var grid = new Grid
            {
                Background = Brushes.White,
                Width = 800,
                Height = 600,
                Margin = new Thickness(20)
            };

            // Definir las columnas
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) }); // Etiquetas
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });  // Dos puntos
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Valores

            int rowIndex = 0;
            foreach (var linea in lineas)
            {
                var partes = linea.Split(new[] { ':' }, 2);
                if (partes.Length != 2) continue;

                var etiqueta = partes[0].Trim();
                var valor = partes[1].Trim();

                // Etiqueta
                var etiquetaText = new TextBlock
                {
                    Text = etiqueta,
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Right
                };

                // Dos puntos
                var dosPuntosText = new TextBlock
                {
                    Text = ":",
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Valor
                var valorText = new TextBlock
                {
                    Text = valor,
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                // Agregar a Grid
                grid.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(etiquetaText, rowIndex);
                Grid.SetColumn(etiquetaText, 0);

                Grid.SetRow(dosPuntosText, rowIndex);
                Grid.SetColumn(dosPuntosText, 1);

                Grid.SetRow(valorText, rowIndex);
                Grid.SetColumn(valorText, 2);

                grid.Children.Add(etiquetaText);
                grid.Children.Add(dosPuntosText);
                grid.Children.Add(valorText);

                rowIndex++;
            }

            // Renderizar y guardar como PNG
            ExportarDatosComoJSON();
        }

        private void ExportarDatosComoJSON()
        {
            var lineas = txtContenidoArchivo.Text.Split('\n');
            var datos = new Dictionary<string, string>();

            foreach (var linea in lineas)
            {
                var partes = linea.Split(new[] { ':' }, 2);
                if (partes.Length == 2)
                {
                    datos[partes[0].Trim()] = partes[1].Trim();
                }
            }

            string jsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "datos.json");
            File.WriteAllText(jsonPath, System.Text.Json.JsonSerializer.Serialize(datos, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
        }




        private void GuardarPrevisualizacionComoImagen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Asegúrate de que solo se renderice contenido visible.
                if (ContenedorVisual.Visibility == Visibility.Collapsed || ContenedorVisual.Children.Count == 0)
                {
                    MessageBox.Show("No hay contenido visible en la previsualización para guardar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                const int dpi = 300; // DPI para alta calidad

                // Crear un RenderTargetBitmap basado solo en el contenido visible
                var bounds = new Rect(ContenedorVisual.RenderSize);
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                    (int)(bounds.Width * dpi / 96),
                    (int)(bounds.Height * dpi / 96),
                    dpi,
                    dpi,
                    PixelFormats.Pbgra32);

                ContenedorVisual.Measure(bounds.Size);
                ContenedorVisual.Arrange(bounds);
                renderBitmap.Render(ContenedorVisual);

                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "PLANTILLA.png");
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(fileStream);
                }

                MessageBox.Show("La imagen visible se guardó correctamente en Descargas.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la imagen: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }









        private void ConfigurarPlantillas_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificarContraseña()) return; // Solicitar la contraseña antes de continuar.

            PlantillasPanel.Visibility = PlantillasPanel.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            MessageBox.Show("Panel de plantillas configurado.");
        }

        private void ConfirmarPrevisualizacion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Previsualización confirmada.");
            btnIniciarMarcado.IsEnabled = true;
        }
        // Método para eliminar una plantilla
        private void EliminarPlantilla_Click(object sender, RoutedEventArgs e)
        {
            string inputId = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el ID de la plantilla a eliminar:", "Eliminar Plantilla", "1");

            var plantilla = ListaDePlantillas.FirstOrDefault(p => p.Id == inputId);
            if (plantilla != null)
            {
                ListaDePlantillas.Remove(plantilla);
                MessageBox.Show($"Plantilla {inputId} eliminada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"No se encontró una plantilla con ID: {inputId}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ConfigurarRutaArchivo(object sender, RoutedEventArgs e)
        {
            if (!VerificarContraseña()) return; // Solicitar la contraseña antes de continuar.

            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Seleccione la carpeta donde se alojará b2merlin.txt"
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RutaArchivo = Path.Combine(dialog.SelectedPath, "b2merlin.txt");

                // Guardar la ruta en App.config
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["ArchivoPath"].Value = RutaArchivo;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show($"Ruta configurada correctamente: {RutaArchivo}", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Configurar el monitoreo y procesar el archivo si existe
                if (File.Exists(RutaArchivo))
                {
                    ConfigurarFileWatcher(RutaArchivo);
                    ProcesarArchivo(RutaArchivo);
                }
                else
                {
                    MessageBox.Show("El archivo b2merlin.txt no existe en la ruta configurada. Se iniciará el monitoreo en cuanto el archivo sea creado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ConfigurarFileWatcher(RutaArchivo);
                }
            }
        }

        private void IniciarMarcado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ruta al ejecutable EzCad
                string ezcadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EZCAD", "EzCad2.exe");
                string workingDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EZCAD");

                // Verificar si la carpeta EZCAD existe
                if (!Directory.Exists(workingDirectory))
                {
                    MessageBox.Show("No se encontró la carpeta EZCAD en el directorio del proyecto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Verificar si el ejecutable EzCad2 existe
                if (!File.Exists(ezcadPath))
                {
                    MessageBox.Show("No se encontró EzCad2.exe en la carpeta EZCAD.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Ejecutar EzCad con el directorio de trabajo correcto
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ezcadPath,
                    WorkingDirectory = workingDirectory, // Directorio de trabajo
                    UseShellExecute = false
                };

                Process.Start(startInfo);

                MessageBox.Show("EzCad iniciado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar EzCad: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Configurar FileSystemWatcher
        private void ConfigurarFileWatcher()
        {
            if (fileWatcher != null)
            {
                fileWatcher.Dispose();
            }

            fileWatcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(RutaArchivo),
                Filter = Path.GetFileName(RutaArchivo),
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                EnableRaisingEvents = true // Activa el monitoreo
            };

            fileWatcher.Changed += OnArchivoModificado;
            fileWatcher.Created += OnArchivoModificado;

        }


        // Evento cuando el archivo es modificado
        private void OnArchivoModificado(object sender, FileSystemEventArgs e)
        {
            // Usar Dispatcher.Invoke para ejecutar en el hilo principal (UI)
            Dispatcher.Invoke(() =>
            {
                ProcesarArchivo(e.FullPath);
            });
        }


        // Procesar el archivo
        private void ProcesarArchivo(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    txtContenidoArchivo.Text = "El archivo no existe.";
                    return;
                }

                string[] lineas = File.ReadAllLines(filePath);
                if (lineas.Length < 1)
                {
                    txtContenidoArchivo.Text = "El archivo está vacío.";
                    return;
                }

                // Leer la plantilla del archivo (sin PlantillaID)
                var campos = ObtenerPlantillaPorId(lineas[0]?.Trim())?.Campos;
                if (campos == null)
                {
                    txtContenidoArchivo.Text = $"No se encontró una plantilla con el ID: {lineas[0]?.Trim()}.";
                    return;
                }

                // Crear Grid dinámico
                var grid = new Grid
                {
                    Background = Brushes.Transparent
                };

                // Crear 2 columnas: 1 para etiquetas y 1 para valores
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Etiquetas
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5) }); // Espacio fijo para ":"
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // Valores

                // Construir el grid dinámicamente
                for (int i = 0; i < campos.Count; i++)
                {
                    string etiqueta = campos[i];
                    string valor = i + 1 < lineas.Length ? lineas[i + 1]?.Trim() : "N/A";

                    // Añadir fila
                    grid.RowDefinitions.Add(new RowDefinition());

                    // Etiqueta
                    var lblEtiqueta = new TextBlock
                    {
                        Text = etiqueta,
                        FontWeight = FontWeights.Bold,
                        FontSize = 12,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    Grid.SetRow(lblEtiqueta, i);
                    Grid.SetColumn(lblEtiqueta, 0);
                    grid.Children.Add(lblEtiqueta);

                    // Agregar los dos puntos solo si el valor no tiene "::"
                    if (!valor.Contains("::"))
                    {
                        var lblDosPuntos = new TextBlock
                        {
                            Text = ":",
                            FontSize = 12,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };
                        Grid.SetRow(lblDosPuntos, i);
                        Grid.SetColumn(lblDosPuntos, 1);
                        grid.Children.Add(lblDosPuntos);
                    }

                    // Valor
                    var lblValor = new TextBlock
                    {
                        Text = valor,
                        FontSize = 12,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    Grid.SetRow(lblValor, i);
                    Grid.SetColumn(lblValor, 2);
                    grid.Children.Add(lblValor);
                }


                // Mostrar el Grid renderizado
                ContenedorVisual.Children.Clear();
                ContenedorVisual.Children.Add(grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }






        // Configurar la ruta del archivo

        // Obtener una plantilla por ID
        private Plantilla ObtenerPlantillaPorId(string idPlantilla)
        {
            return ListaDePlantillas.FirstOrDefault(p => p.Id == idPlantilla);
        }

        private bool VerificarContraseña()
        {
            string inputPassword = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la contraseña para continuar:", "Verificación de Contraseña", "");
            if (inputPassword == ContraseñaConfiguración)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void ConfigurarContraseña_Click(object sender, RoutedEventArgs e)
        {
            // Solicitar la contraseña actual para validar
            string contraseñaActual = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la contraseña actual para continuar:", "Verificación de Contraseña", "");

            // Verificar si la contraseña ingresada es correcta
            if (contraseñaActual == ContraseñaConfiguración)
            {
                // Permitir al usuario ingresar una nueva contraseña
                string nuevaContraseña = Microsoft.VisualBasic.Interaction.InputBox("Ingrese una nueva contraseña:", "Configuración de Contraseña", "");

                if (!string.IsNullOrEmpty(nuevaContraseña))
                {
                    ContraseñaConfiguración = nuevaContraseña;
                    MessageBox.Show("Contraseña actualizada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("La nueva contraseña no puede estar vacía.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                // Si la contraseña actual es incorrecta, mostrar un mensaje de error
                MessageBox.Show("Contraseña incorrecta. No se pudo cambiar la contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        // Cargar una plantilla desde un archivo TXT
        private void CargarPlantillaDesdeTxt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt",
                Title = "Seleccione la plantilla"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string[] lineas = File.ReadAllLines(openFileDialog.FileName);

                if (lineas.Length > 0 && lineas[0].StartsWith("PlantillaID:"))
                {
                    string id = lineas[0].Split(':')[1].Trim();
                    List<string> campos = lineas.Skip(1).ToList();

                    // Verificar si ya existe una plantilla con el mismo ID
                    if (ListaDePlantillas.Any(p => p.Id == id))
                    {
                        MessageBox.Show($"Ya existe una plantilla con el ID: {id}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    ListaDePlantillas.Add(new Plantilla { Id = id, Campos = campos });
                    MessageBox.Show($"Plantilla {id} cargada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Formato de plantilla no válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Clase para representar una plantilla
        public class Plantilla
        {
            public string Id { get; set; }
            public List<string> Campos { get; set; }
        }
    }
}
