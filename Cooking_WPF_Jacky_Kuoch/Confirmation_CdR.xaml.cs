using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cooking_WPF_Jacky_Kuoch
{
    /// <summary>
    /// Logique d'interaction pour Confirmation_CdR.xaml
    /// </summary>
    public partial class Confirmation_CdR : Window
    {

        private int IDClient;

        public Confirmation_CdR(int IDClient)
        {
            this.IDClient = IDClient;
            InitializeComponent();
        }

        private void Non_Click(object sender, RoutedEventArgs e)
        {
            Accueil_Client wnd = new Accueil_Client(IDClient); //on retourner à la page d'accueil client
            this.Close();
            wnd.ShowDialog();
        }

        private void Oui_Click(object sender, RoutedEventArgs e)
        {
            string requete = "INSERT into `projet_cooking`.`Createur de Recettes`(`Nombre_Recettes_Crees`,`Client_idClient`) VALUES (" + 0 +","+ IDClient + ") ;";
            LinkBDD.requete(requete);
            Accueil_Client wnd = new Accueil_Client(IDClient);
            this.Close();
            wnd.ShowDialog();

        }
    }
}
