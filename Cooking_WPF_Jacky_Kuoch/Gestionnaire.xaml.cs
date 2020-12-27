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
using System.Xml;

namespace Cooking_WPF_Jacky_Kuoch
{
    /// <summary>
    /// Logique d'interaction pour Gestionnaire.xaml
    /// </summary>
    public partial class Gestionnaire : Window
    {
        public Gestionnaire()
        {
            InitializeComponent();


            //Récupérer l'idClient du CdR d'Or
            string requete1 = LinkBDD.requete("Select `Createur de Recettes_Client_idClient` From Recette group by `Createur de Recettes_idCreateur de Recettes` order by sum(quantitéVendue_Recette) DESC LIMIT 1;");

            if (requete1 == null || requete1 == "")
            {
                CdROr.Text = "CdR d'or : Pas de CdR d'or";
            }
            else
            {
                int idCDROR = Convert.ToInt32(requete1.Replace(",", ""));

                // Récupérer l'identifiant du CdR d'Or
                string requete2 = "Select Identifiant_Cooking from Client where idClient = " + idCDROR + ";";
                string IdentifiantCdROr = LinkBDD.requete(requete2).Replace(",", "");
                CdROr.Text = "CdR d'or : " + IdentifiantCdROr.ToUpper();

            }

            //Récupérer l'idClient du Créateur de Recettes de la semaine
            string requete3 = LinkBDD.requete("select R.`Createur de Recettes_Client_idClient` From(select * from commande Where TIMESTAMPDIFF(WEEK, Date_Commande, NOW()) < 1) C join details_commande DC on DC.Commande_Reference_Commande = C.Reference_Commande JOIN Recette R on DC.Recette_idRecette = R.idRecette Group By DC.Recette_idRecette order by sum(DC.Quantité_Recette_Commande) DESC LIMIT 1; ");
            
            if (requete3 == null || requete3 == "")
            {
                CdRSemaine.Text = "CdR de la semaine : Aucun";
            }
            else
            {
                int idCDRSemaine = Convert.ToInt32(requete3.Replace(",", ""));

                //Récupérer l'identifiant du CdR de la semaine
                string requete4 = "Select Identifiant_Cooking from Client where idClient = " + idCDRSemaine + ";";
                string IdentifiantCdRSemaine = LinkBDD.requete(requete4).Replace(",", "");
                CdRSemaine.Text = "CdR de la semaine : " + IdentifiantCdRSemaine.ToUpper();
            }
            

            //Top 5 Recettes
            List<Top_Recette> items = new List<Top_Recette>();
            string requete5 = "select R.Nom_Recette, R.QuantitéVendue_Recette, C.Identifiant_Cooking From `createur de recettes` CDR JOIN Recette R ON CDR.`idCreateur de Recettes` = R.`Createur de Recettes_idCreateur de Recettes` JOIN Client C ON C.idClient = CDR.Client_idClient group by R.Nom_Recette order by R.QuantitéVendue_Recette DESC Limit 5;";
            string[] tab = LinkBDD.requete(requete5).Split(",");

            for (int i = 0; i < tab.Length - 1; i++)
            {
                if (i % 3 == 0)
                {
                    items.Add(new Top_Recette() { NomRecette = tab[i], QuantiteVendue = Convert.ToInt32(tab[i + 1]), NomCdR = tab[i + 2].ToUpper() }); ;
                }
            }
            toprecettes.ItemsSource = items;

        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            Accueil_Client wnd = new Accueil_Client(1);
            ErreurCdR.Visibility = Visibility.Hidden;
            ErreurRecette.Visibility = Visibility.Hidden;
            this.Close();
            wnd.ShowDialog();
        }

        //Supprimer une recette
        private void SuppRecette_Click(object sender, RoutedEventArgs e)
        {
            //Recuperer le nom de la recette
            string Recette = NomRecette.Text;
            string requete6 = LinkBDD.requete("Select * from Recette where Nom_Recette = '" + Recette + "';");

            if (requete6 == null || requete6 == "") //Si la recette n'est pas reconnue
            {
                ErreurRecette.Visibility = Visibility.Visible;
                NomRecette.Text = "";
            }
            else
            {
                ErreurRecette.Visibility = Visibility.Hidden;
                string requete7 = LinkBDD.requete("Delete from recette where Nom_Recette = " + NomRecette.Text + ";");
            }
        }

        //Supprimer un client
        private void SuppCdR_Click(object sender, RoutedEventArgs e)
        {
            //Recuperer l'identifiant du CdR à supprimer
            string Identifiant = IdentifiantCdR.Text;
            string requete8 = LinkBDD.requete("Select * from `createur de recettes` CDR JOIN Client C ON C.idClient = CDR.Client_idClient where C.Identifiant_Cooking ='" + IdentifiantCdR.Text + "';");

            if (requete8 == null || requete8 == "") //Si le CdR n'est pas reconnu
            {
                ErreurCdR.Visibility = Visibility.Visible;
                IdentifiantCdR.Text = "";
            }
            else
            {
                //Récupérer l'id Client du CdR à partir de son identifiant pour faire la jointure
                string requete9 = "Select idClient from Client where Identifiant_Cooking = '" + IdentifiantCdR.Text + "';";
                int idClient = Convert.ToInt32(LinkBDD.requete(requete9).Replace(",", ""));

                ErreurCdR.Visibility = Visibility.Hidden;
                LinkBDD.requete("Delete from `createur de recettes` where Client_idClient = " + idClient + ";");
            }
        }

        private void Restock_Click(object sender, RoutedEventArgs e)
        {
            // Modifier le stock min et max si le produit n'a pas été utilisé depuis 30 jours
            string requete10 = "UPDATE Produit SET StockMin_Produit = StockMin_Produit/2 Where TIMESTAMPDIFF(DAY, LastUse, NOW()) > 30; " +
                "UPDATE Produit SET StockMax_Produit = StockMax_Produit/2 Where TIMESTAMPDIFF(DAY, LastUse, NOW()) > 30; ";
            LinkBDD.requete(requete10);

            //Récupération des élements nécessaires à la construction du ficher xml des commandes produits
            List<Produit> infoproduits = new List<Produit>();
            string[] Infos = LinkBDD.requete("Select P.Nom_Produit, P.Unite_Produit, P.Stock_Produit, P.StockMin_Produit, P.StockMax_Produit, F.Nom_Fournisseur from produit P join fournisseur F on P.Fournisseur_idFournisseur = F.idFournisseur order by F.Nom_Fournisseur;").Split(",");

            for (int k = 0; k < Infos.Length -1; k+=6)
            {
                Produit produit = new Produit() { nomProduit = Infos[k], UniteProduit = Infos[k + 1], StockActuel = Convert.ToInt32(Infos[k + 2]), StockMin = Convert.ToInt32(Infos[k + 3]), StockMax = Convert.ToInt32(Infos[k + 4]), NomFournisseur = Infos[k+5]};
                infoproduits.Add(produit);
            }

            //Génération du fichier XML
            Write_XML(infoproduits);
            MessageBox.Show("La liste des produits à commander a été créée sous forme d'un fichier xml dans le Debug");
        }

        private static void Write_XML(List<Produit> P)
        {
            XmlDocument docXml = new XmlDocument();

            // création de l'élement  racine ... qu'on ajoute au document
            XmlElement racine = docXml.CreateElement("Commande");
            docXml.AppendChild(racine);

            //création et insertion de l'en-tête XML (no <=> pas de DTD associée)
            XmlDeclaration xmldecl = docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.InsertBefore(xmldecl, racine);

            XmlElement DateCommande = docXml.CreateElement("Date");
            DateCommande.InnerText = DateTime.Now.Date.ToString("yyyy-MM-dd");
            racine.AppendChild(DateCommande);

            string[] infoFournisseur = LinkBDD.requete("Select * from Fournisseur;").Split(",");
            for (int i = 0; i < infoFournisseur.Length - 1; i += 3)
            {
                XmlElement fournisseur = docXml.CreateElement("Fournisseur");
                racine.AppendChild(fournisseur);

                XmlElement fournisseurnom = docXml.CreateElement("Nom");
                fournisseurnom.InnerText = Convert.ToString(infoFournisseur[i + 1]);
                fournisseur.AppendChild(fournisseurnom);

                XmlElement fournisseurtel = docXml.CreateElement("Telephone");
                fournisseurtel.InnerText = infoFournisseur[i + 2];
                fournisseur.AppendChild(fournisseurtel);

                foreach (Produit Produit in P)
                {
                    XmlElement produit = docXml.CreateElement("Produit");
                    fournisseur.AppendChild(produit);

                    XmlElement nomproduit = docXml.CreateElement("Nom_Produit");
                    nomproduit.InnerText = Convert.ToString(Produit.nomProduit);
                    produit.AppendChild(nomproduit);

                    XmlElement quantite = docXml.CreateElement("Quantité");
                    quantite.InnerText = Convert.ToString(Produit.StockMax - Produit.StockActuel);
                    produit.AppendChild(quantite);

                    XmlElement unite = docXml.CreateElement("Unité");
                    unite.InnerText = Produit.UniteProduit;
                    produit.AppendChild(unite);
  
                }
            }
            //enregistrement du doc XML 
            docXml.Save("CommandeProduit.xml");
        }
    }
}
