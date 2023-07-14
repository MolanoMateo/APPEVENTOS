using APPEVENTOS.Models;
using APPEVENTOS.Services;
//using APPEVENTOS.Utils;

namespace APPEVENTOS;

public partial class BuyDetailsPage : ContentPage
{
    //readonly IServicioApi _servicioApi = new ServicioApi();
    private readonly IServicioApi _dataService;

    public BuyDetailsPage(IServicioApi dataService)
    {
        InitializeComponent();
        _dataService = dataService;
        //_contactosDb = contactosDb;
        // _servicioApi = servicioApi;
    }

    protected override void OnAppearing()
    {
        //Write the code of your page here
        base.OnAppearing();
        DatosCompra contacto = BindingContext as DatosCompra;

        cantidad.Text = contacto.cantidad.ToString();
        fechaCompra.Text = contacto.fechaCompra;
        titulo.Text = contacto.titulo;
        descripcion.Text = contacto.descripcion;
        costo.Text = contacto.costo.ToString();
        ubicacion.Text = contacto.ubicacion;
        fechaEvento.Text = contacto.fechaEvento;
    }
}