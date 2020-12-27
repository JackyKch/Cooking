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
    /// Logique d'interaction pour CdR.xaml
    /// </summary>
    public partial class CdR : Window
    {
        private int IdClient;
        public CdR(int IdClient)
        {
            this.IdClient = IdClient;
            InitializeComponent();

            string requete = "Select Portefeuille_Cook from Client where idClient = " + Convert.ToString(IdClient) + ";";

            //Initialisation du solde de cooks du CdR connecté
            if (LinkBDD.requete(requete) == null || LinkBDD.requete(requete) == "" || LinkBDD.requete(requete).Replace (",","") == "0")
            {
                Solde.Text = "Solde : Aucun Cooks disponible";
            }
            else
            {
                Solde.Text = "Solde : " + LinkBDD.requete(requete).Replace(",", "") + " Cooks";
            }


            //Liste des recettes créées par le Cdr et leurs quantités vendues
            List<Recette> items = new List<Recette>();
            requete = "Select Nom_Recette, QuantitéVendue_Recette from Recette where `Createur de Recettes_Client_idClient` = " + Convert.ToString(IdClient) + ";";
            LinkBDD.requete(requete);
            string[] tab = LinkBDD.requete(requete).Split(",");


            for (int i =0; i<tab.Length-1; i+=1)
            {
                if (i%2==0)
                    items.Add(new Recette() { nomRecette = tab[i], QuantiteVendue = Convert.ToInt32(tab[i+1]) });
            }
            recettes.ItemsSource = items;
            
        }

        private void NewRecette_Click(object sender, RoutedEventArgs e)
        {
            int IDCdR = Convert.ToInt32(LinkBDD.requete("Select `idCreateur de Recettes` from `createur de recettes` where `Client_idClient` = " + IdClient + ";").Replace(",",""));

            NewRecette wnd = new NewRecette(IdClient,IDCdR);
            this.Close();
            wnd.ShowDialog();
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            Accueil_Client wnd = new Accueil_Client(IdClient);
            this.Close();
            wnd.ShowDialog();
        }
    }
}
