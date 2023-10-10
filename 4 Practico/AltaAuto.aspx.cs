using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;
using System.Configuration;
using System.Data;


public partial class AltaAuto : System.Web.UI.Page
{
    //atributos del formulario
    private int _haySeleccion = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //mando a cargar la grilla con los datos que ya contenga dicho archivo
            CargarDatos();

            //por defecto no tenemos nada seleccionado
            _haySeleccion = 0;
            ViewState["_haySeleccion"] = _haySeleccion;

            //determino botones por defecto
            this.BotonesPorDefecto();
        }
        else
        {
            //mantengo el estado de si hubo seleccion o no para modificar y eliminar
            //si ya se había ingresado al PageLoad
            _haySeleccion = (int)ViewState["_haySeleccion"];
        }
    }

    //operaciones extra
    private void CargarDatos()
    {
        try
        {
            //determino el camino completo de la ubicación del archivo XML para ABM
            //leo información del archivo
            DataSet _Ds = new DataSet();
            _Ds.ReadXml(Server.MapPath("~/Xml/Autos.xml"));

            //cargo la información de los libros en la grilla
            GVAutos.DataSource = _Ds;
            GVAutos.DataBind();

            //cargar datos Autores
            _Ds.ReadXml(Server.MapPath("~/XML/Duenios.xml"),XmlReadMode.InferSchema);//me infiere el esquema para tenerlo como tabla 
            DdlDuenio.DataSource = _Ds.Tables["Duenio"];
            DdlDuenio.DataTextField = "Nombre";
            DdlDuenio.DataValueField = "Cedula";
            DdlDuenio.DataBind();


        }
        catch (Exception ex)
        { LblError.Text = ex.Message; }
    }

    private void BotonesPorDefecto()
    {
        BtnAlta.Enabled = true;
        
    }

    private void LimpiarControles()
    {
        TxtMat.Text = "";
        TxtMarca.Text = "";
        TxtModelo.Text = "";
        TxtPrecio.Text = "";
        DdlDuenio.SelectedIndex = 0;
    }
    protected void BtnAlta_Click(object sender, EventArgs e)
        //creo un objeto de XML de tipo XMLNode para agregar
        //siempre que agrega uno se coloca al final del árbol
    {

        try
        {
            //determino archivo de orgien de datos
            string _camino = Server.MapPath(ConfigurationManager.AppSettings["XmlLibros"]);

            //recupero el documento xml, para agregar un nuevo nodo de libro
            XmlDocument _DocumentoXML = new XmlDocument();
            _DocumentoXML.Load(Server.MapPath("~/Xml/Autos.xml")); // genera un archivo de texto dentro del XmlDocument

            //creo el nodo Libro con su atributo
            XmlNode _NodoA = _DocumentoXML.CreateNode(XmlNodeType.Element, "Auto", "");//Tipo de XmlNode (Element representa etiqueta)
                                                                                        //Segundo parm. Nombre de la etiqueta.(CaseSensitive)
                                                                                        //Tercero: Espacio de nombre URI. No es obligatorio.

            //creo el nodo Matricula
            XmlNode _NodoM = _DocumentoXML.CreateNode(XmlNodeType.Element, "Mat", "");
            _NodoM.InnerXml = TxtMat.Text.Trim();
            _NodoA.AppendChild(_NodoM);//lo agrego como Nodo Hijo al Nodo ráíz Libro con AppendChild

            //creo el subnodo Marca.
            XmlNode _NodoMa = _DocumentoXML.CreateNode(XmlNodeType.Element, "Marca", "");
            _NodoMa.InnerXml = TxtMarca.Text.Trim();
            _NodoA.AppendChild(_NodoMa);

            //creo el subnodo Modelo.
            XmlNode _NodoMo = _DocumentoXML.CreateNode(XmlNodeType.Element, "Modelo", "");
            _NodoMo.InnerXml = TxtModelo.Text.Trim();
            _NodoA.AppendChild(_NodoMo);

            //creo el nodo Duenio
            XmlNode _NodoD = _DocumentoXML.CreateNode(XmlNodeType.Element, "Duenio", "");
            _NodoD.InnerXml = DdlDuenio.SelectedValue;
            _NodoA.AppendChild(_NodoD);

            //creo el subnodo Precio.
            XmlNode _NodoP = _DocumentoXML.CreateNode(XmlNodeType.Element, "Precio", "");
            _NodoP.InnerXml = TxtPrecio.Text.Trim();
            _NodoA.AppendChild(_NodoP);

            //agrego el nodo libro al documento xml
            _DocumentoXML.DocumentElement.AppendChild(_NodoA);

            //grabo el archivo con la nueva informacion
            _DocumentoXML.Save(Server.MapPath("~/Xml/Autos.xml")); //para q funcione correctamente y no perder datosw

            //actualizo la pantalla
            this.CargarDatos();
            this.LimpiarControles();
            this.BotonesPorDefecto();

            //si llegue aca es porque está todo OK
            LblError.Text = "Alta con exito";
        }
        catch (Exception ex)
        { LblError.Text = ex.Message; }
    }



    protected void GVLibros_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //determino archivo de origen datos
            //recupero el documento xml , para eliminar nodo de libro
            XmlDocument _DocumentoXML = new XmlDocument();
            _DocumentoXML.Load(Server.MapPath("~/Xml/autos"));

            //obtengo nodo seleccionado
            XmlNode _NodoA= _DocumentoXML.DocumentElement.ChildNodes[GVAutos.SelectedIndex];// la pos en la grilla es la misma pos
                                                                                             // de nodo primario q quiero ver

            //determino que hay nodo seleccionado
            ViewState["_haySeleccion"] = GVAutos.SelectedIndex;

            //cargo datos en pantalla
            TxtMat.Text = _NodoA["Mat"].InnerText;
            TxtMarca.Text = _NodoA["Marca"].InnerText;
            TxtModelo.Text = _NodoA["Modelo"].InnerText;
            TxtPrecio.Text = _NodoA["Precio"].InnerText;
             

            //determino el autor - recordar q solo puede haber un elemento seleccionado
            //en la lista desplegable
            foreach(ListItem _unItem in DdlDuenio.Items)
            {
                if (_unItem.Value == _NodoA["Duenio"].InnerText)
                    _unItem.Selected = true;
                else
                    _unItem.Selected = false;
            }
  
            
        }
        catch (Exception ex)
        { LblError.Text = ex.Message; }
    }


    protected void DdlAutor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}