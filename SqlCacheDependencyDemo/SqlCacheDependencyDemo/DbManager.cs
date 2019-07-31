using System.Data.SqlClient;
using System.Web.Caching;
using System.Web.Hosting;
using System.Data;

namespace SqlCacheDependency
{
    public class DbManager
    {

        private static object GetCacheData(string cacheItemName)
        {
            return HostingEnvironment.Cache.Get(cacheItemName);

        }

        private static void SetCacheData(string cacheItemName, object dataSet, string connString, string tableName)
        {
            string cacheEntryname = "NorthwindCache";

            //Many articles online state that the following statement should be run only once at the start of the application. 
            //Few articles say the object get replaced if called again with same connection string.
            SqlDependency.Start(connString);

            //The following statements needs to be run only once against the connection string and the database table, hence may also be moved to an appropriate place in code.
            SqlCacheDependencyAdmin.EnableNotifications(connString);
            SqlCacheDependencyAdmin.EnableTableForNotifications(connString, tableName);

            System.Web.Caching.SqlCacheDependency dependency = new System.Web.Caching.SqlCacheDependency(cacheEntryname, tableName);
            HostingEnvironment.Cache.Insert(cacheItemName, dataSet, dependency);
        }

        public static DataTable GetCategory(out bool isDataFromCache)
        {
            string sqlQuery = "SELECT [CategoryID],[CategoryName] FROM [dbo].[Categories]";
            string tableName = "Categories";

            string connStringName = "default";
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings[connStringName].ToString();

            isDataFromCache = false;
            DataTable dtTemp = null;
            string cacheItemName = sqlQuery; //assuming the commandtext can be used to uniquely identify the cached item

            object obj = GetCacheData(cacheItemName);
            dtTemp = (DataTable)obj;

            if (dtTemp == null)
            {
                SqlConnection cnMain = new SqlConnection(connString);
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, cnMain);

                dtTemp = new DataTable();
                da.Fill(dtTemp);

                SetCacheData(cacheItemName, dtTemp, connString, tableName);

            }
            else
            {
                isDataFromCache = true;
            }

            return dtTemp;

        }

    }

}