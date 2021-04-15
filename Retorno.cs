using System;

namespace CodigoTestavel
{
    public class Retorno
    {
        public string Valor { get; set; }
        public string Criacao { get; set; } = DateTime.UtcNow.ToLongDateString();
    }
}
