namespace SD.Models
{
    public class usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }

        public usuario()
        {

            this.id = 0;
            this.nome = "";
            this.email = "";
        }
        public usuario(int id, string nome, string email)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
        }

    }
}