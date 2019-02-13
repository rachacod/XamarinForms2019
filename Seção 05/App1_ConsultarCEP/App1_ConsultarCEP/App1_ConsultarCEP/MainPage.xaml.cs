using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1_ConsultarCEP.Servico.Modelo;
using App1_ConsultarCEP.Servico;

namespace App1_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Captura o evento de click do botão e concatena e atribui um metodo que dei o nome de BuscarCEP
            BOTAO.Clicked += BuscarCEP;
        }

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - Validações

            //recebe o texto digitado e armazena na variavel cep
            string cep = CEP.Text.Trim();

            //valida CEP
            if (isValidCEP(cep))
            {
                try
                {
 
                Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereço: {2} de {3} {0},{1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }

                }
                catch (Exception e)
                {

                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }     
        }

        //Método para validar o CEP
        private bool isValidCEP(string cep)
        {
            //variavel que armazena o resultado da verificação
            bool valido = true;

            //verifica se o CEP tem 8 caracteres
            if (cep.Length != 8)
            {
                //Mostra uma mensagem de erro para o usuário
                DisplayAlert("ERRO", "CEP invalido! O CEP deve conte 8 caracteres.", "OK");

                valido = false;
            }

            //variavel que armazena o resultado do TryParse
            int NovoCEP = 0;

            //tenta converter o CEP para int
            // se não for inteiro retorna false para a variavel NovoCEP
            if (!int.TryParse(cep, out NovoCEP))
            {
                //Mostra uma mensagem de erro para o usuário
                DisplayAlert("ERRO", "CEP invalido! O CEP deve ser coposto apenas por números.", "OK");

                valido = false;
            }

            //retorna o resultado da validação
            return valido;
        }
    }
}
