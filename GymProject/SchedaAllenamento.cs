using System;
namespace GymProject
{
	public class SchedaAllenamento
	{
        public string Nome { get; set; }
        public List<Esercizio> Esercizi { get; set; }

        public SchedaAllenamento(string nome, List<Esercizio> esercizi)
        {
            
            Nome = nome;
            Esercizi = esercizi;
        }
    }
}

