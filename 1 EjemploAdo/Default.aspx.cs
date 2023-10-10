using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    //Objeto donde se guardará la información
    private DataSet _Ds; //voy a irlo utilizando para generar XML
    protected void Page_Load(object sender, EventArgs e)
    {
        if(IsPostBack)
        {
            //Es un PostBack, obtengo la información de la memoria
            _Ds = (DataSet)ViewState["Ds"];
        }
        else
        {
            //Es el primer ingreso a la página - Genero objeto para guardar información (vacío el DataSet)
            _Ds = new DataSet("Ventas"); // le asigno el nombre interno Ventas.
            ViewState["Ds"] = _Ds; // guardo el DataSet en el ViewState
        }
    }



    protected void BtnAccion1_Click(object sender, EventArgs e)
    {
        //creo la conexion a la BD
        SqlConnection _cnn = new SqlConnection("Data Source=pc-PC;Initial Catalog=Ventas;Integrated Security=true");

        //creo adaptadores
        SqlDataAdapter _daFam = new SqlDataAdapter("Select * From Familias", _cnn);
        SqlDataAdapter _daArt = new SqlDataAdapter("Select * From Articulos", _cnn);

        //cargo los datos
        _daFam.Fill(_Ds, "Familias");
        _daArt.Fill(_Ds, "Articulos");
        ViewState["Ds"] = _Ds; //cada objeto que se agrega se vuelve a agregar, para que lo serialice a XML.

        //cargo las grillas
        GVFamilias.DataSource = _Ds.Tables["Familias"];
        GVFamilias.DataBind();
        GVArticulos.DataSource = _Ds.Tables["Articulos"];
        GVArticulos.DataBind();
    }

    protected void BtnAccion2_Click(object sender, EventArgs e)
    {
        //creo DataSet para cargar el archivo XML
        DataSet _DsAux = new DataSet();
        _DsAux.ReadXml(Server.MapPath("~/XML/DatosXML.XML")); // el ~ indica la raíz del sitio.

        //combino la información que viene de la BD (y que ya está en el DataSet en memoria)
        //y la información del archivo XML, en un solo DataSet
        _Ds.Merge(_DsAux, true, MissingSchemaAction.Ignore);
        ViewState["_Ds"] = _Ds;

        //cargo las grillas
        GVFamilias.DataSource = _Ds.Tables["Familias"];
        GVFamilias.DataBind();
        GVArticulos.DataSource = _Ds.Tables["Articulos"];
        GVArticulos.DataBind();
    }

    protected void BtnAccion3_Click(object sender, EventArgs e)
    {
        _Ds.WriteXmlSchema(Server.MapPath("~/XML/EsquemaXML.xml"));
        _Ds.WriteXml(Server.MapPath("~/XML/DatosCompletosXML.xml"));
        _Ds.WriteXml(Server.MapPath("~/XML/DatosEsquemaXML.xml"), XmlWriteMode.WriteSchema);
    }
}
