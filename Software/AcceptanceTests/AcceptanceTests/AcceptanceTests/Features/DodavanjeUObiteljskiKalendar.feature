Feature: DodavanjeUObiteljskiKalendar

Kao prijavljeni korisnik
Želim moći unositi nove aktivnosti u obiteljski kalendar
Kako bi ih mogao kasnije pregledati

Background: 
	Given otvorim aplikaciju
	And prijavljen sam u sustav
	And kliknem na Obiteljski kalendar gumb

Scenario Outline: Dodavanje aktivnosti
	When kliknem na gumb Dodaj novu aktivnost
	And upisem naziv <naziv> 
	And upisem datum pocetka <datumPocetka> s vremenom <vrijemePocetka>
	And upisem datum kraja <datumKraja> s vremenom <vrijemeKraja>
	And odaberem clana <clan>
	And kliknem na gumb Dodaj aktivnost
	Then Dobivamo poruku <poruka>

Examples:
| naziv              | datumPocetka | vrijemePocetka | datumKraja | vrijemeKraja | clan      | poruka |
| Testna aktivnost   | 10.4.2023.   | 10:10:10       | 11.4.2023. | 10:10:10     | kvardijan |    uspjeh    |
| Testna aktivnost 2 | 10.4.2023.   | aa:aa:aa       | 11.4.2023. | aa:aa:aa     | kvardijan |    greska    |
| Testna aktivnost 3 | 10.4.2023.   | 10:10:10       | 9.4.2023. | 10:10:10     | kvardijan |    greska    |
| Testna aktivnost 4 | 10.4.2023.   | 25:25:25       | 11.4.2023. | 25:25:25     | kvardijan |    greska    |


Scenario: Pridruživanje aktivnosti u kojoj korisnik već postoji
	When kliknem na datum 11. travnja 2023.
	And kliknem na AktivnostNisam
	And kliknem na gumb Pridruži se aktivnosti
	Then dobivam poruku kako nisam pridruzen aktivnosti