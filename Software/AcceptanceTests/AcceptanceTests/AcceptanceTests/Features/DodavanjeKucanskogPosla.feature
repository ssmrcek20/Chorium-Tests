Feature: DodavanjeKucanskogPosla

Kao prijavljeni korisnik tipa roditelj
Želim moći unositi nove poslove u sustav
Kako bi ih drugi mogli vidjeti i rješiti

Background:
	Given otvorim aplikaciju
	And prijavljen sam u sustav
	And kliknem na Kućanski poslovi

Scenario Outline: Dodavanje novog posla
	When kliknem na Dodaj posao
	And unesem naziv <naziv>
	And odaberem kategoriju <kategorija>
	And upisem datum roka <datumRoka> s vremenom <vrijemeRoka>
	And odaberem zaduzenog <zaduzeni>
	And kliknem na gumb Dodaj
	Then posao je zavrsen <uspjeh>

Examples:
	| naziv           | kategorija    | datumRoka  | vrijemeRoka | zaduzeni  | uspjeh |
	| Testni posao    | Pospremanje   | 10.4.2023. | 10:10:10    | kvardijan | da     |
	| Testni posao 2  | Rad u kuhinji | 10.4.2023. | 10:10:10    | dijete    | dob    |
	| Testni posao  3 | Rad u kuhinji | 10.4.2023. | 10:10:10    |           | ne     |
	| Testni posao  4 | Pospremanje   | 10.4.2023. | aa:aa:aa    | kvardijan | ne     |
	|                 | Pospremanje   |            | aa:aa:aa    |           | ne     |