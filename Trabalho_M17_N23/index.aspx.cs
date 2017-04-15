using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Trabalho_M17_N23
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "A Lojinha das Sementes";
            //listar produtos disponiveis
            if (!IsPostBack)
            {
                DataTable dados = BaseDados.Instance.devolveConsulta("SELECT id,strain,preco,avaliacao,sativa_indica FROM Produtos WHERE stock>0");
                atualizaDivProdutos(dados);
            }
        }

        private void atualizaDivProdutos(DataTable dados)
        {

            if (dados == null || dados.Rows.Count == 0)
            {
                div_produtos.InnerHtml = "";
                return;
            }

            string grelha = "<div class='container-fluid'>";
            grelha += "<div class ='row'>";

            foreach (DataRow produtos in dados.Rows)
            {
                grelha += "</br></br>";
                grelha += "<div class='col-md-2 text-center'>";
                grelha += "<img src='/Imagens/" + produtos[0].ToString() + ".jpg' class='img-responsive'/>";
                grelha += "<span class='stat-title'>" + produtos[4].ToString() +" - "+ produtos[1].ToString() + "</span></br>";
                try
                {
                    decimal avaliacao = Convert.ToDecimal(produtos[3].ToString());
                    avaliacao = Math.Round(avaliacao);

                    grelha += "<img src='/resources/" + avaliacao + "_estrelas.png'  height='30px'/>";
                }
                catch (Exception)
                {

                    grelha += "<img src='/resources/sem_avaliacao.png' height='30px'/>";
                }
                   
                
              
               
                grelha += "<span class='stat-title'>" + string.Format(" | {0:C}", produtos[2].ToString()) + "€</span>";
                grelha += "<br/><a href = 'detalhesProduto.aspx?id=" + produtos[0].ToString() + "'>Detalhes</a>";
                grelha += "</div></div>";
            }

            grelha += "</div></div>";
            div_produtos.InnerHtml = grelha;

        }

        protected void btPesquisa_Click(object sender, EventArgs e)
        {

            DataTable dados = BaseDados.Instance.pesquisaProdutosPeloNome(tbPesquisa.Text);
            atualizaDivProdutos(dados);
        }
    }
}