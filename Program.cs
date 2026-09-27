using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CP2020
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
    static class ListExtensions
    {
        static public void Add<T>(this List<T> list, params T[] additions)
        {
            foreach (T addition in additions)
            {
                list.Add(addition);
            }
        }
    }
}
