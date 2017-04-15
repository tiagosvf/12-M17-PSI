using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace Trabalho_M17_N23
{
    public partial class areacliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbErro.Visible = false;

                bloquearCampos();
                Page.Title = "Area Cliente - A Lojinha das Sementes";

                if (Session["perfil"] == null)
                    Response.Redirect("index.aspx");
                if (!IsPostBack)
                {
                    divDados.Visible = false;

                    divHistorico.Visible = false;
                }

                gvHistorico.RowCommand += new GridViewCommandEventHandler(this.gvHistorico_RowCommand);

                DataTable dados = BaseDados.Instance.devolveDadosUtilizador(int.Parse(Session["id"].ToString()));
                tbNome.Text = dados.Rows[0][1].ToString();
                tbApelido.Text = dados.Rows[0][2].ToString();
                tbEmail.Text = dados.Rows[0][3].ToString();
                tbCodigo_postal.Text = dados.Rows[0][6].ToString();
                tbMorada.Text = dados.Rows[0][7].ToString();

            }

        }

        private void gvHistorico_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int linha = int.Parse(e.CommandArgument as string);
            int id_venda = int.Parse(gvHistorico.Rows[linha].Cells[1].Text);
            if (e.CommandName == "detalhes")
            {
                Response.Redirect("detalhescompra.aspx?id=" + id_venda);
            }
        }

        protected void btDados_Click(object sender, EventArgs e)
        {
            divDados.Visible = true;
            divHistorico.Visible = false;
            btHistorico.CssClass = "btn btn-info";

            btDados.CssClass = "btn btn-info active";
            atualizaDivDados();
        }

        private void atualizaDivDados()
        {

        }

        protected void btHistorico_Click(object sender, EventArgs e)
        {

            divDados.Visible = false;
            divHistorico.Visible = true;
            btHistorico.CssClass = "btn btn-info active";

            btDados.CssClass = "btn btn-info";
            atualizaGrelhaHistorico();
        }

        private void atualizaGrelhaHistorico()
        {
            gvHistorico.Columns.Clear();
            int idUtilizador = int.Parse(Session["id"].ToString());
            gvHistorico.DataSource = BaseDados.Instance.listaVendasUtilizador(idUtilizador);

            //CRIAR PAGINA DOS DETALHES DA COMPRA ONDE MOSTRA OS PRODUTOS


            ButtonField btDetalhes = new ButtonField();
            btDetalhes.HeaderText = "Detalhes";
            btDetalhes.Text = "Detalhes";
            btDetalhes.ButtonType = ButtonType.Button;
            btDetalhes.CommandName = "detalhes";
            gvHistorico.Columns.Add(btDetalhes);

            gvHistorico.DataBind();
        }

        private void bloquearCampos()
        {
            tbNome.Enabled = false;
            tbMorada.Enabled = false;
            tbEmail.Enabled = false;
            tbApelido.Enabled = false;
            tbCodigo_postal.Enabled = false;
        }
        private void desbloquearCampos()
        {
            tbNome.Enabled = true;
            tbMorada.Enabled = true;

            tbApelido.Enabled = true;
            tbCodigo_postal.Enabled = true;
        }

        protected void btAlterarPassword_Click(object sender, EventArgs e)
        {

        }

        protected void btEditar_Click(object sender, EventArgs e)
        {
            if (tbNome.Enabled == false)
                desbloquearCampos();
            else
            {
                try
                {
                    bloquearCampos();


                    string nome = "", apelido = "", cp = "", morada = "";


                    nome = tbNome.Text;
                    apelido = tbApelido.Text;
                    cp = tbCodigo_postal.Text;
                    morada = tbMorada.Text;

                    if (String.IsNullOrEmpty(nome) || String.IsNullOrEmpty(apelido) || String.IsNullOrEmpty(morada) || String.IsNullOrEmpty(cp))
                        throw new Exception("Deve preencher todos os campos.");
                    else
                    {
                        BaseDados.Instance.editarDadosCliente(int.Parse(Session["id"].ToString()), nome, apelido, cp, morada);
                        lbErro.Visible = true;
                        lbErro.Text = "Dados alterados";
                        lbErro.CssClass = "alert alert-success";

                    }


                }
                catch (Exception erro)
                {

                    lbErro.Visible = true;
                    lbErro.Text = "Ocorreu um erro." + erro.Message;
                    lbErro.CssClass = "alert alert-danger";
                }
            }

        }
    }
}