namespace SD.Models
{
    public class resultado
    {
        public string ddd;
        public string estado;
        public string cidade;
        public string operadora;

        public resultado() { }

        public resultado(string ddd, string estado, string cidade, string operadora)
        {
            this.ddd = ddd;
            this.estado = estado;
            this.cidade = cidade;
            this.operadora = operadora;
        }
    }
}