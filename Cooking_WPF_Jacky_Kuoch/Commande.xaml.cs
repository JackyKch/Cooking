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
    /// Logique d'interaction pour Commande.xaml
    /// </summary>
    public partial class Commande : Window
    {
        private int IDClient;

        Dictionary<string, int> detailsCommande = new Dictionary<string, int>();
        
        //Liste des recettes et leurs quantités commandées 
        List<Recettes_Commande> items = new List<Recettes_Commande>();

        public Commande(int IDClient)
        {
            this.IDClient = IDClient;
            InitializeComponent();

            //Affichage des recettes
            List<Recette_Clients> items = new List<Recette_Clients>();
            string requete2 = "Select Nom_Recette, Type_Recette, Descriptif_Recette, Prix_Recette from Recette;";
            string[] tab = LinkBDD.requete(requete2).Split(",");

            for (int i = 0; i < tab.Length - 1; i++)
            {
                if (i % 4 == 0)
                {
                    items.Add(new Recette_Clients() { NomRecette = tab[i], TypeRecette = tab[i + 1], DescriptifRecette = tab[i + 2], PrixRecette = tab[i + 3] });
                }
            }
            listerecettes.ItemsSource = items;
        }


        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            //Vérifions que la recette sélectionnée existe 
            string requetetest = "Select * from Recette where Nom_Recette = '" + NomRecette.Text + "';";
            string InfoRecette = LinkBDD.requete(requetetest);
            if (InfoRecette == null || InfoRecette == "")
            {
                //affichage de l'erreur
                Erreur.Visibility = Visibility.Visible;
                NomRecette.Text = "";
                QuantiteRecette.Text = "";
            }
            else
            {
                Erreur.Visibility = Visibility.Hidden;

                //On stocke la recette commandée et la quantité voulue
                detailsCommande.Add(NomRecette.Text, Convert.ToInt32(QuantiteRecette.Text));

                //Remplissage de la ListeView 
                items.Add(new Recettes_Commande() { NomRecette = NomRecette.Text, QuantiteCommandee = Convert.ToInt32(QuantiteRecette.Text) });
                recettes.ItemsSource = items;
                recettes.Items.Refresh();

                //On reinitialise les cases nomRecette et QuantiteRecette
                NomRecette.Text = "";
                QuantiteRecette.Text = "";

            }
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            //Annuler la recette et retour à la page client
            Accueil_Client wnd = new Accueil_Client(IDClient);
            Erreur.Visibility = Visibility.Hidden;
            this.Close();
            wnd.ShowDialog();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            // insertion de la commande dans la base de données
            string requete1 = "INSERT INTO `projet_cooking`.`commande`(`Date_Commande`,`Client_idClient`) VALUES ('" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'," + IDClient + ");";
            LinkBDD.requete(requete1);

            foreach (KeyValuePair <string, int> element in detailsCommande)
            {
                //Insertion dans la BDD//

                //Récupération de la référence de la commande
                string requete2 = "SELECT MAX(Reference_Commande) from commande";
                int RefCommande = Convert.ToInt32(LinkBDD.requete(requete2).Replace(",",""));

                //Recupérer le numéro de la recette commandée et le idCdR affilié
                string requete3 = "SELECT idRecette from Recette where Nom_Recette = '" + element.Key + "';";
                int idRecette;
                idRecette = Convert.ToInt32(LinkBDD.requete(requete3).Replace(",", ""));

                string requete3bis = "SELECT `Createur de Recettes_Client_idClient` from Recette where idRecette = " + idRecette + ";";
                int idCdR;
                idCdR = Convert.ToInt32(LinkBDD.requete(requete3bis).Replace(",",""));

                //insertion des détails de la commande dans la base de données
                string requete4 = "INSERT INTO `projet_cooking`.`details_commande`(`Commande_Reference_Commande`,`Recette_idRecette`,`Quantité_Recette_Commande`) VALUES (" + RefCommande + "," + idRecette + "," + element.Value + ");";
                LinkBDD.requete(requete4);


                //Réglement de la commande//

                //Récupérer le prix de la recette
                string requete5 = "SELECT Prix_Recette from Recette where idRecette = " + idRecette + ";";
                int prixRecette;
                prixRecette = Convert.ToInt32(LinkBDD.requete(requete5).Replace(",", ""));

                //Déduire le montant de la commande du portefeuille de cooks du client
                string requete6 = "UPDATE Client set Portefeuille_Cook = Portefeuille_Cook - " + prixRecette * element.Value + " where idClient = " + IDClient + ";";
                LinkBDD.requete(requete6);


                //Modification stock produit//

                //Récupérer les produits utilisés et leurs quantités utilisées dans la recette commandée
                string requete7 = "Select Produit_idProduit, Quantité_Produit from ingrédients_recette where Recette_idRecette = " + idRecette + ";";
                string [] produits = LinkBDD.requete(requete7).Split(",");

                //On modifie les stocks des produits utilisés
                for (int i = 0; i< produits.Length-1; i++)
                {
                    if (i%2 == 0)
                    {
                        string requete8 = "UPDATE Produit SET Stock_Produit = Stock_Produit - " + (Convert.ToInt32(produits[i + 1]))*element.Value + " where idProduit = " + Convert.ToInt32(produits[i]) + ";";
                        LinkBDD.requete(requete8);
                    }
                }

                //Changer la dernière date d'utilisation des produits utilisés
                for (int j = 0; j< produits.Length -1; j+=2)
                {
                    string requete8bis = "UPDATE Produit SET LastUse = NOW() WHERE idProduit = " + produits[j] + ";";
                    LinkBDD.requete(requete8bis);
                }


                //Modification compteur vente recette//
                string requete9 = "UPDATE Recette SET QuantitéVendue_Recette = QuantitéVendue_Recette + " + element.Value + " where idRecette =" + idRecette + ";";
                LinkBDD.requete(requete9);


                //Modification du prix de la recette si le compteur de vente dépasse 10 ou 50 et de la rémunération du CdR
                string requete10 = "SELECT QuantitéVendue_Recette from Recette where idRecette = " + idRecette + ";";
                int QuantiteVendue = Convert.ToInt32(LinkBDD.requete(requete10).Replace(",", ""));

                if (10<QuantiteVendue && QuantiteVendue < 50)
                {
                    string requete11 = "UPDATE Recette SET Prix_Recette = Prix_Recette + 2 where idRecette = " + idRecette + ";";
                    LinkBDD.requete(requete11);
                }
                if (50<QuantiteVendue)
                {
                    string requete12 = "UPDATE Recette SET Prix_Recette = Prix_Recette + 5 where idRecette = " + idRecette + ";" +
                        "UPDATE Recette SET Remuneration_CdR = 4 where idRecette = " + idRecette +";";
                    LinkBDD.requete(requete12);
                }

                //Rémunération du Cdr

                //Récupération de la part reversé au CdR
                string requete13 = "Select Remuneration_CdR from Recette where idRecette = " + idRecette + ";";
                int Remuneration = Convert.ToInt32(LinkBDD.requete(requete13).Replace(",",""));

                //Créditons le portefeuille du CdR
                string requete14 = "UPDATE Client SET Portefeuille_Cook = Portefeuille_Cook + " + Remuneration * element.Value + " where idClient = " + idCdR + ";";
                LinkBDD.requete(requete14);
                
            }

            //Retour à l'onglet Client
            Accueil_Client wnd = new Accueil_Client(IDClient);
            Erreur.Visibility = Visibility.Hidden;
            this.Close();
            wnd.ShowDialog();
        }
    }
}