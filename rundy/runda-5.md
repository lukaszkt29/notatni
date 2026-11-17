# Wynik Rundy 5: Prośba poprawiona przez model (Meta-prompt)

## Wiadomość wejściowa (z Rundy 1)
> Zrób notatnik: pole na tytuł, pole na treść, przycisk zapisu i lista zapisanych notatek. Notatki mają być zapisywane do pliku.

## Meta-prompt
> To jest moja prośba do agenta kodującego. Nie wykonuj jej. Wypisz, czego w niej brakuje, żeby wynik był jednoznaczny, i zaproponuj poprawioną wersję.

## Analiza braków dokonana przez model
W pierwotnej prośbie zidentyfikowano następujące niejednoznaczności:
1. **Lokalizacja i nazwa pliku**: Brak informacji, gdzie i pod jaką nazwą plik ma być zapisany (`notatki.md`, `notes.txt`, `notes.json`).
2. **Format danych i struktura**: Brak definicji sposobu reprezentacji danych (JSON, Markdown, prosty tekst) oraz separatorów między notatkami.
3. **Kolejność wpisów**: Brak określenia, czy nowe notatki mają być dopisywane na końcu (`Append`), czy wstawiane na górze pliku (`Prepend`).
4. **Kodowanie tekstu**: Brak wymogu kodowania UTF-8, co może prowadzić do zniekształcenia polskich znaków.
5. **Zachowanie interfejsu**: Brak określenia, czy po zapisie pola tekstowe mają być czyszczone oraz czy przy starcie programu istniejące notatki mają być wczytywane do listy.
6. **Znaczniki czasu**: Brak informacji, czy notatka ma zawierać automatyczną datę i godzinę utworzenia.

## Poprawiona wersja prośby wygenerowana przez model
> Stwórz aplikację notatnika w Avalonia UI (.NET):
> 
> **Interfejs użytkownika:**
> - Pole na tytuł notatki (`TextBox`).
> - Wieloliniowe pole na treść notatki (`TextBox` z `AcceptsReturn="True"` i zawijaniem wierszy).
> - Przycisk zapisu (`Button` z podpisem "Zapisz notatkę").
> - Lista zapisanych notatek (`ListBox`) prezentująca wpisy od najnowszego do najstarszego.
> - Po kliknięciu zapisu pola formularza mają zostać wyczyszczone.
> 
> **Zapis i format pliku:**
> - Wszystkie notatki zapisuj do jednego pliku `notatki.md` w katalogu projektu.
> - Kodowanie pliku: UTF-8 z pełną obsługą polskich znaków diakrytycznych.
> - Każda nowo zapisana notatka musi trafiać na samą górę pliku (najnowszy wpis pierwszy).
> - Format pojedynczego wpisu:
>   `## YYYY-MM-DD HH:mm — [Tytuł]`
>   `[Treść - może mieć wiele wierszy]`
> - Pomiędzy poszczególnymi wpisami zachowaj dokładnie jedną pustą linię odstępu.
> - Przy uruchomieniu programu odczytaj istniejący plik i wyświetl wpisy na liście.

## Wygenerowany plik na dysku: `notatki.md`

```markdown
## 2026-11-17 15:56 — Druga notatka z prośby poprawionej
Treść sprawdzająca obsługę wielu linii,
polskich znaków: zażółć gęślą jaźń,
oraz poprawnego umieszczania na początku pliku.

## 2026-11-17 15:55 — Pierwsza notatka
Początkowy wpis testowy w ustrukturyzowanym formacie.
```

## Porównanie z Rundą 2 (ręczny opis formatu)
- **Kompletność**: Poprawiona prośba z meta-promptu jest znacznie bardziej precyzyjna niż wersja pisana ręcznie w Rundzie 2. Zabezpiecza cykl życia aplikacji (wczytywanie przy starcie, czyszczenie pól, format czasu, kodowanie).
- **Zjawisko nadmiarowości (over-specification)**: Model zapytany o braki ma tendencję do dopisywania wymagań, których nikt pierwotnie nie planował (np. czyszczenie formularza, szczegółowa walidacja). Jeśli programista nie przejrzy tej listy krytycznym okiem, może niechcący zaakceptować ograniczenia lub funkcjonalności, które komplikują kod bez realnej potrzeby.
