﻿using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.AcervoLivros;
using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.Reserva;
using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AdaTech.ProjetoFinal.BibliotecaCentral.Models.Usuarios.UsuariosData
{
    internal static class UsuarioData
    {
        private static List<Atendente> _atendentes = new List<Atendente> { 
            new Atendente("12345678", "Amandinha Linda", "07723268513", "amandinha@linda.com.br") };

        private static List<Bibliotecario> _bibliotecarios;
        private static List<Diretor> _diretores;
        private static List<ComunidadeAcademica> _comunidadeAcademica;

        private static readonly string _FILE_PATH_DIRETOR = "../../../Data/Diretores.txt";
        private static readonly string _FILE_PATH_BIBLIOTECARIO = "../../../Data/Bibliotecarios.txt";
        private static readonly string _FILE_PATH_ATENDENTE = "../../../Data/Atendentes.txt";
        private static readonly string _FILE_PATH_CA = "../../../Data/ComunidadeAcademica.txt";


        static UsuarioData()
        {
            _diretores = LerDiretoresTxt();
            _bibliotecarios = LerBibliotecariosTxt();
            //_atendentes = LerAtendentesTxt();
            _comunidadeAcademica = LerComunidadeAcademicaTxt();
        }

        public static List<Atendente> Atendentes
        {
            get { return _atendentes; }
        }

        public static List<Bibliotecario> Bibliotecarios
        {
            get { return _bibliotecarios; }
        }

        public static List<Diretor> Diretores
        {
            get { return _diretores; }
        }

        public static List<ComunidadeAcademica> ComunidadeAcademica
        {
            get { return _comunidadeAcademica; }
        }


        internal static void IncluirUsuario(Usuario usuario)
        {
            if (usuario is Atendente)
            {
                _atendentes.Add((Atendente)usuario);
            }
            else if (usuario is Bibliotecario)
            {
                _bibliotecarios.Add((Bibliotecario)usuario);
            }
            else if (usuario is Diretor)
            {
                _diretores.Add((Diretor)usuario);
            }
            else if (usuario is ComunidadeAcademica)
            {
                _comunidadeAcademica.Add((ComunidadeAcademica)usuario);
            }
        }

        internal static void IncluirUsuario (List<Usuario> usuarios)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario is Atendente)
                {
                    _atendentes.Add((Atendente)usuario);
                }
                else if (usuario is Bibliotecario)
                {
                    _bibliotecarios.Add((Bibliotecario)usuario);
                }
                else if (usuario is Diretor)
                {
                    _diretores.Add((Diretor)usuario);
                }
                else if (usuario is ComunidadeAcademica)
                {
                    _comunidadeAcademica.Add((ComunidadeAcademica)usuario);
                }
            }
        }   

        internal static void RemoverUsuario (Usuario usuario)
        {
            if (usuario is Atendente)
            {
                _atendentes.Remove((Atendente)usuario);
            }
            else if (usuario is Bibliotecario)
            {
                _bibliotecarios.Remove((Bibliotecario)usuario);
            }
            else if (usuario is Diretor)
            {
                _diretores.Remove((Diretor)usuario);
            }
            else if (usuario is ComunidadeAcademica)
            {
                _comunidadeAcademica.Remove((ComunidadeAcademica)usuario);
            }
        }

        internal static void RemoverUsuario (List<Usuario> usuarios)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario is Atendente)
                {
                    _atendentes.Remove((Atendente)usuario);
                }
                else if (usuario is Bibliotecario)
                {
                    _bibliotecarios.Remove((Bibliotecario)usuario);
                }
                else if (usuario is Diretor)
                {
                    _diretores.Remove((Diretor)usuario);
                }
                else if (usuario is ComunidadeAcademica)
                {
                    _comunidadeAcademica.Remove((ComunidadeAcademica)usuario);
                }
            }
        }


        internal static void AtualizarUsuario (Usuario usuario)
        {
            if (usuario is Atendente)
            {
                _atendentes.Remove((Atendente)usuario);
                _atendentes.Add((Atendente)usuario);
            }
            else if (usuario is Bibliotecario)
            {
                _bibliotecarios.Remove((Bibliotecario)usuario);
                _bibliotecarios.Add((Bibliotecario)usuario);
            }
            else if (usuario is Diretor)
            {
                _diretores.Remove((Diretor)usuario);
                _diretores.Add((Diretor)usuario);
            }
            else if (usuario is ComunidadeAcademica)
            {
                _comunidadeAcademica.Remove((ComunidadeAcademica)usuario);
                _comunidadeAcademica.Add((ComunidadeAcademica)usuario);
            }
        }

        internal static void AtualizarUsuario (List<Usuario> usuarios)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario is Atendente)
                {
                    _atendentes.Remove((Atendente)usuario);
                    _atendentes.Add((Atendente)usuario);
                }
                else if (usuario is Bibliotecario)
                {
                    _bibliotecarios.Remove((Bibliotecario)usuario);
                    _bibliotecarios.Add((Bibliotecario)usuario);
                }
                else if (usuario is Diretor)
                {
                    _diretores.Remove((Diretor)usuario);
                    _diretores.Add((Diretor)usuario);
                }
                else if (usuario is ComunidadeAcademica)
                {
                    _comunidadeAcademica.Remove((ComunidadeAcademica)usuario);
                    _comunidadeAcademica.Add((ComunidadeAcademica)usuario);
                }
            }
        }

        internal static Usuario SelecionarUsuario (string login)
        {
            Usuario usuario = _atendentes.Where(u => u.Login == login).FirstOrDefault();
            if (usuario != null) return usuario;

            usuario = _bibliotecarios.Where(u => u.Login == login).FirstOrDefault();
            if (usuario != null) return usuario;

            usuario = _diretores.Where(u => u.Login == login).FirstOrDefault();
            if (usuario != null) return usuario;

            usuario = _comunidadeAcademica.Where(u => u.Login == login).FirstOrDefault();
            if (usuario != null) return usuario;

            throw new InvalidOperationException("Usuário não encontrado.");
        }


        internal static List<Diretor> LerDiretoresTxt()
        {
            List<Diretor> listaDiretor = new List<Diretor>();

            try
            {
                if (File.Exists(_FILE_PATH_DIRETOR))
                {

                    using (StreamReader sr = new StreamReader(_FILE_PATH_DIRETOR))
                    {
                        while ((!sr.EndOfStream) != null)
                        {
                            string linha = sr.ReadLine();
                            Diretor diretor = ConverterLinhaParaDiretor(linha);
                            listaDiretor.Add(diretor);
                        }
                    }
                    Console.WriteLine("Dados carregados do arquivo txt.");
                }
                else
                {
                    Console.WriteLine("O arquivo txt não existe.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("O arquivo não pôde ser aberto: " + e.Message);
            }
            return listaDiretor;
        }

        private static Diretor ConverterLinhaParaDiretor(string linha)
        {
            string[] objetoString = linha.Split(',');

            string senha = objetoString[0];
            string nomeCompleto = objetoString[1];
            string cpf = objetoString[2];
            string email = objetoString[3];
            bool ativo = bool.Parse(objetoString[4]);

            return new Diretor(senha, nomeCompleto, cpf, email, ativo);
        }


        internal static List<Atendente> LerAtendentesTxt()
        {
            List<Atendente> listaAtendente = new List<Atendente>();

            try
            {
                if (File.Exists(_FILE_PATH_ATENDENTE))
                {

                    using (StreamReader sr = new StreamReader(_FILE_PATH_ATENDENTE))
                    {
                        

                        while ((!sr.EndOfStream) != null)
                        {
                            string linha = sr.ReadLine();
                            Atendente atendente = ConverterLinhaParaAtendente(linha);
                            listaAtendente.Add(atendente);
                        }
                    }
                    Console.WriteLine("Dados carregados do arquivo txt.");
                }
                else
                {
                    Console.WriteLine("O arquivo txt não existe.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("O arquivo não pôde ser aberto: " + e.Message);
            }
            return listaAtendente;
        }

            private static Atendente ConverterLinhaParaAtendente(string linha)
            {
                string[] objetoString = linha.Split(',');

                string senha = objetoString[0];
                string nomeCompleto = objetoString[1];
                string cpf = objetoString[2];
                string email = objetoString[3];
                bool ativo = bool.Parse(objetoString[4]);

                return new Atendente(senha, nomeCompleto, cpf, email, ativo);
            }

            internal static List<Bibliotecario> LerBibliotecariosTxt()
            {
            List<Bibliotecario> listaBibliotecario = new List<Bibliotecario>();

            try
            {
                if (File.Exists(_FILE_PATH_BIBLIOTECARIO))
                {

                    using (StreamReader sr = new StreamReader(_FILE_PATH_BIBLIOTECARIO))
                    {
                        while ((!sr.EndOfStream) != null)
                        {
                            string linha = sr.ReadLine();
                            Bibliotecario bibliotecario = ConverterLinhaParaBibliotecario(linha);
                            listaBibliotecario.Add(bibliotecario);
                        }
                    }
                    Console.WriteLine("Dados carregados do arquivo txt.");
                }
                else
                {
                    Console.WriteLine("O arquivo txt não existe.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("O arquivo não pôde ser aberto: " + e.Message);
            }
            return listaBibliotecario;
            }




        internal static List<ComunidadeAcademica> LerComunidadeAcademicaTxt()
        {
            List<ComunidadeAcademica> listaComunidadeAcademica = new List<ComunidadeAcademica>();

            try
            {
                if (File.Exists(_FILE_PATH_CA))
                {

                    using (StreamReader sr = new StreamReader(_FILE_PATH_CA))
                    {
                        while ((!sr.EndOfStream) != null)
                        {
                            string linha = sr.ReadLine();
                            ComunidadeAcademica comunidadeAcademica = ConverterLinhaParaComunidadeAcademica(linha);
                            listaComunidadeAcademica.Add(comunidadeAcademica);
                        }
                    }
                    Console.WriteLine("Dados carregados do arquivo txt.");
                }
                else
                {
                    Console.WriteLine("O arquivo txt não existe.");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("O arquivo não pôde ser aberto: " + e.Message);
            }
 
            return listaComunidadeAcademica;
        }

        internal static ComunidadeAcademica ConverterLinhaParaComunidadeAcademica(string linha)
        {
            string[] objetoString = linha.Split(',');

            string senha = objetoString[0];
            string nomeCompleto = objetoString[1];
            string cpf = objetoString[2];
            string email = objetoString[3];
            string matricula = objetoString[4];
            string curso = objetoString[5];
            TipoUsuarioComunidade tipoUsuario = Conversores.StringParaTipoUsuarioComunidade(objetoString[6]);
            bool ativo = bool.Parse(objetoString[6]);

            return new ComunidadeAcademica(senha, nomeCompleto, cpf, email, matricula, curso, tipoUsuario);
        }

        internal static Bibliotecario ConverterLinhaParaBibliotecario(string linha)
        {
            string[] objetoString = linha.Split(',');

            string senha = objetoString[0];
            string nomeCompleto = objetoString[1];
            string cpf = objetoString[2];
            string email = objetoString[3];
            bool ativo = bool.Parse(objetoString[4]);

            return new Bibliotecario(senha, nomeCompleto, cpf, email, ativo);
        }

        internal static void SalvarAtendentesTxt(List<Atendente> atendentes)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_FILE_PATH_ATENDENTE))
                {
                    foreach (Atendente atendente in atendentes)
                    {
                        string linha = ConverterAtendenteParaLinha(atendente);
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

        internal static void SalvarDiretoresTxt(List<Diretor> diretores)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_FILE_PATH_DIRETOR))
                {
                    foreach (Diretor diretor in diretores)
                    {
                        string linha = ConverterDiretorParaLinha(diretor);
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

        internal static void SalvarComunidadeAcademicaTxt(List<ComunidadeAcademica> comunidadeAcademica)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_FILE_PATH_CA))
                {
                    foreach (ComunidadeAcademica CA in comunidadeAcademica)
                    {
                        string linha = ConverterComunidadeAcademicaParaLinha(CA);
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

        internal static void SalvarBibliotecariosTxt(List<Bibliotecario> bibliotecarios)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_FILE_PATH_BIBLIOTECARIO))
                {
                    foreach (Bibliotecario bibliotecario in bibliotecarios)
                    {
                        string linha = ConverterBibliotecarioParaLinha(bibliotecario);
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

        internal static string ConverterDiretorParaLinha(Diretor diretor)
        {
            return $"{diretor.SenhaCripto},{diretor.NomeCompleto},{diretor.Cpf},{diretor.Email},{diretor.Ativo},{diretor.EhAdmin}";
        }

        internal static string ConverterAtendenteParaLinha(Atendente atendente)
        {
            return $"{atendente.SenhaCripto},{atendente.NomeCompleto},{atendente.Cpf},{atendente.Email},{atendente.Ativo}";
        }

        internal static string ConverterComunidadeAcademicaParaLinha(ComunidadeAcademica usuario)
        {
            return $"{usuario.Matricula},{usuario.Curso},{usuario.TipoUsuario}";
        }

        internal static string ConverterBibliotecarioParaLinha(Bibliotecario bibliotecario)
        {
            return $"{bibliotecario.SenhaCripto},{bibliotecario.NomeCompleto},{bibliotecario.Cpf},{bibliotecario.Email},{bibliotecario.Ativo}";
        }

    }
}