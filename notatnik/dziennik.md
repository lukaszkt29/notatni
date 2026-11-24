# Dziennik weryfikacji

## 2026-11-24 - 3.4 Rozbierz to

- Sprawdzone: `dotnet build --nologo` → 0 błędów kompilacji, 2 ostrzeżenia `NU1900` dotyczące niedostępnego indeksu podatności NuGet.
- Sprawdzone: uruchomienie `bin/Debug/net10.0/notatnik.exe` → okno `Notatnik` pojawiło się i proces był responsywny.
- Sprawdzone: zapis dokładnie tak, jak mówi opis notatnika → przez UI Automation wpisano tytuł `Weryfikacja 24 listopada` i treść `Zapis żółty — test 3.4`; po kliknięciu `Zapisz notatkę` pojawił się komunikat `Zapisano`, a pierwszy wpis w `notatki.md` ma nagłówek `## 2026-11-24 15:06 — Weryfikacja 24 listopada`, zawiera polskie znaki i poprzednie notatki nadal są obecne.
- Nie wiadomo: jak notatnik zachowa się przy jednoczesnym otwarciu tego samego pliku `notatki.md` przez inny program i zmianie lub blokadzie pliku w trakcie zapisów.

- Nazwa do sprawdzenia w dokumentacji: `TextBox.PlaceholderText` — [dokumentacja właściwości API Avalonia](https://api-docs.avaloniaui.net/docs/P_Avalonia_Controls_TextBox_PlaceholderText.html), odczytana 24 listopada 2026 r. Właściwość ustawia tekst podpowiedzi wyświetlany, gdy `TextBox` jest pusty i nie ma ustawionego kursora/fokusu. Użycie wymagało zastąpienia starszego `Watermark`.

## 2026-11-17 - Budowa 2

- Sprawdzone: przyjęto bez sprawdzenia możliwość, że parser notatek poprawnie obsługuje wszystkie dotychczasowe wpisy i że zapis nie usuwa wcześniejszych danych.
- Sprawdzone: przyjęto bez sprawdzenia działanie aplikacji po uruchomieniu poza samym istnieniem pliku wynikowego i bez ręcznego testu zapisania kolejnej notatki.
- Nie wiadomo: które zachowania interfejsu i zapisów działają po rzeczywistym uruchomieniu, a które są tylko zakładane na podstawie poprawnie wygenerowanego kodu.
