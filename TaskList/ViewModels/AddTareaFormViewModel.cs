using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Models;
using TaskList.Services;

namespace TaskList.ViewModels
{
    public partial class AddTareaFormViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string nombre;

        [ObservableProperty]
        private string estado;

        [ObservableProperty]
        private string prioridad;

        private readonly TareaService TareaService;

        //crear la vista en el archivo cs agregar constructores

        public AddTareaFormViewModel()
        {
            TareaService = new TareaService();
        }

        public AddTareaFormViewModel(Tarea Tarea)
        {
            TareaService = new TareaService();
            Id = Tarea.Id;
            Nombre = Tarea.Nombre;
            Estado = Tarea.Estado;
            Prioridad = Tarea.Prioridad;
        }

        private void Alerta(string Titulo, string Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert(Titulo, Mensaje, "Aceptar"));
        }

        [RelayCommand]

        private async Task AddUpdate()
        {
            try
            {
                Tarea Tarea = new Tarea(){
                    Id = Id,
                    Nombre= Nombre,
                    Estado= Estado, 
                    Prioridad= Prioridad
                };

                if (Validar(Tarea))
                {
                    if (Id == 0)
                    {
                        TareaService.Insert(Tarea);
                    }
                    else
                    {
                        TareaService.Update(Tarea);
                    }

                    await App.Current!.MainPage!.Navigation.PopAsync();

                }
                


            }
            catch (Exception ex)
            {
                Alerta("ERROR", ex.Message);
            }
        }

        //Metodo que valida atributos requeridos
        private bool Validar(Tarea Tarea)
        {
            if (Tarea.Nombre == null || Tarea.Nombre == "")
            {
                Alerta("ADVERTENCIA", "Ingrese el nombre completo");
                return false;

            }else if (Tarea.Estado == null || Tarea.Estado == "")
            {
                Alerta("ADVERTENCIA", "Ingrese el estado de la tarea");
                return false;

            }
            else
            {
                return true;
            }
        }


    }
}
