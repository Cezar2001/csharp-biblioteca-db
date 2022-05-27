using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_biblioteca_db
{
    enum Stato { Disponibile, Prestito }

    class Autore : Persona
    {
        public long iCodiceAutore;
        public string sMail;
        public Autore(string Nome, string Cognome, string sPosta) : base(Nome, Cognome)
        {
            sMail = sPosta;
            iCodiceAutore = GeneraCodiceAutore();
        }

        public long GeneraCodiceAutore()
        {
            return db.GetUniqueId();
        }
    }


    class Documento
    {
        public long Codice { get; set; }
        public string Titolo { get; set; }
        public int Anno { get; set; }
        public string Settore { get; set; }
        public Stato Stato { get; set; }
        public List<Autore> Autori { get; set; }
        public Scaffale Scaffale { get; set; }

        public Documento(long Codice, string Titolo, string Settore, string nomeScaffale)
        {
            this.Codice = Codice;
            this.Titolo = Titolo;
            this.Settore = Settore;
            this.Autori = new List<Autore>();
            this.Stato = Stato.Disponibile;
            this.Scaffale = new Scaffale(nomeScaffale);
        }

        public override string ToString()
        {
            return string.Format("Codice:{0}\nTitolo:{1}\nSettore:{2}\nStato:{3}\nScaffale numero:{4}",
                this.Codice,
                this.Titolo,
                this.Settore,
                this.Stato,
                this.Scaffale.Numero);
        }

        public void ImpostaInPrestito()
        {
            this.Stato = Stato.Prestito;
        }

        public void ImpostaDisponibile()
        {
            this.Stato = Stato.Disponibile;
        }

    }

    class Libro : Documento
    {
        public int NumeroPagine { get; set; }

        public Libro(long Codice, string Titolo, string Settore, int NumeroPagine, string Scaffale) : base(Codice, Titolo, Settore, Scaffale)
        {
            this.NumeroPagine = NumeroPagine;
        }

        public override string ToString()
        {
            return string.Format("{0}\nNumeroPagine:{1}",
                base.ToString(),
                this.NumeroPagine);
        }
    }

    class DVD : Documento
    {
        public int Durata { get; set; }

        public DVD(long Codice, string Titolo, string Settore, int Durata, string Scaffale) : base(Codice, Titolo, Settore, Scaffale)
        {
            this.Durata = Durata;
        }

        public override string ToString()
        {
            return string.Format("{0}\nDurata:{1}",
                base.ToString(),
                this.Durata);
        }
    }
}