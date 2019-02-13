using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using App1_ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace App1_ConsultarCEP.Servico
{
    //Classe responsavel por fazer o download das informações e também para ter o endereço preenchido com o endereco do site ViaCEP
    public class ViaCEPServico
    {
        //Propriedade do tipo estática da classe
        private static string EnderecoUrl = "https://viacep.com.br/ws/{0}/json/";

        //Método que busca o endereço
        public static Endereco BuscarEnderecoViaCEP(string cep)
        {
            //subistitui a URL
            string NovoEnderecoURL = string.Format(EnderecoUrl, cep);

            //Classe utilizda para fazer download das informações
            WebClient wc = new WebClient();

            //método da responsavel por fazer download do tipo string (OBS. testar os metodos Async, Complete e TaskAsync)
            string Conteudo = wc.DownloadString(NovoEnderecoURL);

            //Converte a variavel Conteudo em um objeto do tipo Endereco
            //JasonConvert é utilizado para converter o que foi
            Endereco end = JsonConvert.DeserializeObject<Endereco>(Conteudo);

            if (end.cep == null) return null;

            //retorna a variavel end
            return end;

            
        }
    }
}
