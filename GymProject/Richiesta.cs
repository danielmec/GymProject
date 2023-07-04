using System;
namespace GymProject
{
    public class Richiesta
    {
        public string Tipo { get; set; }
        public string Descrizione { get; set; }
        public string Email { get; set; }
        public double Peso { get; set; }
        public int Eta { get; set; }
        public double Altezza { get; set; }

        public Richiesta(string tipo,string descrizione, int eta, string email, double peso, double altezza)
        {
            Tipo = tipo;
            Descrizione = descrizione;
            Eta = eta;
            Email = email;
            Peso = peso;
            Altezza = altezza;
        }
    }
}

