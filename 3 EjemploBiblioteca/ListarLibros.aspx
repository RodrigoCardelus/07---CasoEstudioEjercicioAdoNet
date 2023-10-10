<%@ Page Title="" Language="C#" MasterPageFile="~/MP.master" AutoEventWireup="true" CodeFile="ListarLibros.aspx.cs" Inherits="ListarLibros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
   <p >Listado de Libros Formateado con XSLT<asp:Xml ID="XmlDespliego" runat="server" DocumentSource="~/XML/Libros.xml" TransformSource="~/XML/Libro.xslt"></asp:Xml>
    </p>
   
</asp:Content>

