using StudentsAndSpecialistsWEB;
using StudentsAndSpecialistsWEB.CQRS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfAppStudentsAndSpecialists.View;
using WpfAppStudentsAndSpecialists.VMTools;

namespace WpfAppStudentsAndSpecialists.ViewModel
{
    internal class GroupWindowViewModel : BaseVM
    {
        private static readonly HttpClient client = new HttpClient();
        private List<GroupDTO> groups = new List<GroupDTO>();
        private int indexSpec;

        public List<GroupDTO> Groups 
        { 
            get => groups; 
            set
            {
                groups = value;
                Signal();
            }
        }

        public int IndexSpec 
        { 
            get => indexSpec; 
            set
            {
                indexSpec = value;
                Signal();
            }
        }

        public ICommand ShowEmptyGroups { get; set; }
        public ICommand ShowStatsBySpec { get; set; }
        public ICommand ClearList { get; set; }
        public ICommand GoToBack { get; set; }
        public ICommand GoToAddGroupWindow { get; set; }

        public GroupWindowViewModel(GroupWindow window) 
        {
            client.BaseAddress = new Uri("http://localhost:5135/api/Students/");


            ShowEmptyGroups = new CommandVM(async () =>
            {
                var resultGroups = await client.GetAsync($"ListEmptyGroups");
                var dataGroups = await resultGroups.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
                var list = new List<GroupDTO>();
                foreach (var group in dataGroups)
                {
                    list.Add(group);
                }
                Groups = list.ToList();
            }, () => true);

            ShowStatsBySpec = new CommandVM(async () =>
            {
                var resultGroups = await client.GetAsync($"ListGroupsCountStudentsAndIndex");
                var dataGroups = await resultGroups.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();
                var list = new List<GroupDTO>();
                foreach (var group in dataGroups)
                {
                    list.Add(group);
                }
                Groups = list.ToList();

            }, () => true);

            ClearList = new CommandVM(async () =>
            {
                var list = new List<GroupDTO>();
                Groups = list.ToList();
               
            }, () => true);

            GoToBack = new CommandVM(async () =>
            {
                window.Close();
            }, () => true);

            GoToAddGroupWindow = new CommandVM(async () =>
            {
                AddGroupWindow addGroupWindow = new AddGroupWindow();
                addGroupWindow.ShowDialog();
            }, () => true);
        }
    }
}
