using TaskList.Models;
using TaskList.ViewModels;

namespace TaskList.Views;

public partial class AddTareaForm : ContentPage
{
	private AddTareaFormViewModel ViewModel;

	public AddTareaForm()
	{
		InitializeComponent();
		ViewModel = new AddTareaFormViewModel();
		BindingContext = ViewModel;
	}

	public AddTareaForm(Tarea tarea)
	{
		InitializeComponent();
		ViewModel = new AddTareaFormViewModel(tarea);
		BindingContext = ViewModel;
	}


}