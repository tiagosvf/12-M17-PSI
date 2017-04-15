using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;


namespace Trabalho_M17_N23
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Login - A Lojinha das Sementes";
            //se tem login vai para index
            if (Session["perfil"] != null) Response.Redirect("index.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            DataTable utilizador = BaseDados.Instance.verificarLogin(email, password);
            if (utilizador == null)
            {
                lbErro.Text = "Login falhou.";
                lbErro.CssClass = "alert alert-danger";
                return;
            }
            Session["nome_proprio"] = utilizador.Rows[0]["nome_proprio"].ToString();
            Session["perfil"] = utilizador.Rows[0]["perfil"].ToString();
            Session["id"] = utilizador.Rows[0]["id"].ToString();
            div_login.Visible = false;
            if (Session["perfil"].Equals("0"))
                Response.Redirect("areaadmin.aspx");
            else
                Response.Redirect("areacliente.aspx");
        }

        protected void btRecuperarPass_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbEmail.Text == String.Empty)
                {
                    throw new Exception("Tem de indicar um email");
                }
                //verificar se o email existe
                string email = tbEmail.Text;
                DataTable dados = BaseDados.Instance.devolveDadosUtilizador(email);
                if (dados == null || dados.Rows.Count == 0)
                {
                    throw new Exception("O email indicado não existe");
                }
                //GUID
                Guid g = Guid.NewGuid();

                //guardar na bd
                BaseDados.Instance.recuperarPassword(email, g.ToString());
                //enviar email com link
                //dominio/recuperar_password.aspx?id=guid
                string assunto = "Clique no link para recuperar a sua password.\n";
                assunto += "<a href='http://" + Request.Url.Authority + "/recuperar_password.aspx?id=" + Server.UrlEncode(g.ToString()) + "'>Clique aqui</a>";
                string senha = ConfigurationManager.AppSettings["senha"].ToString();

                Helper.enviarMail("alunosnet@gmail.com", senha, email,
                    "Recuperação de palavra passe", assunto);
                lbErro.Text = "Foi enviado um email de recuperação";
                lbErro.CssClass = "alert alert-sucess";
            }
            catch (Exception erro)
            {
                lbErro.Text = "Ocorreu o seguinte erro: " + erro.Message;
                lbErro.CssClass = "alert alert-danger";
            }
        }


    }
}