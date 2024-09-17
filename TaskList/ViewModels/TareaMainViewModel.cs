using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Models;
using TaskList.Services;
using TaskList.Views;


namespace TaskList.ViewModels
{
    public partial class TareaMainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Tarea> tareaCollection = new ObservableCollection<Tarea>();

        //Agregado
        [ObservableProperty]
        private ObservableCollection<Tarea> selectedTareas = new ObservableCollection<Tarea>();

        private readonly TareaService TareaService;

        public TareaMainViewModel()
        {
            TareaService = new TareaService();
        }

        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        public void GetAll()
        {
            var getAll = TareaService.GetAll();

            if (getAll.Count > 0)
            {
                TareaCollection.Clear();

                foreach (var tarea in getAll)
                {
                    //TareaCollection.Add(tarea);
                    tarea.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(Tarea.IsSelected))
                        {
                            if (tarea.IsSelected && !SelectedTareas.Contains(tarea))
                            {
                                SelectedTareas.Add(tarea);
                            }
                            else if (!tarea.IsSelected && SelectedTareas.Contains(tarea))
                            {
                                SelectedTareas.Remove(tarea);
                            }
                        }
                    };

                    //
                    TareaCollection.Add(tarea);

                }


            }
        }

        //en este punto como ese constructor no existe hay que crear la vista con el archivo xaml 
        //configurar constructores en css
        
        [RelayCommand]

        private async Task goToAddTareaForm()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new AddTareaForm());
        }

        [RelayCommand]

        private async Task SelectTarea(Tarea tarea)
        {
            try
            {
                string actualizar = "Actualizar";
                string eliminar = "Eliminar";

                string res = await App.Current!.MainPage!.DisplayActionSheet("OPCIONES", "Cancelar", null, actualizar, eliminar);

                if (res == actualizar)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddTareaForm(tarea));

                }else if (res == eliminar)
                {
                    bool respuesta = await App.Current!.MainPage!.DisplayAlert("ELIMINAR TAREA", "Desea eliminar el  registro de tarea", "Si", "No");

                    if (respuesta)
                    {
                        int del = TareaService.Delete(tarea);

                        if (del > 0) 
                        { 
                            TareaCollection.Remove(tarea);
                        }

                    }
                }


            }catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }


        }

        //Agregar

        [RelayCommand]
        private void DeleteSelectedTareas()
        {
            foreach (var tarea in SelectedTareas.ToList()) // Usar ToList para evitar modificar la colección mientras se itera  
            {
                TareaService.Delete(tarea);
                TareaCollection.Remove(tarea);
            }

            SelectedTareas.Clear(); // Limpiar la lista de tareas seleccionadas  
        }



    }
}
