using APPEVENTOS.Services;
using APPEVENTOS.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace APPEVENTOS;

public partial class ComprasPage : ContentPage
{
    private readonly IServicioApi _dataService;
    Usuario _usuario;

    public ComprasPage(IServicioApi dataService, Usuario u)
    {
        InitializeComponent();
        _dataService = dataService;
        _usuario = u;
        //_contactosDb = contactosDb;
        // _servicioApi = servicioApi;
    }

    private async void DetalleItem(object sender, SelectedItemChangedEventArgs e)
    {
        Venta contacto = (Venta)e.SelectedItem;
        await Navigation.PushAsync(new BuyDetailsPage(_dataService)
        {
            BindingContext = contacto
        });
    }

    protected async override void OnAppearing()
    {
        //Write the code of your page here

        base.OnAppearing();
        //var listaContacto = await _servicioApi.ListarContactos();
        var listaContacto = await _dataService.ObtenerCompras(_usuario.cedula);
        var contactos = new ObservableCollection<DatosCompra>(listaContacto);
        listaContactos.ItemsSource = contactos;

    }

}