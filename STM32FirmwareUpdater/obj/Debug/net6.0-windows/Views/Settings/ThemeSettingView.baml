   M S B A M L   `   `   `  ����  STM32FirmwareUpdater:    4STM32FirmwareUpdater.Views.Settings.ThemeSettingView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      1local#STM32FirmwareUpdater.Views.Settings   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	      $	��350��5
      $	��300��5        � 
ResizeMode$  NoResize=�5       � ShowInTaskbar$ False��5       � WindowStartupLocation$ CenterOwner=�5      .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� 
������  �� SelectionMode$ Single=�5      ��5      �� *Themes+ �� SelectedItem 5      �� *CurrentTheme+��5      �� 5      .R�5      A�  A� Columns$ 1��6"   5      ��5      �� 5      .R�5       	� $��5q�5!      
������  d� 	IsChecked 5#      ��  �� Path$ 
IsSelected�	 �� RelativeSource	 �� 
 �� AncestorType"
 ��.��5$      �  � ColumnDefinitions 5%      �� 5&   "   $��* �63   �� 5'   "   $��3* �63   .+�5)      �� $��0��5*   !   $	��120��5+   !   $��20��5,   !   $��5q�5-   !   ��5.   !   �� *ShowcaseBrush+�� 5/      $��1��50   !   $��5,0,5,0q�51   !   $��Center=�52   !   ��53   !   �� *Name+54      55      56      58      �� 59   
   $��1��5:      $��Right=�5;       �� Orientation$ 
Horizontal=�5<      .+�5=      �� $	��100��5>      $��5q�5?       �� IsCancel$
 True��5@      .��5A      �� $ 
Horizontal=�6   .+�5B      �� $��15��5C      $��15��5D      $��3q�5E      ��5F      4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Close=�8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 �  ���� 5H      $��3q�5I      
���   �   s_Close#���   5K      5L      5M      5N      5O      5P      