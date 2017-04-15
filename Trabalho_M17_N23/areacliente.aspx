<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="areacliente.aspx.cs" Inherits="Trabalho_M17_N23.areacliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1>Área Cliente</h1>
    <div class="btn-group">
        <asp:Button ID="btDados" runat="server" Text="Dados" CssClass="btn btn-info" OnClick="btDados_Click"/>
      
        <asp:Button ID="btHistorico" runat="server" Text="Histórico" CssClass="btn btn-info" OnClick="btHistorico_Click" />
    </div>
    <div id="divDados" runat="server">
        <h2>Dados do Cliente</h2>

        
    <div class="form-group">
        <label for="tbEmail">Email</label>
        <asp:TextBox ID="tbEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbNome">Nome Próprio</label>
        <asp:TextBox ID="tbNome" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
      <div class="form-group">
        <label for="tbApelido">Apelido</label>
        <asp:TextBox ID="tbApelido" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbMorada">Morada</label>
        <asp:TextBox ID="tbMorada" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <label for="tbCodigo_postal">Codigo-postal</label>
        <asp:TextBox ID="tbCodigo_postal" runat="server"   CssClass="form-control"></asp:TextBox>
    </div>
    <asp:Button   ID="btEditar" runat="server" Text="Editar" CssClass="btn btn-default" OnClick="btEditar_Click" />
          <asp:Label ID="lbErro" runat="server" />

         <h2>Alterar password</h2>
          <div class="form-group">
        <label for="tbPassword">Password atual</label>
        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
    </div>
           <div class="form-group">
        <label for="tbPassword">Nova password</label>
        <asp:TextBox ID="tbPasswordNova" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
    </div>

            <asp:Button  ID="btAlterarPassword" runat="server" Text="Alterar password" OnClick="btAlterarPassword_Click" CssClass="btn btn-default" />
           <asp:Label ID="lbErroPassword" runat="server" />
    </div>
  
    <div id="divHistorico" runat="server">
        <h2>Histórico de Compras</h2>
        <asp:GridView ID="gvHistorico" runat="server" CssClass="table table-responsive"></asp:GridView>
    </div>
</asp:Content>
