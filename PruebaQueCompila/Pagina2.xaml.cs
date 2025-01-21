namespace PruebaQueCompila
{
    using SQLite;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Maui.Controls;
    using System.IO;

    public partial class Pagina2 : ContentPage
    {
        private SQLiteAsyncConnection basedatos;

        public Pagina2()
        {
            InitializeComponent();
            conexionbd(); 
            LeerPaises(); 
        }

      
        private async void conexionbd()
        {
            string rutaBD = Path.Combine(FileSystem.AppDataDirectory, "Base.db3");
            basedatos = new SQLiteAsyncConnection(rutaBD);
            await basedatos.CreateTableAsync<Pais>(); 
        }

        
        private async void LeerPaises()
        {
            var paises = await basedatos.Table<Pais>().ToListAsync(); 

            var paisesList = new List<PaisDisplay>();

            foreach (var pais in paises)
            {
                paisesList.Add(new PaisDisplay
                {
                    DisplayText = $"Nombre País: {pais.NombreOficial}, Región: {pais.Region}, Link: {pais.GoogleMapsLink}, NombreBD: Scadena"
                });
            }

            listViewPaises.ItemsSource = paisesList; 
        }

       
        public class PaisDisplay
        {
            public string DisplayText { get; set; }
        }

        
        public class Pais
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string NombreOficial { get; set; }
            public string Region { get; set; }
            public string GoogleMapsLink { get; set; }
        }
    }
}
