   M S B A M L   `   `   `  ����L  HUninstall Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp9WpfApp9  5   	       WpfApp9.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp9     5   	     � Title$  Unistall Manager��5      $	��550��6   $	��750��6+    � WindowStartupLocation$ CenterScreen=�67    � 
ResizeMode$ CanMinimize=�6\    � WindowStyle$
 None=�6u   
���   �
������  � AllowsTransparency . .��5	      � -   5         �Name$ Form1��5	      $��30q�6   
������� J� Effect 5
   
   3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6      	Direction$	 -90Q�6/   	   RenderingBias$	 Quality=�6?   
   ShadowDepth$
 2Q�6W   .+�5   
   �� $
��Left=�6   $	��152��6+   $��0,-1,0,0q�68   $	��Top=�6J   $	��690��6b   $��logo.png��6n   �� 5   
   -   5         ��Name$
 exit��5      $��r��6   $
��Left=�6*   $��	660,5,0,0q�6E   $	��Top=�6X   $��Webdings)�6p   
������� � Cursor$
 Hand��6�   6�   �� 5   
   -   5        $
 hide��5      $��_��6   $
��Left=�6*   $��	641,2,0,0q�6E   $	��Top=�6X   $��Verdana Pro SemiBold)�6p   
�������$
 Hand��6�   6�   �� 5   
   -   5        $ title��5      $��Uninstall Manager��6   $
��Left=�6;   $��	67,13,0,0q�6V   $	��Top=�6i   $��36��6�   
�������$��18(�6�   �� 5   
   $
��Left=�6   $��73��6+   $��3,14,0,0q�67   $	��Top=�6I   $��62��6a   $��if_edit-delete_23231.ico��6l    J� RenderTransformOrigin$ 0.516,0.712&�6�   �� 5   
   -   5         ��Name$ listView��5      $��	2,97,2,37q�6%   
�������
���   ���5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   $��46��6:   $��Stretch=�6G   .+�5      �� $��32��6=   $��32��6H   ��6    �� *	ImageData+6H   �� 5      $ Vertical=�6%   $��Center=�6<   $��5,0,0,0q�6W   .+�5      �� $��Center=�6?   $��4,0,0,0q�6Z   $��14(�6k   ��6(   �� *Title+6k   �� 5      $��Center=�6B   $��4,0,0,0q�6]   $��12(�6n   ���	#FF383838��6(   �� *
Provider+6|   5      5      5      5      �� 5    
   -   5        $ listView_Copy��5       $��	2,97,2,37q�6*   
�������$y�Hidden=�6Q   ��5!      �� 5"      .R�5#      �� $ 
Horizontal=�6!   $��46��6:   $��Stretch=�6G   .+�5%      �� $��32��6=   $��32��6H   ��6    �� *	ImageData+6H   �� 5&      $ Vertical=�6%   $��Center=�6<   $��5,0,0,0q�6W   .+�5'      �� $��Center=�6?   $��4,0,0,0q�6Z   $��14(�6k   ��6(   �� *Title+6k   �� 5(      $��Center=�6B   $��4,0,0,0q�6]   $��12(�6n   ���	#FF383838��6(   �� *
Provider+6|   5)      5*      5+      5-      �� 5.   
   -   5         ��Name$ textBox��5.      $
��Left=�6#   $��23��6>   $��
496,64,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��186��6�    �� SelectionOpacity$ 5Q�6�    �� SelectionBrush �	#FF252525���	#FFB99828 J� OpacityMask �	#FFDADADA$��14(�6   ���	#FFEEAE3E$ 0.853,0.217&�6E  ���	#FF5959596i  �� 5/   
   $
��Left=�6   $��22��6+   $��
469,63,0,0q�67   $	��Top=�6K   $��29��6c   $��
search.png��6n   $ -0.621,-0.208&�6�   $y�Visible=�6�    J� Opacity$	 0.5Q�6�   �� 50   
   -   5         ��Name$ 	uninstall��50      $��Unistall��6$   $
��Left=�67   $��574,459,0,0q�6R   $	��Top=�6g   $	��111��6   $��23��6�   
����������	#FFB4B4B4{�.  6�   �� 51   
   -	   5        $ programs_count��51      $��
Loading...��6(   $
��Left=�6=   $��	67,33,0,0q�6X   $	��Top=�6k   $��36��6�   
�������$��14(�6�   52      53      