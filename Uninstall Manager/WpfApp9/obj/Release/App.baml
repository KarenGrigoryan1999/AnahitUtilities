   M S B A M L   `   `   `  ���� L  HUninstall Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp9WpfApp9      WpfApp9.App   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     'localclr-namespace:WpfApp9       �� 	Resources     ScrollThumbs �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName � RowDefinitions ]�Name ]� Thumb n�Name ]� IncreaseRepeatButton ��Name J� Opacity ]� DecreaseRepeatButton P� 
SourceName  ButtonSelectBrush �� Angle��%r  &	        '�� O    �� " n�.���  � �� �� " n�.R�� $
 Grid��.+��� $��Stretch=�$��Stretch=�$
��Auto��$
��Auto��
������ �� $ 
Rectangle1��$ 2��$��Stretch=�$��Stretch=�$
��Auto��$
��Auto��#��z�� P� 	 �@�Tag$
 
Horizontal��. ��� $ 
Rectangle1�� � ��$
 Auto���� $ 
Rectangle1�� � ��$ 7���� " ��.���  �@��IsFlicksEnabled .  ��  � ��
 �������  � ��
 ���� ��  � ��$ 8����  � �� �� " ��.R�� $ GridRoot��$��8��#��z�� �� $��0.00001* �.+�]� $ 
PART_Track��$��0����. |�.   n� $ Thumb��#��z��#���    �� $ PageUp��$��ScrollBar.PageDownCommand��$ 0Q�|�.   �� $ PageDown��$��ScrollBar.PageUpCommand��$ 0Q�|�.   P� $ Thumb��	 �@J�IsMouseOver
 . . ��� # �  $ Thumb�� � ��P� $ Thumb��	 �@n�
IsDragging
 . . ��� $ Thumb�� � ��
 �����P� 	 � {�
 .  . ��� $ Thumb�� � y�$ 	Collapsed=�P� 	 �@��Orientation$
 
Horizontal=�. ��� $ GridRoot�� �@�LayoutTransform �� $	 -90Q��� $ 
PART_Track�� �@�LayoutTransform �� $	 -90Q���  � ��$
 Auto����  � ��$ 8���� $ Thumb�� �@�Tag$ 
Horizontal���� $ PageDown�� � ��$ ScrollBar.PageLeftCommand���� $ PageUp�� � ��$  ScrollBar.PageRightCommand��