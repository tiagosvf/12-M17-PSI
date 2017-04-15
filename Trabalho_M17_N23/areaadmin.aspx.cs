using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trabalho_M17_N23
{
    public partial class areaadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Area Admin - A Lojinha das Sementes";

            if (Session["perfil"] == null || !Session["perfil"].Equals("0"))
                Response.Redirect("index.aspx");

            


            gvProdutos.RowDeleting += new GridViewDeleteEventHandler(this.gvProdutos_RowDeleting);
            gvVendas.RowCommand += new GridViewCommandEventHandler(this.gvVendas_RowCommand);
            gvProdutos.RowEditing += new GridViewEditEventHandler(this.gvProdutos_RowEditing);
            gvProdutos.RowCancelingEdit += new GridViewCancelEditEventHandler(this.gvProdutos_RowCancelingEdit);
            gvProdutos.RowUpdating += new GridViewUpdateEventHandler(this.gvProdutos_RowUpdating);

            gvUtilizadores.RowDeleting += new GridViewDeleteEventHandler(this.gvUtilizadores_RowDeleting);
            gvUtilizadores.RowEditing += new GridViewEditEventHandler(this.gvUtilizadores_RowEditing);
            gvUtilizadores.RowCancelingEdit += new GridViewCancelEditEventHandler(this.gvUtilizadores_RowCancelingEdit);
            gvUtilizadores.RowUpdating += new GridViewUpdateEventHandler(this.gvUtilizadores_RowUpdating);
            if (!IsPostBack)
            {
                atualizaGrelhaProdutos();
                lbErroUtilizador.Visible = false;

                divProdutos.Visible = false;
                divVendas.Visible = false; divUtilizadores.Visible = false;

                divNewsletter.Visible = false;
                btVendas.CssClass = "btn btn-info";
                btProdutos.CssClass = "btn btn-info";
                btUtilizadores.CssClass = "btn btn-info";
                btNewsletter.CssClass = "btn btn-info";
            }

            }

        protected void btVendas_Click(object sender, EventArgs e)
        {
            divProdutos.Visible = false;
            divVendas.Visible = true;
            divUtilizadores.Visible = false;
            btUtilizadores.CssClass = "btn btn-info";
            btVendas.CssClass = "btn btn-info active";
            btNewsletter.CssClass = "btn btn-info";
            divNewsletter.Visible = false;
            btProdutos.CssClass = "btn btn-info";
            atualizaGrelhaVendas();
        }
        private void atualizaGrelhaVendas()
        {
            gvVendas.Columns.Clear();

            gvVendas.DataSource = BaseDados.Instance.listaTodasAsVendas();

            //CRIAR PAGINA DOS DETALHES DA COMPRA ONDE MOSTRA OS PRODUTOS


            ButtonField btDetalhes = new ButtonField();
            btDetalhes.HeaderText = "Detalhes";
            btDetalhes.Text = "Detalhes";
            btDetalhes.ButtonType = ButtonType.Button;
            btDetalhes.CommandName = "detalhes";
            gvVendas.Columns.Add(btDetalhes);

            gvVendas.DataBind();
        }
        private void gvVendas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linha = int.Parse(e.CommandArgument as string);
            int id_venda = int.Parse(gvVendas.Rows[linha].Cells[1].Text);
            if (e.CommandName == "detalhes")
            {
                Response.Redirect("detalhescompra.aspx?id=" + id_venda);
            }
        }
        private void gvProdutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int linha = e.RowIndex;
            string id = gvProdutos.Rows[linha].Cells[2].Text;
            BaseDados.Instance.removerProduto(int.Parse(id));
            //atualizar grelha
            atualizaGrelhaProdutos();
        }
        private void gvProdutos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int linha = e.RowIndex;
            string strain = ((TextBox)gvProdutos.Rows[linha].Cells[3].Controls[0]).Text;
            int percentagem_sativa = int.Parse(((TextBox)gvProdutos.Rows[linha].Cells[5].Controls[0]).Text);
            int percentagem_thc = int.Parse(((TextBox)gvProdutos.Rows[linha].Cells[10].Controls[0]).Text);
            bool feminizada = ((CheckBox)gvProdutos.Rows[linha].Cells[6].Controls[0]).Checked;
            bool automatica = ((CheckBox)gvProdutos.Rows[linha].Cells[7].Controls[0]).Checked;
            int stock = int.Parse(((TextBox)gvProdutos.Rows[linha].Cells[8].Controls[0]).Text);
            decimal preco = decimal.Parse(((TextBox)gvProdutos.Rows[linha].Cells[11].Controls[0]).Text);

            string sativa_ou_indica = ((TextBox)gvProdutos.Rows[linha].Cells[4].Controls[0]).Text;
            int id = int.Parse(((TextBox)gvProdutos.Rows[linha].Cells[2].Controls[0]).Text);

            BaseDados.Instance.atualizaProduto(id, strain, sativa_ou_indica, percentagem_sativa, feminizada, automatica, stock, percentagem_thc, preco);

            gvProdutos.EditIndex = -1;
            atualizaGrelhaProdutos();

        }

        private void gvProdutos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProdutos.EditIndex = -1;
            atualizaGrelhaProdutos();
        }

        private void gvProdutos_RowEditing(object sender, GridViewEditEventArgs e)
        {

            int linha = e.NewEditIndex;
            gvProdutos.EditIndex = linha;

            atualizaGrelhaProdutos();
            ((TextBox)gvProdutos.Rows[linha].Cells[2].Controls[0]).Enabled = false;
            ((TextBox)gvProdutos.Rows[linha].Cells[1].Controls[0]).Enabled = false;
            ((TextBox)gvProdutos.Rows[linha].Cells[9].Controls[0]).Enabled = false;
            divProdutos.Visible = true;
            divVendas.Visible = false;
            divUtilizadores.Visible = false;
            btUtilizadores.CssClass = "btn btn-info";
            btVendas.CssClass = "btn btn-info";
            btNewsletter.CssClass = "btn btn-info";
            divNewsletter.Visible = false;
            btProdutos.CssClass = "btn btn-info active";
        }

        private void gvUtilizadores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int linha = e.RowIndex;
            int id = int.Parse(gvUtilizadores.Rows[linha].Cells[1].Text);
            DataTable dados = BaseDados.Instance.listaVendasUtilizador(id);
            try
            {
                int idx = int.Parse(dados.Rows[0][0].ToString());

                lbErroUtilizador.Visible = true;
                lbErroUtilizador.Text = "Não pode eliminar utilizadores que tenham realizado compras.";
                lbErroUtilizador.CssClass = "alert alert-danger";
              
            }
            catch 
            {
                lbErroUtilizador.Visible = true;
                lbErroUtilizador.Text = "Utilizador eliminado com sucesso";
                lbErroUtilizador.CssClass = "alert alert-success";
                BaseDados.Instance.removerUtilizador(id);

                //atualizar grelha
                atualizaGrelhaUtilizadores();
            }
            divUtilizadores.Visible = true;
        }
        private void gvUtilizadores_RowUpdating(object sender, GridViewUpdateEventArgs e)

        {
            int linha = e.RowIndex;
            string[] nome = ((TextBox)gvUtilizadores.Rows[linha].Cells[2].Controls[0]).Text.Split(' ');
            int estado = int.Parse(((TextBox)gvUtilizadores.Rows[linha].Cells[4].Controls[0]).Text);
            string cp = ((TextBox)gvUtilizadores.Rows[linha].Cells[5].Controls[0]).Text;
            string morada = ((TextBox)gvUtilizadores.Rows[linha].Cells[6].Controls[0]).Text;
            int perfil = int.Parse(((TextBox)gvUtilizadores.Rows[linha].Cells[7].Controls[0]).Text);

         

          
            int id = int.Parse(((TextBox)gvUtilizadores.Rows[linha].Cells[1].Controls[0]).Text);

            BaseDados.Instance.atualizarUtilizador(id, nome[0], nome[1], estado, perfil, morada, cp);

            gvUtilizadores.EditIndex = -1;
            atualizaGrelhaUtilizadores();

        }

        private void gvUtilizadores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUtilizadores.EditIndex = -1;
            atualizaGrelhaUtilizadores();
        }

        private void gvUtilizadores_RowEditing(object sender, GridViewEditEventArgs e)
        {

            int linha = e.NewEditIndex;
            gvUtilizadores.EditIndex = linha;

            atualizaGrelhaUtilizadores();
              ((TextBox)gvUtilizadores.Rows[linha].Cells[1].Controls[0]).Enabled = false;
            ((TextBox)gvUtilizadores.Rows[linha].Cells[3].Controls[0]).Enabled = false;
            ((TextBox)gvUtilizadores.Rows[linha].Cells[8].Controls[0]).Enabled = false;

            divProdutos.Visible = false;
            divVendas.Visible = false;
            divUtilizadores.Visible = true;
            btUtilizadores.CssClass = "btn btn-info active";
            btVendas.CssClass = "btn btn-info";
            btNewsletter.CssClass = "btn btn-info";
            divNewsletter.Visible = false;
            btProdutos.CssClass = "btn btn-info ";
        }

        protected void btProdutos_Click(object sender, EventArgs e)
        {
            btNewsletter.CssClass = "btn btn-info";
            divNewsletter.Visible = false;
            divProdutos.Visible = true;
            divVendas.Visible = false;
            divUtilizadores.Visible = false;
            btUtilizadores.CssClass = "btn btn-info";
            btVendas.CssClass = "btn btn-info";

            btProdutos.CssClass = "btn btn-info active";
            atualizaGrelhaProdutos();
        }

        private void atualizaGrelhaProdutos()
        {
            gvProdutos.Columns.Clear();

            gvProdutos.DataSource = BaseDados.Instance.listaProdutos();



            gvProdutos.AutoGenerateDeleteButton = true;
            gvProdutos.AutoGenerateEditButton = true;

            ImageField ifImagem = new ImageField();
            ifImagem.HeaderText = "Imagem";
            ifImagem.DataImageUrlFormatString = "~/Imagens/{0}.jpg";
            ifImagem.DataImageUrlField = "id";
            ifImagem.ControlStyle.Width = 100;
            gvProdutos.Columns.Add(ifImagem);

           
            gvProdutos.DataBind();
        }
        private void atualizaGrelhaUtilizadores()
        {
            gvUtilizadores.Columns.Clear();

            gvUtilizadores.DataSource = BaseDados.Instance.listaTodosUtilizadores();



            gvUtilizadores.AutoGenerateDeleteButton = true;
            gvUtilizadores.AutoGenerateEditButton = true;       


            gvUtilizadores.DataBind();
        }

        protected void btAdicionarProduto_Click(object sender, EventArgs e)
        {
            try { 
            string strain = tbStrain.Text;
            int percentagem_sativa = int.Parse(tbPctSativa.Text);
            int percentagem_thc = int.Parse(tbTHC.Text);
            bool feminizada = cbFeminizada.Checked;
            bool automatica = cbAutomatica.Checked;
            int stock = int.Parse(tbStock.Text);
            decimal preco = decimal.Parse(tbPreco.Text);

            string sativa_ou_indica="";

            if (rbIndica.Checked == true)
                sativa_ou_indica = "Indica";
            else if (rbSativa.Checked == true)
                sativa_ou_indica = "Sativa";
            else
                throw new Exception("Deve indicar se a strain é sativa ou indica");





            if (preco < 0) throw new Exception("O preço não pode ser inferior a zero");
            //verificar se existe capa
            if (fuImagem.HasFile == false) throw new Exception("Tem de indicar uma imagem");
            int id = BaseDados.Instance.adicionarProduto(strain,sativa_ou_indica,percentagem_sativa,feminizada,automatica,stock,percentagem_thc,preco);
            //guardar imagem
            if (fuImagem.PostedFile.ContentType == "image/jpeg"
                && fuImagem.PostedFile.ContentLength > 0)
            {
                string ficheiro = Server.MapPath(@"~\Imagens\");
                ficheiro += id + ".jpg";
                fuImagem.SaveAs(ficheiro);
            }
            tbPctSativa.Text = "";
            tbStock.Text = "";
            tbStrain.Text = "";
            tbTHC.Text = "";
            cbAutomatica.Checked = false;
            cbFeminizada.Checked = false;
            rbIndica.Checked = false;
            rbSativa.Checked = false;
            tbPreco.Text = "0";
            lbErroProduto.Text = "Dados adicionados com sucesso";
            lbErroProduto.CssClass = "alert alert-success";
               
                atualizaGrelhaProdutos();

            }
            catch(Exception erro)
            {
                lbErroProduto.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErroProduto.CssClass = "alert alert-danger";
            }
   
        }

        protected void btUtilizadores_Click(object sender, EventArgs e)
        {
            btNewsletter.CssClass = "btn btn-info";
            divNewsletter.Visible = false;
            divProdutos.Visible = false;
            divVendas.Visible = false;
            divUtilizadores.Visible = true;
            btUtilizadores.CssClass = "btn btn-info active";
            btVendas.CssClass = "btn btn-info";

            btProdutos.CssClass = "btn btn-info";
            atualizaGrelhaUtilizadores();
        }

        protected void btEnviar_Click(object sender, EventArgs e)
        {
            try
            {

                string texto = tbNewsletter.Text;
                               DataTable emailsNewsletter = BaseDados.Instance.listaEmailsNewsletter();

                for (int i = 0; i < emailsNewsletter.Rows.Count; i++)
                {
                    string email = emailsNewsletter.Rows[i][0].ToString();
                    string password = ConfigurationManager.AppSettings["senha"].ToString();
                    Helper.enviarMail("testetrabalhoasp@gmail.com", password, email, "A Lojinha das Sementes - Newsletter",
                       texto);
                }
                //a password dos emails é "testeasp"

                lbResultadoNewsletter.Visible = true;
                lbResultadoNewsletter.Text = "Newsletter enviado!";
                lbResultadoNewsletter.CssClass = "alert alert-success";
            }
            catch (Exception)
            {

                lbResultadoNewsletter.Visible = true;
                lbResultadoNewsletter.Text = "Ocorreu um erro ao enviar o newsletter.";
                lbResultadoNewsletter.CssClass = "alert alert-danger";
            }

        }

        protected void btNewsletter_Click(object sender, EventArgs e)
        {
            btNewsletter.CssClass = "btn btn-info active";
            divNewsletter.Visible = true ;
            divProdutos.Visible = false;
            divVendas.Visible = false;
            divUtilizadores.Visible = false;
            btVendas.CssClass = "btn btn-info";
            btUtilizadores.CssClass = "btn btn-info";
            btProdutos.CssClass = "btn btn-info";
        }
    }
}