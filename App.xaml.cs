using APPEVENTOS.Services;

namespace APPEVENTOS;

public partial class App : Application
{
    public static string StorageDirectory { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Photos");
	public static CarritoDb carritorepo { get; set; }

    public App(CarritoDb repo)
	{
		InitializeComponent();

		MainPage = new AppShell();
		carritorepo=repo;
	}
}
