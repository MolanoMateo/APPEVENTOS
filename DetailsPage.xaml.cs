using APPEVENTOS.Models;
using APPEVENTOS.Services;
//using APPEVENTOS.Utils;

namespace APPEVENTOS;

public partial class DetailsPage : ContentPage
{
    //readonly IServicioApi _servicioApi = new ServicioApi();
    private readonly IServicioApi _dataService;
    private Usuario _usuario;

    public DetailsPage(IServicioApi dataService, Usuario u)
    {
        InitializeComponent();
        _dataService = dataService;
        _usuario= u;
        //_contactosDb = contactosDb;
        // _servicioApi = servicioApi;
    }

    protected override void OnAppearing()
    {
        //Write the code of your page here
        base.OnAppearing();
        Usuario contacto = new Usuario();
        if (_usuario != null)
        {
            contacto = _usuario;
        }
        else
        {
            contacto = BindingContext as Usuario;
        }
        foto.Source = contacto.foto;
        nombre.Text = contacto.nombre;
        telefono.Text = "Telf: "+ contacto.telefono;
        mail.Text = contacto.mail;
        cedula.Text = "CI: "+contacto.cedula;
        edad.Text = "Edad: "+contacto.edad.ToString();
    }

    private async void onClickEliminarContacto(object sender, EventArgs e)
	{
        Usuario contacto = BindingContext as Usuario;
        await _dataService.EliminarUsuario(contacto.cedula);
        //await  _servicioApi.BorrarContacto(contacto.cedula);
        //Util.listContacto.Remove(contacto);
        await Navigation.PopAsync();
	}

	private async void onClickModificarContacto(Object sender, EventArgs e)
	{

		await Navigation.PushAsync(new NewContactPage(_dataService)
		{
			BindingContext = _usuario as Usuario,
		}) ;
    }
}