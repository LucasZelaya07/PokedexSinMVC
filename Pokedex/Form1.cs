using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokedex
{
    public partial class FrmPokedex : Form
    {
        private List<Pokemon> listaPokemon;
        public FrmPokedex()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Creo una variable de PokemonNegocio y una instancia de PokemonNegocio
            PokemonNegocio negocio = new PokemonNegocio();
            //Negocio.listaPokemon va a la base de datos y devuelve una lista de datos
            //DGVPokemon.DataSource recibe un origen de datos y lo modela en la tabla
            //DataSource lee con "Reflexion" la estructura de los datos y la mapea
            //Los datos que trae negocio.listaPokemon los guardo en un atributo privado
            listaPokemon = negocio.listaPokemon();
            DGVPokemon.DataSource = listaPokemon;
            //Oculto la columna "UrlImagen"
            DGVPokemon.Columns["UrlImagen"].Visible = false;
            //Cargo la imagen de los pokemones
            cargarImagen(listaPokemon[0].UrlImagen);
            //Guardo en una variable
        }

        private void DGVPokemon_SelectionChanged(object sender, EventArgs e)
        {
            //Cuando cambie la selección, cambiara la imagen
            //Devuelve un objeto pero yo se que es un pokemon, asi que lo convierto
            //Con un casteo explicito para guardarlo en una variable pokemon
            Pokemon Seleccionado = (Pokemon)DGVPokemon.CurrentRow.DataBoundItem;
            cargarImagen(Seleccionado.UrlImagen);

        }
        private void cargarImagen(string Imagen)
        {
            //Modulo la funcion para que ejecute el Load
            //Creo una excepción para que no se rompa la app cuando una url no exista mas
            try
            {
                //Si va bien, mostrará la imagen de manera correcta
                PcBxPokemon.Load(Imagen);
            }
            catch (Exception ex)
            {
                //En caso de que no exista o este rota, se mostrará un "image placeholder"
                PcBxPokemon.Load("https://ducasseindustrial.com/wp-content/uploads/2020/07/placeholder.png");
            }
        }
    }
}
