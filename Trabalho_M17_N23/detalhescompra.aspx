<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="detalhescompra.aspx.cs" Inherits="Trabalho_M17_N23.detalhescompra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divDados" runat="server">
     <div class="form-group">
        <label for="tbID">ID</label>
        <asp:label id="lbID" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbNome">Nome do destinatário</label>
        <asp:label id="lbNome" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbCp">Código-postal</label>
        <asp:label id="lbCp" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbMorada">Morada</label>
        <asp:label id="lbMorada" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbEstado">Estado da compra</label>
        <asp:label id="lbEstado" runat="server" text="Label"></asp:label>
    </div>
    


       <div class="form-group">
        <label for="tbTotal">Valor total</label>
        <asp:label id="lbTotal" runat="server" text="Label"></asp:label>
    </div>

           <asp:GridView ID="gvProdutos" runat="server" CssClass="table table-responsive"></asp:GridView>

        </div>
    <asp:Label ID="lbErro" runat="server" Text="" />

    


</asp:Content>
