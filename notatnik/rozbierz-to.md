# Rozbierz to - rozpoznanie błędów fragmentu

## 1. Zegar próbuje zmienić kontrolkę interfejsu z niewłaściwego wątku

- **Na czym polega:** `System.Timers.Timer.Elapsed` wykonuje kod w innym wątku niż wątki interfejsu Avalonia. Zapisanie komunikatu przez `StatusText.Text = "Zapisano"` z tego wątku może naruszyć regułę wątkowości interfejsu i spowodować `InvalidOperationException` albo inny błąd dostępu do kontrolki.
- **Kształt halucynacji:** coś prawdziwe, użyte źle.
- **Kiedy ujawni się błąd:** dopiero po uruchomieniu programu i upływie okresu timera, czyli po około 60 sekundach.
- **Jak sprawdziłbym:** uruchomiłbym aplikację, poczekałbym na zdarzenie `Elapsed` i obserwował log konsoli oraz stan okna.
- **Mój notatnik:** nie ma tego błędu. W [MainWindow.axaml.cs](./MainWindow.axaml.cs) nie używam `Timer` ani `Elapsed`; samo wczytywanie i zapis są obsługiwane w kodzie obsługi przycisku. Ustaliłem to przez odczytanie całego pliku i uruchomienie aplikacji bez awarii UI.

## 2. Zapis `WriteAllText` kasuje dotychczasowy plik

- **Na czym polega:** `File.WriteAllText(path, NoteBox.Text)` zastępuje całą dotychczasową zawartość wskazanego pliku. Przy zapisie kolejnej notatki poprzednie dane nie są wczytywane ani scalane, więc po kilku zapisach w pliku zostaje tylko ostatnia notatka.
- **Kształt halucynacji:** coś prawdziwego, użyte źle.
- **Kiedy ujawni się błąd:** po drugim lub kolejnym zapisaniu notatki, zamiast po budowie programu.
- **Jak sprawdziłbym:** przygotowałbym plik z dwoma znanymi wpisami, zapisał trzeci wpis i następnie sprawdził pełną zawartość pliku po każdej operacji.
- **Mój notatnik:** nie ma tego błędu. W [MainWindow.axaml.cs](./MainWindow.axaml.cs) najpierw odczytuje istniejącą zawartość `notatki.md`, buduje nowy tekst z nowym wpisem na górze i dopiero zapisuje całość. Pięć wcześniejszych wpisów oraz wpis testowy z 24 listopada 2026 pozostają widoczne; ustaliłem to przez zapis testowy wykonany w działającym UI i odczytanie początku pliku.

## 3. Komentarz twierdzi, że obsłużono błędy, choć tak naprawdę ich nie ma

- **Na czym polega:** komentarz `Obsługa błędów zapisu jest w środku` opisuje odpowiedzialną funkcję, której ten fragment nie zawiera. Wywołanie `ShowAsync` i `File.WriteAllText` nie są objęte sprawdzaniem wyjątków ani kontrolą anulowania okna.
- **Kształt halucynacji:** zdanie o kodzie, które nie jest prawdą.
- **Kiedy ujawni się błąd:** nie widać go przy budowaniu ani natychmiast po uruchomieniu. Wykrywa się dopiero przez przeczytanie kodu, np. przed zapisem do pliku bez uprawnień lub po anulowaniu dialogu.
- **Jak sprawdziłbym:** odnalazłbym pełny blok `try/catch` i przetestowałbym zapis z brakiem uprawnień oraz anulowanie wyboru pliku.
- **Mój notatnik:** nie ma tego błędu. Kod [MainWindow.axaml.cs](./MainWindow.axaml.cs) rzeczywiście obejmuje `File.WriteAllText` blokiem `try/catch (Exception ex)`, a komunikat o błędzie trafia do `StatusText`. Nie opisano tego wprowadzająco fałszywie: ustaliłem zgodność przez odczytanie kodu i sprawdzenie komunikatu `Zapisano` po zapisaniu testowego wpisu.
