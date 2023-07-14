using APPEVENTOS.Services;
using APPEVENTOS.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace APPEVENTOS;

public partial class CarritoPage : ContentPage
{
    
    private readonly IServicioApi _dataService;
    Usuario _usuario;

    public CarritoPage(IServicioApi dataService, Usuario u)
    {
        InitializeComponent();
        _dataService = dataService;
        _usuario = u;
        //_contactosDb = contactosDb;
        // _servicioApi = servicioApi;
    }


    protected async override void OnAppearing()
    {
        base.OnAppearing();

        //var listaContacto = await _contactosDb.GetCarritoAsync();
        List<Carrito> carritos = await App.carritorepo.obtenerCarrito();
        
            var contactos = new ObservableCollection<Carrito>(carritos);
            listaContactos.ItemsSource = contactos;
        
    }

    private async void onClickPagar(object sender, EventArgs e)
    {

        List<Carrito> cc = await App.carritorepo.obtenerCarrito();
        List<Venta> v = new List<Venta>();
        foreach (var c in cc)
        {
            Venta nv = new Venta();
            nv.idEvento = c.idEvento;
            nv.cantidad = c.cantidad;
            nv.usuario = _usuario.cedula;
            nv.fecha = c.fechaCompra;
            await _dataService.GuardarVenta(nv);
        }
        foreach(var item in cc)
        {
            await App.carritorepo.eliminarCarrito(item);
        }
        
        await Navigation.PopAsync();

    }
    private async void onClickVaciar(object sender, EventArgs e)
    {
        List<Carrito> cc = await App.carritorepo.obtenerCarrito();
        foreach (var item in cc)
        {
            await App.carritorepo.eliminarCarrito(item);
        }
        List<Carrito> carritos = await App.carritorepo.obtenerCarrito();
        var contactos = new ObservableCollection<Carrito>(carritos);
        listaContactos.ItemsSource = contactos;

    }
}