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
    /// Logique d'interaction pour Accueil_Client.xaml
    /// </summary>
    public partial class Accueil_Client : Window
    {
       
        public int IdClient;
        

        public Accueil_Client(int IdClient)
        {
            this.IdClient = IdClient;
            InitializeComponent();

            //Récupération de l'IDClient pour autoriser l'accès à l'interface Créateur de Recettes OU donner le choix de devenir Créateur de Recettes
            string requete = "Select Client_idClient from `Createur de Recettes` where Client_idClient =" + Convert.ToString(IdClient) + " ;";
            
            
            if (IdClient == 1)
            {
                Gestionnaire_Button.Visibility = Visibility.Visible;
            }


            //Si le Client n'est pas encore CdR
            if (LinkBDD.requete(requete) == null || LinkBDD.requete(requete) == "")
            {
                Button_CdR.Visibility = Visibility.Hidden;
                Inscription_CdR.Visibility = Visibility.Visible;
            }
            else //si le client est CdR
            {
                Inscription_CdR.Visibility = Visibility.Hidden;
                Button_CdR.Visibility = Visibility.Visible;

            }

            //Affichage du nombre de Cooks disponibles 
            string requete1 = "Select Portefeuille_Cook from Client where idClient = " + Convert.ToString(IdClient) + ";";

            //Si l'utilisateur n'a pas de Cooks
            if (LinkBDD.requete(requete1) == null || LinkBDD.requete(requete1) == "" || LinkBDD.requete(requete1).Replace(",", "") == "0")
            {
                Solde.Text = "Solde : Aucun Cooks disponible";
            }
            else //Afficher son solde de cooks
            {
                Solde.Text = "Solde : " + LinkBDD.requete(requete1).Replace(",", "") + " Cooks";
            }


            //Affichage des recettes
            List<Recette_Clients> items = new List<Recette_Clients>();
            string requete2 = "Select Nom_Recette, Type_Recette, Descriptif_Recette, Prix_Recette from Recette;";
            string[] tab = LinkBDD.requete(requete2).Split(",");

            for (int i =0; i < tab.Length -1 ; i+=4)
            {
                
              items.Add(new Recette_Clients() { NomRecette = tab[i], TypeRecette = tab[i + 1], DescriptifRecette = tab[i + 2], PrixRecette = tab[i + 3] });
              
            }
            recettes.ItemsSource = items; 

        }


        private void CdR_Click(object sender, RoutedEventArgs e)
        {
            //Ouverture interface Créateur de Recettes
            CdR wnd = new CdR(IdClient);
            this.Close();
            wnd.ShowDialog();
        }

        private void Inscription_Click(object sender, RoutedEventArgs e)
        {
            //Demande de confirmation sur le choix de devenir Créateur de Recettes
            Confirmation_CdR wnd = new Confirmation_CdR(IdClient);
            this.Close();
            wnd.ShowDialog();

        }

        private void Commande_Click(object sender, RoutedEventArgs e)
        {
            Commande wnd = new Commande(IdClient);
            this.Close();
            wnd.ShowDialog();

        }

        private void Gestionnaire_Click(object sender, RoutedEventArgs e)
        {
            Gestionnaire wnd = new Gestionnaire();
            this.Close();
            wnd.ShowDialog();
        }
    }
}
