Feature: UredivanjeBrisanjeKucanskihPoslova

Kao prijavljeni korisnik tipa Roditelj
Želim moći urediti ili obrisati oređeni posao
Kako bih mogao promjeniti željene informacije

Background: 
	Given otvorim aplikaciju
	And prijavljen sam u sustav
	And kliknem na Kućanski poslovi

Scenario: Uređivanje posla
	Given postoji nerješeni posao
	When kliknem na posao Ovo bude rjeseno uskoro
	And kliknem na gumb UrediBrisi
	And promjenim informacije
	And kliknem na gumb Uredi posao
	Then posao je izmjenjen

Scenario: Uređivanje neodabranog posla
	When kliknem na gumb UrediBrisi
	Then dobijem poruku greške

Scenario: Brisanje posla
	Given postoji nerješeni posao
	When kliknem na posao Ovo bude rjeseno uskoro
	And kliknem na gumb UrediBrisi
	And kliknem na gumb Obriši posao
	Then posao je obrisan