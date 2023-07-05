Feature: RjesavanjeKucanskihPoslova

Kao prijavljeni korisnik
Želim moći rješiti neki kućanski posao
Kako bi on bio rješen

Background: 
	Given otvorim aplikaciju
	And prijavljen sam u sustav
	And kliknem na Kućanski poslovi

Scenario: Rješavanje posla
	Given postoji nerješeni posao
	When kliknem na posao Ovo bude rjeseno uskoro
	And kliknem na gumb Riješi posao
	Then Posao je riješen

Scenario: Rješavanje neodabranog posla
	When kliknem na gumb Riješi posao
	Then dobivam poruku greške
