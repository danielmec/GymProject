using System;
namespace GymProject
{
	public class Esercizio
	{
        
        public TipoEsercizio TipoEsercizio { get; set; }
        public int NumeroRipetizioni { get; set; }
        public int NumeroSerie { get; set; }
        public int TempoSerie { get; set; }
        public int DurataRecupero { get; set; }
        public String Priorita { get; set; }

        public Esercizio(TipoEsercizio tipoEsercizio, int numeroRipetizioni, int numeroSerie, int tempoSerie, int durataRecupero, String priorita)
        {
            TipoEsercizio = tipoEsercizio;
            NumeroRipetizioni = numeroRipetizioni;
            NumeroSerie = numeroSerie;
            TempoSerie = tempoSerie;
            DurataRecupero = durataRecupero;
            Priorita = priorita;
        }
    }
}

