import json
import ezdxf

# Ruta del archivo JSON generado por la aplicación C#
json_path = "C:/Users/Usuario/Downloads/datos.json"  # Ajusta esta ruta
output_dxf_path = "C:/Users/Usuario/Downloads/plantilla_final.dxf"

# Leer el archivo JSON
with open(json_path, "r") as file:
    datos = json.load(file)

# Crear un nuevo archivo DXF
doc = ezdxf.new()
msp = doc.modelspace()

# Configuración de posición y espaciado
x_center = 100  # Coordenada X centrada
y_position = 100  # Coordenada Y inicial
line_spacing = 10  # Espacio entre líneas

# Agregar texto al DXF
for etiqueta, valor in datos.items():
    texto = f"{etiqueta}: {valor}"
    msp.add_text(texto, dxfattribs={"height": 5}).set_placement((x_center, y_position), align="MIDDLE_CENTER")
    y_position -= line_spacing  # Ajustar posición Y

# Guardar el archivo DXF
doc.saveas(output_dxf_path)
print(f"Archivo DXF generado correctamente en: {output_dxf_path}")
