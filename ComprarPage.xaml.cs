using APPEVENTOS.Models;
using APPEVENTOS.Services;
using System;
//using APPEVENTOS.Utils;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace APPEVENTOS;

public partial class ComprarPage : ContentPage
{
    //readonly IServicioApi _servicioApi = new ServicioApi();
    private readonly IServicioApi _dataService;
    private Carrito c=new Carrito();
    Usuario _usuario;

    public ComprarPage(IServicioApi dataService, Usuario u)
    {
        InitializeComponent();
        _dataService = dataService;
        _usuario = u;
        //_contactosDb = contactosDb;
        // _servicioApi = servicioApi;
    }

    protected override void OnAppearing()
    {
        //Write the code of your page here
        base.OnAppearing();
        Evento contacto = BindingContext as Evento;
        c.idEvento = contacto.Id;
        c.cantidad = 0;
        c.fechaCompra = DateTime.Today.ToString();
        c.titulo = contacto.titulo;
        c.descripcion = contacto.descripcion;
        c.costo = contacto.costo;
        c.total = 0;
        titulo.Text = contacto.titulo;
        descripcion.Text = contacto.descripcion;
        costo.Text = "$"+ contacto.costo.ToString();
        ubicacion.Text = contacto.ubicacion;
        fecha.Text = "Fecha: "+contacto.fecha;
    }


	private async void onClickCarrito(Object sender, EventArgs e)
	{
        c.cantidad = int.Parse(cantidad.Text);
        c.total = c.costo * c.cantidad;
        await App.carritorepo.saveCarrito(c);
        await Navigation.PopAsync();
    }
}