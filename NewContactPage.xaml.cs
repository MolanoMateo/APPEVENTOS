//using APPEVENTOS.Utils;
using APPEVENTOS.Models;
using System.Collections.ObjectModel;
using APPEVENTOS.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
namespace APPEVENTOS;


public partial class NewContactPage : ContentPage
{
	Usuario contacto;
    private string fotoCapturada;
    private readonly IServicioApi _dataService;
    //private ContactosDb _contactosDb = new ContactosDb();
    //string imageString = (string)ImageSourceConverter.ConvertToInvariantString(fotoCapturada);

    private void CameraPage_PhotoCaptured(object sender, ImageSource photo)
    {
        fotoCapturada = (sender as CameraPage).CapturedImagePath;
        foto.Source = ImageSource.FromFile(fotoCapturada);
    }
    
    public NewContactPage(IServicioApi dataService)
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
        contacto = BindingContext as Usuario;
        if (contacto != null)
        {
    
            nombre.Text = contacto.nombre;
            mail.Text = contacto.mail;
            edad.Text = contacto.edad.ToString();
            password.Text = contacto.password;
            telefono.Text = contacto.telefono;
            cedula.Text = contacto.cedula;
			cedula.IsEnabled = false;
		}
		else
		{
            cedula.IsEnabled = true;
        }

    }

    private async void onClickGuardarContacto(object sender, EventArgs e)
	{
        //ImageSourceConverter converter = new ImageSourceConverter();
        if (contacto == null)
		{
            if (cedula.Text != null && password.Text != null && mail.Text != null && edad.Text != null)
            {
                if(nombre.Text == null)
                {
                    nombre.Text = " ";
                }if(telefono.Text == null)
                {
                    telefono.Text = " ";
                }
			contacto = new Usuario()
            {
                nombre = nombre.Text,
                mail = mail.Text,
                edad = int.Parse(edad.Text),
                password = password.Text,
                telefono = telefono.Text,
                cedula = cedula.Text,

                foto = fotoCapturada != null ? fotoCapturada : "imagen1.png",
                rol = "usuario"
            };
                await _dataService.GuardarUsuario(contacto);
                await Navigation.PopAsync();
            }
                
		}
		else
		{
            if (nombre.Text != null)
            {
                contacto.nombre = nombre.Text;
            }
            if (edad.Text != null)
            {
                contacto.edad = int.Parse(edad.Text);
            }
            if (mail.Text != null)
            {
                contacto.mail = mail.Text;
            }
            if (password.Text != null)
            {
                contacto.password = password.Text;
            }
            if (telefono.Text != null)
            {
                contacto.telefono = telefono.Text;
            }            
            contacto.foto = fotoCapturada;
            //await _servicioApi.EditarContacto(contacto.cedula, contacto);
            await _dataService.EditarUsuario(contacto.cedula, contacto);
            await Navigation.PopAsync();
            BindingContext = contacto;
		}
        
    }

    //private async void onClickAbrirCamara(object sender, EventArgs e)
    //{
    //	await Navigation.PushAsync(new CameraPage());
    //}

    private async void onClickAbrirCamara(object sender, EventArgs e)
    {
        var cameraPage = new CameraPage();
        cameraPage.PhotoCaptured += CameraPage_PhotoCaptured;
        await Navigation.PushAsync(cameraPage);
    }
}