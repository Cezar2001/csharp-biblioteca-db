using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_biblioteca_db
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Biblioteca b = new Biblioteca("Civica");
            List<Autore> lAutoriProva = new List<Autore>();
            StreamReader reader = new StreamReader("elenco.txt");
            string linea;
            while ((linea = reader.ReadLine()) != null)
            {
                //una linea è, ad esempio: giuseppe mazzini e altri autori:a carlo alberto di savoja
                var vett = linea.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                string s = vett[0];
                var cn = s.Split(new char[] { ' ' });
                string nome = cn[0];
                string cognome = "";
                try
                {
                    cognome = s.Substring(cn[0].Length + 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                string titolo = vett[1];
                Console.WriteLine("Nome: {0}, Cognome: {1}, Titolo: {2}", nome, cognome, titolo);
                string email = nome + "@email.it";
                Autore AutoreMioLibro = new Autore(nome, cognome, email);
                lAutoriProva.Add(AutoreMioLibro);
                b.AggiungiLibro(db.GetUniqueId(), titolo, "Libro", 200, "SS1", lAutoriProva);
            }
            reader.Close();

            db.StampaLibriAutori();

            Console.WriteLine("Inserire il Nome autore da cercare");
            string searchNome = Console.ReadLine();
            Console.WriteLine("Inserire il Cognome autore da cercare");
            string searchCognome = Console.ReadLine();

            var listaSearch = new List<List<string>>();
            listaSearch = db.SearchByAutore(searchNome, searchCognome);
            db.StampaLibriAutoriRicerca(listaSearch);
        }
    }
}