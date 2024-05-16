using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SeniorsRegistry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // this will create our database file if it does not exist
            // manually add EntityFrameWork's Infrastucture
            DatabaseFacade facade = new DatabaseFacade(new DataContext());
            facade.EnsureCreated();  // takes care of creating the DB file is it does not exist yet                     

        }
    }

}
