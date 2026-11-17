using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace notatnik;

public class NoteItem
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

public partial class MainWindow : Window
{
    private const string FilePath = "notes.json";
    private readonly List<NoteItem> _notes = new();

    public MainWindow()
    {
        InitializeComponent();
        LoadNotes();
    }

    private void LoadNotes()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            var loaded = JsonSerializer.Deserialize<List<NoteItem>>(json);
            if (loaded != null)
            {
                _notes.Clear();
                _notes.AddRange(loaded);
            }
        }
        UpdateList();
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var title = TitleBox.Text ?? string.Empty;
        var content = ContentBox.Text ?? string.Empty;

        var note = new NoteItem { Title = title, Content = content };
        _notes.Add(note);

        var json = JsonSerializer.Serialize(_notes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);

        TitleBox.Text = string.Empty;
        ContentBox.Text = string.Empty;

        UpdateList();
    }

    private void UpdateList()
    {
        NotesList.ItemsSource = null;
        NotesList.ItemsSource = _notes.ConvertAll(n => $"{n.Title}: {n.Content}");
    }
}
