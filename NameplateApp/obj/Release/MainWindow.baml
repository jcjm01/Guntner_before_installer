   M S B A M L   `   `   `  ����   NameplateApp    NameplateApp.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Marcado de Nameplate��$	��800��$	��900�����	#FF2D2D30
������� � WindowStartupLocation$ CenterScreen=� � 	Resources    ButtonStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   	Direction	   ShadowDepth��%=  &	        �� " ��.���  � �� �	#FF6C757D��  � ��
 �������  � ��$ 16(���  � ��$
 Bold"���  � ��$	 300����  � ��$ 50����  � ��$ 
10,15,10,0q���  �@�Cursor$
 Hand����  � ��$ 0q���  �@J�Effect   $ Black��$ 8Q�$	 320Q�$	 4Q�.��� .+��� -   
 ��Name$
 btnConfigurarContraseña��$	��⚙��$��40��$��40��$
��Left=�$	��Top=�$��10q����	#FF6C757D
�������$��18(�$��0q� J� Effect   $ Black��$ 8Q�$	 320Q�$	 5Q�]� .+��� $	��Top=�$��Center=�$��	0,20,0,20q�.+��� $��Images/Guntner.png��$��Center=�$	��120��$��0,0,0,30q��� $	��Top=�$��Center=�$	��Top=�.+��� -   $
 btnIniciarMarcado��$��Iniciar marcado��#��[  ���	#FF28A745�� -   $!��Configurar Ruta del Archivo��#��[  �� -   $
 btnGuardarImagen��$+��%Guardar Previsualización como Imagen��#��[  ���	#FF17A2B8�� -   $��Configurar Plantillas��#��[  ���	#FF17A2B8�� -    ��Name$ PlantillasPanel��$y�	Collapsed=�$��Center=�.+��� -   $��Cargar Plantilla TXT��#��[  ���	#FF17A2B8�� -   $��Eliminar Plantilla��#��[  ���	#FF28A745�� -	   $"
 btnConfirmarPrevisualizacion��$!��Confirmar Previsualización��#��[  ���	#FF17A2B8�� $��Bottom=�$
��Auto=�$
��Auto=�$��20,20,20,20q�.���� .+��� -
   $ ContenedorVisual��$��10q��� -   $��txtContenidoArchivo�� �� TextWrapping$
 Wrap=�$��16(�$��Arial)�$
��Bold"�$	��700��$��15q�$��Center=�$	��Top=�
�������
������  �� 
IsReadOnly . $y�	Collapsed=�