<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="AltaAuto.aspx.cs" Inherits="AltaAuto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 773px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <table class="auto-style1" >
            <tr>
                <td colspan="3" class="style4" style="text-align: center"> <strong>ABM Libros </strong> </td>
            </tr>
            <tr>
                <td>Matricula</td>
                <td><asp:TextBox ID="TxtMat" runat="server"></asp:TextBox></td>
                <td rowspan="6"> <asp:GridView ID="GVAutos" runat="server" Width="530px" 
                        AutoGenerateSelectButton="True" OnSelectedIndexChanged="GVLibros_SelectedIndexChanged" 
                        ></asp:GridView> </td>           
            </tr>
            <tr>
                <td>Marca</td>
                <td><asp:TextBox ID="TxtMarca" runat="server"></asp:TextBox></td>
           </tr>
            <tr>
                <td>Modelo</td>
                <td><asp:TextBox ID="TxtModelo" runat="server"></asp:TextBox></td>
           </tr>
            <tr>
                <td>Precio</td>
                <td><asp:TextBox ID="TxtPrecio" runat="server"></asp:TextBox></td>
           </tr>
           <tr>
                <td>Dueños</td>
                <td><asp:DropDownList ID="DdlDuenio" runat="server" OnSelectedIndexChanged="DdlAutor_SelectedIndexChanged"></asp:DropDownList></td>
           </tr>
           <tr>
                <td>&nbsp;</td>
                <td><asp:Button id="BtnAlta" runat="server" Text="Agregar" OnClick="BtnAlta_Click" 
                        /> 
                </td>
           </tr>
            <tr>
                <td colspan="3"><asp:Label ID="LblError" runat="server"></asp:Label> </td>
            </tr>
      </table>
</asp:Content>

