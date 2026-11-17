# Wynik Rundy 2: Prośba z dopisanym formatem (Zero-shot z formatem)

## Prośba (Prompt)
> Notatki zapisuj do jednego pliku notatki.md w folderze projektu. Format wpisu: ## 2026-09-08 14:32 — Tytuł notatki treść notatki, może mieć kilka linii Między wpisami pusta linia. Najnowsza notatka na górze pliku. Polskie znaki mają zostać polskimi znakami.

## Wygenerowany plik na dysku: `notatki.md`

```markdown
## 2026-11-17 15:53 — Projekt 3.2
Notatnik i cztery rundy próśb

## 2026-11-17 15:52 — Zakupy
Mleko, chleb, jajka
```

## Obserwacje
- Wynik jest teraz w jednym pliku Markdown `notatki.md`, a nie w JSON ani luźnych plikach tekstowych.
- Najnowszy wpis znajduje się na samej górze pliku, a starsze wpisy zostały przesunięte w dół bez utraty zawartości.
- Kodowanie UTF-8 zachowało polskie znaki diakrytyczne (np. „ó”, „ś”) w ich naturalnej postaci zamiast znaków ucieczki `\u00F3` czy krzaczków.
- Opis słowny wymusił konkretną strukturę, jednak wciąż pozostaje drobne pole do interpretacji (np. dokładna liczba pustych linii, czy format myślnika to dywiz czy półpauza).
