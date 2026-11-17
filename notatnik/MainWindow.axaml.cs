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

    public string DisplayText => $"{Timestamp} — {Title}\n{Content}";
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
            var blocks = text.Split(new[] { "\n## ", "\r\n## " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var block in blocks)
            {
                var cleanBlock = block.StartsWith("## ") ? block.Substring(3) : block;
                var lines = cleanBlock.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                if (lines.Length > 0)
                {
                    var header = lines[0];
                    var dashIndex = header.IndexOf(" — ");
                    var timestamp = dashIndex > 0 ? header.Substring(0, dashIndex).Trim() : string.Empty;
                    var title = dashIndex > 0 ? header.Substring(dashIndex + 3).Trim() : header.Trim();
                    var content = lines.Length > 1 ? string.Join(Environment.NewLine, lines, 1, lines.Length - 1).Trim() : string.Empty;

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
        var title = TitleBox.Text ?? string.Empty;
        var content = ContentBox.Text ?? string.Empty;
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        var newEntryText = $"## {timestamp} — {title}{Environment.NewLine}{content}{Environment.NewLine}";

        string existingText = File.Exists(FilePath) ? File.ReadAllText(FilePath, Encoding.UTF8) : string.Empty;
        string fullContent = string.IsNullOrEmpty(existingText)
            ? newEntryText
            : newEntryText + Environment.NewLine + existingText.TrimStart();

        File.WriteAllText(FilePath, fullContent, Encoding.UTF8);

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
