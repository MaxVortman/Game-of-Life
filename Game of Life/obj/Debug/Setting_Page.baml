   M S B A M L   `   `   `  ����G  CGame of Life, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Game_of_LifeGame_of_Life  5          Game_of_Life.Setting_Page   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Game_of_Life     5        N� Title$  Setting_Page��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $��5* �6   �� 5      $	��30* �6   �� 5      $��5* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      �� 5      $
��Auto �6   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��1��6   $��2��6    $��Stretch=�6-   $��12(�6I   .��6W   
Game Mode:6c   �� 5    
   -   5        $��	Game_Mode��5    0   $��1��6   $��3��6#   .@�5!      �� 
Standart6"   �� 5"      	Special6!   5#      ]� 5$   
   $��1��6   $��5��6$   $
��Auto��61   $
��Auto��6?   .+�5%      �� $	��Top=�6   $��Stretch=�6)   $��12(�6E   .��6S   Cells Count:6a   �� 5&      -   5         ��Name$ Cells_Count��5&   �   $��Right=�6l    �� TextAlignment$ Right=�6�   $��25��6�   $	��Top=�6�   ��6   ��  �� ElementName$ slValue�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5'      -   5        $��slValue��5'   l    � Maximum$	 100Q�6   	 �� TickPlacement$	 BottomRight=�6#   
 �� TickFrequency$
 5Q�6?    �� IsSnapToTickEnabled . $��Bottom=�6{   5(      �� 5*   
   -   5        $��1��5*      $��8��6!   .��6C   OK6G   ]� 5,   
   $��1��6   $��0��6$   $��2��61   $
��Auto��6B   $
��Auto��6P   .+�5-      �� -   5        $��
CheckCells��5-      $��Draw cells?��6)   $	��Top=�6?   6V   �� 5.      -   5        $��	CheckStat��5.      $(��"Do you want to display statistics?��6(   $��Bottom=�6U   6o   �� 5/      -   5        $��	CheckScan��5/      $��Do you want to scan?��6(   6I   50      51      52      