﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.Emprestimos
{
    using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.AcervoLivros;
    using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.Reserva;
    using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Usuarios.UsuariosData;
    using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Utilities;
    using System.IO;
    using System.Runtime.ConstrainedExecution;
    using System.Windows.Forms;
    using Usuarios.UsuariosComunidadeAcademica;
    internal class EmprestimoData
    {
        private static List<Emprestimo> _emprestimoLivros = new List<Emprestimo>();

        private static readonly string _DIRECTORY_PATH = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "\\Data");
        private static readonly string _FILE_PATH = Path.Combine(_DIRECTORY_PATH, "Emprestimo.txt");

        internal static List<Emprestimo> EmprestimoLivros { get => _emprestimoLivros;}

        //static EmprestimoData()
        //{
        //    _emprestimoLivros = new List<Emprestimo>();
        //    LerEmprestimosTxt();
        //}

        //internal List<Emprestimo> SelecionarEmprestimo(ComunidadeAcademica usuario)
        //{

        //}
        //internal List<Emprestimo> SelecionarEmprestimo(Emprestimo emprestimos)
        //{

        //}

        internal static void CarregarEmprestimos()
        {
            _emprestimoLivros = LerEmprestimosTxt();
        }

        internal static Emprestimo AdicionarEmprestimo(Emprestimo emprestimo)
        {       
            if (!_emprestimoLivros.Contains(emprestimo))
            {
                _emprestimoLivros.Add(emprestimo);
                SalvarEmprestimosTxt(_emprestimoLivros);
            }
            else
            {
                throw new InvalidOperationException("O empréstimo já existe.");
            }

            return emprestimo;
        }

        internal static void IncluirEmprestimos(List<Emprestimo> emprestimosParaAdd)
        {
            _emprestimoLivros.AddRange(emprestimosParaAdd);
            SalvarEmprestimosTxt(_emprestimoLivros);
        }
        internal static void ExcluirEmprestimos(Livro livro, ComunidadeAcademica usuarioEmprestimo)
        {
            Emprestimo emprestimo = _emprestimoLivros.FirstOrDefault(e => e.ComunidadeAcademica.Matricula == usuarioEmprestimo.Matricula && e.Livro.Isbn == livro.Isbn);
            _emprestimoLivros.Remove(emprestimo);
            SalvarEmprestimosTxt(_emprestimoLivros);
        }
        internal static List<Emprestimo> SelecionarEmprestimo(Livro livro)
        {
            return _emprestimoLivros.Where(e => e.Livro == livro).ToList();
        }

        internal static Emprestimo SelecionarEmprestimo(string idEmprestimo)
        {
            if (int.TryParse(idEmprestimo, out int idEmprestimoInt))
            {
                return _emprestimoLivros.Where(e => e.IdEmprestimo == idEmprestimoInt).FirstOrDefault();
            }
            else
            {
                throw new ArgumentException("O id do empréstimo deve ser um número inteiro.");
            }
        }

        internal static List<Emprestimo> SelecionarEmprestimo(ComunidadeAcademica usuario)
        {
            return _emprestimoLivros.Where(e => e.ComunidadeAcademica == usuario).ToList();
        }

        internal static List<Emprestimo> SelecionarEmprestimo(ReservaLivro reserva)
        {
            return _emprestimoLivros.Where(e => e.ReservaLivro == reserva).ToList();
        }

        internal static List<Emprestimo> LerEmprestimosTxt()
        {
            try
            {
                List<Emprestimo> emprestimos = new List<Emprestimo>();

                using (StreamReader sr = new StreamReader(_FILE_PATH))
                {
                    while (!sr.EndOfStream)
                    {
                        string linha = sr.ReadLine();
                        try
                        {
                            Emprestimo emprestimo = ConverterLinhaParaEmprestimo(linha);
                            emprestimos.Add(emprestimo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao converter linha para empréstimo: {ex.Message}");
                        }
                    }
                }

                return emprestimos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ler o arquivo: {ex.Message}");
                return new List<Emprestimo>();
            }
        }

        internal static Emprestimo ConverterLinhaParaEmprestimo(string linha)
        {
            string[] partes = linha.Split(',');

            if (partes.Length < 2)
            {
                throw new ArgumentException("Formato de linha inválido para empréstimo.");
            }

            string isbnLivro = partes[0];
            Livro livro = LivroData.SelecionarLivro(isbnLivro);

            string cpfUsuario = partes[1];
            ComunidadeAcademica usuario = UsuarioData.SelecionarUsuarioCA(cpfUsuario);

            return new Emprestimo(null, livro, usuario);
        }



        internal static void SalvarEmprestimosTxt(List<Emprestimo> emprestimos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_FILE_PATH))
                {
                    foreach (Emprestimo emprestimo in emprestimos)
                    {
                        string linha = ConverterEmprestimoParaLinha(emprestimo);
                        sw.WriteLine(linha);
                    }
                }

                LerEmprestimosTxt();

                MessageBox.Show("Alterações salvas com sucesso no arquivo.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar as alterações no arquivo: {ex.Message}");
            }
        }

        internal static string ConverterEmprestimoParaLinha(Emprestimo emprestimo)
        {

            return $"{emprestimo.Livro.Isbn},{emprestimo.ComunidadeAcademica.Cpf}";
        }

        internal static void CriarEmprestimo (Emprestimo emprestimo)
        {
            _emprestimoLivros.Add(emprestimo);

            SalvarEmprestimosTxt(_emprestimoLivros);
        }

        internal static void CriarEmprestimoSemReserva(ComboBox cbUsuarios, ComboBox lbLivros)
        {
            ComunidadeAcademica usuario = cbUsuarios.SelectedItem as ComunidadeAcademica;
            Livro livro = lbLivros.SelectedItem as Livro;

            Emprestimo emprestimo = new Emprestimo(null, livro, usuario);

            _emprestimoLivros.Add(emprestimo);

            SalvarEmprestimosTxt(_emprestimoLivros);
        }

    }
}
