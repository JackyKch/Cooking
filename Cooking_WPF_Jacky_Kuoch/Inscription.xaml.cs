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
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : Window
    {
        
        public Inscription()
        {
            InitializeComponent();
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string[] client = new string[6];
            string password = Mdp.Text;
            string confirmation = Confirmation.Text;

            if (password == confirmation)
            {
                client[0] = Nom.Text;
                client[1] = Telephone.Text;
                client[2] = Adresse.Text;
                client[3] = Cooks.Text;
                client[4] = Identifiant.Text;
                client[5] = Mdp.Text;

                string requetetest = "Select * from Client where Identifiant_Cooking = + '" + Identifiant.Text + "';";

                if (LinkBDD.requete(requetetest) == null || LinkBDD.requete(requetetest) == "")
                {
                    ErreurIdentifiant.Visibility = Visibility.Hidden;
                    string requete = "INSERT into `projet_cooking`.`client` (`Nom_Client`,`Telephone_Client`,`Adresse_Client`,`Portefeuille_Cook`,`Identifiant_Cooking`,`MotdePasse_Cooking`) VALUES ('" + client[0] + "','" + client[1] + "','" + client[2] + "'," + Convert.ToInt32(client[3]) + ",'" + client[4] + "','" + client[5] + "');";
                    LinkBDD.requete(requete);
                }
                else
                {
                    ErreurIdentifiant.Visibility = Visibility.Visible;
                    Identifiant.Text = "";
                }

                
                this.Close();
            }

            else
            {
                Erreur.Visibility = Visibility.Visible;
                Mdp.Text = "";
                Confirmation.Text = "";
            }


        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            Erreur.Visibility = Visibility.Hidden;
            ErreurIdentifiant.Visibility = Visibility.Hidden;
            this.Close();
        }
    }
}
