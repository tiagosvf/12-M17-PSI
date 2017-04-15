using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Trabalho_M17_N23
{
    public partial class detalhescompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (Request["id"] == null)
                Response.Redirect("index.aspx");
            if (Session["id"] == null)
                Response.Redirect("login.aspx");

            lbErro.Visible = false;
            
            divDados.Visible = false;

            try
            {

                int id = int.Parse(Request["id"].ToString());


                DataTable dados = BaseDados.Instance.listaDadosVenda(id);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Não existe nenhuma venda com este id");

                if (int.Parse(dados.Rows[0][1].ToString()) != int.Parse(Session["id"].ToString()) && int.Parse(Session["perfil"].ToString())==1)
                    Response.Redirect("index.aspx");



                Page.Title = "Detalhes da compra #" + dados.Rows[0][0].ToString() +" - A Lojinha das Sementes";

              

                lbID.Text = dados.Rows[0][0].ToString();
                lbNome.Text = dados.Rows[0][2].ToString() + " " + dados.Rows[0][3].ToString();
                lbCp.Text = dados.Rows[0][4].ToString();
                lbMorada.Text = dados.Rows[0][5].ToString();
                lbEstado.Text = dados.Rows[0][6].ToString();
                lbTotal.Text = dados.Rows[0][7].ToString()+"€";


                atualizaGrelhaProdutos();



            }
            catch (Exception erro)
            {

                lbErro.Visible = true;
                lbErro.Text = "Ocorreu um erro. " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
           
        }
        private void atualizaGrelhaProdutos()
        {
            lbErro.Visible = false;
            gvProdutos.Columns.Clear();
            gvProdutos.DataSource = null;
            gvProdutos.DataBind();

            DataTable dados = BaseDados.Instance.devolveProdutosVenda(int.Parse(Request["id"].ToString()));
            if (dados == null || dados.Rows.Count == 0)
            {
                lbErro.Text = "Não tem produtos no carrinho";
                lbErro.Visible = true;
                lbErro.CssClass = "alert alert-danger";
                divDados.Visible = false;
                gvProdutos.Visible = false;
            }
            else
            {
                divDados.Visible = true;
                gvProdutos.Visible = true;

                gvProdutos.DataSource = dados;
                gvProdutos.AutoGenerateColumns = true;



            

                ImageField ifImagem = new ImageField();
                ifImagem.HeaderText = "Imagem";
                ifImagem.DataImageUrlFormatString = "~/Imagens/{0}.jpg";
                ifImagem.DataImageUrlField = "id";
                ifImagem.ControlStyle.Width = 100;
                gvProdutos.Columns.Add(ifImagem);



                gvProdutos.DataBind();
            }
        }
    }
}