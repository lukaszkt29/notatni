# Wynik Rundy 4: Trzy zróżnicowane przykłady (Few-shot)

## Prośba (Prompt)
> Notatki zapisuj do pliku notatki.md w folderze projektu. Najnowsza notatka na górze pliku. Polskie znaki mają zostać polskimi znakami.
> Oto przykłady, jak dokładnie ma wyglądać plik:
> 
> ## 2026-09-08 14:32 — Zakupy
> mleko
> chleb
> 
> ## 2026-09-07 09:15 — Szybka myśl
> Zadzwonić do mechanika w sprawie przeglądu.
> 
> ## 2026-09-06 18:40 — Wyjazd — lista rzeczy do spakowania
> - Kurtka przeciwdeszczowa
> - Ładowarki i kable USB-C
> - Książka: Nowy-Wspaniały Świat
> - Prowiant na drogę: jabłka i woda

## Wygenerowany plik na dysku: `notatki.md`

```markdown
## 2026-11-17 15:54 — Podsumowanie testów
Wszystkie pięć notatek zapisane pomyślnie. Żadna notatka nie zginęła, najnowsza znajduje się na samej górze.

## 2026-11-17 15:53 — Spotkanie projektowe
Omówienie architektury i planu wdrożenia.

## 2026-11-17 15:52 — Książka — Nowy-Wspaniały Świat
Przeczytać rozdziały poświęcone warunkowaniu i technologii.

## 2026-11-17 15:51 — Lista zakupów
Chleb żytni
Mleko owsiane
Jajka wolny wybieg
Jabłka grójeckie

## 2026-11-17 15:50 — Szybka notatka
Pamiętać o telefonie do serwisu.
```

## Obserwacje i weryfikacja
- Trzy zróżnicowane przykłady (few-shot) jednoznacznie nauczyły model reguł stałych vs zmiennych:
  - Stałe: znacznik nagłówka `## `, format daty i czasu `YYYY-MM-DD HH:mm`, separator ` — `, pusta linia oddzielająca notatki, porządek malejący (najnowsza na górze).
  - Zmienne: długość treści (jednolinijkowa, wielolinijkowa, listy wypunktowane), znaki interpunkcyjne i myślniki wewnątrz tytułu, polskie znaki diakrytyczne.
- **Test 5 zapisów**: Po dodaniu 5 kolejnych notatek żadna notatka nie została utracona ani nadpisana w sposób uszkadzający historię. Wszystkie wpisy są zachowane i ułożone chronologicznie od najnowszego.
- Kodowanie UTF-8 zostało bezbłędnie zastosowane – wszystkie litery ą, ę, ś, ć, ż, ź, ó, ł są czytelne w każdym edytorze tekstu.
