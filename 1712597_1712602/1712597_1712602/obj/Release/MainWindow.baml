   M S B A M L   `   `   `  ���� J  F1712597_1712602, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:_1712597_1712602_1712597_1712602  !    _1712597_1712602.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QMaterialDesignThemes.Wpf, Version=3.0.1.882, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  *localclr-namespace:_1712597_1712602  -   $��Roboto)�$��12(�  � Title$  MM Music��$	��530��$	��900�� � WindowStartupLocation$ CenterScreen=�
����İ�.��#  MaterialDesignThemes.Wpf.Card  $��Stretch=�   UniformCornerRadius$ 0Q�.���  J� RenderTransformOrigin$ 0.5,0.5&�.+�  $ 0Q�$	��350��$	��300��$	��Top=�$
��Left=����	#FFDCD9C8.��� $��10q� � RowDefinitions �� $��9* ��� $��* ��� $��* �.+��� $��0�� �� Orientation$ Vertical=�$��Center=�.+��� $	��300��$��logo.png��$��0,50,0,0q� �� BitmapScalingMode$ HighQuality=��� $��1��$ 
Horizontal=�$	��300��$��Center=�$��0,15,0,0q�.+��� -   $��	timeStart��
���   �$��Center=�00:00�� -   $��timeEnd��
���   �$��Center=�$��	240,0,0,0q�$��Right=�00:00�� -   $��sTimerMusic��$��	15,0,15,0q�$	��300��$��Bottom=�$��2�� �� TickFrequency$ 5Q� �� Orientation$ 
Horizontal=�	 � Minimum$	 0Q�
 � Value$
 0Q����	#FFED7945  $ 0Q�$	��350��$	��200��$��Bottom=�$
��Left=�
�������.��� $��10q� �� $��3* ��� $��5* ��� $��2* �.+��� $��0��$ 
Horizontal=�$	��350��$��Center=�.+��� -    0  ,MaterialDesignFloatingActionMiniAccentButton#��[   � ToolTip$4 .Resource name: MaterialDesignRaisedLightButton��$��0q�$��30��$��30��$��Right=�$��	40,0,20,0q���Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon  -   $��piRepeat��   Kind$ Repeat=�$��30��$��30��$��Center=�
��������� -   #��[  $��0q�$��36��$��36����Z� ��Z� .��  $ SkipPrevious=�$��30��$��30��$��Center=�
���   ��� -   #��[  $��56��$��56��$��0q�$
��15,0q���Z� ��Z� .��  -	   $��piPlay��$ PlayCircleOutline=�$��56��$��56��
���   ��� -
   #��[  $��36��$��36��$
��Left=�$��Center=���Z� ��Z� .��  $ SkipNext=�$��30��$��30��$��Center=�
���   ��� -   #��[  $��0q�$��36��$��36��$��Right=�$��20,0,0,0q���Z� ��Z� .��  -   $��	piShuffle��$ ShuffleDisabled=�$��30��$��30��$��Center=�
��������� $��1��$��10,20,10,10q�.+��� -   $��txbNameArtist��$��20(�$��Roboto)�
���   �$��Center=��� -   $��txbSongTitle��$��30(�$��Roboto)�
���   �$��Center=�MMM�� $��2��$ 
Horizontal=�$��10,0,0,0q�.+�  -   $��piVol��$ Audio=�$��20��$��20��$
��Left=�$��Center=����	#FF0E0F0F�� -   $	��vol��$��
15,0,15,10q�$��Bottom=�$��2��$ 5Q�$	��240��$ 
Horizontal=�$	 0Q� � Maximum$ 1Q�$	
 0.5Q����	#FFED7945�� -   $��txbVol��$��Right=�$��Center=�
���   �$��roboto)�50  $ 0Q�$	��550��$	��150��$��0,0,0,0q�$	��Top=�$��Right=����#1E3D59.��� $��10q� �� $
��2.5* ��� $��5* �.+��� $��0��$ 
Horizontal=�$	��500��$��Center=�.+�'  !MaterialDesignThemes.Wpf.PopupBox   3 /MaterialDesignMultiFloatingActionAccentPopupBox#��[ 
�������   PlacementMode$ RightAndAlignMiddles=�+   $ToggleCheckedContentCommandParameter$ wowsers��$��40��$��40�����#408ec6   ToggleCheckedContent   $ Buffer=�$��25��$��25��$��Center=�$��Center=�   PopupContent. �� $ 
Horizontal=�.+��� -   $ Add PlayList��.��  $ PlaylistAdd=�$��25��$��25��$��Center=�
���   �$��Center=��� -   $ Load PlayList��.��  $ Album=�$��25��$��25��$��Center=�$��Center=�
���   �)  #MaterialDesignThemes.Wpf.DialogHost  -   $��Center=�$��Center=�   DialogContent �� $��16q�.+��� Add a new playlist.�� -   $��
txbNewName��$��0 8 0 0q�$��Stretch=��� -   $��ManyCheckBoxes��$ 
Horizontal=�$��Right=�.+��� -   $��checkTxt��$��0,0,10,0q�$
��.txt�� d� 	IsChecked$
 TrueY�  MaterialDesignCheckBox#��[ �� -   $��checkXml��$
��.xml��#��[ �� $ 
Horizontal=�$��Right=�.+��� -     MaterialDesignFlatButton#��[ $��ACCEPT�� �� 	IsDefault . $��0 8 8 0q�$2��,materialDesign:DialogHost.CloseDialogCommand���� #��[ $��CANCEL�� �� IsCancel . $��0 8 8 0q�$2��,materialDesign:DialogHost.CloseDialogCommand��.���� $ Save PlayList��   OpenDialogCommand#��Z .��  $ PlaylistCheck=�$��25��$��25��$��Center=�$��Center=�
���   ��� -   $ Load WishList��.��  $ Heart=�$��25��$��25��$��Center=�$��Center=����#ca1f7b�� $��1��$��0q�$ 
Horizontal=�.+��� -   $��txbQuatitySong��$��50,0,0,0q�$��60��$��Center=�$��Bottom=����#f5f0e1
0 Song /�� -   $��txbTotalTime��$��50��$��Center=�$��Bottom=����#f5f0e100:00�� $��15,0,0,0q�$	��200��$	��100��$��	LogoT.png��$��Center=�$��Center=�$ HighQuality=�  $ 0Q�$	��550��$	��320��$��0,0,0,30q�$��Bottom=�$��Right=����#408ec6.��&   System.Windows.Controls.DataGrid  -   $��
dgListSong��$   HorizontalScrollBarVisibility$ Disabled=�$��0 8 0 0q�   CanUserSortColumns .    CanUserAddRows .     AutoGenerateColumns .  -  'MaterialDesignThemes.Wpf.DataGridAssist   CellPadding$ 13 8 8 8q�   ColumnHeaderPadding$ 8q����� *	Playlists+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ WindowL�  �� Mode$  OneWay=�! �� UpdateSourceTrigger$! PropertyChanged=�"   Columns" 1  +MaterialDesignThemes.Wpf.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn#   Header$# #�� 5 1MaterialDesignDataGridTextColumnPopupEditingStyle1	  +System.Windows.Controls.DataGridBoundColumn$ 	  EditingElementStyle#$ [ % 	  Binding% �� *Number+&   HeaderStyle& �� ' �� 
TargetType=
  7System.Windows.Controls.Primitives.DataGridColumnHeader"' 
  & "MaterialDesignDataGridColumnHeader( �� BasedOn#( [ .��� ) �� Property) � ��* �� Value$* Center=��� ) � ��* �� .R��� $
��Wrap=�+ �� TextAlignment$
+ Left=����� , 	  ElementStyle, �� "' ��.��� ) � ��$* Center=��� ) � ��$* Center=�4  .System.Windows.Controls.DataGridTemplateColumn  -   CellTemplate- �� .R��� -   #��[  $��25��$��25����Z� ��Z� .��  $��20��$��20��
���   � �� *IconSong.Kind+.   	CellStyle. �� *  $System.Windows.Controls.DataGridCell"'  .��� ) � ��$* 0q��� ) � ��
* ���� �� ) � ��* �	#FF424242  /   Width5  /System.Windows.Controls.DataGridLengthConverter$	/ 130 $# Title��#$ [ % �� *Title+& �� "' 
 #( [ .��� ) � ��$* Stretch=��� ) � ��* �� .R��� $
��Wrap=�$
+ Left=����� , �� "' ��.��� ) � ��$
* Left=��� ) � ��$* Center=�  $	/ 100 $# Artist��#$ [ % �� *Artist+& �� "' 
 #( [ .��� ) � ��$
* Left=��� ) � ��* �� .R��� $
��Wrap=�$
+ Left=����� , �� "' ��.��� ) � ��$
* Left=��� ) � ��$* Center=�  $# Album��0   	MaxLength$	0 255��$	/ 100 #$ [ % �� *Album+& �� "' 
 #( [ .��� ) � ��$
* Left=��� ) � ��* �� .R��� $
��Wrap=�$
+ Left=����� , �� "' ��.��� ) � ��$
* Left=��� ) � ��$* Center=�  $
# Time��$/ 70 #$ [ % �� *Time+& �� "' 
 #( [ .��� ) � ��$* Stretch=��� ) � ��* �� .R��� $
��Wrap=�$+ Center=����� , �� "' ��.��� ) � ��$* Center=��� ) � ��$* Center=�  $# Favorite��- �� .R�d� -    ) %MaterialDesignFlatPrimaryToggleButton#��[ $+ %MaterialDesignFlatPrimaryToggleButton�� �� *IsFav+.��  $ HeartOutline=�$��21��$��21��. �� "'  .��� ) � ��$* 0q��� ) � ��
* ���� �� ) � ��* �	#FF424242  $ 0Q�$	��550��$��30��$��Bottom=�$��Right=����#408ec6.���� -    $��	btnDelete��$��0,0,10,0q�#��[  $��0q�$��30��$��30��$��Right=���Z� ��Z� .��  $ DeleteForever=�$��30��$��30��$��Center=����#408ec6
�������