# Wynik Powtórki Rundy 1: Ta sama prośba uruchomiona drugi raz (Niedeterminizm)

## Prośba (Identyczna jak w Rundzie 1)
> Zrób notatnik: pole na tytuł, pole na treść, przycisk zapisu i lista zapisanych notatek. Notatki mają być zapisywane do pliku.

## Wygenerowany plik na dysku: `notes.txt` (zamiast `notes.json`)

W powtórce model zinterpretował zapis „do pliku” w zupełnie inny sposób niż za pierwszym razem: zamiast jednego pliku JSON ze strukturą obiektową, utworzył prosty plik tekstowy `notes.txt`, dopisując każdą nową notatkę na końcu:

```text
=== Zakupy ===
Mleko, chleb, jajka
----------------------------------------
=== Projekt 3.2 ===
Notatnik i cztery rundy próśb
----------------------------------------
```

## Co się różni w stosunku do pierwszego uruchomienia Rundy 1?
1. **Format pliku**: Za pierwszym razem powstał plik `notes.json` o strukturze tablicy obiektów JSON (`[{"Title": "...", "Content": "..."}]`). W powtórce powstał plik `notes.txt` z ręcznie formatowanymi separatorami (`===`, `---`).
2. **Kolejność i sposób zapisu**: W powtórce zastosowano `File.AppendAllText`, więc kolejne wpisy lądują na dole, podczas gdy za pierwszym razem serializowano całą listę w pamięci.
3. **Kodowanie i znaki diakrytyczne**: W pliku tekstowym znaki zostały zapisane domyślnym kodowaniem systemowym (w .NET domyślnie UTF-8 bez BOM w `File.AppendAllText`), dzięki czemu `ó` nie zamieniło się w sekwencję ucieczki `\u00F3`, jak miało to miejsce w JSON.
4. **Parsowanie i odczyt**: Plik tekstowy z luźnymi separatorami okazał się znacznie trudniejszy do ponownego sparsowania do listy w UI niż wcześniejszy JSON.

## Wniosek
Ani jedna litera w promptcie się nie zmieniła, a plik wynikowy ma inne rozszerzenie, inną strukturę, inną metodę zapisu i inną podatność na błędy odczytu.
