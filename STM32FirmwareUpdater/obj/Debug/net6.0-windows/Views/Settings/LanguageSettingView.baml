   M S B A M L   `   `   `  ����  STM32FirmwareUpdater=    7STM32FirmwareUpdater.Views.Settings.LanguageSettingView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      $	��350��5	        � 
ResizeMode$  NoResize=�5
       � ShowInTaskbar$ False��5       � SizeToContent$ Height=�5       � WindowStartupLocation$ CenterOwner=�5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��5      $��10q�5       �� SelectionMode$ Single=�5      ��5      �� *
Cultures+ �� SelectedItem 5      �� *	Culture+��5      �� 5      .R�5      	� $	��200��5      $��50��5      
������ $��12(�5!      $��	UltraBold"�5"      ��5       �� *Value+ d� 	IsChecked 5#      ��  �� Path$ 
IsSelected�	 �� RelativeSource	 �� 
 �� AncestorType"
 ��5$      ��5&      �� 5'      .R�5(      A�  A� Columns$ 1��6"   5)      5+      �� 5,   
   $��1��5-      $��Right=�5.       �� Orientation$ 
Horizontal=�5/      .+�50      �� $	��100��51      $��5q�52      ��53      �� *AcceptCommand+.��54      �� $ 
Horizontal=�6   .+�55      �� $��15��56      $��15��57      $��3q�58      ��59      4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Check=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 � � ��� 5;      $��3q�5<      
���   �   s_Accept#���   5>      5?      5@      �� 5A      $	��100��5B      $��5q�5C       �� IsCancel$
 True��5D      .��5E      �� $ 
Horizontal=�6   .+�5F      �� $��15��5G      $��15��5H      $��3q�5I      ��5J        $ Close=�
 �  ���� 5L      $��3q�5M      
���   �  s_Cancel#���  5O      5P      5Q      5R      5S      5T      