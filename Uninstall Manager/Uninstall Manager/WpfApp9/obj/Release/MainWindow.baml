   M S B A M L   `   `   `  ���� L  HUninstall Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp9WpfApp9      WpfApp9.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp9       � Title$  Unistall Manager��$	��550��$	��750�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=� � WindowStyle$
 None=�
���   �
������  � AllowsTransparency . .��� -    �Name$ Form1��$��30q�
������� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 15Q�   	Direction$	 -90Q�	   RenderingBias$	 Quality=�
   ShadowDepth$
 2Q�.+��� $
��Left=�$	��152��$��0,-1,0,0q�$	��Top=�$	��690��$��logo.png���� -    ��Name$
 exit��$��r��$
��Left=�$��	660,5,0,0q�$	��Top=�$��Webdings)�
������� � Cursor$
 Hand���� -   $
 hide��$��_��$
��Left=�$��	641,2,0,0q�$	��Top=�$��Verdana Pro SemiBold)�
�������$
 Hand���� -   $ title��$��Uninstall Manager��$
��Left=�$��	67,13,0,0q�$	��Top=�$��36��
�������$��18(��� $
��Left=�$��73��$��3,14,0,0q�$	��Top=�$��62��$��if_edit-delete_23231.ico�� J� RenderTransformOrigin$ 0.516,0.712&��� -    ��Name$ listView��$��	2,97,2,37q�
�������
���   ����� .R���  �� Orientation$ 
Horizontal=�$��60��$��Stretch=�.+��� $��32��$��32������ *	ImageData+�� $ Vertical=�$��Center=�$��5,0,0,0q�.+��� $��Center=�$��4,0,0,0q�$��14(����� *Title+�� $��Center=�$��4,0,0,0q�$��12(����	#FF383838���� *
Provider+�� $��Center=�$��4,0,0,0q�$��12(����	#FF383838���� *Size+�� -   $ listView_Copy��$��	2,97,2,37q�
�������$y�Hidden=����� .R��� $ 
Horizontal=�$��46��$��Stretch=�.+��� $��32��$��32������ *	ImageData+�� $ Vertical=�$��Center=�$��5,0,0,0q�.+��� $��Center=�$��4,0,0,0q�$��14(����� *Title+�� $��Center=�$��4,0,0,0q�$��12(����	#FF383838���� *
Provider+�� $��Center=�$��4,0,0,0q�$��12(����	#FF383838���� *Size+�� -    ��Name$ textBox��$
��Left=�$��23��$��
496,64,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��186�� �� SelectionOpacity$ 5Q� �� SelectionBrush �	#FF252525���	#FFB99828 J� OpacityMask �	#FFDADADA$��14(����	#FFEEAE3E$ 0.853,0.217&����	#FF595959�� $
��Left=�$��22��$��
469,63,0,0q�$	��Top=�$��29��$��
search.png��$ -0.621,-0.208&�$y�Visible=� J� Opacity$	 0.5Q��� -    ��Name$ 	uninstall��$��Unistall��$
��Left=�$��574,459,0,0q�$	��Top=�$	��111��$��23��
����������	#FFB4B4B4{�.  �� -	   $ programs_count��$��
Loading...��$
��Left=�$��	67,33,0,0q�$	��Top=�$��36��
�������$��14(��� -
   $ programs_size��$��
Loading...��$
��Left=�$��	67,48,0,0q�$	��Top=�$��31��
�������$��14(�