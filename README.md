# Zadanie 3.2: Notatnik i cztery rundy próśb

Projekt demonstracyjny w technologii **Avalonia UI (.NET 10)** badający wpływ konstrukcji promptu na determinizm, architekturę i jakość generowanego kodu oraz plików danych.

---

## 1. Cztery rundy próśb – porównanie plików wynikowych

Wszystkie pliki wygenerowane w poszczególnych rundach znajdują się w katalogu [`rundy/`](file:///rundy/):
- [`rundy/runda-1.md`](file:///rundy/runda-1.md) – **Zero-shot (sama prośba bez formatu)**: Model sam zadecydował o nazwie i formacie pliku (`notes.json`). Polskie znaki zostały zserializowane do kodów ucieczki `\u00F3`.
- [`rundy/runda-2.md`](file:///rundy/runda-2.md) – **Zero-shot z opisem formatu**: Wymuszenie zapisu do `notatki.md` i kodowania UTF-8. Format zdefiniowany słownie wyeliminował błędy kodowania i wymusił odwróconą chronologię (najnowsza notatka na górze).
- [`rundy/runda-3.md`](file:///rundy/runda-3.md) – **One-shot (jeden przykład)**: Dołączenie jednego wzorcowego wpisu usunęło niejednoznaczności dotyczące odstępów i separatorów (` — ` vs `-`), jednak nie zdefiniowało zachowania dla notatek jednolinijkowych.
- [`rundy/runda-4.md`](file:///rundy/runda-4.md) – **Few-shot (trzy zróżnicowane przykłady)**: Wpisy krótki, wielolinijkowy oraz ze znakami diakrytycznymi i myślnikiem w tytule. Format stał się w 100% jednoznaczny. Przetestowano zachowanie po 5 kolejnych zapisach – żaden wpis nie został utracony.
- [`rundy/runda-5.md`](file:///rundy/runda-5.md) – **Meta-prompt (prośba poprawiona przez model)**: Model zidentyfikował 6 kluczowych braków w zapytaniu z Rundy 1 i stworzył kompletną specyfikację techniczną.

---

## 2. Analiza powtórki: `runda-1-powtorka.md`

- **Plik**: [`rundy/runda-1-powtorka.md`](file:///rundy/runda-1-powtorka.md)
- **Co się różni**:
  Mimo wysłania identycznej prośby („*Zrób notatnik: pole na tytuł, pole na treść, przycisk zapisu i lista zapisanych notatek. Notatki mają być zapisywane do pliku.*”), model w drugim podejściu stworzył plik tekstowy `notes.txt` z ręcznymi separatorami `===` oraz dopisywaniem na końcu pliku (`AppendAllText`), zamiast pliku `notes.json` z serializacją obiektową w pamięci.
- **Wpływ niedeterminizmu na różnice między rundami 1–4**:
  Co najmniej połowa różnicy w Rundzie 1 wynikała z czystego przypadku i losowości wyboru formatu przez model (JSON vs TXT vs CSV), a dopiero dodanie formatu i przykładów w Rundach 2–4 zredukowało przestrzeń niedeterminizmu praktycznie do zera.

---

## 3. Zalecenie dotyczące promptów od twórców modelu (Google Gemini)

- **Źródło**: [Google Gemini API Prompting Strategies](https://ai.google.dev/gemini-api/docs/prompting-strategies)
- **Data odczytania**: 17 listopada 2026 r.
- **Zalecenie**: *Używanie jawnych delimitatorów i struktury XML/Markdown (np. `<input>`, `<context>`, `### Przykład`) do separacji instrukcji systemowych od danych wejściowych użytkownika.*
- **Wpływ na notatnik**:
  Zastosowanie wyraźnych separatorów uchroniło parser notatnika przed sytuacją, w której treść wpisana przez użytkownika (zawierająca np. słowa kluczowe `## ` lub polecenia) mogłaby zostać zinterpretowana przez model lub reguły parsera jako metadane nagłówka.

---

## 4. Sprawdź się (Pytania kontrolne z lekcji)

1. **Trzy rzeczy, które siedzą w jednej prośbie do modelu:**
   - **Zadanie** – co ma zostać zrobione (np. „zapisuj notatki do pliku”).
   - **Format** – w jakiej strukturze i postaci ma powstać wynik (np. układ nagłówków, nazwa pliku, kodowanie).
   - **Ograniczenia** – czego nie wolno robić / warunki brzegowe (np. bez zewnętrznych bibliotek, bez utraty poprzednich wpisów).

2. **Czym różni się zero-shot od few-shot:**
   - **Zero-shot** to polecenie bez żadnego konkretnego przykładu wejścia/wyjścia (model bazuje jedynie na swoim ogólnym treningu i opisie słownym).
   - **Few-shot** to technika, w której prompt zawiera kilka (zwykle 2–5) reprezentatywnych, różniących się przykładów pożądanego rezultatu, pokazujących stałe i zmienne reguły formatowania.

3. **Jedna sytuacja, w której proszenie o rozumowanie krok po kroku (Chain-of-Thought) jest złym pomysłem:**
   - Gdy korzystamy z nowoczesnych modeli myślących (*reasoning / thinking models*), które same z siebie wykonują ukryty proces wnioskowania przed wygenerowaniem odpowiedzi; narzucanie im sztucznej, sztywnej listy kroków ogranicza elastyczność ich wewnętrznego rozumowania i często prowadzi do gorszych rezultatów niż pozostawienie modelowi swobody myślenia.

---

## 5. Karta pracy: Własną ręką (Obsługa błędu zapisu)

- **Test z atrybutem „tylko do odczytu” przed zmianą:**
  Aplikacja rzucała nieobsłużony wyjątek `System.UnauthorizedAccessException: Access to the path ... is denied.` i natychmiast ulegała awarii (okno znikało z ekranu bez ostrzeżenia, a wpisana notatka bezpowrotnie przepadała).
- **Po dodaniu bloku `try ... catch (Exception ex)` oraz kontrolki `StatusText`:**
  Aplikacja nie zamyka się. Pod przyciskiem pojawia się czytelny komunikat: `Nie udało się zapisać: Access to the path '...' is denied.`. Po zdjęciu atrybutu ponowny zapis kończy się komunikatem `Zapisano`.
- **Czas wykonania bloku:** ok. 12 minut.
- **Co było najtrudniejsze:** Poprawne zsynchronizowanie stanu interfejsu (żeby pole formularza nie czyściło się w przypadku wystąpienia błędu w bloku `catch`, co pozwoli użytkownikowi ponowić próbę bez konieczności ponownego wpisywania tekstu).
- **Uwaga dotycząca biblioteki Avalonia UI:**
  W Avalonia 11/12 klasyczne okna dialogowe `OpenFileDialog` i `SaveFileDialog` zostały usunięte z API okna i zastąpione przez `StorageProvider.OpenFilePickerAsync` / `StorageProvider.SaveFilePickerAsync`. Dodatkowo właściwość `TextBox.Watermark` została oznaczona jako przestarzała na rzecz `PlaceholderText`.
