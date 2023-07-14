using APPEVENTOS.Services;
using APPEVENTOS.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace APPEVENTOS;

public partial class EventosPage : ContentPage
{
    private readonly IServicioApi _dataService;
    Usuario _usuario;

    public EventosPage(IServicioApi dataService, Usuario u)
    {
        InitializeComponent();
        _dataService = dataService;
        _usuario = u;
        //_contactosDb = contactosDb;
        // _servicioApi = servicioApi;
    }

    private async void DetalleItem(object sender, SelectedItemChangedEventArgs e)
    {
        Evento contacto = (Evento)e.SelectedItem;
        await Navigation.PushAsync(new ComprarPage(_dataService, _usuario)
        {
            BindingContext = contacto
        });
    }

    protected async override void OnAppearing()
    {
        //Write the code of your page here

        base.OnAppearing();
        //var listaContacto = await _servicioApi.ListarContactos();
        var listaContacto = await _dataService.ListarEventos();
        var contactos = new ObservableCollection<Evento>(listaContacto);
        listaContactos.ItemsSource = contactos;

    }

    private async void onClickHistorial(object sender, EventArgs e)
    {
        //var page = Navigation.NavigationStack.LastOrDefault();
        await Navigation.PushAsync(new ComprasPage(_dataService, _usuario));
        //Navigation.RemovePage(page);

    }
    private async void onClickCarrito(object sender, EventArgs e)
    {
        //var page = Navigation.NavigationStack.LastOrDefault();
        await Navigation.PushAsync(new CarritoPage(_dataService, _usuario));
        //Navigation.RemovePage(page);

    }
    private async void onClickDatosUsuario(object sender, EventArgs e)
    {
        //var page = Navigation.NavigationStack.LastOrDefault();
        await Navigation.PushAsync(new DetailsPage(_dataService, _usuario));
        //Navigation.RemovePage(page);

    }
}