<%@ Page Title="" Language="C#" MasterPageFile="~/mp.Master" AutoEventWireup="true" CodeBehind="detalhesProduto.aspx.cs" Inherits="Trabalho_M17_N23.detalhesProduto" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div class="form-group">
        <label for="tbStrain">Strain</label>
        <asp:label id="lbStrain" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbTipo">Tipo</label>
        <asp:label id="lbTipo" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbTHC">THC</label>
        <asp:label id="lbTHC" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbFeminizada">Feminizada</label>
        <asp:label id="lbFeminizada" runat="server" text="Label"></asp:label>
    </div>
     <div class="form-group">
        <label for="tbAutomatica">Automática</label>
        <asp:label id="lbAutomatica" runat="server" text="Label"></asp:label>
    </div>
    


       <div class="form-group">
        <label for="tbPreco">Preço</label>
        <asp:label id="lbPreco" runat="server" text="Label"></asp:label>
    </div>

  
    <asp:Image ID="img" runat="server"  Width="100"  /><br />
    <asp:label id="Disponivel" runat="server" text="Label"></asp:label>
    <asp:Label id="lbQuantidade" runat="server" Text="Quantidade" />
    <asp:TextBox id="tbQuantidade" runat="server" TextMode="Number"></asp:TextBox>
    <asp:Button  CssClass="btn" ID="btAdicionarCarrinho" runat="server" Text="Adicionar ao carrinho" OnClick="btAdicionarCarrinho_Click" />
    <asp:Label ID="lbErro" runat="server" Text="" />
    <br/>
    <div id="divComentar" runat="server" class="form-group">               
        <br/>
    
   <div class="form-group">
       
        <label for="tbComentario">Avalie este produto</label> 
 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
        
       <cc1:Rating ID="Rating1" runat="server"   MaxRating="5"
                            CurrentRating="1"
                            CssClass="ratingStar"
                            StarCssClass="ratingItem"
                            WaitingStarCssClass="Saved"
                            FilledStarCssClass="Filled"
                            EmptyStarCssClass="Empty" AutoPostBack="True" OnChanged="Rating1_Changed"></cc1:Rating>

    
            
        <asp:TextBox ID="tbComentario" runat="server"   CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>
       <asp:TextBox Text="1" ID="hdnAvaliacao" runat="server" Visible="false" />
       
       <asp:Button  CssClass="btn" ID="btAvaliar" runat="server" Text="Avaliar" onclick="btAvaliar_Click" />
    </div>     
            </div>
    
        <asp:Label ID="lbResultadoComentario" runat="server" Text="" />
        <div id="divComentarios"  runat="server" class="form-group">
          
    </div>
    
</asp:Content>
