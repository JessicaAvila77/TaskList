using TaskList.Views;

namespace TaskList
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TareaMain());
        }
    }
}
