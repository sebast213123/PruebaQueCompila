namespace PruebaQueCompila;
using SQLite;
public partial class Pagina1 : ContentPage
{
	public Pagina1()
	{

		InitializeComponent();
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