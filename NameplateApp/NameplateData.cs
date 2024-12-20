using System.Collections.Generic;

public class NameplateData
{
    // Diccionario para almacenar todos los campos dinámicamente
    public Dictionary<string, string> Campos { get; set; }

    public int TemplateId { get; set; }

    public NameplateData()
    {
        Campos = new Dictionary<string, string>();
    }

    // Método para obtener el valor de un campo específico
    public string GetCampo(string nombreCampo)
    {
        if (Campos.ContainsKey(nombreCampo))
        {
            return Campos[nombreCampo];
        }
        return "%Vacio%";  // Devuelve "%Vacio%" si no encuentra el campo
    }
}
