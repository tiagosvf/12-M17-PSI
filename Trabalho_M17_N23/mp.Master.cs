using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Trabalho_M17_N23
{
    public partial class mp : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //testar se existe o cookie
            HttpCookie cookie = Request.Cookies["avisoTurno2"] as HttpCookie;
            if (cookie != null)
                div_aviso.Visible = false;
            string ficheiro = @"~\resources\logotipo.png";
            logo.ImageUrl = ficheiro;
        }

        protected void btCookie_Click(object sender, EventArgs e)
        {
            Guid g = Guid.NewGuid();
            //criar o cookie
            HttpCookie cookie = new HttpCookie("avisoTurno2", g.ToString());
            //definir o prazo
            cookie.Expires = DateTime.Now.AddYears(1);
            //enviar o cookie
            Response.Cookies.Add(cookie);
            //esconder o aviso
            div_aviso.Visible = false;
        }
    }
}