using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;
namespace PruebaQueCompila {
    public partial class Pagina2 : ContentPage {
        private SQLiteAsyncConnection basedatos; public Pagina2()
        { InitializeComponent(); conexionbd(); LeerPaises();
        
        } private async void conexionbd()
        { string rutaBD = Path.Combine(FileSystem.AppDataDirectory, "Base.db3"); basedatos = new SQLiteAsyncConnection(rutaBD);
          await basedatos.CreateTableAsync<Pais>(); }
          private async void LeerPaises() { 
            var paises = await basedatos.Table<Pais>().ToListAsync(); 
            var paisesUnicos = paises.GroupBy(p => p.NombreOficial).Select(grupo => grupo.First()).ToList();
            var paisesList = paisesUnicos.Select(pais => new PaisDisplay 
            { DisplayText = $"Nombre País: {pais.NombreOficial}, Región: {pais.Region}, Link: {pais.GoogleMapsLink}, NombreBD: Scadena" }).ToList();
            listViewPaises.ItemsSource = paisesList; }

          private void OnActualizarClicked(object sender, System.EventArgs e) { LeerPaises(); }
          public class PaisDisplay { public string DisplayText { get; set; } } 
          public class Pais { [PrimaryKey, AutoIncrement] public int Id { get; set; }
          public string NombreOficial { get; set; } public string Region { get; set; }
          public string GoogleMapsLink { get; set; } } } }
