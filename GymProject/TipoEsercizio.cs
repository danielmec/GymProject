using System;
namespace GymProject
{
	public class TipoEsercizio
	{
        public string Nome { get; set; }
        public string Descrizione { get; set; }

        public TipoEsercizio(string nome, string descrizione)
        {
            Nome = nome;
            Descrizione = descrizione;
        }
    }
}

