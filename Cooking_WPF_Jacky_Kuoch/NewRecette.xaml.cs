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
    /// Logique d'interaction pour NewRecette.xaml
    /// </summary>
    public partial class NewRecette : Window
    {
        private int IDClient;
        private int IDCdR;

        Dictionary<string, int> IngredientsRecette = new Dictionary<string, int>();

        public NewRecette(int IDClient, int IDCdR)
        {
            this.IDClient = IDClient;
            this.IDCdR = IDCdR;
            InitializeComponent();

            //Liste des produits et leurs quantités disponibles pour la création d'une nouvelle recette
            List<Produit> items2 = new List<Produit>();
            string requete2 = "Select Nom_Produit, Stock_Produit, Unite_Produit from Produit where Stock_Produit > 0;";
            string[] tab2 = LinkBDD.requete(requete2).Split(",");

            //Remplissage de la ListeView 
            for (int j = 0; j < tab2.Length - 1; j += 1)
            {
                if (j % 3 == 0)
                    items2.Add(new Produit() { nomProduit = tab2[j], QuantiteDispo = (tab2[j + 1]) + " " + tab2[j + 2] });
            }
            produits.ItemsSource = items2;

        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {

            //Récupérer les informations de la recette
            string[] recette = new string[4];
            int idProduit;
            int NumeroRecette;

            recette[0] = Nom.Text;
            recette[1] = TypeRecette.Text;
            recette[2] = Descriptif.Text;
            recette[3] = Prix.Text;

            //Insertion de la Recette dans la base de données 
            string requete1 = "INSERT INTO `projet_cooking`.`recette`(`Nom_Recette`,`Type_Recette`,`Descriptif_Recette`,`Prix_Recette`,`QuantitéVendue_Recette`,`Remuneration_CdR`,`Createur de Recettes_idCreateur de Recettes`,`Createur de Recettes_Client_idClient`) VALUES ('" + recette[0] + "','" + recette[1] + "','" + recette[2] + "'," + Convert.ToInt32(recette[3]) + "," + 0 + ",2," + IDCdR + "," + IDClient + ");";
            LinkBDD.requete(requete1);

            //Augmenter le compteur de nombre de recettes créées par le CdR en question
            string requete1bis = "UPDATE `createur de recettes` SET Nombre_Recettes_Crees = Nombre_Recettes_Crees + 1 where `idCreateur de Recettes` = " + IDCdR + ";";
            LinkBDD.requete(requete1bis);
            
     
            // Insertion des produits nécessaires à cette recette dans la base de données
            foreach (KeyValuePair < string,int> element in IngredientsRecette)
            {
                //Recupérer le numéro de produit qui est appelé
                string requete2 = "SELECT idProduit from Produit where Nom_Produit = '" + element.Key + "';";
                idProduit = Convert.ToInt32(LinkBDD.requete(requete2).Replace(",",""));

                //Récupér le numéro de la recette affiliée au produit
                string requete3 = "SELECT idRecette from Recette where Nom_Recette = '" + recette[0] + "';";
                NumeroRecette = Convert.ToInt32(LinkBDD.requete(requete3).Replace(",", ""));

                //Mise à jour du stock minimal du produit utilisé
                string requete4 = "UPDATE Produit SET StockMin_Produit = StockMin_Produit/2 + 2 * " + element.Value + " where idProduit = " + idProduit + ";";
                LinkBDD.requete(requete4);

                //Mise à jour du stock maximal du produit utilisé
                string requete5 = "UPDATE Produit SET StockMax_Produit = StockMax_Produit + 2 * " + element.Value + " where idProduit = " + idProduit + ";";
                LinkBDD.requete(requete5);

                //Ajout dans la base de données de chaque ingrédient 
                string requete6 = "INSERT INTO `projet_cooking`.`ingrédients_recette`(`Recette_idRecette`,`Produit_idProduit`,`Quantité_Produit`) VALUES (" + NumeroRecette + "," + idProduit + "," + element.Value + " );";
                LinkBDD.requete(requete6);
            }

            //Retour à l'onglet CdR
            CdR wnd = new CdR(IDClient);
            Erreur.Visibility = Visibility.Hidden;
            this.Close();
            wnd.ShowDialog();
        }



        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            CdR wnd = new CdR(IDClient);
            Erreur.Visibility = Visibility.Hidden;
            this.Close();
            wnd.ShowDialog();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {

            string requetetest = "Select * from Produit where Nom_Produit = '" + NomProduit.Text + "';";
            string InfoProduit = LinkBDD.requete(requetetest);
            if (InfoProduit == null || InfoProduit == "")
            {
                Erreur.Visibility = Visibility.Visible;
                NomProduit.Text = "";
                QuantiteProduit.Text = "";
            }

            else
            {
                Erreur.Visibility = Visibility.Hidden;
                //On stocke le nom du produit ainsi que sa quantité dans un dictionnaire
                IngredientsRecette.Add(NomProduit.Text, Convert.ToInt32(QuantiteProduit.Text));

                //On réinitialise les cases pour que l'utilisateur puisse rentrer un nouveau produit
                NomProduit.Text = "";
                QuantiteProduit.Text = "";
            }
           
        }
    }
}
