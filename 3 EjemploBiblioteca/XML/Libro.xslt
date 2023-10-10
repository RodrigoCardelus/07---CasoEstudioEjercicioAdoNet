<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <!--Determino que el formato se aplica a todo el documento( dentro el match ' / ' )-->
  <xsl:template match="/">

    <!--Creo una etiqueta HTML Table para poder colocar texto en columnas dentro de la pág.-->
    <table>
      <!--Determino como quiero desplegar cada Nodo libro-->
      <xsl:for-each select="Biblioteca/Libro">
        <!--Para cada libro creo un renglón de despliegue-->
        <tr>
          <!--Primero despliego el titulo-->
          <td style="background-color:#008800;padding:4px;font-size:15pt;
                                  font-weight:bold;color:white">
            Titulo: <xsl:value-of select="Titulo" />
          </td>
          <!--Segundo despliego el ISBN y la categoria-->
          <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
            <xsl:value-of select="@Categoria" />
          </td>
          <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
            ISBN: <xsl:value-of select="ISBN" />
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>
