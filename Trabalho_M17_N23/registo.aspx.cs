using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trabalho_M17_N23
{
    public partial class registo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Registo - A Lojinha das Sementes";
            //se tem login vai para index
            if (Session["perfil"] != null) Response.Redirect("index.aspx");

                lbErro.Visible = false;
        }
        protected void btRegistar_Click(object sender, EventArgs e)
        {
            lbErro.Visible = false;
            lbErro.Text = "";
            try
            {
                string email = tbEmail.Text;
                string nome_proprio = tbNome.Text;
                string apelido = tbApelido.Text;
                string morada = tbMorada.Text;
                string codigo_postal = tbCodigo_postal.Text;
                string password = tbPassword.Text;
                int newsletter = 0;
                if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(nome_proprio) || String.IsNullOrEmpty(apelido) || String.IsNullOrEmpty(morada) || String.IsNullOrEmpty(codigo_postal) || String.IsNullOrEmpty(password))
                    throw new Exception("Deve preencher todos os campos.");

                if (nome_proprio.Length < 3 || apelido.Length < 3)
                    throw new Exception("Nome ou apelido muito curto.");

                if (email.IndexOf('.') == -1)
                    throw new Exception("Email inválido.");
                if (cbNewsletter.Checked == true)
                    newsletter = 1;
               

                //registar
                BaseDados.Instance.registarUtilizador(email, nome_proprio, apelido, morada, codigo_postal, password,newsletter);
                Response.Redirect("index.aspx");

            }
            catch (Exception erro)
            {
                lbErro.Visible = true;
                lbErro.Text = "Ocorreu um erro. " +erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }
    }
}