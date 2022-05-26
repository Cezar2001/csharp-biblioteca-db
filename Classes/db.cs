using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace csharp_biblioteca_db
{
    internal class db
    {
        private static string stringaDiConnessione = "Data Source=localhost;Initial Catalog=biblioteca;Integrated Security=True;Pooling=False";
        private static SqlConnection Connect()
        {
            SqlConnection conn = new SqlConnection(stringaDiConnessione);
            try
            {
                conn.Open();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return conn;
        }
        internal static int scaffaleAdd(string nuovo)
        {
            var conn = Connect();
            if (conn == null)
                throw new Exception("Unable to connect to the dabatase");
            var cmd = String.Format("insert into Scaffale (Scaffale) values ('{0}')", nuovo);
            using (SqlCommand insert = new SqlCommand(cmd, conn))
            {
                try
                {
                    var numrows = insert.ExecuteNonQuery();
                    return numrows;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                } finally
                {
                    conn.Close();
                }
            }
        }
        internal static List<string> scaffaleGet()
        {
            List<string> ls = new List<string>();
            var conn = Connect();
            if (conn == null)
                throw new Exception("Unable to connect to the dabatase");
            var cmd = String.Format("select Scaffale from Scaffale");  //Li prendo tutti
            using (SqlCommand select= new SqlCommand(cmd, conn))
            {
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    //Console.WriteLine(reader.FieldCount);
                    while (reader.Read())
                    {
                        ls.Add(reader.GetString(0));
                    }
                }
            }
            conn.Close();
            return ls;
        }

        // insert into Libri(codice, NumPagine) 
        //values(1, 800)

        //insert into Libri(codice, Titolo, Settore, Stato, Tipo, Scaffale) 
        //values(1, 'I PROMESSI SPOSI', 'Romanzo', 'disponibile', 'libro', 'S001')

        //insert into Autori(Nome, Cognome, mail) values ('Alessandro', 'Manzoni', 'nd')

        //insert into Autori_Documenti(codice_autore, codice_documento) values(1000, 1)

        internal static int libroAdd(Libro libro, List<Autore> lAutori)
        {
            //devo collegarmi e inviare un comando di insert del nuovo scaffale
            var conn = Connect();
            if (conn == null)
            {
                throw new System.Exception("Unable to connect to database");
            }
            var cmd = string.Format(@"insert into Documenti(codice, Titolo, Settore, Stato, Tipo, Scaffale)
                VALUES({0}, '{1}', '{2}', '{3}', 'LIBRO', '{4}')", libro.Codice,libro.Titolo,libro.Settore,libro.Stato.ToString(),libro.Scaffale.Numero);
            using (SqlCommand insert = new SqlCommand(cmd, conn))
            {
                try
                {
                    var numrows = insert.ExecuteNonQuery();
                    if (numrows != 1)
                        throw new System.Exception("Valore di ritorno errato prima query");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
            var cmd1 = string.Format(@"insert into Libri(codice, NumPagine) VALUES({0},{1})",libro.Codice,libro.NumeroPagine);
            using (SqlCommand insert = new SqlCommand(cmd1, conn))
            {
                try
                {
                    var numrows = insert.ExecuteNonQuery();
                    if (numrows != 1)
                        throw new System.Exception("Valore di ritorno errato seconda query");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
            string cmd2;
            foreach (Autore autore in lAutori)
            {
                cmd2 = string.Format(@"INSERT INTO Autori(codice, Nome, Cognome, mail) values({0},'{1}','{2}', '{3}') ", autore.iCodiceAutore,  autore.Nome, autore.Cognome, autore.sMail);
                using (SqlCommand insert = new SqlCommand(cmd2, conn))
                {
                    try
                    {
                        var numrows = insert.ExecuteNonQuery();
                        if (numrows != 1)
                            throw new System.Exception("Valore di ritorno errato terza query");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return 0;
                    }
                }
            }
            string cmd3;
            foreach (Autore autore in lAutori)
            {
                cmd3 = string.Format(@"INSERT INTO Autori_Documenti(codice_autore, codice_documento) values({0},{1})", autore.iCodiceAutore, libro.Codice);
                using (SqlCommand insert = new SqlCommand(cmd3, conn))
                {
                    try
                    {
                        var numrows = insert.ExecuteNonQuery();
                        if (numrows != 1)
                            throw new System.Exception("Valore di ritorno errato seconda query");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        conn.Close();
                        return 0;
                    }
                }
            }
            return 0;
        }

        //nel caso ci siano più attributi, allora potete utilizzare le tuple
        internal static List<Tuple<int, string, string, string, string, string>> documentiGet()
        {
            var ld = new List<Tuple<int, string, string, string, string, string>>();
            var conn = Connect();
            if (conn == null)
                throw new Exception("Unable to connect to the dabatase");
            var cmd = String.Format("select codice, Titolo, Settore, Stato, Tipo, Scaffale from Documenti");  //Li prendo tutti
            using (SqlCommand select = new SqlCommand(cmd, conn))
            {
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = new Tuple<Int32, string, string, string, string, string>(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetString(5));
                        ld.Add(data);
                    }
                }
            }
            conn.Close();
            return ld;
        }
    }
}