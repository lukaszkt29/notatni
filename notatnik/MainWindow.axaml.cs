using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace notatnik;

public class NoteEntry
{
    public string Timestamp { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public string DisplayText => string.IsNullOrWhiteSpace(Content) 
        ? $"[{Timestamp}] {Title}" 
        : $"[{Timestamp}] {Title}\n{Content}";
}

public partial class MainWindow : Window
{
    private const string FilePath = "notatki.md";
    private readonly List<NoteEntry> _notes = new();

    public MainWindow()
    {
        InitializeComponent();
        LoadNotes();
    }

    private void LoadNotes()
    {
        _notes.Clear();
        if (File.Exists(FilePath))
        {
            var text = File.ReadAllText(FilePath, Encoding.UTF8);
            var rawEntries = text.Split(new[] { "\r\n## ", "\n## " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var raw in rawEntries)
            {
                var entry = raw.StartsWith("## ") ? raw.Substring(3) : raw;
                var lines = entry.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                if (lines.Length > 0)
                {
                    var header = lines[0].Trim();
                    string timestamp = string.Empty;
                    string title = header;

                    const string separator = " — ";
                    int sepIndex = header.IndexOf(separator, StringComparison.Ordinal);
                    if (sepIndex >= 0)
                    {
                        timestamp = header.Substring(0, sepIndex).Trim();
                        title = header.Substring(sepIndex + separator.Length).Trim();
                    }

                    string content = lines.Length > 1 
                        ? string.Join(Environment.NewLine, lines, 1, lines.Length - 1).Trim() 
                        : string.Empty;

                    _notes.Add(new NoteEntry
                    {
                        Timestamp = timestamp,
                        Title = title,
                        Content = content
                    });
                }
            }
        }
        UpdateList();
    }

    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        var title = (TitleBox.Text ?? string.Empty).Trim();
        var content = (ContentBox.Text ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(content))
        {
            return;
        }

        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        var newEntry = $"## {timestamp} — {title}{Environment.NewLine}{content}";

        string existingText = File.Exists(FilePath) ? File.ReadAllText(FilePath, Encoding.UTF8).Trim() : string.Empty;
        string fullContent = string.IsNullOrEmpty(existingText)
            ? newEntry + Environment.NewLine
            : newEntry + Environment.NewLine + Environment.NewLine + existingText + Environment.NewLine;

        try
        {
            File.WriteAllText(FilePath, fullContent, Encoding.UTF8);
            StatusText.Text = "Zapisano";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"Nie udało się zapisać: {ex.Message}";
        }

        TitleBox.Text = string.Empty;
        ContentBox.Text = string.Empty;

        LoadNotes();
    }

    private void UpdateList()
    {
        NotesList.ItemsSource = null;
        NotesList.ItemsSource = _notes.ConvertAll(n => n.DisplayText);
    }
}
