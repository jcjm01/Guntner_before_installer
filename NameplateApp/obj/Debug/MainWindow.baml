   M S B A M L   `   `   `  ����  NameplateApp    NameplateApp.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Marcado de Nameplate��5   	   $	��800��6&   $	��900��63   ���	#FF2D2D30
������� � WindowStartupLocation$ CenterScreen=�6i    � 	Resources 5
         ButtonStyle �� 
TargetType �� Property �� Value3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   	Direction	   ShadowDepth��%
  &	        �� 5   
   " ��6$   .�5      ��  � �� �	#FF6C757D6+   �� 5       � ��
 �����6+   �� 5       � ��$ 16(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$	 300��6&   �� 5       � ��$ 50��6'   �� 5       � ��$ 
10,15,10,0q�6'   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 0q�60   �� 5       �@J�Effect 5        5      $ Black��6'   $ 8Q�65   $	 320Q�6D   $	 4Q�6T   5      5      5      .��5      � .+�5   
   �� -   5        
 ��Name$
 btnConfigurarContraseña��5      $	��⚙��5       $��40��5!      $��40��5"      $
��Left=�5#      $	��Top=�5$      $��10q�5%      ���	#FF6C757D
�������$��18(�5)      $��0q�5*       J� Effect 5+        5,      $ Black��6#   $ 8Q�61   $	 320Q�6@   $	 5Q�6P   5.      ]� 51   
   .+�53      �� $	��Top=�6   $��Center=�6.   $��	0,20,0,20q�6K   .+�54      �� $��Images/Guntner.png��6   $��Center=�64   $	��120��6Q   $��0,0,0,30q�6^   55      �� 58      $	��Top=�6   $��Center=�6.   $	��Top=�6K   .+�59      �� -   5        $
 btnIniciarMarcado��59      $��Iniciar marcado��64   #��[  6k   ���	#FF28A7456�   �� 5:      -   5        $!��Configurar Ruta del Archivo��5:      #��[  6]   �� 5;      -   5        $
 btnGuardarImagen��5;      $+��%Guardar Previsualización como Imagen��63   #��[  6�   ���	#FF17A2B86�   �� 5<      -   5        $��Configurar Plantillas��5<      #��[  6\   ���	#FF17A2B86�   �� 5?      -   5         ��Name$ PlantillasPanel��5?      $y�	Collapsed=�66   $��Center=�6M   .+�5@      �� -   5        $��Cargar Plantilla TXT��5@      #��[  6b   ���	#FF17A2B86�   �� 5A      -   5        $��Eliminar Plantilla��5A      #��[  6Z   ���	#FF28A7456   5B      �� 5D      -	   5        $"
 btnConfirmarPrevisualizacion��5D      $!��Confirmar Previsualización��6?   #��[  6�   ���	#FF17A2B86�   5E      �� 5H      $��Bottom=�6   $
��Auto=�63   $
��Auto=�6X   $��20,20,20,20q�6{   .��5I      �� .+�5K      �� -
   5        $ ContenedorVisual��5K   !   $��10q�6;   �� 5M      -   5        $��txtContenidoArchivo��5M       �� TextWrapping$
 Wrap=�5N   
   $��16(�5O   
   $��Arial)�5P   
   $
��Bold"�5Q   
   $	��700��5R   
   $��15q�5S   
   $��Center=�5T   
   $	��Top=�5U   
   
�������
������  �� 
IsReadOnly . $y�	Collapsed=�5Y   
   5[      5\      5]      5^      5_      