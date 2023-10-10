<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="ListarAutos.aspx.cs" Inherits="ListarAutos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <p >Listado de Libros Formateado con XSLT<asp:Xml ID="XmlDespliego" runat="server" DocumentSource="~/XML/Autos.xml" TransformSource="~/XML/Autos.xslt"></asp:Xml>
    </p>
   
</asp:Content>

