using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;
using System.Configuration;
using System.Data;


public partial class AbmLibro : System.Web.UI.Page
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
            string _camino = Server.MapPath(ConfigurationManager.AppSettings["XmlLibros"]);//ConfigurationManager: lee web.config

            //leo información del archivo
            DataSet _Ds = new DataSet();
            _Ds.ReadXml(_camino);

            //cargo la información de los libros en la grilla
            GVLibros.DataSource = _Ds;
            GVLibros.DataBind();

            //cargar datos Autores
            _Ds.ReadXml(Server.MapPath("~/XML/Autores.xml"),XmlReadMode.InferSchema);//me infiere el esquema para tenerlo como tabla 
            DdlAutor.DataSource = _Ds.Tables["Autor"];
            DdlAutor.DataTextField = "Nombre";
            DdlAutor.DataValueField = "codigo";
            DdlAutor.DataBind();
        
        }
        catch (Exception ex)
        { LblError.Text = ex.Message; }
    }

    private void BotonesPorDefecto()
    {
        BtnAlta.Enabled = true;
        BtnBaja.Enabled = false;
        BtnModificar.Enabled = false;
    }

    private void BotonesBM()
    {
        BtnAlta.Enabled = false;
        BtnBaja.Enabled = true;
        BtnModificar.Enabled = true;
    }

    private void LimpiarControles()
    {
        TxtIsbn.Text = "";
        TxtTitulo.Text = "";
        TxtCategoria.Text = "";
        DdlAutor.SelectedIndex = 0;
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
            _DocumentoXML.Load(_camino); // genera un archivo de texto dentro del XmlDocument

            //creo el nodo Libro con su atributo
            XmlNode _NodoL = _DocumentoXML.CreateNode(XmlNodeType.Element, "Libro", "");//Tipo de XmlNode (Element representa etiqueta)
                                                                                        //Segundo parm. Nombre de la etiqueta.(CaseSensitive)
                                                                                        //Tercero: Espacio de nombre URI. No es obligatorio.
            XmlAttribute _AtributoCategoria = _DocumentoXML.CreateAttribute("Categoria");
            _AtributoCategoria.InnerText = TxtCategoria.Text.Trim();
            _NodoL.Attributes.Append(_AtributoCategoria);//engancho el atributo a la etiqueta para que quede dentro.

            //creo el nodo ISBN
            XmlNode _NodoI = _DocumentoXML.CreateNode(XmlNodeType.Element, "ISBN", "");
            _NodoI.InnerXml = TxtIsbn.Text.Trim();
            _NodoL.AppendChild(_NodoI);//lo agrego como Nodo Hijo al Nodo ráíz Libro con AppendChild

            //creo el subnodo Titulo.
            XmlNode _NodoT = _DocumentoXML.CreateNode(XmlNodeType.Element, "Titulo", "");
            _NodoT.InnerXml = TxtTitulo.Text.Trim();
            _NodoL.AppendChild(_NodoT);

            //creo el nodo Autor
            XmlNode _NodoA = _DocumentoXML.CreateNode(XmlNodeType.Element, "Autor", "");
            _NodoA.InnerXml = DdlAutor.SelectedValue;
            _NodoL.AppendChild(_NodoA);

            //agrego el nodo libro al documento xml
            _DocumentoXML.DocumentElement.AppendChild(_NodoL);

            //grabo el archivo con la nueva informacion
            _DocumentoXML.Save(_camino); //para q funcione correctamente y no perder datosw

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

    protected void BtnModificar_Click(object sender, EventArgs e)
    {
       try
        {
            //determino archivo de origen datos
            string _camino = Server.MapPath(ConfigurationManager.AppSettings["XmlLibros"]);

            //recupero el documento xml , para modificar nodo libro
            XmlDocument _DocumentoXML = new XmlDocument();
            _DocumentoXML.Load(_camino);

            //primero verifico si se selecciono algun libro desde la grilla
            XmlNode _NodoL = _DocumentoXML.DocumentElement.ChildNodes[(int)ViewState["_haySeleccion"]];
            if(_NodoL ==null)
            {
                LblError.Text = "No se puede modificar, porque no se seleccionó nada";
                return;
            }

            //modifico el nodo seleccionado
            _NodoL["ISBN"].InnerText = TxtIsbn.Text.Trim();
            _NodoL["Titulo"].InnerText = TxtTitulo.Text.Trim();
            _NodoL.Attributes["Categoria"].InnerText = TxtCategoria.Text.Trim();//coloco la coleccion Attributes porque es el 
                                                                                // atributo del nodo primario .
            _NodoL["Autor"].InnerText = DdlAutor.SelectedValue;

            //salvo los cambios
            _DocumentoXML.Save(_camino);

            //determino que ya no hay seleccion
            ViewState["_haySeleccion"] = 0;

            //actualizo la pantalla
            this.CargarDatos();
            this.LimpiarControles();
            this.BotonesPorDefecto();

            //si llegue aca es porque todo Ok.
            LblError.Text = "Modificación con éxito";
        }
        catch (Exception ex)
        { LblError.Text = ex.Message; }
    }

    protected void GVLibros_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //determino archivo de origen datos
            string _camino = Server.MapPath(ConfigurationManager.AppSettings["XmlLibros"]);

            //recupero el documento xml , para eliminar nodo de libro
            XmlDocument _DocumentoXML = new XmlDocument();
            _DocumentoXML.Load(_camino);

            //obtengo nodo seleccionado
            XmlNode _NodoL = _DocumentoXML.DocumentElement.ChildNodes[GVLibros.SelectedIndex];// la pos en la grilla es la misma pos
                                                                                              // de nodo primario q quiero ver

            //determino que hay nodo seleccionado
            ViewState["_haySeleccion"] = GVLibros.SelectedIndex;

            //cargo datos en pantalla
            TxtIsbn.Text = _NodoL["ISBN"].InnerText;
            TxtTitulo.Text = _NodoL["Titulo"].InnerText;
            TxtCategoria.Text = _NodoL.Attributes["Categoria"].InnerText;//es el atributo

            //determino el autor - recordar q solo puede haber un elemento seleccionado
            //en la lista desplegable
            foreach(ListItem _unItem in DdlAutor.Items)
            {
                if (_unItem.Value == _NodoL["Autor"].InnerText)
                    _unItem.Selected = true;
                else
                    _unItem.Selected = false;
            }

            //determino acciones
            this.BotonesBM();
            
        }
        catch (Exception ex)
        { LblError.Text = ex.Message; }
    }

    protected void BtnBaja_Click(object sender, EventArgs e)
    {
        try
        {
            //determino archivo de origen datos
            string _camino = Server.MapPath(ConfigurationManager.AppSettings["XmlLibros"]);

            //recupero el documento xml , para eliminar el nodo de libro
            XmlDocument _DocumentoXML = new XmlDocument();
            _DocumentoXML.Load(_camino);

            //primero verifico si se selecciono algun libro desde la grilla
            XmlNode _NodoL = _DocumentoXML.DocumentElement.ChildNodes[(int)ViewState["_haySeleccion"]];//Nodo raíz
            //Ya tengo un puntero a un objeto con el ChildNodes indicando la pos de grilla.

            if (_NodoL == null)
            {
                LblError.Text = "No se pude eliminar, porque no se seleccionó nada";
                return;
            }

            //elimino el nodo seleccionado
            _DocumentoXML.DocumentElement.RemoveChild(_NodoL);//nunca eliminar el Nodo Raíz, solo Nodos Primarios.
            //se elimina mediante punteros no posiciones

            //salvo los cambios
            _DocumentoXML.Save(_camino);//guardo el archivo luego de la eliminacion

            //determino que ya no hay seleccion
            ViewState["_haySeleccion"] = 0;

            //Actualizo la pantalla
            this.CargarDatos();
            this.LimpiarControles();
            this.BotonesPorDefecto();

            //si llegue aca es porque todo OK
            LblError.Text = "Baja con éxito";
        }
        catch (Exception ex)
        { LblError.Text = ex.Message; }
    }

    protected void DdlAutor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}