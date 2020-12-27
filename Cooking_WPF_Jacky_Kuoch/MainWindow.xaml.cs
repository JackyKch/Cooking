using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.IO;

namespace Cooking_WPF_Jacky_Kuoch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            //Récupérer l'identifiant et le mot de passe lors d'une connexion
            string identifiant = Identifiant.Text;
            string password = Mdp.Password;

            //Vérification de l'existence du Client
            string command = "Select * From Client where `Identifiant_Cooking` = '" + identifiant + "' and `MotdePasse_Cooking` = '" + password + "';";
            string InfoConnexion = LinkBDD.requete(command);

            //Si l'utilisateur n'existe pas
            if (InfoConnexion == null || InfoConnexion == "")
            {
                Erreur.Visibility = Visibility.Visible;
                Identifiant.Text = "";
                Mdp.Password = "";
            }
            else //Ouverture de l'onglet Client
            {
                int idClient = Convert.ToInt32(LinkBDD.requete("Select idClient from Client where `Identifiant_Cooking` = '" + identifiant + "';").Replace(",", ""));
                Accueil_Client wnd = new Accueil_Client(idClient);
                this.Close();
                wnd.ShowDialog();
                
            }
            
        }

        private void Inscription_Click (object sender, RoutedEventArgs e)
        {
            //Ouverture de l'onglet Inscription
            Erreur.Visibility = Visibility.Hidden;
            Inscription wnd = new Inscription();
            wnd.ShowDialog();
            
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            //Ouverture de l'onglet Demo
            Erreur.Visibility = Visibility.Hidden;
            Demo wnd = new Demo();
            wnd.ShowDialog();
        }
    }
}
