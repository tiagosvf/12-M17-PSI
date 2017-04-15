<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Trabalho_M17_N23.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pull-left col-md-4 col-sm-4 input-group">
        <asp:TextBox runat="server" ID="tbPesquisa" CssClass="form-control" />
        <span class="input-group-btn">
            <asp:Button ID="btPesquisa" runat="server" Text="Pesquisar" CssClass="btn btn-info" OnClick="btPesquisa_Click" />
        </span>
    </div>

    <div id="div_produtos" runat="server">

    </div>

</asp:Content>
