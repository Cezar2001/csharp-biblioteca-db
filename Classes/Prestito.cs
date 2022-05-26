using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_biblioteca_db
{
    class Prestito
    {
        public String Numero { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public Utente Utente { get; set; }
        public Documento Documento { get; set; }

        public Prestito(String Numero, DateTime Dal, DateTime Al, Utente Utente, Documento Documento)
        {
            this.Numero = Numero;
            this.Dal = Dal;
            this.Al = Al;
            this.Utente = Utente;
            this.Documento = Documento;
            this.Documento.Stato = Stato.Prestito;
        }

        public override string ToString()
        {
            return string.Format("Numero:{0}\nDal:{1}\nAl:{2}\nStato:{3}\nUtente:\n{4}\nDocumento:\n{5}",
                this.Numero,
                this.Dal,
                this.Al,
                this.Documento.Stato,
                this.Utente.ToString(),
                this.Documento.ToString());
        }
    }
}