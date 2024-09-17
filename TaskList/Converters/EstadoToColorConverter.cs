using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskList.Converters
{
    public class EstadoToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string estado)
            {
                return estado switch
                {
                    "Finalizada" => Colors.Green,   // Verde para tareas finalizadas
                    "En proceso" => Colors.Blue, // Azul para tareas en progreso
                    "Por hacer" => Colors.Red,      // Rojo para tareas por hacer
                    _ => Colors.Gray,               // Color gris para estados desconocidos
                };
            }
            return Colors.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
