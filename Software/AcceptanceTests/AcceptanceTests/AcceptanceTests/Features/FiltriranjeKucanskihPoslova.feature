Feature: FiltriranjeKucanskihPoslova

Kao korisnik 
Kada sam prijavljen u sustav
Želim moći filtrirati kućanske poslove po kriterijima kategorije posla,korisnika i dovršenosti posla

Background:
	Given aplikacija je upaljena
	And Korisnik je prijavljen
	And nalazim se na prikazu poslova
	And filtriranje je resetirano

Scenario: Resetiranje prikaza kucanskih poslova
	
	When pritisnem gumb resetiraj
	Then osobni prikaz poslova je prikazan

Scenario: Filtriranje prikaza kucanskih poslova po korisniku
	When odaberem opciju stanko
	When pritisnem gumb filtriraj
	Then prikaz poslova za korisnika stanko je prikazan

Scenario: Filtriranje prikaza kucanskih poslova po korisniku i stanju dovrsenosti
	When odaberem opciju kvardijan
	And odaberem opciju dovrsen
	When pritisnem gumb filtriraj
	Then prikaz dovrsenih poslova za korisnika kvardijan je prikazan

Scenario: Filtriranje prikaza kucanskih poslova po korisniku i kategoriji posla
	When odaberem opciju kvardijan
	And odaberem opciju Pospremanje
	When pritisnem gumb filtriraj
	Then prikaz poslova kategorije Pospremanje za korisnika kvardijan je prikazan

Scenario: Filtriranje prikaza kucanskih poslova po stanju dovršenosti
	When odaberem opciju na_cekanju
	When pritisnem gumb filtriraj
	Then prikaz poslova na cekanju je prikazan

Scenario: Filtriranje prikaza kucanskih poslova po stanju dovršenosti i kategoriji posla
	When odaberem opciju na_cekanju
	And odaberem opciju Rad u vrtu
	When pritisnem gumb filtriraj
	Then prikaz poslova kategorije Rad u vrtu na cekanju za korisnika kvardijan je prikazan

Scenario: Filtriranje prikaza kucanskih poslova po stanju kategoriji posla
	When odaberem opciju Rad u vrtu
	When pritisnem gumb filtriraj
	Then prikaz poslova kategorije Rad u vrtu je prikazan

Scenario: Filtriranje prikaza kucanskih poslova po svim filtrima
	When odaberem opciju kvardijan
	And odaberem opciju Pospremanje
	And odaberem opciju dovrsen
	When pritisnem gumb filtriraj
	Then prikaz dovrsenih poslova kategorije Rad u vrtu za korisnika kvardijan je prikazan
