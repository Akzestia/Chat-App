using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App
{
    public static class SqlMethods
    {
        //Connection string
        public static string connectionstring = "Data Source = DESKTOP-SH54RD2; Initial Catalog = ChatAppdb ;Trusted_Connection=True; TrustServerCertificate = True ";

        public static void AddUser(User user) // Add user to table
        {
            DataContext db = new DataContext(connectionstring);
            db.GetTable<User>().InsertOnSubmit(user);
            db.SubmitChanges();
        }
    }
}
