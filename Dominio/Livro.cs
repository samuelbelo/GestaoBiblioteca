using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Livro
    {
        public string Nome { get; set; }

        public DateTime DataLancamento { get; private set; }

        public Livro(string nome, DateTime dataLancamento)
        {
            Nome = nome;
            DataLancamento = dataLancamento;
        }

        public int CalcularQuantosAnosFoiLancado()
        {
            return DateTime.Now.Year - DataLancamento.Year;
        }
    }
}
