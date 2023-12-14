using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dispashopta
{
    static class processes
    {
        static int SelectedIndex = 0;

        private static List<info> GetTasks(Process[] processes)
        {
            List<info> result = new List<info>();
            Console.WriteLine("Диспетчер задач Ашота");
            Console.WriteLine("Имя                                                                 ID        Память                                   ");
            for (int i = 0; i < processes.Length; i++)
            {
                result.Add(new info(processes[i].ProcessName, processes[i].Id, processes[i].PagedMemorySize64));
            }
            return result;
        }
        static public void DisplayTasks()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ResetColor();
            Process[] processes = Process.GetProcesses();
            List<info> tasks = GetTasks(processes);

            string[] options = new string[tasks.Count];
            for (int i = 0; i < options.Length; i++)
            {

                options[i] = tasks[i].Name;

                Console.SetCursorPosition(0, 2 + i);
                Console.WriteLine(tasks[i].Name);
                Console.SetCursorPosition(70, 2 + i);
                Console.WriteLine(tasks[i].Id);
                Console.SetCursorPosition(80, 2 + i);
                Console.WriteLine(tasks[i].Memory);
                Console.SetCursorPosition(90, 2 + i);
                Console.Write("Байта");
            }
            Console.SetCursorPosition(0, 0);
            while (true)
            {
                Console.SetCursorPosition(0, SelectedIndex + 15);
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if (SelectedIndex + 1 < tasks.Count)
                        {
                            SelectedIndex++;
                            if (SelectedIndex == tasks.Count)
                            {
                                SelectedIndex = 0;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.ResetColor();
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (SelectedIndex > 0)
                        {
                            SelectedIndex--;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.ResetColor();
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, SelectedIndex + 3);
                        try
                            {
                            Console.WriteLine("-----------------------------------");
                            Console.WriteLine($"Физическая память: {processes[SelectedIndex].WorkingSet64}");
                            Console.WriteLine($"Базовый приоритет: {processes[SelectedIndex].BasePriority}");
                            Console.WriteLine($"Приоритетный класс: {processes[SelectedIndex].PriorityClass}");
                            Console.WriteLine($"Процессорное время: {processes[SelectedIndex].UserProcessorTime}");
                            Console.WriteLine($"Привелигерованное процессорное время: {processes[SelectedIndex].PrivilegedProcessorTime}");
                            Console.WriteLine($"Общее время работы процессора: {processes[SelectedIndex].TotalProcessorTime}");
                            Console.WriteLine($"Размер системной памяти: {processes[SelectedIndex].PagedSystemMemorySize64} Bytes");
                            Console.WriteLine($"Размер памяти: {processes[SelectedIndex].PagedMemorySize64} Bytes");

                            if (processes[SelectedIndex].Responding)
                            {
                                Console.WriteLine("Статус - запущен");
                            }
                            else
                            {
                                Console.WriteLine("Статус - не отвечает");
                            }

                            Console.WriteLine("-------------------------------------                ");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("-------------------------------------                ");

                            Console.WriteLine($"Физическая память: {processes[SelectedIndex].WorkingSet64}");
                            Console.WriteLine($"Базовый приоритет: Доступ запрещен");
                            Console.WriteLine($"Приоритетный класс: Доступ запрещен");
                            Console.WriteLine($"Процессорное время: Доступ запрещен");
                            Console.WriteLine($"Привелигерованное процессорное время: Доступ запрещен");
                            Console.WriteLine($"Общее время работы процессора: Доступ запрещен");
                            Console.WriteLine($"Размер системной памяти: {processes[SelectedIndex].PagedSystemMemorySize64} Bytes");
                            Console.WriteLine($"Размер памяти: {processes[SelectedIndex].PagedMemorySize64} Bytes");

                            if (processes[SelectedIndex].Responding)
                            {
                                Console.WriteLine("Статус - запущен");
                            }
                            else
                            {
                                Console.WriteLine("Статус - не отвечает");
                            }

                            Console.WriteLine("-------------------------------------                ");
                        }
                        finally
                        {
                            Console.ReadKey(true);
                            Console.Clear();
                            DisplayTasks();
                        }
                        break;
                    case ConsoleKey.Delete:
                        try
                        {
                            processes[SelectedIndex].Kill(false);

                            Console.SetCursorPosition(0, SelectedIndex + 2);
                            Console.WriteLine(options[SelectedIndex] + " Остановка....");

                            Console.Clear();
                            DisplayTasks();
                        }
                        catch (Exception)
                        {
                            int errorindex = new Random().Next(1, 6);
                            Errors error = (Errors)errorindex;
                            Console.SetCursorPosition(0, SelectedIndex + 2);
                            Console.WriteLine(options[SelectedIndex]);
                        }
                        break;
                    case ConsoleKey.D:
                        try
                        {
                            processes[SelectedIndex].Kill(true);

                            Console.SetCursorPosition(0, SelectedIndex + 2);
                            Console.WriteLine(options[SelectedIndex] + "Остановка....");

                            Console.Clear();
                            DisplayTasks();
                        }
                        catch (Exception)
                        {
                            int errorindex = new Random().Next(1, 6);
                            Errors error = (Errors)errorindex;

                            Console.SetCursorPosition(0, SelectedIndex + 2);
                            Console.WriteLine(options[SelectedIndex]);
                        }
                        break;
                }

            }
        }
    }
    internal enum Errors
    {
        error1 = 1,
        error2 = 2,
        error3 = 3,
        error4 = 4,
        error5 = 5,
        error6 = 6
    }
}

