Feature: Mjesecni_izvjestaj

Kao korisnik 
ja želim generirati mjesečne izvještaje
kako bi znao koliko poslova mjesečno svaki član obavi

Scenario: Prikaz prozora za Mjesecne izvjestaje
	When ja pokrenem program
	And ja kliknem na gumb Mjesecni izvjestaj
	Then ja vidim prozor za Mjesecni izvjestaj

Scenario: Generiranje za ovaj mjesec
	Given ja sam na prozoru za Mjesecne izvjestaje
	When ja unesem trenutni mjesec
	And ja pritisnem gumb Generiraj izvjestaj
	Then Izvjestaj se generira


Scenario: Generiranje za prosli mjesec
	Given ja sam na prozoru za Mjesecne izvjestaje
	When ja unesem prosli mjesec
	And ja pritisnem gumb Generiraj izvjestaj
	Then Izvjestaj se generira
	
Scenario: Generiranje za naknadni mjesec
	Given ja sam na prozoru za Mjesecne izvjestaje
	When ja unesem naknadni mjesec
	And ja pritisnem gumb Generiraj izvjestaj
	Then ja vidim poruku da se izvještaj ne može generirati za taj mjesec
