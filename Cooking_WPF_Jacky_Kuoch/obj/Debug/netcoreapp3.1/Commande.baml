   M S B A M L   `   `   `  ����R  NCooking_WPF_Jacky_Kuoch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Cooking_WPF_Jacky_KuochCooking_WPF_Jacky_Kuoch  5   	   &     Cooking_WPF_Jacky_Kuoch.Commande   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:Cooking_WPF_Jacky_Kuoch     5   	     � Title$  Commande��5   	   $	��451��6   $	��669��6'   
���iii�.��5	      � .+�5
   
   �� $
��Left=�6   $��68��6/   $��16,6,0,0q�6;   $��Commande��6M   $
��Wrap=�6]   $	��Top=�6q   $	��199��6�   $��50(�6�   $��Brush Script MT)�6�   $��Normal"�6�   $��Normal$�6�   
������� �� TextAlignment$
 Left=�6�   �� 5   
   $
��Left=�6   $	��327��6/   $��	14,80,0,0q�6<   
�������$	��Top=�6^   $	��201��6v   �� 5   
   $
��Left=�6   $��30��6/   $��	22,86,0,0q�6;   $��Recette��6N   $
��Wrap=�6]   $	��Top=�6q   $	��112��6�   $��25(�6�   $��
Montserrat)�6�   
�������6�   �� 5   
   $
��Left=�6   $��21��6/   $��
24,132,0,0q�6;   $��Nom Recette��6O   $
��Wrap=�6b   $	��Top=�6v   $	��174��6�   $��18(�6�   $��
Montserrat)�6�   
�������6�   �� 5   
   -   5         ��Name$ 
NomRecette��5   �   $
��Left=�6   $��30��6-   $��
24,159,0,0q�69   $�� ��6M    �� TextWrapping$
 Wrap=�6U   $	��Top=�6i   $	��174��6�   $��15(�6�   $��Montserrat Light)�6�   $
��Bold"�6�   �� 5   
   $
��Left=�6   $��21��6/   $��24,0,0,0q�6;   $��Quantité Recette��6M   $
��Wrap=�6e   $��Center=�6y   $	��174��6�   $��18(�6�   $��
Montserrat)�6�   
�������6�   �� 5   
   -   5        $ QuantiteRecette��5   �   $
��Left=�6   $��30��6-   $��
23,234,0,0q�69   $�� ��6M   $
 Wrap=�6U   $	��Top=�6i   $	��174��6�   $��15(�6�   $��Montserrat Light)�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ Appui_Bouton��5     $��Ajouter��6   $
��Left=�6#   $��36��6>   $��
55,291,0,0q�6J   $	��Top=�6^   $	��110��6v   $��20(�6�   ���	#FFFFFBFB$��Montserrat Light)�6�   $
��Bold"�6�    J� RenderTransformOrigin$ 
0.753,0.85&�6�   6  �� 5   
   -   5         ��Name$ Erreur��5   y   $��La recette n'existe pas��6   $
��Left=�62   $��
16,354,0,0q�6M   $	��Top=�6a   $	��199��6�   $��17(�6�   $��Montserrat Light)�6�   $
��Bold"�6�   
�������$��39��6�   $y�Hidden=�6�   �� 5   
   -   5        $��Valider��5      $
��Left=�6#   $��35��6>   $��245,368,0,0q�6J   $	��Top=�6_   $	��121��6w   $��20(�6�   ���	#FFFFFBFB$��Montserrat Light)�6�   $
��Bold"�6�   $ 
0.753,0.85&�6�   6�   �� 5   
   -   5        $��Annuler��5      $
��Left=�6#   $��35��6>   $��516,361,0,0q�6J   $	��Top=�6_   $	��120��6w   $��20(�6�   ���	#FFFFFBFB$��Montserrat Light)�6�   $
��Bold"�6�   6�   �� 5   
   -   5        $��recettes��5   *   $��245,244,33,82q�6    �� View 5      �� 5      .P�5      �� $��Recette��6%    �� Width$	 200��66   	 �� DisplayMemberBinding	 6B   �� *
NomRecette+�� 5      $��Quantité Commandée��6%   $	 190��6A   	 6M   �� *QuantiteCommandee+5      5!      �� 5#   
   -   5        $��listerecettes��5#   *   $��245,80,33,246q�6    5$      �� 5%      .P�5&      �� $��Recette��6%   $	 145��66   	 6B   �� *
NomRecette+�� 5'      $
��Type��6%   $ 70��63   	 6>   �� *TypeRecette+�� 5(      $��
Descriptif��6%   $	 130��69   	 6E   �� *DescriptifRecette+�� 5)      $
��Prix��6%   $ 40��63   	 6>   �� *PrixRecette+5*      5,      �� 5-   
   $
��Left=�6   $��41��6/   $��	412,0,0,0q�6;   $��Panier��6N   $
��Wrap=�6\   $��Center=�6p   $��74��6�   $��30(�6�   $��Brush Script MT)�6�   $��Normal"�6�   $��Normal$�6�   
�������$
 Left=�6�   5/      50      