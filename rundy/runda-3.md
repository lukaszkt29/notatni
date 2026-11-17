# Wynik Rundy 3: Przykład w prompcie (One-shot)

## Prośba (Prompt)
> Notatki zapisuj do pliku notatki.md w folderze projektu. Najnowsza notatka ma trafiać na górę pliku. Polskie znaki mają zostać polskimi znakami.
> Plik ma wyglądać dokładnie tak:
> ## 2026-09-08 14:32 — Zakupy
> mleko
> chleb
> 
> ## 2026-09-07 09:15 — Pomysł na projekt
> minutnik z historią sesji

## Wygenerowany plik na dysku: `notatki.md`

```markdown
## 2026-11-17 15:53 — Projekt 3.2
Notatnik i cztery rundy próśb

## 2026-11-17 15:52 — Zakupy
mleko
chleb
jajka
```

## Obserwacje
- Jeden przykład (one-shot) natychmiast wyjaśnił wszelkie wątpliwości z rundy 2:
  - Odstępy: dokładnie jedna pusta linia między notatkami.
  - Tytuł: znak półpauzy ` — ` po dacie bez sekund.
  - Treść: linie treści umieszczone bezpośrednio pod nagłówkiem, bez zbędnych prefiksów (typu `Treść:` czy cudzysłowów).
- Model nie musiał zgadywać ani interpretować opisu tekstowego – skopiował strukturę z przykładu.
- Ograniczenie one-shot: gdy w przykładzie jest tylko jeden rodzaj treści (np. dwulinijkowy wpis), model może nie być pewny zachowania przy wpisie jednolinijkowym lub z nietypowymi znakami (np. myślnik wewnątrz samego tytułu).
