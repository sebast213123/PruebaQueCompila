using SQLite;

using System.Net.Http;

using Newtonsoft.Json;

using System.IO;

using Microsoft.Maui.Storage;



namespace PruebaQueCompila

{

    public partial class Pagina1 : ContentPage

    {

        private SQLiteAsyncConnection basedatos;



        public Pagina1()

        {

            InitializeComponent();

            conexionbd();

        }



        private async void conexionbd()

        {

            string rutaBD = Path.Combine(FileSystem.AppDataDirectory, "Base.db3");

            basedatos = new SQLiteAsyncConnection(rutaBD);

            await basedatos.CreateTableAsync<Pais>();

        }



        public class Pais

        {

            [PrimaryKey, AutoIncrement]

            public int Id { get; set; }

            public string NombreOficial { get; set; }

            public string Region { get; set; }

            public string GoogleMapsLink { get; set; }

        }



        public async Task BuscarPais(string nombrePais)

        {

            var client = new HttpClient();

            var url = $"https://restcountries.com/v3.1/name/{nombrePais}?fields=name,region,maps";

            try

            {

                var response = await client.GetStringAsync(url);

                var paisesJson = JsonConvert.DeserializeObject<List<dynamic>>(response);



                if (paisesJson != null && paisesJson.Count > 0)

                {

                    var paisJson = paisesJson[0];

                    var pais = new Pais

                    {

                        NombreOficial = paisJson.name.official,

                        Region = paisJson.region,

                        GoogleMapsLink = paisJson.maps.googleMaps

                    };



                    GuardarPais(pais);

                    MostrarPais(pais);

                }

                else

                {

                    await DisplayAlert("Error", "No se encontró el país", "OK");

                }

            }

            catch (Exception ex)

            {

                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");

            }

        }



        public async void GuardarPais(Pais pais)

        {

            await basedatos.InsertAsync(pais);

        }



        private void MostrarPais(Pais pais)

        {

            labelPais.Text = $"Nombre: {pais.NombreOficial}\n" +

                             $"Región: {pais.Region}\n" +

                             $"Google Maps: {pais.GoogleMapsLink}";

        }



        private async void OnBuscarClicked(object sender, EventArgs e)

        {

            var nombrePais = entryNombrePais.Text;

            if (!string.IsNullOrEmpty(nombrePais))

            {

                await BuscarPais(nombrePais);

            }

        }



        private void OnLimpiarClicked(object sender, EventArgs e)

        {

            entryNombrePais.Text = string.Empty;

            labelPais.Text = string.Empty;

        }

    }

}