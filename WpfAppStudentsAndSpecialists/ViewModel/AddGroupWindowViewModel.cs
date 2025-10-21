using StudentsAndSpecialistsWEB;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppStudentsAndSpecialists.View;
using WpfAppStudentsAndSpecialists.VMTools;

namespace WpfAppStudentsAndSpecialists.ViewModel
{
    internal class AddGroupWindowViewModel : BaseVM
    {
        private static readonly HttpClient client = new HttpClient();
        private int idSpec;
        private string title;

        public int IdSpec 
        { 
            get => idSpec; 
            set
            {
                idSpec = value;
                Signal();
            }
        }
        public string Title 
        {
            get => title;
            set
            {
                title = value;
                Signal();
            }
        }

        public ICommand SaveNewGroup { get; set; }
        public ICommand GoToBack { get; set; }
        public AddGroupWindowViewModel(AddGroupWindow window) 
        {
            client.BaseAddress = new Uri("http://localhost:5135/api/Students/");

            SaveNewGroup = new CommandVM(async () =>
            {
                var responce = await client.PostAsync($"AddGroupBySpecial?idSpecial={IdSpec}&title={Title}", null);
                if (responce.IsSuccessStatusCode)
                {
                    MessageBox.Show("Все четко");
                    
                }
                else
                {
                    var error = await responce.Content.ReadAsStringAsync();
                    MessageBox.Show($"{error}");
                }
                window.Close();
            }, () => true);

            GoToBack = new CommandVM(async () =>
            {
                window.Close();
            }, () => true);
        }

    }
}
