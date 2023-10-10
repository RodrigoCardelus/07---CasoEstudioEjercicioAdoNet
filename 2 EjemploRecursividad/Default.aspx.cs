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

//agrego uso de XML
using System.Xml;

public partial class _Default : System.Web.UI.Page 
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }




    protected void BtnProcesar_Click(object sender, EventArgs e)
    {
        //limpio caja de despliegue - para que no se repita
        TxtMostrar.Text = ""; 

        //levanto el archivo XML que voy a mostrar en la página
        XmlDocument _XmlAlumnos = new XmlDocument();
        _XmlAlumnos.Load(Server.MapPath("~/XML/Alumnos.xml")); //Operación Load - levanto estructura del archivo
        //MapPath obtiene la dirección física (real) del archivo que está en mi sitio web.

        //obtengo el nodo raíz
        XmlNode _NodoRaiz = _XmlAlumnos.DocumentElement; //partimos del nodo raíz con el DocumentElement (puntero al comienzo del Nodo de datos)

        //muestro el nodo raiz en el listbox
        TxtMostrar.Text += "Nombre Nodo Raiz: " + _NodoRaiz.Name + "\n"; //la propiedad Name me dice cómo se llama la etiqueta.

        //invoco al método recursivo para mostrar todo el contenido
        MostrarNodoRecursivo(_NodoRaiz); //operación recursiva (MostrarNodoRecursivo) - es recursiva porque recibe un nodo y muestra
        //el contenido del mismo sin importar el nivel del nodo.

   
    }

    private void MostrarNodoRecursivo (XmlNode pNodo) //recibe un Nodo como parámetro ya que todos los niveles de mi árbol son de ese tipo.
    {
       //recorro todos los nodos hijos directos del nodo que viene por parámetro
       for (int indice=0;indice < pNodo.ChildNodes.Count;indice++)//para recorrer los nodos hijos uso ChildNodes.Count (marco todos)
        {
            if(pNodo.ChildNodes[indice].NodeType == XmlNodeType.Element) //pregunta si es una etiqueta
            {
                // agrego el nombre de la etiqueta si estoy ante un elemento del tipo <elemento>
                //pregunto si tiene elementos hijos, asi no indento (tabulador)
                //(por eso pregunto por la cantidad de hijos mayor a 1,
                //ya que el texto contenido se considera un hijo más.
                if (pNodo.ChildNodes[indice].ChildNodes.Count > 1) //etiqueta nodo (más de un hijo: etiquetas contenidas)
                    TxtMostrar.Text += pNodo.ChildNodes[indice].Name + "\n";
                else //o etiqueta con texto contenido (solo un hijo: texto)
                    TxtMostrar.Text += "\t" + pNodo.ChildNodes[indice].Name; // \t es un tabulador (para hacer un espacio)
            }
            else//o es el texto contenido de la etiqueta.
            {
                //agrego el texto si estoy ante el contenido de un elemento
                //<elemento> contenido </elemento>
                TxtMostrar.Text += ": " + pNodo.ChildNodes[indice].InnerText + "\n";//indico dónde está el txt contenido (InnerText).
            }

            //si hay nodos hijos, mando cada uno a mostrarse
            if (pNodo.ChildNodes.Count>0)//regla para hacer la recursividad, si no hay regla no existe la recursividad.
                MostrarNodoRecursivo(pNodo.ChildNodes[indice]); // operacion recursiva (se nombra a si misma dentro del método)
        }//fin for 
    }//fin método

    protected void BtnAlReves_Click(object sender, EventArgs e) //Planteo todo al revés, cambio el método recursivo y coloco luego
        //el nombre del Nudo Raíz
    {
        //limpio caja de despliegue - para que no se repita
        TxtMostrar.Text = "";

        //levanto el archivo XML que voy a mostrar en la página
        XmlDocument _XmlAlumnos = new XmlDocument();
        _XmlAlumnos.Load(Server.MapPath("~/XML/Alumnos.xml")); //Operación Load - levanto estructura del archivo
        //MapPath obtiene la dirección física (real) del archivo que está en mi sitio web.

        //obtengo el nodo raíz
        XmlNode _NodoRaiz = _XmlAlumnos.DocumentElement; //partimos del nodo raíz con el DocumentElement (puntero al comienzo del Nodo de datos)
                                                         //invoco al método recursivo para mostrar todo el contenido

        MostrarNodoRecursivoAlReves(_NodoRaiz); //operación recursiva (MostrarNodoRecursivo) - es recursiva porque recibe un nodo y muestra
        //el contenido del mismo sin importar el nivel del nodo.
        
        //muestro el nodo raiz en el listbox
        TxtMostrar.Text += "Nombre Nodo Raiz: " + _NodoRaiz.Name + "\n"; //la propiedad Name me dice cómo se llama la etiqueta.
        
    }
    private void MostrarNodoRecursivoAlReves(XmlNode pNodo) //recibe un Nodo como parámetro ya que todos los niveles de mi árbol son de ese tipo.
    {
        //recorro todos los nodos hijos directos del nodo que viene por parámetro
        for (int indice = pNodo.ChildNodes.Count-1; indice >= 0; indice--)//para recorrer los nodos hijos uso ChildNodes.Count (marco todos)
        {
            if (pNodo.ChildNodes[indice].NodeType == XmlNodeType.Element) //pregunta si es una etiqueta
            {
                // agrego el nombre de la etiqueta si estoy ante un elemento del tipo <elemento>
                //pregunto si tiene elementos hijos, asi no indento (tabulador)
                //(por eso pregunto por la cantidad de hijos mayor a 1,
                //ya que el texto contenido se considera un hijo más.
                if (pNodo.ChildNodes[indice].ChildNodes.Count > 1) //etiqueta nodo (más de un hijo: etiquetas contenidas)
                    TxtMostrar.Text += pNodo.ChildNodes[indice].Name + "\n";
                else //o etiqueta con texto contenido (solo un hijo: texto)
                    TxtMostrar.Text += "\t" + pNodo.ChildNodes[indice].Name; // \t es un tabulador (para hacer un espacio)
            }
            else//o es el texto contenido de la etiqueta.
            {
                //agrego el texto si estoy ante el contenido de un elemento
                //<elemento> contenido </elemento>
                TxtMostrar.Text += ": " + pNodo.ChildNodes[indice].InnerText + "\n";//indico dónde está el txt contenido (InnerText).
            }

            //si hay nodos hijos, mando cada uno a mostrarse
            if (pNodo.ChildNodes.Count > 0)//regla para hacer la recursividad, si no hay regla no existe la recursividad.
                MostrarNodoRecursivoAlReves(pNodo.ChildNodes[indice]);
        }//fin for 
    }//fin método

}
