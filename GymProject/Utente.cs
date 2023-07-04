public class Utente
{
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string DataNascita { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Utente(string nome, string cognome, string data, string email, string pass)
    {
        Nome = nome;
        Cognome = cognome;
        DataNascita = data;
        Email = email;
        Password = pass;
    }

}
