using System.Text.RegularExpressions;

class Calculator
{
    public static void Main()
    {
        var _chooser = new[]
        {
            new ChooserPick("Реверс строки.") { Command = ReversCommanHandler},
            new ChooserPick("Подсчёт символов в строке.") { Command = CountCommanHandler},
            new ChooserPick("Подсчёт слов в строке.") { Command = CountWordCommanHandler},
            new ChooserPick("Выход из приложения."){ Command = ExitCommandHandler }
        };
        Menu menu = new Menu(_chooser);
        while (true)
        {
            menu.Draw();
            Console.WriteLine(" - - - - - > Выберите способ работы со строками.");
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
   
    public static void ReversCommanHandler()
    {
        Console.WriteLine("Впишите строку.");
        string text = Console.ReadLine();
        char[] charArray = text.ToCharArray();
        Array.Reverse(charArray);
        Console.WriteLine(charArray);
        Message();
        Console.Clear();
    }

    public static void CountCommanHandler()
    {
        Console.WriteLine("Впишите строку.");
        string text = Console.ReadLine();
        var lastLetterIndex = Array.FindLastIndex(text.ToCharArray(), Char.IsLetter);
        Console.WriteLine(text);
        Console.WriteLine("Letter Length : " + (lastLetterIndex + 1));
        Message();
        Console.Clear();
    }

    public static void CountWordCommanHandler()
    {
        Console.WriteLine("Впишите строку.");
        string text = Console.ReadLine();
        int _textcounter = 1;
        for (int i = 0; i < text.Length; i++)
        {
            if ((text[i] == ' ') && (i != 0) && (text[i - 1] != ' ')) _textcounter++;
        }
        Console.WriteLine($"В строке {_textcounter} слов(-а).");
        Message();
        Console.Clear();
    }
    private static void Message() //Повторяющееся сообщение для SWITCH на выбор 
    {
        Console.WriteLine("Нажмите ENTER для того, чтобы вернуться в меню.");
        Console.Read();
        Console.Clear();
        Main();
    }
    public static void ExitCommandHandler()
    {
        Environment.Exit(0);
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