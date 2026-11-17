# Wynik Rundy 1: Sama prośba (Zero-shot)

## Prośba (Prompt)
> Zrób notatnik: pole na tytuł, pole na treść, przycisk zapisu i lista zapisanych notatek. Notatki mają być zapisywane do pliku.

## Wygenerowany plik na dysku: `notes.json`

```json
[
  {
    "Title": "Zakupy",
    "Content": "Mleko, chleb, jajka"
  },
  {
    "Title": "Projekt 3.2",
    "Content": "Notatnik i cztery rundy pr\u00F3sb"
  }
]
```

## Obserwacje
- Model samodzielnie wybrał nazwę i format pliku: `notes.json`.
- Polskie znaki zostały potraktowane domyślnym enkoderem JSON i zamienione na sekwencje ucieczki unicode (`\u00F3` zamiast `ó`).
- Brak określonego formatu daty, brak formatu nagłówków, brak zdefiniowanego zachowania kolejności notatek (nowe dodawane na koniec listy).
- Nikt o to nie prosił, ale model musiał sam dopowiedzieć wszystkie brakujące szczegóły.
