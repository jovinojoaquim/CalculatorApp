using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalculatorApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        double primeiroNumero = 0;
        double segundoNumero = 0;
        private string operadorPenultimo;
        string operadorUltimo = "";
        bool zerarPaineis = false;
        bool zerarPainelTexto = false;
        private string numeroClicado;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnNumero(object sender, EventArgs e)
        {
            var botao = (Button)sender;
            numeroClicado += botao.Text;
            if (etPainel.Text.FirstOrDefault() == '0')
                etPainel.Text = "";
            if (double.TryParse(etPainel.Text, out primeiroNumero) && etPainel.Text.Length < 8 && primeiroNumero != 0 && zerarPaineis == false && zerarPainelTexto == false)
            {
                etPainel.Text += botao.Text;
                etHistorico.Text += botao.Text;
            }
            else if (double.TryParse(etPainel.Text, out primeiroNumero) && etPainel.Text.Length < 8 && primeiroNumero != 0 && zerarPaineis == false && zerarPainelTexto == true)
            {
                etPainel.Text = botao.Text;
                etHistorico.Text += botao.Text;
                zerarPainelTexto = false;
            }
            else if (double.TryParse(etPainel.Text, out primeiroNumero) && etPainel.Text.Length < 8 && primeiroNumero != 0 && zerarPaineis == true)
            {
                etHistorico.Text = "" + botao.Text;
                etPainel.Text = "" + botao.Text;
                zerarPaineis = false;
                zerarPainelTexto = false;
            }
            else
            {
                etHistorico.Text = botao.Text;
                etPainel.Text = botao.Text;
            }
        }

        private void OnAcao(object sender, EventArgs e)
        {
            var botao = (Button)sender;

            if (botao.Text.Equals("="))
            {
                OperacaoMatematica(operadorUltimo);
                etHistorico.Text = etHistorico.Text.Remove(etHistorico.Text.LastIndexOf(operadorUltimo));
                segundoNumero = 0;
                zerarPaineis = true;
            }
            else
                OperacaoMatematica(botao.Text);
            numeroClicado = "";
            primeiroNumero = 0;
        }

        private void OperacaoMatematica(string operador)
        {
            etHistorico.Text += $" {operador} ";

            operadorPenultimo = operadorUltimo;
            operadorUltimo = operador;

            if (operadorPenultimo.Equals("+") && segundoNumero != 0)
            {
                segundoNumero = double.Parse(etPainel.Text) + segundoNumero;
                etPainel.Text = segundoNumero.ToString();
                primeiroNumero = 0;
            }

            if (operadorPenultimo.Equals("-") && segundoNumero != 0)
            {
                segundoNumero -= double.Parse(etPainel.Text);
                etPainel.Text = segundoNumero.ToString();
                primeiroNumero = 0;
            }

             if (operadorPenultimo.Equals("/") && segundoNumero != 0)
            {
                segundoNumero /= double.Parse(etPainel.Text);
                etPainel.Text = segundoNumero.ToString();
                primeiroNumero = 0;
            }
            if (operadorPenultimo.Equals("x") && segundoNumero != 0)
            {
                etHistorico.Text += " x ";
                segundoNumero *= double.Parse(etPainel.Text);
                etPainel.Text = segundoNumero.ToString();
                primeiroNumero = 0;
            }

             if(segundoNumero == 0)
                InverterValores(double.Parse(etPainel.Text));

            zerarPainelTexto = true;
        }

        private void OnAcaoInverter(object sender, EventArgs e)
        {
            var botao = (Button)sender;
            if (botao.Text.Equals("+/-"))
            {
                etPainel.Text = etPainel.Text.Remove(etPainel.Text.LastIndexOf(numeroClicado));
                etPainel.Text += (double.Parse(numeroClicado) * -1).ToString();
            }
        }


        public void InverterValores(double primeiro)
        {
            segundoNumero = primeiro;
            primeiroNumero = 0;
        }

        private void OnClearAll(object sender, EventArgs e)
        {
            etPainel.Text = "";
            etHistorico.Text = "";
            primeiroNumero = 0;
            segundoNumero = 0;
            operadorPenultimo = "";
            operadorUltimo = "";
            numeroClicado = "";
        }
    }
}
