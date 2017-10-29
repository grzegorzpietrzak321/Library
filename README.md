# Library

Program zrealizowany jako zadanie rekrutacyjne.

Temat programu: Domowa biblioteka - spis książek
-
Wymagania funkcjonalne:
- dodanie nowej książki;
- edycja książki;
- usunięcie książki z biblioteki;
- wyświetlenie wszystkich książek;
- wyświetlenie szczegółów książki;



Aplikacja stworzona została przy pomocy VS2017 jako strona internetowa ASP.NET MVC. Wykorzystuje wzorzec projektowy MVC. Przy pomocy EntityFramework aplikacja zapisuje dane do bazy MS SQL, w chwili obecnej skonfigurowane aby łączyło się z bazą w pliku mdf. Zmieniając connectionString można połączyć się z dowolną bazą (inny plik, hostowana na serwerze MS SQL). Do repozytorium dołączony jest plik bazy danych zawierający przykładowe dane. Aby uzyskać pełny dostęp konieczne jest zalogowanie się ("w@gmail.com", "Qwerty123."). Można również utworzyć nowego użytkownika, który również będzie miał dostęp do danych. Dane można przeglądać będąc niezalogowanym, po zalogowaniu można tworzyć nowe elementy, edytować, usuwać itd.

Sama baza danych jest w postaci jednej tabeli gdyż nie ma konieczności jej rozbudowywania do tak małego projektu. W przypadku większego projektu (większa ilość rekordów, każdy użytkownik ma swoją bazę danych) należałoby rozbić wszystkie dane na różne tabele, czyli np.: książki (id, tytuł, isbn, ocena) w jednej, autorzy w kolejnej (imie, nazwisko, itd), recenzje w następnej. Pomiędzy encjami książki i autorzy zachodziłaby relacja typu wiele do wielu, więc konieczna by była tabela pomocnicza (AuthorHasBook). Książki i recenzje jako jeden do wielu.

Dodatkowo jest możliwość eksportu danych do pliku json oraz importu również z takowego.

Dodatkowo dołączona jest dokumentacja kodu w postaci strony html oraz pliku rtf (foldery html oraz rtf). Wygenerowana poprzez program Doxygen. 

Strona po sklonowaniu repozytorium powinna się bez problemu otworzyć w Visual Studio 2017 jak również hostując ją na serwerze IIS.

Pozdrawiam ;)
