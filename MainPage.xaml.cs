using APPEVENTOS.Models;
using APPEVENTOS.Services;
using Microsoft.EntityFrameworkCore;
//using APPEVENTOS.Utils;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace APPEVENTOS;

public partial class MainPage : ContentPage
{
    private readonly IServicioApi _dataService;

    public MainPage(IServicioApi dataService)
    {
        InitializeComponent();
        _dataService = dataService;
        //_contactosDb = contactosDb;
        // _servicioApi = servicioApi;
    }

    private async void onClickLogin(object sender, EventArgs e)
    {
        Usuario inicio=new Usuario();
        inicio.mail = mail.Text;
        inicio.password = password.Text;
        Usuario u = await _dataService.LoginUsuario(inicio);
        if(u != null)
        {
            await Navigation.PushAsync(new EventosPage(_dataService, u));
        }
        else
        {
            mail.Text="";
            password.Text = "";
        }
    }
    private async void Label_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewContactPage(_dataService));
    }

}

