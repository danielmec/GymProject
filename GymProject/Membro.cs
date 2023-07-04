using System;

	public class Membro
	{
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string DataNascita { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TipoMembro { get; set; }

        public Membro(string nome, string cognome, string dataNascita, string email, string password, string tipoMembro)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            Email = email;
            Password = password;
            TipoMembro = tipoMembro;
        }
    }


