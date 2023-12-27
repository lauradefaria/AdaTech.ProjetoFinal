﻿using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.AcervoLivros
{
    internal static class LivroData
    {
        private static List<Livro> _acervoLivros = new List<Livro>();
        private static readonly string FILE_PATH = "../../../Data/Livros.txt";

        static LivroData()
        {
            _acervoLivros = LerLivrosTxt();
        }

        internal static void IncluirLivros(List<Livro> livros)
        {
            _acervoLivros.AddRange(livros);
            SalvarLivrosTxt(_acervoLivros);
        }

        internal static void IncluirLivros(string titulo, string autor, string isbn, int anoPublicacao, int edicao, string editora,
            int exemplares)
        {
            _acervoLivros.Add(new Livro(titulo, autor, isbn, anoPublicacao, edicao, editora, exemplares - 1, TipoAcervoLivro.AcervoPublico));
            _acervoLivros.Add(new Livro(titulo, autor, isbn + 'b', anoPublicacao, edicao, editora, 1, TipoAcervoLivro.AcervoParticular));
            SalvarLivrosTxt(_acervoLivros);
        }

        internal static void AtualizarLivros(List<Livro> livros)
        {
            SalvarLivrosTxt(livros);
        }

        internal static List<Livro> ListarLivros(TipoAcervoLivro? tipoAcervoLivro)
        {
            List<Livro> livrosAcervo = _acervoLivros.Where(l => l.TipoAcervoLivro == tipoAcervoLivro).ToList();
            return livrosAcervo;
        }

        internal static void ExcluirLivros(string[] isbnLivros)
        {
            foreach (string isbn in isbnLivros)
            {
                Livro livro = _acervoLivros.Where(l => l.Isbn == isbn).FirstOrDefault();
                _acervoLivros.Remove(livro);
                SalvarLivrosTxt(_acervoLivros);
            }
        }

        internal static Livro SelecionarLivro(string isbn)
        {
            return _acervoLivros.Where(l => l.Isbn == isbn).FirstOrDefault();
        }

        internal static List<Livro> LerLivrosTxt()
        {
            List<Livro> livros = new List<Livro>();

            try
            {
                using (StreamReader sr = new StreamReader(FILE_PATH))
                {
                    while (!sr.EndOfStream)
                    {
                        string linha = sr.ReadLine();
                        Livro livro = ConverterLinhaParaLivro(linha);
                        livros.Add(livro);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
            }

            return livros;
        }

        private static Livro ConverterLinhaParaLivro(string linha)
        {
            string[] partes = linha.Split(',');
            string titulo = partes[0];
            string autor = partes[1];
            string isbn = partes[2];
            int anoPublicacao = Conversores.StringParaInt(partes[3]);
            int edicao = Conversores.StringParaInt(partes[4]);
            string editora = partes[5];
            int exemplares = Conversores.StringParaInt(partes[6]);
            int exemplaresDisponiveis = Conversores.StringParaInt(partes[7]);
            int livrosBomEstado = Conversores.StringParaInt(partes[8]);
            int livrosEstadoMediano = Conversores.StringParaInt(partes[9]);
            int livrosMauEstado = Conversores.StringParaInt(partes[10]);
            TipoAcervoLivro tipoAcervoLivro = Conversores.StringParaTipoAcervoLivro(partes[11]);

            return new Livro(titulo, autor, isbn, anoPublicacao, edicao, editora, exemplares, exemplaresDisponiveis, livrosBomEstado, livrosEstadoMediano, livrosMauEstado, tipoAcervoLivro);
        }

        internal static void SalvarLivrosTxt(List<Livro> livros)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FILE_PATH))
                {
                    foreach (Livro livro in livros)
                    {
                        string linha = ConverterLivroParaLinha(livro);
                        sw.WriteLine(linha);
                    }
                }

                Console.WriteLine("Alterações salvas com sucesso no arquivo.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar as alterações no arquivo: {ex.Message}");
            }
        }

        private static string ConverterLivroParaLinha(Livro livro)
        {
            return $"{livro.Titulo},{livro.Autor},{livro.Isbn},{livro.AnoPublicacao},{livro.Edicao},{livro.Editora},{livro.Exemplares},{livro.ExemplaresDisponiveis},{livro.LivrosBomEstado},{livro.LivrosEstadoMediano},{livro.LivrosMauEstado},{livro.TipoAcervoLivro}";
        }
    }
}

