using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SD.Models
{
    public class feedback
    {
        public string status { get; set; }
        public string mensagem { get; set; }
        public feedback() { }

        public feedback(string mensagem, string status)
        {
            this.mensagem = mensagem;
            this.status = status;
        }
    }
}
