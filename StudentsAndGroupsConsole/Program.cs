using StudentsAndSpecialistsWEB.CQRS.DTO;
using System.Net.Http.Json;

namespace StudentsAndGroupsConsole
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            client.BaseAddress = new Uri("http://localhost:5135/api/Students/");
            Console.WriteLine("Api");

            try
            {
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("\nВыберите действие:");
                    Console.WriteLine("1 - Получить список студентов из группы по указанному индексу группы");
                    Console.WriteLine("2 - Получить список кол-ва мальчиков и девочек из группы по указанному индексу группы");
                    Console.WriteLine("3 - Получить список студентов не привязанных к группе");
                    Console.WriteLine("4 - Получить список пустых групп");
                    Console.WriteLine("5 - Получить список групп с количеством студентов в них");
                    Console.WriteLine("6 - Получить список групп с количеством студентов в них с индексом специальности");
                    Console.WriteLine("7 - Добавить группу по указанной специальности");
                    Console.WriteLine("8 - Выход");
                    Console.WriteLine("9 - Выход");
                    Console.WriteLine("10 - Выход");
                    Console.Write("Ваш выбор: ");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            GetListStudentsByIndex();
                            break;
                        case "2":
                            GetCountGendersByIndexGroup();
                            break;
                        case "3":
                            GetListUnboundStudents();
                            break;
                        case "4":
                            GetListEmptyGroups();
                            break;
                        case "5":
                            GetListGroupsCountStudents();
                            break;
                        case "6":
                            GetListGroupsCountStudentsAndIndex();
                            break;
                        case "7":
                            AddGroupBySpecial();
                            break;
                        case "8":
                            exit = true;
                            break;
                        case "9":
                            exit = true;
                            break;
                        case "10":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        public static async Task GetListStudentsByIndex()
        {
            try
            {
                Console.WriteLine("Введите индекс группы");
                int.TryParse(Console.ReadLine(), out int index);
                
                var result = await client.GetAsync($"ListStudentsIndexGroup?indexGroup={index}");

                var data = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();

                Console.WriteLine($"Ответ");

                foreach (var student in data)
                {
                    Console.WriteLine($"Имя: {student.FirstName} Фамилия: {student.LastName}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }

        public static async Task GetCountGendersByIndexGroup()
        {
            try
            {
                Console.WriteLine("Введите индекс группы");
                int.TryParse(Console.ReadLine(), out int index);

                var result = await client.GetAsync($"CountGenderByIndexGroup?indexGroup={index}");

                var data = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();

                Console.WriteLine($"Ответ");

                var male = 0;
                var female = 0;

                foreach (var student in data)
                {
                    if (student.Gender == 1)
                        male++;

                    else
                        female++;
                }
                Console.WriteLine($"Мальчиков: {male} \tДевочек: {female}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }

        public static async Task GetListUnboundStudents()
        {
            try
            {               
                var result = await client.GetAsync($"ListUnboundStudents");

                var data = await result.Content.ReadFromJsonAsync<IEnumerable<StudentDTO>>();

                Console.WriteLine($"Ответ");
                Console.WriteLine($"Кол-во не привязанных студентов {data.Count()}");

                foreach (var student in data)
                {
                    Console.WriteLine($"Имя: {student.FirstName} Фамилия: {student.LastName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }

        public static async Task GetListEmptyGroups()
        {
            try
            {
                var resultGroups = await client.GetAsync($"ListEmptyGroups");
                var dataGroups = await resultGroups.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();

                var count = 0;
                foreach (var group in dataGroups)
                {
                    Console.WriteLine($"Пустая группа: ID={group.Id}, Название={group.Title}");
                    count++;
                }

                Console.WriteLine($"Всего пустых групп: {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }

        public static async Task GetListGroupsCountStudents()
        {
            try
            {
                var resultGroups = await client.GetAsync($"ListGroupsCountStudents");
                var dataGroups = await resultGroups.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();

                var count = 0;
                foreach (var group in dataGroups)
                {
                    Console.WriteLine($"Группа: Название={group.Title}, кол-во студентов={group.StudentsCount}");
                    count++;
                }

                Console.WriteLine($"Всего групп: {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }

        public static async Task GetListGroupsCountStudentsAndIndex()
        {
            try
            {
                var resultGroups = await client.GetAsync($"ListGroupsCountStudentsAndIndex");
                var dataGroups = await resultGroups.Content.ReadFromJsonAsync<IEnumerable<GroupDTO>>();

                var count = 0;
                foreach (var group in dataGroups)
                {
                    Console.WriteLine($"Группа: Название={group.Title}, кол-во студентов={group.StudentsCount}, индекс={group.IdSpecial}");
                    count++;
                }

                Console.WriteLine($"Всего групп: {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }

        public static async Task AddGroupBySpecial()
        {
            try
            {
                Console.WriteLine("Введите Id специальности");
                var idSpecial = Console.ReadLine();

                Console.WriteLine("Введите название новой группы");
                var title = Console.ReadLine();

                var responce = await client.PostAsync($"AddGroupBySpecial?idSpecial={idSpecial}&title={title}", null);

                if (responce.IsSuccessStatusCode)
                    Console.WriteLine("Группа успешно добавлена!");
                else
                {
                    var error = await responce.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message}");
            }
        }
    }
}
