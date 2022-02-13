using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Pokedex
{
     public class PokemonNegocio
    {
        //Donde creo los metodos de acceso a DB para los pokemones
        public List<Pokemon> listaPokemon()
        {
            List<Pokemon> lista = new List<Pokemon>();
            //Creo conexión a la DB
            SqlConnection conexion = new SqlConnection();
            //Creo comando a la DB
            SqlCommand comando = new SqlCommand();
            //Creo un lector de data pero NO una instancia del objeto porque el comando
            //devuelve un objeto para que lo lea con "lector"
            SqlDataReader lector;
            //En caso de ejecutarse todo bien, seguira este proceso
            try
            {
                //Configuro la conexión a la DB
                //Digo a donde me voy a conectar, a que base y el ingreso
                conexion.ConnectionString = "server = .\\SQLEXPRESS; database = POKEDEX_DB; integrated security = true;";
                //Digo que voy a leer los datos
                comando.CommandType = System.Data.CommandType.Text;
                //Escribo la consulta a realizar en la base de datos
                comando.CommandText = "Select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad From POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo AND D.Id = P.IdDebilidad";
                //Declaro que los comando que estoy realizando, la quiero ejecutar en "conexion"
                comando.Connection = conexion;
                //Abro la conexion
                conexion.Open();
                //Ya que ExecuteReader devuelve un dato de tipo SQLDataReader, lo guardo en lector
                lector = comando.ExecuteReader();
                //Creo un while para que ".Read()" lea la información que tiene "lector"
                //Y luego empiece a ordenar los datos en las columnas correspondientes
                while (lector.Read())
                {
                    //Creo un pokemon auxiliar
                    //En cada vuelta va a reutilizar la variable pero creara una nueva instancia
                    //Y la lista tendra referencias a distintos objetos
                    Pokemon aux = new Pokemon();
                    aux.Numero = lector.GetInt32(0);
                    //Le declaro que lo que va a recibir es un string
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    //Genero una instancia para que no tenga un error "null"
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)lector["Tipo"]; 
                    //Repito proceso con "Debilidad"
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    //Agrego el pokemon a la lista de pokemones
                    lista.Add(aux);
                }
                return lista;
          
            }
            //En caso de un error, arroja una excepcion para evitar que la app se rompa
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Cierro la conexión a DB
                conexion.Close();
            }
            
        }
    }
}
