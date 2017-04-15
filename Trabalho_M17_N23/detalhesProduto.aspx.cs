using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AjaxControlToolkit;

namespace Trabalho_M17_N23
{
    public partial class detalhesProduto : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            lbQuantidade.Visible = false;
            tbQuantidade.Visible = false;
            btAdicionarCarrinho.Visible = false;
            if (Request["id"] == null)
                Response.Redirect("index.aspx");
            try
            {
                int id = int.Parse(Request["id"].ToString());


                DataTable dados = BaseDados.Instance.devolveDadosProdutos(id);
                if (dados == null || dados.Rows.Count == 0)
                    throw new Exception("Não existe nenhum produto com o id indicado");


                Page.Title = dados.Rows[0][1].ToString() + " - A Lojinha das Sementes";
                lbStrain.Text = dados.Rows[0][1].ToString();
                lbTipo.Text = dados.Rows[0][2].ToString() + " (Sativa:" + dados.Rows[0][3].ToString() + "%, Indica:" + Convert.ToString(100 - int.Parse(dados.Rows[0][3].ToString())) + "%)";
                lbTHC.Text = dados.Rows[0][8].ToString() + "%";
                lbPreco.Text = string.Format("{0:C}", Decimal.Parse(dados.Rows[0]["preco"].ToString()));

                if (dados.Rows[0][4].ToString() == "false")
                    lbFeminizada.Text = "Não";
                else
                    lbFeminizada.Text = "Sim";

                if (dados.Rows[0][5].ToString() == "false")
                    lbAutomatica.Text = "Não";
                else
                    lbAutomatica.Text = "Sim";

                divComentar.Visible = false;

                if (Session["perfil"] != null)
                {
                    DataTable comentariosutilizador = BaseDados.Instance.listaComentariosUtilizador(int.Parse(Session["id"].ToString()));
                    DataTable verificarcompra = BaseDados.Instance.verificaCompraProduto(int.Parse(Session["id"].ToString()), int.Parse(Request["id"].ToString()));
                    if (comentariosutilizador.Rows.Count == 0 && verificarcompra.Rows.Count > 0)
                        divComentar.Visible = true;
                }

                try
                {
                    DataTable comentarios = BaseDados.Instance.devolveConsulta("SELECT comentarios.id,utilizadores.Nome_proprio, comentarios.Comentario, comentarios.avaliacao  FROM Comentarios INNER JOIN Utilizadores ON comentarios.utilizador=utilizadores.id WHERE comentarios.produto=" + Request["id"].ToString());
                    atualizaDivComentarios(comentarios);
                }
                catch (Exception)
                {


                }




                //capa
                string ficheiro = @"~\Imagens\" + dados.Rows[0][0].ToString() + ".jpg";
                img.ImageUrl = ficheiro;
                img.Width = 100;
                //Estado
                string grelha = "<a href=index.aspx>login</a>";
                string register = "<a href=registo.aspx>registre-se</a>";
                //   string adicionar_carrinho = "<a href=adicionarcarrinho.aspx?id=" + id+">Adicionar ao carrinho</a>";

                if (int.Parse(dados.Rows[0][6].ToString()) > 0)
                {
                    if (Session["perfil"] != null)
                    {

                        lbQuantidade.Visible = true;
                        tbQuantidade.Visible = true;
                        Disponivel.Visible = false;
                        btAdicionarCarrinho.Visible = true;
                    }
                    else
                        Disponivel.Text = "Este produto está dísponivel, faça o seu " + grelha + " ou então " + register;
                }
                else
                    Disponivel.Text = "Este produto não está disponível.";

            }
            catch
            {
                Response.Redirect("index.aspx");
            }
        }
        private void atualizaDivComentarios(DataTable dados)
        {



            if (dados == null || dados.Rows.Count == 0)
            {
                divComentarios.InnerHtml = "";
                return;
            }

            divComentarios.Visible = true;
            string grelha = "<h2>Comentários</h2>";
            grelha += "<div class='container-fluid'>";
            grelha += "<div class ='row'>";

            foreach (DataRow comentario in dados.Rows)
            {
                grelha += "<br/><br/>";
                grelha += " <div class='form-group'>";
                grelha += "<label for='tbComentario" + comentario[0].ToString() + "'>" + comentario[1].ToString() + "</label> ";
                grelha += "<br/>";
                grelha += "<span id='ContentPlaceHolder1_lbComentario" + comentario[0].ToString() + "'>" + comentario[2].ToString() + "'</span>";
                grelha += "<br/>";


                try
                {
                    decimal avaliacao = Convert.ToDecimal(comentario[3].ToString());
                    avaliacao = Math.Round(avaliacao);

                    grelha += "<img src='/resources/" + avaliacao + "_estrelas.png'  height='30px'/>";
                }
                catch (Exception)
                {

                    grelha += "<img src='/resources/sem_avaliacao.png' height='30px'/>";
                }
                grelha += "</div>";

                grelha += "__________________________________________________________________________________________________________________________________";

            }
            grelha += "</div></div>";

            divComentarios.InnerHtml = grelha;
            if (!IsPostBack)
                lbResultadoComentario.Visible = false;

        }


        protected void btAdicionarCarrinho_Click(object sender, EventArgs e)
        {
            try
            {
                int quantidade = 0;
                try
                {

                    quantidade = int.Parse(tbQuantidade.Text.ToString());

                    string sql = "SELECT stock from produtos where id=" + Request["id"].ToString();
                    DataTable dados = BaseDados.Instance.devolveConsulta(sql);
                    string stock = dados.Rows[0][0].ToString();

                    if (quantidade > int.Parse(stock))
                        throw new Exception("Não existe stock suficiente.");
                    if (quantidade < 1)
                        throw new Exception();

                    sql = "Select quantidade from carrinhos where utilizador=" + Session["id"].ToString() + " AND produto=" + Request["id"].ToString();
                    dados = BaseDados.Instance.devolveConsulta(sql);

                    if (dados == null || dados.Rows.Count == 0)
                    {

                        BaseDados.Instance.adicionarAoCarrinho(int.Parse(Request["id"].ToString()), int.Parse(Session["id"].ToString()), quantidade);
                    }
                    else
                    {

                        if (quantidade + int.Parse(dados.Rows[0][0].ToString()) > int.Parse(stock))
                            throw new Exception("Não existe stock suficiente.");
                        else
                        {
                            BaseDados.Instance.aumentarQuantidadeCarrinho(int.Parse(Request["id"].ToString()), int.Parse(Session["id"].ToString()), quantidade);
                        }
                    }


                }
                catch (Exception erro)
                {
                    if (String.IsNullOrEmpty(erro.Message))
                        lbErro.Text = "Introduza uma quantidade válida.";
                    else
                        lbErro.Text = erro.Message;

                    lbErro.CssClass = "alert alert-danger";
                    return;
                }


                lbErro.Text = "Produto adicionado ao carrinho.";
                lbErro.CssClass = "alert alert-success";
            }
            catch (Exception)
            {

                lbErro.Text = "Ocorreu um erro.";
                lbErro.CssClass = "alert alert-danger";
            }
        }

        protected void btAvaliar_Click(object sender, EventArgs e)
        {
            try
            {

                string comentario = tbComentario.Text;
                int utilizador = int.Parse(Session["id"].ToString());
                int produto = int.Parse(Request["id"].ToString());

                BaseDados.Instance.adicionarComentario(produto, comentario, int.Parse(hdnAvaliacao.Text), utilizador);

                divComentar.Visible = false;
                lbResultadoComentario.Visible = true;
                lbResultadoComentario.Text = "Comentário adicionado!";
                lbResultadoComentario.CssClass = "alert alert-success";
            }
            catch (Exception)
            {

                lbResultadoComentario.Visible = true;
                lbResultadoComentario.Text = "Ocorreu um erro ao adicionar o comentário.";
                lbResultadoComentario.CssClass = "alert alert-danger";
            }


        }
        protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            hdnAvaliacao.Text = e.Value.ToString();

        }

    }
}