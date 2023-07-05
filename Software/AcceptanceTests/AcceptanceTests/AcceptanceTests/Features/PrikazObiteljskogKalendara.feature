Feature: PrikazObiteljskogKalendara

Kao prijavljeni korisnik
Želim moći pregledati obiteljski kalendar
Kako bi mogao vidjeti zabilježene aktivnosti

Background: 
	Given otvorim aplikaciju
	And prijavljen sam u sustav

Scenario: Pocetni prikaz kalendara
	When kliknem na Obiteljski kalendar gumb
	Then vidim prikaz obiteljskog kalendara

Scenario: Prikaz aktivnosti za ručno odabrani datum
	When kliknem na Obiteljski kalendar gumb
	And kliknem na datum 14. travnja 2023.
	Then vidim aktivnost AktivnostiSutra

Scenario: Pregled sudionika aktivnosti
	When kliknem na Obiteljski kalendar gumb
	And kliknem na datum 14. travnja 2023.
	And kliknem na aktivnost AktivnostiSutra
	Then vidim sudionika kvardijan