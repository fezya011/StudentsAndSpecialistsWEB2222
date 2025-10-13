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
                    Console.WriteLine("2 - ");
                    Console.WriteLine("3 - Выход");
                    Console.Write("Ваш выбор: ");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            GetListStudentsByIndex();
                            break;
                        case "2":
                           
                            break;
                        case "3":
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
    }
}
