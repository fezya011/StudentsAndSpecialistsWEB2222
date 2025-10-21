using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
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
    internal class MainWindowViewModel : BaseVM
    {
        private static readonly HttpClient client = new HttpClient();
        private int indexGroup;
        private List<StudentDTO> students = new List<StudentDTO>();
        private int female;
        private int male;

        public int IndexGroup 
        { 
            get => indexGroup;
            set
            {
                indexGroup = value;
                Signal();
            }
        }

        public List<StudentDTO> Students 
        { 
            get => students; 
            set
            {
                students = value;
                Signal();
            }
        }

        public int Female 
        { 
            get => female; 
            set
            {
                female = value;
                Signal();
            }
        }

        public int Male 
        { 
            get => male; 
            set
            {
                male = value;
                Signal();
            }
        }

        public ICommand ShowListStudentsByIndexGroup { get; set; }
        public ICommand ClearList { get; set; }
        public ICommand ShowGendersCountByIndexGroup { get; set; }
        public ICommand GoToGroupsWindow { get; set; }

        public MainWindowViewModel() 
        {
            client.BaseAddress = new Uri("http://localhost:5135/api/Students/");
            ShowListStudentsByIndexGroup = new CommandVM(async () =>
            {
                var result = await client.GetAsync($"CountGenderByIndexGroup?indexGroup={IndexGroup}");
                var data = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
                foreach (var student in data)
                {
                    if (student.Gender == 1)
                        Male++;

                    else
                        Female++;
                }
                if (indexGroup == 0)
                {
                    var result3 = await client.GetAsync($"ListUnboundStudents");
                    var data3 = await result3.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
                    var list3 = new List<StudentDTO>();
                    foreach (var student in data)
                    {
                        list3.Add(student);
                    }
                    Students = list3.ToList();
                }
                else
                {
                   
                    var result2 = await client.GetAsync($"ListStudentsIndexGroup?indexGroup={IndexGroup}");
                    var data2 = await result2.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();
                    var list2 = new List<StudentDTO>();
                    foreach (var student in data)
                    {
                        list2.Add(student);
                    }
                    Students = list2.ToList();
                }
            }, () => true);

            ClearList = new CommandVM(async () =>
            {
                var list = new List<StudentDTO>();
                Students = list.ToList();
                Female = 0;
                Male = 0;
            }, () => true);

            GoToGroupsWindow = new CommandVM(async () =>
            {
                GroupWindow group = new GroupWindow();
                group.ShowDialog();
            }, () => true);


        }

    }
}
