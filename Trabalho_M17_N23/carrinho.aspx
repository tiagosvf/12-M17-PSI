<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="carrinho.aspx.cs" Inherits="Trabalho_M17_N23.carrinho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:Label ID="lbErro" runat="server" Text="" />

    <div id="divProdutos" runat="server">
          <h1>Produtos</h1>
            <asp:GridView ID="gvProdutos" runat="server" CssClass="table table-responsive"></asp:GridView>
    </div>
    <div id="divDados" runat="server">
        <h2>Morada de envio</h2><br />
        Nome<asp:TextBox ID="tbNome" runat="server" CssClass="form-control"></asp:TextBox>  <br />
        Apelido<asp:TextBox ID="tbApelido" runat="server" CssClass="form-control"></asp:TextBox><br />
        Codigo-postal<asp:TextBox ID="tbCp" runat="server" CssClass="form-control"  /><br />
        Morada<asp:TextBox ID="tbMorada" runat="server" CssClass="form-control" /><br />

        <asp:Button id="btTerminar" runat="server" Text="Terminar compra" CssClass="btn btn-success" TextMode="text-right" OnClick="btTerminar_Click"/>
         <asp:Label ID="lbErroTerminar" runat="server" Text="" />
    </div>
</asp:Content>
