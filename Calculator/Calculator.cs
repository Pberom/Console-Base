using System.Text.RegularExpressions;

class Calculator
{
    public static void Main()
    {
        var _chooser = new[]
        {
            new ChooserPick("Да.") { Command = AsseptCommandHandler},
            new ChooserPick("Нет.") { Command = RejectCommandHandler},
            new ChooserPick("Выход из приложения."){ Command = ExitCommandHandler }
        };
        Menu menu = new Menu(_chooser);
        while (true)
        {
            menu.Draw();
            Console.WriteLine(" - - - - - > Калькулятор. Использовать ручной ввод функции?");
            switch (Console.ReadKey(true).Key) //Переключение по кнопкам
            {
                case ConsoleKey.UpArrow:
                    menu.SelectPrev();
                    break;
                case ConsoleKey.DownArrow:
                    menu.SelectNext();
                    break;
                case ConsoleKey.Enter:
                    menu.ExecuteSelected();
                    break;
                default: return;
            }
        }
    }

    private static void AsseptCommandHandler()
    {
        Console.Clear();
        Console.WriteLine("Введите данные примера. Символы через пробел.");
        string? _counter = Console.ReadLine();
        string[] counter = _counter.Split(" "); //Разделяю на отдельные символы там где есть строка
        if (counter == null) //Если вышло так, что _counter вернулся пустым, запускаю метод снова, чтобы вызвать ReadLine
        {
            AsseptCommandHandler();
        }
        else
        if (counter.Length > 3) //Проверяю сколько значений ввели
        {
            Console.WriteLine("Доступно только 2 значения - X и Y.");
            Message();
        }
            try
            {
                switch (counter[1])
                {
                    case "+":
                        int resultPlus = int.Parse(counter[0]) + int.Parse(counter[2]); //Парсирую отдельно значения и считаю
                        Console.WriteLine($"Результат: {counter[0]} прибавить к {counter[2]} будет {resultPlus}."); //Вывожу
                        Message();
                        break;
                    case "-":
                        int resultMinus = int.Parse(counter[0]) - int.Parse(counter[2]);
                        Console.WriteLine($"Результат: {counter[0]} вычесть {counter[2]} будет {resultMinus}.");
                        Message();
                        break;
                    case "*":
                        int resultMulti = int.Parse(counter[0]) * int.Parse(counter[2]);
                        Console.WriteLine($"Результат: {counter[0]} помножить на {counter[2]} будет {resultMulti}.");
                        Message();
                        break;
                    case "/":
                        if (int.Parse(counter[2]) == 0)
                        {
                            Console.WriteLine("Делить на 0 запрещено.");
                            Message();
                        }
                        int resultDelete = int.Parse(counter[0]) / int.Parse(counter[2]);
                        Console.WriteLine($"Результат: {counter[0]} поделить на {counter[2]} будет {resultDelete}.");
                        Message();
                        break;
                    default:
                        Console.WriteLine("Ошибка исполнении действий.");
                        Message();
                        break;
                }
            }
            catch
            {
                AsseptCommandHandler();
            }
    }
    private static void RejectCommandHandler()
    {
        var _chooser = new[]
        {
            new ChooserPick("Сложение") { Command = PlusCommandHandler},
            new ChooserPick("Вычитание") { Command = MinusCommandHandler},
            new ChooserPick("Умножение"){ Command = MultiCommandHandler },
            new ChooserPick("Деление"){ Command = DeleteCommandHandler },
        };
        Menu menu = new Menu(_chooser);
        while (true)
        {
            menu.Draw();
            Console.WriteLine(" - - - - - > Выберите функцию для исполнения.");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    menu.SelectPrev();
                    break;
                case ConsoleKey.DownArrow:
                    menu.SelectNext();
                    break;
                case ConsoleKey.Enter:
                    menu.ExecuteSelected();
                    break;
                default: return;
            }
        }
    }
    //Везде схожий принцип кода. Проверяю наличие регулярки и в принципе смотрю, пустая ли строка
    //В любых других случаях сработает try-catch
    public static void PlusCommandHandler()
    {
        ErrorHandler errorHandler1 = new();
        ErrorHandler errorHandler = errorHandler1;
        try
        {
            Console.Clear();
            Console.Write("Введите первое значение: ");
            string? x = Console.ReadLine();
            if ((x != null) && (Regex.IsMatch(x, @"^[a-zA-ZА-Яа-я]+$") != true))
            {
                Console.Write("Введите второе значение: ");
                string? y = Console.ReadLine();
                if ((y != null) && (Regex.IsMatch(y, @"^[a-zA-ZА-Яа-я]+$") != true))
                {
                    int z = int.Parse(x) + int.Parse(y);
                    Console.WriteLine($"Результат: {x} прибавить к {y} равняется {z}.");
                    Message();
                }
                else
                {
                    errorHandler1.UncorrectSymbolError();
                    PlusCommandHandler();
                }
            }
            else
            errorHandler1.UncorrectSymbolError();
            PlusCommandHandler();
        }
        catch
        {
            errorHandler1.UnexpectedError();
            PlusCommandHandler();
        }
    }
    public static void MinusCommandHandler()
    {
        ErrorHandler errorHandler1 = new();
        try
        {
            Console.Clear();
            Console.Write("Введите первое значение: ");
            string? x = Console.ReadLine();
            if ((x != null) && (Regex.IsMatch(x, @"^[a-zA-ZА-Яа-я]+$") != true))
            {
                Console.Write("Введите второе значение: ");
                string? y = Console.ReadLine();
                if ((y != null) && (Regex.IsMatch(y, @"^[a-zA-ZА-Яа-я]+$") != true))
                {
                    int z = int.Parse(x) - int.Parse(y);
                    Console.WriteLine($"Результат: {x} вычесть {y} равняется {z}.");
                    Message();
                }
                else
                errorHandler1.UncorrectSymbolError();
                MinusCommandHandler();
            }
            else
            errorHandler1.UncorrectSymbolError();
            MinusCommandHandler();
        }
        catch
        {
            errorHandler1.UnexpectedError();
            MinusCommandHandler();
        }
    }
    public static void MultiCommandHandler()
    {
        ErrorHandler errorHandler1 = new();
        try
        {
            Console.Clear();
            Console.Write("Введите первое значение: ");
            string? x = Console.ReadLine();
            if ((x != null) && (Regex.IsMatch(x, @"^[a-zA-ZА-Яа-я]+$") != true))
            {
                Console.Write("Введите второе значение: ");
                string? y = Console.ReadLine();
                if ((x != null) && (Regex.IsMatch(x, @"^[a-zA-ZА-Яа-я]+$") != true))
                {
                    int z = int.Parse(x) * int.Parse(y);
                    Console.WriteLine($"Результат: {x} помножить на {y} равняется {z}.");
                    Message();
                }
                else
                errorHandler1.UncorrectSymbolError();
                MultiCommandHandler();
            }
            else
            errorHandler1.UncorrectSymbolError();
            MultiCommandHandler();
        }
        catch
        {
            errorHandler1.UnexpectedError();
            MultiCommandHandler();
        }
    }
    public static void DeleteCommandHandler()
    {
        ErrorHandler errorHandler1 = new();
        try
        {
            Console.Clear();
            Console.Write("Введите первое значение: ");
            string? x = Console.ReadLine();
            if ((x != null) && (Regex.IsMatch(x, @"^[a-zA-ZА-Яа-я]+$") != true))
            {
                Console.Write("Введите второе значение: ");
                string? y = Console.ReadLine();
                if ((y != null) && (Regex.IsMatch(y, @"^[a-zA-ZА-Яа-я]+$") != true) && (int.Parse(y) != 0))
                {
                    int z = int.Parse(x) / int.Parse(y);
                    Console.WriteLine($"Результат: {x} поделить на {y} равняется {z}.");
                    Message();
                }
                else
                errorHandler1.UncorrectSymbolError();
                DeleteCommandHandler();
            }
            else
            errorHandler1.UncorrectSymbolError();
            DeleteCommandHandler();
        }
        catch
        {
            errorHandler1.UnexpectedError();
            DeleteCommandHandler();
        }
    }
    public static void ExitCommandHandler()
    {
        Environment.Exit(0);
    }
    private static void Message() //Повторяющееся сообщение для SWITCH на выбор 
    {
        Console.WriteLine("Нажмите ENTER для продолжения.");
        Console.Read();
        StartAgainCommandHandler();
    }
    private static void StartAgainCommandHandler()
    {
        Console.Clear();
        Console.WriteLine("Желаете вернуться в меню?");
        var _chooser = new[]
        {
            new ChooserPick("В меню") { Command = Main},
            new ChooserPick("К ручному калькулятору") { Command = AsseptCommandHandler},
            new ChooserPick("К автоматическому калькулятору") { Command = RejectCommandHandler},
        };
        Menu menu = new Menu(_chooser);
        while (true)
        {
            menu.Draw();
            Console.WriteLine(" - - - - - > Калькулятор. Использовать ручной ввод функции?");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    menu.SelectPrev();
                    break;
                case ConsoleKey.DownArrow:
                    menu.SelectNext();
                    break;
                case ConsoleKey.Enter:
                    menu.ExecuteSelected();
                    break;
                default: return;
            }
        }
    }
}

delegate void CommandHandler();
class Menu
{
    public ChooserPick[] _chooserPick { get; set; }
    public int Index { get; set; }

    public Menu(ChooserPick[] picker)
    {
        this.Index = 0;
        this._chooserPick = picker;
        _chooserPick[Index].IsSelected = true;
    }

    public void Draw()
    {
        Console.Clear();
        foreach (var element in _chooserPick)
        {
            element.Print();
        }
    }

    public void SelectNext()
    {
        if (Index == _chooserPick.Length - 1) return;
        _chooserPick[Index].IsSelected = false;
        _chooserPick[++Index].IsSelected = true;
    }

    public void SelectPrev()
    {
        if (Index == 0) return;
        _chooserPick[Index].IsSelected = false;
        _chooserPick[--Index].IsSelected = true;
    }

    public void ExecuteSelected()
    {
        _chooserPick[Index].Execute();
    }
}

class ChooserPick
{
    public string Text { get; set; }
    public ConsoleColor SelectedForeColor { get; set; }
    public ConsoleColor SelectedBackColor { get; set; }
    public bool IsSelected { get; set; }
    public CommandHandler Command;

    public ChooserPick(string text)
    {
        this.Text = text;
        this.SelectedForeColor = ConsoleColor.Black;
        this.SelectedBackColor = ConsoleColor.Gray;
        this.IsSelected = false;
    }

    public void Print()
    {
        if (this.IsSelected)
        {
            Console.BackgroundColor = this.SelectedBackColor;
            Console.ForegroundColor = this.SelectedForeColor;
        }
        Console.WriteLine(this.Text);
        Console.ResetColor();
    }

    public void Execute()
    {
        if (Command == null) return;
        Command.Invoke();
    }
}

class ErrorHandler
{
    public void UnexpectedError()
    {
        Console.WriteLine("Непредвиденная ошибка. Нажмите любую клавишу для перезапуска метода.");// Ожидаем когда юзер нажмёт что-либо
        Console.ReadLine();
        Console.Clear();
    }
    public void UncorrectSymbolError()
    {
        Console.WriteLine("Вы совершили ошибку в написании (использовали буквы или недопустимые спецсимволы)." +
        "\nБолее не используйте. Нажмитите любую клавишу для перезапуска метода."); // Ожидаем когда юзер нажмёт что-либо
        Console.ReadLine();
        Console.Clear();
    }
}