﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.Reserva;
using AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.AcervoLivros;
using System.Windows.Forms;

namespace AdaTech.ProjetoFinal.BibliotecaCentral.Models.Business.Emprestimos
{
    internal class Emprestimo
    {
        private readonly ReservaLivro _reservaLivro;
        private readonly Livro _livro;
        private readonly ComunidadeAcademica _usuarioComunidadeAcademica;
        private readonly DateTime _dataEmprestimo;
        private DateTime _dataDevolucaoPrevista;
        private DateTime _dataDevolucaoUsuario;
        private bool _devolucao;
        private Multa _multaAtraso;
        private int _renovacoes;
        private int _idEmprestimo;
        private static int _idEmprestimoAtual = 1;

        internal ReservaLivro ReservaLivro { get { return _reservaLivro; } }
        internal Livro Livro { get { return _livro; } }
        internal ComunidadeAcademica ComunidadeAcademica { get { return _usuarioComunidadeAcademica; } }

        internal int IdEmprestimo { get { return _idEmprestimo; } }

        internal DateTime DataEmprestimo { get { return _dataEmprestimo; } }

        internal DateTime DataDevolucaoPrevista
        {
            get { return _dataDevolucaoPrevista; }
            set { _dataDevolucaoPrevista = value; }
        }

        internal Multa Multa { get { return _multaAtraso; } }

        internal DateTime DataDevolucaoUsuario
        {
            get
            {
                return _dataDevolucaoUsuario;
            }
            private set
            {
                _dataDevolucaoUsuario = value;
            }
        }

        internal bool Devolucao
        {
            get
            {
                return _devolucao;
            }
            private set
            {
                _devolucao = value;
            }
        }


        internal Emprestimo(ReservaLivro reservaLivro = null, Livro livro = null, ComunidadeAcademica usuarioComunidadeAcademica = null, bool devolucao = false)
        {
            try 
            {
                if (reservaLivro == null && (livro == null || usuarioComunidadeAcademica == null))
                {
                    throw new Exception("Não é possível criar um empréstimo sem um livro e um usuário.");

                }
                else if (reservaLivro != null)
                {
                    _reservaLivro = reservaLivro;
                    _livro = _reservaLivro.Livro;
                    _usuarioComunidadeAcademica = _reservaLivro.UsuarioComunidadeAcademica;
                }
                else if (reservaLivro == null && livro != null && usuarioComunidadeAcademica != null)
                {
                    _livro = livro;
                    _usuarioComunidadeAcademica = usuarioComunidadeAcademica;
                }

                _dataEmprestimo = DateTime.Now;
                _dataDevolucaoPrevista = _dataEmprestimo.AddDays(7);
                _devolucao = devolucao;
                _renovacoes = 3;
                _livro.ExemplaresDisponiveis--;
                _idEmprestimo = GerarIdEmprestimo();

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void DevolverLivro()
        {
            Devolucao = true;
            DataDevolucaoUsuario = DateTime.Now;
            CalcularMulta();
        }

        internal int GerarIdEmprestimo()
        {
            _idEmprestimo = _idEmprestimoAtual;
            _idEmprestimoAtual++;
            return _idEmprestimo;
        }


        internal void RenovarLivro()
        {
            if (ReservaLivroData.SelecionarReserva(this) == null && this._renovacoes > 0)
            {
                this.DataDevolucaoPrevista = this.DataDevolucaoPrevista.AddDays(7);
                this._renovacoes--;
            } else if (this._renovacoes == 0)
            {
                MessageBox.Show("Não é possível renovar o livro, pois o limite de renovações foi atingido.");
            } else
            {
                MessageBox.Show("Não é possível renovar o livro, pois o mesmo está reservado.");
            }
        }

        private void CalcularMulta()
        {
            if (DataDevolucaoUsuario > DataDevolucaoPrevista)
            {
                _multaAtraso = new Multa(DataDevolucaoUsuario, DataDevolucaoPrevista);
            }
        }

        public override string ToString()
        {
            DateTime DataDevolucao = (_devolucao) ? _dataDevolucaoUsuario : _dataDevolucaoPrevista
            return $"- Livro: {_livro.Titulo}\r\n" +
                $"- Usuario: {_usuarioComunidadeAcademica.ToString()}\r\n " +
                $"- Data de emprestimo: {_dataEmprestimo}\r\n" +
                $"- Data de Devolução: {DataDevolucao}" +
                $"- Devolução: {_devolucao}";
        }
    }
}
