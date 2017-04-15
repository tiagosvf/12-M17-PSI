using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Trabalho_M17_N23
{
    public partial class carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.Title = "Carrinho - A Lojinha das Sementes";
            lbErroTerminar.Visible = false;
            lbErro.Visible = false;
            divProdutos.Visible = false;
            divDados.Visible = false;
            if (Session["perfil"] == null )
                Response.Redirect("index.aspx");

            gvProdutos.RowCommand += new GridViewCommandEventHandler(this.gvProdutos_RowCommand);
            
            
            atualizaGrelhaProdutos();
        }
        private void atualizaGrelhaProdutos()
        {
            lbErro.Visible = false;
            gvProdutos.Columns.Clear();
            gvProdutos.DataSource = null;
            gvProdutos.DataBind();

            DataTable dados = BaseDados.Instance.devolveDadosProdutosCarrinho(int.Parse(Session["id"].ToString()));
            if (dados == null || dados.Rows.Count == 0)
            {
                lbErro.Text = "Não tem produtos no carrinho";
                lbErro.Visible = true;
                lbErro.CssClass = "alert alert-danger";
                divDados.Visible = false;
                divProdutos.Visible = false;
            }
            else
            {
                divDados.Visible = true;
                divProdutos.Visible = true;

                gvProdutos.DataSource = dados;
                gvProdutos.AutoGenerateColumns = true;



                //adicionar coluna para remover
                ButtonField btRemover = new ButtonField();
                btRemover.HeaderText = "Remover";
                btRemover.Text = "Remover";
                btRemover.ButtonType = ButtonType.Button;
                btRemover.CommandName = "Remover";
                gvProdutos.Columns.Add(btRemover);

                ImageField ifImagem = new ImageField();
                ifImagem.HeaderText = "Imagem";
                ifImagem.DataImageUrlFormatString = "~/Imagens/{0}.jpg";
                ifImagem.DataImageUrlField = "id";
                ifImagem.ControlStyle.Width = 100;
                gvProdutos.Columns.Add(ifImagem);



                gvProdutos.DataBind();
            }
        }
        private void gvProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = 0;
            try
            {

            int linha = int.Parse(e.CommandArgument as string);
             id = int.Parse(gvProdutos.Rows[linha].Cells[2].Text);
            }
            catch
            {
                return;
            }
            if (e.CommandName == "Remover")
            {
                try
                {
                    BaseDados.Instance.removerDoCarrinho(id, int.Parse(Session["id"].ToString()));
                 
                    atualizaGrelhaProdutos();
                    lbErro.Text = "Produto removido";
                    lbErro.CssClass = "alert alert-success";
                }
                catch (Exception)
                {

                    lbErro.Text = "Ocorreu um erro.";
                    lbErro.CssClass = "alert alert-danger";
                }
             
            }
       
        }

        protected void btTerminar_Click(object sender, EventArgs e)
        {
            lbErroTerminar.Visible = false;

            try
            {

               
                int id = int.Parse(Session["id"].ToString());

                DataTable teste_carrinho = BaseDados.Instance.devolveDadosProdutosCarrinho(id);
                if (teste_carrinho == null || teste_carrinho.Rows.Count == 0)
                    throw new Exception("Não tem produtos no carrinho.");

                string nome = tbNome.Text;
                string apelido = tbApelido.Text;
                string morada = tbMorada.Text;
                string cp = tbCp.Text;
                float total = 0;

                int id_venda= BaseDados.Instance.adicionarVenda(id, nome, apelido, morada, cp);


                for (int i = 0; i < gvProdutos.Rows.Count; i++)
                {
                  
                    int id_produto = int.Parse(gvProdutos.Rows[i].Cells[2].Text.ToString());
                    int quantidade = int.Parse(gvProdutos.Rows[i].Cells[5].Text.ToString());

                    total += float.Parse(gvProdutos.Rows[i].Cells[4].Text.ToString())*quantidade;
                    BaseDados.Instance.adicionarProdutosVendas(id_venda, id_produto, quantidade);
                    BaseDados.Instance.diminuirStock(id_produto, quantidade);

                }


                BaseDados.Instance.adicionarPrecoVenda(id_venda, total);
                BaseDados.Instance.removerCarrinho(id);
                lbErroTerminar.Visible = true;
                lbErroTerminar.Text = "Compra concluída!";
                lbErroTerminar.CssClass = "alert alert-success";
              

                
            }
            catch (Exception erro)
            {

                lbErroTerminar.Visible = true;
                lbErroTerminar.Text = "Ocorreu um erro. " +erro.Message;
                lbErroTerminar.CssClass = "alert alert-danger";
            }
        }
    }
}