   M S B A M L   `   `   `  ????O  KSTM32FirmwareUpdater, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:STM32FirmwareUpdater.ViewsSTM32FirmwareUpdater.Views  5      *    $STM32FirmwareUpdater.Views.ShellView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a;icons-http://metro.mahapps.com/winfx/xaml/iconpacks   5      6local(clr-namespace:STM32FirmwareUpdater.Views   5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5	        ? Title$  STM32 Firmware Updater??5
      $	??660??5      $	??450??5       ? 
ResizeMode$ NoResize=?5      (  "MahApps.Metro.Controls.MetroWindow   TitleBarHeight$ 45??5         TitleCharacterCasing$ Normal=?5         RightWindowCommands 5      +  %MahApps.Metro.Controls.WindowCommands  5   
   .@?5      ?? $	??5,0q?5       )  %MahApps.Styles.Button.Calendar.Header#??[  5        
s_Language ? ToolTip# ?  5      ??5      ?? *LanguageSettingCommand+.??5      ?? $??18??6   ??6#   4  .MahApps.Metro.IconPacks.MaterialImageExtension     Kind$ Earth=?8  2MahApps.Metro.IconPacks.BasePackIconImageExtension   Brush
 ?????5      ?? 5      $	??5,0q?5      #??[  5        s_Theme# ?  5      ??5      ?? *ThemeSettingCommand+.??5      ?? $??18??6   ??6#     $ TshirtV=?
 ?????5       ?? 5!      $	??5,0q?5"      #??[  5#       5$      ?? *	Version+ ?? Mode$ OneWay=?.??5%      ?? $??18??6   ??6#     $ Information=?
 ?????5&      5'      	 ? 	Resources	 5)        MahApps.Styles.TextBlock
 ?? BasedOn ?? 
TargetType ?? Property ?? Value??%?   '??       7  ?? 5*   
   8
   " ??6D   .?5+      ??  ? ??$ Center=?62   ?? 5,       ? ??$ 14(?6)   ?? 5-       ? ??$ 3q?6'   5.      5/         IconTemplate 50      ?? 51   
   .R?52      ?? $??30??53      $??30??54      $??5q?55      ??56        $ SailBoat=?
 ?????58      .??5:      ? $??1??6   $??30,10,30,20q?6    ? RowDefinitions 5;   
   ?? 5<      $
??Auto ?6   ?? 5=      $
??Auto ?6   ?? 5>      $
??Auto ?6   ?? 5?      $??* ?6   ?? 5@      $
??Auto ?6   .+?5B   
   ??  ?? Orientation$ 
Horizontal=?6   .+?5C      ?? $??20??5D      $??20??5E      $??3q?5F      ??5G        $ Harddisk=?  MahApps.Brushes.Accent# [ ?? 5I        s_FirmwareFile#???  6   5J      ? 5K   
   $??1??6    ? ColumnDefinitions 5L      ?? 5M      $??* ?6#   ?? 5N      $
??Auto ?6#   .+?5P      ?? $??3q?5Q      $??Center=?5R        s_FirmwareWatermask*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark# ?  5S       ?? 
IsReadOnly$
 True??5T      ??5U      ??  ?? Path$ FirmwarePath??? 5V      $??1??5W      $??3q?5X      #??[  5Z        s_Browse# ?  5[      ??5Y      ?? *BrowseFirmwareFileCommand+.??5\      ?? $??25??5]      $	??5,0q?5^      ??5_        $ FolderOpenOutline=?# [ 5a      5b      ?? 5c   
   $??2??5d      $??0,0,0,5q?5e      $ 
Horizontal=?5f      .+?5g      ?? $??3q?6   ??6     $ Devices=?# [ ?? 5h        s_DeviceList#???  6   5i      ?? 5j   
   $??3??5k      
???????$??1q?5m       	 MahApps.Styles.ListView#??[	 5p      ??5n      ?? $ 
DfuDevices? ?? SelectedItem 5o      ?? $ CurrentDevice? ?? View 5q      ?? 5r      .P?5s      ??  ?? Width$	 100??5t       
 s_Index#??? 
 5v       ?? DisplayMemberBinding 5u      ?? $ ID?5v      ?? 5w      $	 200??5x        s_Description#???  5z       5y      ?? $ Description?5z      ?? 5{      $	 500??5|       
 s_Path#???  5~       5}      ?? $
 Path?5~      5      5?      ? 5?   
   $??4??6    5?      ?? 5?      $??* ?6#   ?? 5?      $
??Auto ?6#   .+?5?      -  'MahApps.Metro.Controls.MetroProgressBar  $??5q?5?      #???  5?       ? IsIndeterminate 5?      ?? *	IsRunning+?? 5?      -  'MahApps.Metro.Controls.VisibilityHelper   	IsVisible 6   ?? *	Upgrading+.+?5?        $??5??5?      $??5q?5?      #???  5?       ? Value 5?      ?? *
Progress+?? 5?      $??Center=?6   ??69   ?? *ProgressText+5?      A? 5?      $??1??6    A? Rows$ 1??6*   .+?5?      ?? $??5q?6   ??6$   ?? *RefreshCommand+.??5?      ?? $ 
Horizontal=?6!   .+?5?      ?? $??15??5?      $	??3,0q?5?      ??5?        $ FileRefreshOutline=?# [ ?? 5?      $	??3,0q?5?      $??Center=?5?      #??[ 5?        	s_Refresh#???  5?      5?      5?      ?? 5?      $??5q?6   ??6$   ?? *UpgradeCommand+.??5?      ?? $ 
Horizontal=?6!   .+?5?      ?? $??15??5?      $	??3,0q?5?      ??5?        $ ArrowUpBoldCircle=?# [ ?? 5?      $	??3,0q?5?      $??Center=?5?      #??[ 5?        	s_Upgrade#???  5?      5?      5?      5?      5?      5?      5?      