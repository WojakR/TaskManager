using System;
using System.Collections.Generic;
using System.IO;

class TaskManager
{
    private List<string> tasks;
    
    public TaskManager()
    {
        tasks = new List<string>();
    }

    public void AddTask(string task)
    {
        tasks.Add(task);
    }

    public void RemoveTask(int index)
    {
        if (index >= 0 && index < tasks.Count) tasks.RemoveAt(index);
        else Console.WriteLine("Błędny indeks.");    
    }

    public void ShowTasks()
    {
        if (tasks.Count == 0) Console.WriteLine("Brak zadań do wyświetlenia");
        else
        {
            Console.WriteLine("\nLista zadań:");
            for (int i = 0; i < tasks.Count; i++) Console.WriteLine($"{i + 1}. {tasks[i]}");
        }
    }

    public void SaveToFile(string fileName)
    {
        try
        {
            File.WriteAllLines(fileName, tasks);
            Console.WriteLine("Zadania zostały zapisane do pliku.");
        }
        catch(Exception e)
        {
            Console.WriteLine($"Błąd przy zapisywaniu: {e.Message}");
        }
    }

    public void LoadFromFile(string fileName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                tasks = new List<string>(File.ReadAllLines(fileName));
                Console.WriteLine("Zadania zostały załadowane.");
            }
            else Console.WriteLine("Plik nie istnieje. Tworzę nowy.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Błąd przy ładowaniu: {e.Message}");
        }
    }
}

class Program
{
    static void Main()
    {
        TaskManager taskManager = new TaskManager();
        string fileName = "tasks.txt";
        taskManager.LoadFromFile(fileName);
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menadżer zadań:");
            Console.WriteLine("1. Dodaj zadanie");
            Console.WriteLine("2. Usuń zadanie");
            Console.WriteLine("3. Wyświetl zadania");
            Console.WriteLine("4. Zapisz zadania");
            Console.WriteLine("5. Wczytaj zadania");
            Console.WriteLine("6. Wyjdź");
            Console.WriteLine("Wybierz opcję: ");

            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    Console.Write("Wpisz zadanie: ");
                    string task = Console.ReadLine();
                    taskManager.AddTask(task);
                    break;
                case "2":
                    taskManager.ShowTasks();
                    Console.WriteLine("Wybierz numer zadania do usunięcia: ");
                    if (int.TryParse(Console.ReadLine(), out int index)) taskManager.RemoveTask(index - 1);
                    break;
                case "3":
                    taskManager.ShowTasks();
                    break;
                case "4":
                    taskManager.SaveToFile(fileName);
                    break;
                case "5":
                    taskManager.LoadFromFile(fileName);
                    break;
                case "6":
                    Console.WriteLine("Do zobaczenia!");
                    return;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }
}