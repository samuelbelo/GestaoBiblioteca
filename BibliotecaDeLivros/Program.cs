using Dominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BibliotecaDeLivros
{
    class Program
    {
        static void Main()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";
            string opcaoEscolhida;

            //var listaLivros = new Dictionary<string, DateTime>();
            var listaLivros = new List<Livro>();
            do
            {
                Console.Clear();
                Console.WriteLine("Gerenciador de Livros\nSelecione uma das opções abaixo:");
                Console.WriteLine("1 - Pesquisar Livros");
                Console.WriteLine("2 - Adicionar Livros");
                Console.WriteLine("3 - Sair");

                opcaoEscolhida = Console.ReadLine();

                if (opcaoEscolhida == "1")
                {
                    Console.WriteLine("Informe o nome, ou parte do nome do livro que deseja encontrar:");
                    var termoDePesquisa = Console.ReadLine();
                    var livrosEncontrados = listaLivros.Where(x => x.Nome.ToLower().Contains(termoDePesquisa.ToLower()))
                                                       .ToList();

                    if (livrosEncontrados.Count > 0)
                    {
                        Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de um dos livros encontrados:");
                        for (var index = 0; index < livrosEncontrados.Count; index++)
                            Console.WriteLine($"{index} - {livrosEncontrados[index].Nome}");

                        ushort indexAExibir;
                        if (!ushort.TryParse(Console.ReadLine(), out indexAExibir) || indexAExibir >= livrosEncontrados.Count)
                        {
                            Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
                            Console.ReadKey();
                            continue;
                        }

                        if (indexAExibir < livrosEncontrados.Count)
                        {
                            var livro = livrosEncontrados[indexAExibir];

                            Console.WriteLine("Dados da livro");
                            Console.WriteLine($"Nome: {livro.Nome}");
                            Console.WriteLine($"Data de lançamento: {livro.DataLancamento:dd/MM/yyyy}");

                            var qtdeAnos = livro.CalcularQuantosAnosFoiLancado();

                            if (qtdeAnos > 0)
                            {
                                var tempo = DateTime.Now.Year - livro.DataLancamento.Year;
                                Console.Write($"Este livro foi lançado há {tempo} ano(s). {pressioneQualquerTecla}");
                            }
                            else if (qtdeAnos == 0)
                            {
                                Console.Write($"Este livro foi lançado este ano! {pressioneQualquerTecla}");
                            }
                            else
                            {
                                Console.Write($"Este livro ainda não foi lançado. {pressioneQualquerTecla}");
                            }

                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Não foi encontrado nenhum livro! {pressioneQualquerTecla}");
                        Console.ReadKey();
                    }
                }

                else if (opcaoEscolhida == "2")
                {
                    Console.WriteLine("Informe o nome do livro que deseja adicionar");
                    var nomeLivro = Console.ReadLine();

                    Console.WriteLine("Informe a data de lançamento no formato dd/MM/yyyy");
                    
                    DateTime dataLancamento;
                    if (!DateTime.TryParse(Console.ReadLine(), out dataLancamento))
                    {
                        Console.WriteLine($"Data inválida! Dados descartados! {pressioneQualquerTecla}");
                        Console.ReadKey();
                        continue;
                    }

                    Console.WriteLine("Os dados estão corretos?");
                    Console.WriteLine($"Nome: {nomeLivro}");
                    Console.WriteLine($"Data de lançamento: {dataLancamento:dd/MM/yyyy}");
                    Console.WriteLine("1 - Sim \n2 - Não");

                    var opcaoParaAdicionar = Console.ReadLine();

                    if (opcaoParaAdicionar == "1")
                    {
                        var livro = new Livro(nomeLivro, dataLancamento);
                        listaLivros.Add(livro);
                        Console.WriteLine($"Dados adicionados com sucesso! {pressioneQualquerTecla}");

                    }
                    else if (opcaoParaAdicionar == "2")
                        Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                    else
                        Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");

                    Console.ReadKey();
                }

                else if (opcaoEscolhida != "3")
                {
                    Console.WriteLine($"Opção inválida. {pressioneQualquerTecla}");
                    Console.ReadKey();
                }


            } while (opcaoEscolhida != "3");

        }
    }
}
