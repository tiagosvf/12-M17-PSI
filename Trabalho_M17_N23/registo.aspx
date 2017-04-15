<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="registo.aspx.cs" Inherits="Trabalho_M17_N23.registo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
    <div class="form-group">
        <label for="tbPassword">Password</label>
        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
    </div>
    
  <div class="form-group">
    <asp:CheckBox ID="cbNewsletter"  runat="server" Text=" Subscrever ao newsletter" CssClass="form-control" />
      </div>    

    <asp:Label ID="lbErro" runat="server" />
<br />
    <asp:Button ID="btRegistar" runat="server" Text="Registar" CssClass="btn btn-info" OnClick="btRegistar_Click" />
</asp:Content>
