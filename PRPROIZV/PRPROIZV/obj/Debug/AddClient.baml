   M S B A M L   `   `   `  ����C  ?PRPROIZV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PRPROIZVPRPROIZV  5   	       PRPROIZV.AddClient   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:PRPROIZV     5   	     � Title$  	AddClient��5   	   $	��650��6   $	��500��6(    � WindowStartupLocation$ CenterScreen=�64    � WindowStyle$
 None=�6Y    � 
ResizeMode$ NoResize=�6l   .��5	      �  � RowDefinitions 5
   
   �� 5      $��40 �6   6*   �� 5      $��* �6   6)   .+�5   
   �� ���	#FF349C3D$��5��6(    �� CornerRadius$ 0,0,0,0��6<   6V   �� 5   
   $��Resource/logo.png��6   $
��Left=�6+   �� 5   
   $��1��6    �� TextAlignment$ Center=�6$   $��Center=�6;   $��28(�6V   $)��#Добавление клиента��6d   6�   �� 5   
   -   5        $��RoolUp��5      
�������$	��Top=�67    � ToolTip$ Свернуть��6O   $��2��6b   $��Right=�6r   $��0,0,20,0q�6�   $��20��6�   $��__��6�   6�   �� 5   
   -   5         ��Name$ Close��5      
�������$	��Top=�69   $ Закрыть��6Q   $��2��6c   $��Right=�6s   $��20��6�   $	��✕��6�   6�   � 5   
   $��1��6    5      �� 5      $��10 �6    �� 5      $��40 �6    �� 5      $��10 �6    �� 5      $��40 �6    �� 5      $��10 �6    �� 5      $��40 �6    �� 5      $��10 �6    �� 5      $��40 �6    �� 5      $��10 �6    �� 5      $��40 �6    �� 5      $��10 �6    �� 5       $��40 �6    �� 5!      $��10 �6    �� 5"      $��40 �6    �� 5#      $��10 �6    �� 5$      $��40 �6    �� 5%      $��10 �6    �� 5&      $��40 �6    �� 5'      $��10 �6    �� 5(      $��40 �6    �� 5)      $��10 �6    �� 5*      $��40 �6    �� 5+      $��10 �6    �� 5,      $��40 �6    �� 5-      $��10 �6    	 � ColumnDefinitions	 5/      �� 50      $	��130 �6#   �� 51      $��* �6#   �� 52      $	��185 �6#   �� 53      $	��100 �6#   .+�55      �� $��Фамилия��6   $��1��6'   $��18(�64   $��Center=�6B   $��Center=�6]   �� 56      $��Имя��6   $��3��6#   $��18(�60   $��Center=�6>   $��Center=�6Y   �� 57      $��Отчество��6   $��5��6(   $��18(�65   $��Center=�6C   $��Center=�6^   �� 58      $��
Серия��6   $��7��6%   $��18(�62   $��Center=�6@   $��Center=�6[   �� 59      $��
Номер��6   $��7��6%   $��2��62   $��18(�6B   $��Center=�6P   $��Center=�6k   �� 5:      $��Дата рождения��6   $��9��6-   $��18(�6:   $��Center=�6H   $��Center=�6c   �� 5;      $��Индекс��6   $��11��6&   $��18(�64   $��Center=�6B   $��Center=�6]   �� 5<      $��
Город��6   $��13��6%   $��18(�63   $��Center=�6A   $��Center=�6\   �� 5=      $��
Улица��6   $��15��6%   $��18(�63   $��Center=�6A   $��Center=�6\   �� 5>      $��Дом��6   $��17��6#   $��18(�61   $��Center=�6?   $��Center=�6Z   �� 5?      $��Квартира��6   $��19��6(   $��18(�66   $��Center=�6D   $��Center=�6_   �� 5@      $��
Почта��6   $��21��6%   $��18(�63   $��Center=�6A   $��Center=�6\   �� 5B      -   5        
 ��Name$
 Surname��5B      $��1��6'    �� 	MaxLength$ 24��6�   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5C      -   5        $

 Name��5C      $��3��6$   $ 24��6�   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5D      -   5        $
 Forename��5D      $��5��6|   $ 24��6�   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5E      -   5        $
 Series��5E      $��7��6y   $ 4��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5F      -   5        $
 Number��5F      $��7��6y   $ 6��6�   $��0,0,10,0q�6�   $��3��6�   $��18(�6�   $��Center=�6�   (  "System.Windows.Controls.DatePicker  5G      -   5          Name$ DateBirthday��5G      $��9��6f   $��1��6s   $��2��6�   $��18(�6�   $��Center=�6�   �� 5H      -	   5        $
 Index��5H      $��11��6x   $ 6��6�   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5I      -
   5         ��Name$
 City��5I      $��13��6�   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5J      -   5        $ Street��5J      $��15��6z   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5K      -   5        $

 Home��5K      $��17��6w   $ 24��6�   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5L      -   5        $

 Flat��5L      $��19��6w   $ 24��6�   $��2��6�   $��1��6�   $��18(�6�   $��Center=�6�   �� 5M      -   5        $
 Email��5M      $��21��6M   $ 24��6[   $��2��6j   $��1��6~   $��18(�6�   $��Center=�6�   �� 5N      -   5        $	 Add��5N      $��4��64   $��23��6H   
���   �$��2q�6j   $��Добавить��6~   $��24(�6�   $��Center=�6�   $��Center=�6�    � 	Resources 5O       �� 
TargetType �� Property �� Value��%^   '��       �� 5P      " ��6   .�5Q      ��  �@��CornerRadius$ 5��69   5R      5S      5T      5U      5V      5W      