using TaskList.ViewModels;

namespace TaskList.Views;

public partial class TareaMain : ContentPage
{
	private TareaMainViewModel ViewModel;

	public TareaMain()
	{
		InitializeComponent();
		ViewModel = new TareaMainViewModel();
		this.BindingContext = ViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		ViewModel.GetAll();
    }

}