using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Models;

namespace TaskList.Services
{
    public class TareaService
    {
        private readonly SQLiteConnection dbConecction;

        public TareaService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tarea.db3");

            dbConecction = new SQLiteConnection(dbPath);

            dbConecction.CreateTable<Tarea>();


        }

        public List<Tarea> GetAll()
        {
            var res = dbConecction.Table<Tarea>().ToList();
            return res;
        }

        public int Insert(Tarea tarea)
        {
            return dbConecction.Insert(tarea);
        }

        public int Update(Tarea tarea)
        {
            return dbConecction.Update(tarea);
        }

        public int Delete(Tarea tarea)
        {
            return dbConecction.Delete(tarea);
        }
    }
}
