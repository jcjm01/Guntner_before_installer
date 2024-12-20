import pyautogui
import time
import win32gui

# Nombre de la ventana de EzCad
EZCAD_WINDOW_TITLE = "EzCad-Lite  - No title"

# Coordenadas del centro del área de trabajo
CENTER_X, CENTER_Y = 960, 540  # Cambiar según el tamaño y resolución del área de trabajo

def bring_ezcad_to_front():
    hwnd = win32gui.FindWindow(None, EZCAD_WINDOW_TITLE)
    if hwnd:
        win32gui.ShowWindow(hwnd, 5)  # Restaurar ventana si está minimizada
        win32gui.SetForegroundWindow(hwnd)  # Traer ventana al frente
        return True
    else:
        print("No se encontró la ventana de EzCad. Asegúrate de que esté abierta.")
        return False

def detect_and_drag_to_center():
    print("Intentando mover el PNG al centro en EzCad...")

    # Intentar seleccionar el PNG haciendo clic en su posición inicial
    try:
        # Coordenadas iniciales aproximadas (puedes ajustarlas según la posición inicial típica del PNG)
        initial_x, initial_y = 400, 300  # Cambia estas coordenadas según donde se cargue el PNG

        # Mover el mouse a la posición inicial y hacer clic para seleccionarlo
        pyautogui.moveTo(initial_x, initial_y, duration=0.5)
        pyautogui.mouseDown(button='left')  # Simula mantener presionado el botón izquierdo del mouse

        # Arrastrar hacia el centro del área de trabajo
        pyautogui.moveTo(CENTER_X, CENTER_Y, duration=1)  # Mueve el mouse al centro
        pyautogui.mouseUp(button='left')  # Suelta el botón izquierdo del mouse

        print(f"PNG arrastrado desde ({initial_x}, {initial_y}) hacia el centro ({CENTER_X}, {CENTER_Y}).")
    except Exception as e:
        print(f"Error al mover el PNG: {e}")

def main():
    print("Esperando a que EzCad esté abierto...")
    while True:
        if bring_ezcad_to_front():
            time.sleep(2)  # Esperar a que la ventana esté lista
            detect_and_drag_to_center()
            break
        else:
            print("EzCad no está abierto. Reintentando en 5 segundos...")
            time.sleep(5)

if __name__ == "__main__":
    main()
