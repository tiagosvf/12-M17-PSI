<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="areaadmin.aspx.cs" Inherits="Trabalho_M17_N23.areaadmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <h1>Área Admin</h1>
    <div class="btn-group">
        <asp:Button ID="btVendas" runat="server" Text="Vendas" CssClass="btn btn-info" OnClick="btVendas_Click"/>
      
        <asp:Button ID="btProdutos" runat="server" Text="Gerir Produtos" CssClass="btn btn-info" OnClick="btProdutos_Click" />
         <asp:Button ID="btUtilizadores" runat="server" Text="Gerir Utilizadores" CssClass="btn btn-info" OnClick="btUtilizadores_Click" />
        <asp:Button ID="btNewsletter" runat="server" Text="Newsletter" CssClass="btn btn-info" onclick="btNewsletter_Click" />
  
    </div>
    <div id="divProdutos" runat="server">
        <h2>Gerir Produtos</h2>
          <div class="container">
            <h1>Produtos</h1>
            <asp:GridView ID="gvProdutos" runat="server" CssClass="table table-responsive"></asp:GridView>
            <h1>Adicionar</h1>
            <div class="form-group">
                <label for="tbStrain">Strain</label>
                <asp:TextBox ID="tbStrain" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
            
            <div class="form-group">
                <label for="tbTHC">Percentagem de THC</label>
                <asp:TextBox ID="tbTHC" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>
                     <div class="form-group">
                <label for="cbFeminizada">Feminizada</label>
               <asp:CheckBox ID="cbFeminizada"  runat="server" Text="Feminizada" CssClass="form-control" />
                         </div>

              <div class="form-group">
                
               <asp:RadioButton ID="rbSativa" Text="Sativa" runat="server" GroupName="tipo" CssClass="form-control" />
              
                
               <asp:RadioButton ID="rbIndica" Text="Indica" runat="server" GroupName="tipo" CssClass="form-control" />
                         </div>

                </div>
                <div class="form-group">
                <label for="tbPctSativa">Percentagem sativa</label>
                <asp:TextBox ID="tbPctSativa" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>

                 <div class="form-group">
                <label for="cbAutomatica">Automática</label>
               <asp:CheckBox ID="cbAutomatica"  runat="server" Text="Automatica" CssClass="form-control" />
                         </div>
            <div class="form-group">
                <label for="tbPreco">Preço</label>
                <asp:TextBox ID="tbPreco" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

               <div class="form-group">
                <label for="tbStock">Stock</label>
                <asp:TextBox ID="tbStock" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="fuImagem">Imagem</label>
                <asp:FileUpload ID="fuImagem" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btAdicionarProduto" runat="server" Text="Adicionar" OnClick="btAdicionarProduto_Click" />
            <asp:Label ID="lbErroProduto" runat="server" Text=""></asp:Label>
        </div>
        </div>
  
    <div id="divVendas" runat="server">
        <h2>Histórico de vendas</h2>
        <asp:GridView ID="gvVendas" runat="server" CssClass="table table-responsive"></asp:GridView>

    </div>

      <div id="divUtilizadores" runat="server">
        <h2>Gerir Utilizadores</h2>
          <div class="container">
            
            <asp:GridView ID="gvUtilizadores" runat="server" CssClass="table table-responsive"></asp:GridView>
            
            <asp:Label ID="lbErroUtilizador" runat="server" Text=""></asp:Label>
        </div>
        </div>

     <div id="divNewsletter" runat="server">
        <h2>Enviar Newsletter</h2>
          <div class="container">
             <asp:TextBox ID="tbNewsletter" runat="server"   CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>
     
       <asp:Button  CssClass="btn" ID="btEnviar" runat="server" Text="Enviar" onclick="btEnviar_Click" />
              <br />
              <asp:Label ID="lbResultadoNewsletter" runat="server" Text="" Visible="false" />
        </div>
        </div>
  
</asp:Content>
