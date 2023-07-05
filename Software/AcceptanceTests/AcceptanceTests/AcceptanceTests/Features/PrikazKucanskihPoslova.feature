Feature: PrikazKucanskihPoslova

Kao korisnik 
Kada se prijavim u sustav
Želim moći vidjeti svoje kućanske poslove

Background: 
	Given aplikacija je upaljena

Scenario: Prikaz poslova
	Given nalazim se na prikazu poslova
	Then prikaz poslova za sve clanove kucanstva je prikazan

	Scenario: Prikaz poslova nakon prijave
	Given Korisnik je prijavljen
	And nalazim se na prikazu poslova
	Then osobni prikaz poslova je prikazan