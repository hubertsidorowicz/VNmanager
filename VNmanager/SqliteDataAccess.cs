using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNmanager
{
    public class SqliteDataAccess
    {
        public static List<GamesModel> LoadGames()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<GamesModel>("select * from Games", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<GamesModel> LoadOneGame(string title)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string query = "select * from Games where title=" + title;
                var output = cnn.Query<GamesModel>("select * from Games where Title='" + title + "'");
                return output.ToList();
            }
        }

        public static void SaveGames(GamesModel game)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Games (Title,GameUrl,LastPlayed,Added,Icon,TitleImage,PageImage,XT,YT,WidthT,HeightT,XP,YP,WidthP,HeightP) values (@Title,@GameUrl,@LastPlayed,@Added,@Icon,@TitleImage,@PageImage,@XT,@YT,@WidthT,@HeightT,@XP,@YP,@WidthP,@HeightP)", game);
            }
        }

        public static void DeleteGame(string gameTitle)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Games where title = '"+gameTitle+"'");
            }
        }

        #region Updates

        public static void UpdateGame(GamesModel newModel, string title, string itemToEdit)
        {
            string query = "update Games set "+itemToEdit+"= (@"+itemToEdit+") where Title = '" +title+"'";
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute(query, newModel);
            }
        }

        #endregion

        #region Helpers
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        #endregion
    }
}
