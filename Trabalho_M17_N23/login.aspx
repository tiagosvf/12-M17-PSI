<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Trabalho_M17_N23.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="div_login" runat="server" class="">
        Email<asp:TextBox ID="tbEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox><br />
        Password<asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password" /><br />
        <asp:Button CssClass="btn" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
        <asp:Button CssClass="btn" ID="btRecuperarPass" runat="server" Text="Recuperar Password" OnClick="btRecuperarPass_Click" />
        <asp:Label ID="lbErro" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
