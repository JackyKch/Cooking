using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Cooking_WPF_Jacky_Kuoch
{
    static class LinkBDD
    {
        static public string connectionString = "SERVER=127.0.0.1;PORT=3306;DATABASE=Projet_Cooking;UID=root;PASSWORD=";
        static public MySqlConnection connection = new MySqlConnection(connectionString);

        static public string requete (string CommandeSql)
        {
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = CommandeSql; //requete
            MySqlDataReader reader;
            reader = command.ExecuteReader();

            string reponse = "";
            while (reader.Read()) //parcours ligne par ligne
            {
                string currentRowasString = "";
                for (int i =0; i< reader.FieldCount; i++)
                {
                    string valuesasString = reader.GetValue(i).ToString(); //récupération de la première colonne sous forme de string
                    currentRowasString += valuesasString + ", "; // ajout à la ligne
                }
                reponse += currentRowasString; //ligne finale 
            }
            connection.Close();
            return reponse;
        }


    }
}
