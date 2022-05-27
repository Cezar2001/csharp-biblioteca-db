using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_biblioteca_db
{
    class Biblioteca
    {
        public string Nome { get; set; }
        public List<Scaffale> ScaffaliBiblioteca { get; set; }

        public Biblioteca(string Nome)
        {
            this.Nome = Nome;
            this.ScaffaliBiblioteca = new List<Scaffale>();
            List<string> elencoScaffali = db.scaffaleGet();
            elencoScaffali.ForEach((string item) =>
            {
                AggiungiScaffale(item, false);
                //Scaffale nuovo = new Scaffale(item);
                //this.ScaffaliBiblioteca.Add(nuovo);
            });
        }

        public void AggiungiScaffale(string sNomeScaffale, bool addToDb = true)
        {
            Scaffale nuovo = new Scaffale(sNomeScaffale);
            this.ScaffaliBiblioteca.Add(nuovo);
            if(addToDb)
                db.scaffaleAdd(nuovo.Numero);
        }

        public void AggiungiLibro(long sCodice, string sTitolo, string sSettore, int iNumPagine, string sScaffale, List<Autore> lListaAutori)
        {
            Libro MioLibro = new Libro(sCodice, sTitolo, sSettore, iNumPagine, sScaffale);
            MioLibro.Stato = Stato.Disponibile;
            db.libroAdd(MioLibro, lListaAutori);
        }
        public void AggiungiDVD(long sCodice, string sTitolo, string sSettore, int iDurata, string sScaffale, List<Autore> lListaAutori)
        {
            DVD MioDVD = new DVD(sCodice, sTitolo, sSettore, iDurata, sScaffale);
            MioDVD.Stato = Stato.Disponibile;
            db.DVDAdd(MioDVD, lListaAutori);
        }

        public int GestisciOperazioneBiblioteca(int iCodiceOperazione)
        {
            List<Documento> llResult;
            string sAppo;
            switch (iCodiceOperazione)
            {
                case 1:
                    Console.WriteLine("Inserisci autore:");
                    sAppo = Console.ReadLine();
                    llResult = SearchByAutore(sAppo);
                    if (llResult == null)
                        return 1;
                    else
                        StampaListaDocumenti(llResult);
                    break;
            }

            return 0;
        }

        public void StampaListaDocumenti(List<Documento> lListaDoc)
        {
            return;
        }

        public List<Documento> SearchByCodice(string Codice)
        {

            Console.WriteLine("Metodo da implementare");
            return null;
        }

        public List<Documento> SearchByTitolo(string Titolo)
        {
            Console.WriteLine("Metodo da implementare");
            return null;
        }

        public List<Documento> SearchByAutore(string sAutore)
        {
            Console.WriteLine("Metodo da implementare");
            return null;
        }

        public List<Prestito> SearchPrestiti(string Numero)
        {
            return null;
        }

        public List<Prestito> SearchPrestiti(string Nome, string Cognome)
        {
            return null;
        }
    }

    class Scaffale
    {
        public string Numero { get; set; }

        public Scaffale(string Numero)
        {
            this.Numero = Numero;
        }
    }
}