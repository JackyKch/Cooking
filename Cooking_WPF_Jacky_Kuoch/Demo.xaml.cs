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
    /// Logique d'interaction pour Demo.xaml
    /// </summary>
    public partial class Demo : Window
    {
        public Demo()
        {
            InitializeComponent();

            //Nombre de Clients
            string NbrClients = (LinkBDD.requete("Select count(*) from Client;").Replace(",", ""));
            NbrClient.Text = "Nombre de clients : " + NbrClients;

            //Nombre de CdR
            string NbrCdrs = (LinkBDD.requete("Select count(*) from `createur de recettes`").Replace(",", ""));
            NbrCdR.Text = "Nombre de CdR : " + NbrCdrs;

            //Noms et Recettes vendues des CdR
            List<CreateurdeRecettes> cdr = new List<CreateurdeRecettes>();
            string[] Createurderecettes = LinkBDD.requete("select C.Identifiant_Cooking, sum(QuantitéVendue_Recette) From Recette R JOIN  Client C ON R.`Createur de Recettes_Client_idClient` = C.idClient group by R.`Createur de Recettes_idCreateur de Recettes` order by sum(quantitéVendue_Recette) DESC; ").Split(",");

            for (int i =0; i <Createurderecettes.Length -1; i++)
            {
                if (i%2 == 0)
                {
                    cdr.Add(new CreateurdeRecettes() { NomCdR = Createurderecettes[i].ToUpper(), NombreRecetteVendue = Createurderecettes[i + 1] });
                }
            }
            ListeCDR.ItemsSource = cdr;


            //Nombre de Recettes
            string NbrRecettes = (LinkBDD.requete("Select count(*) from Recette").Replace(",", ""));
            NbrRecette.Text = "Nombre de recettes : " + NbrRecettes;

            //Liste des produits ayant une quantité en stock <= 2*StockMini
            List<Produit> produitsrestock = new List<Produit>();
            string[] produitsmanquants = LinkBDD.requete("Select Nom_Produit from Produit where Stock_Produit <= 2 * StockMin_Produit; ").Split(",");

            for (int j = 0; j<produitsmanquants.Length-1; j ++)
            {
                produitsrestock.Add(new Produit() { nomProduit = produitsmanquants[j] });
            }
            ListeProduitsManquants.ItemsSource = produitsrestock;

        }

        private void Produit_Click(object sender, RoutedEventArgs e)
        {
            //Liste des recettes utilisant le produit sélectionné et la quantité utilisée du produit
            string NomProduit = Produit.Text;
            List<Recette> recettesproduits = new List<Recette>();

            //Vérification de l'existence du produit
            string command = "Select * From Produit where `Nom_Produit` = '" + NomProduit + "';";
            string InfoConnexion = LinkBDD.requete(command);

            //Si le produit n'existe pas
            if (InfoConnexion == null || InfoConnexion == "")
            {
                Erreur.Visibility = Visibility.Visible;
                Produit.Text = "";
            }
            else
            {
                Erreur.Visibility = Visibility.Hidden;
                int idProduit = Convert.ToInt32(LinkBDD.requete("Select idProduit from Produit where Nom_Produit ='" + NomProduit + "';").Replace(",", ""));

                string[] recettesusing = LinkBDD.requete("select R.Nom_Recette, IR.Quantité_Produit from ingrédients_recette IR JOIN Recette R on IR.Recette_idRecette = R.idRecette where IR.Produit_idProduit = " + idProduit + ";").Split(",");

                for (int k = 0; k < recettesusing.Length - 1; k++)
                {
                    if (k % 2 == 0)
                    {
                        recettesproduits.Add(new Recette() { nomRecette = recettesusing[k], QuantiteProduitUtilisee = recettesusing[k + 1] });
                    }
                }
                ListeRecettes.ItemsSource = recettesproduits;
            }

        }
    }
}
