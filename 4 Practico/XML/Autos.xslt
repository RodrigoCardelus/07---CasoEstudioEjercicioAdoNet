<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <!--Determino que el formato se aplica a todo el documento( dentro el match ' / ' )-->
  <xsl:template match="/">

    <!--Creo una etiqueta HTML Table para poder colocar texto en columnas dentro de la pág.-->
    <table>
      <!--Determino como quiero desplegar cada Nodo libro-->
      <xsl:for-each select="Autos/Auto">
        <!--Para cada libro creo un renglón de despliegue-->
        <tr>
          <!--Primero despliego el titulo-->
          <td style="background-color:#008800;padding:4px;font-size:15pt;
                                  font-weight:bold;color:white">
             <xsl:value-of select="Mat" />
          </td>
          <!--Segundo despliego el ISBN y la categoria-->
          <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
            <xsl:value-of select="Marca" />
          </td>
          <td style="margin-left:20px;margin-bottom:1em;font-size:15pt">
             <xsl:value-of select="Modelo" />
          </td>
        <td style="background-color:#004400;padding:4px;margin-left:20px;margin-bottom:1em;font-size:15pt">
             <xsl:value-of select="Precio" />
          </td>
      
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>

</xsl:stylesheet>
