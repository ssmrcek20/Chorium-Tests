Feature: Registracija

Kao korisnik 
ja se želim moći registrirati
kako bi mogao korisiti sve funkcionalnosti programa

Scenario: Prikaz prozora za registraciju
	When ja pokrenem program
	And ja kliknem na gumb Registracija
	Then ja vidim prozor za Registraciju

Scenario Outline: Uneseni validni podaci
	Given ja sam na prozoru za Registraciju
	When ja unesem validne podatke
	And ja odaberem Status <status>
	And ja pritisnem gumb Registriraj
	Then ja vidim poruku da sam se uspjesno registrirao
	And ja sam prebacen na prozor Prijava

Examples: 
	| status   |
	| Dijete   |
	| Roditelj |

Scenario: Podaci nisu uneseni
	Given ja sam na prozoru za Registraciju
	When ja pritisnem gumb Registriraj
	Then ja vidim poruku da trebam unjeti sve podatke

Scenario: Uneseno korisnicko ime koje vec postoji
	Given ja sam na prozoru za Registraciju
	When ja unesem validne podatke osim korisničkog imena
	And ja odaberem Status Roditelj
	And ja pritisnem gumb Registriraj
	Then ja vidim poruku da se to korisnicko ime vec koristi

Scenario: Unesen ne validan E-mail
	Given ja sam na prozoru za Registraciju
	When ja unesem validne podatke osim E-maila
	And ja odaberem Status Roditelj
	And ja pritisnem gumb Registriraj
	Then ja vidim poruku da E-mail adresa nije ispravna

Scenario: Unesen nemoguci datum rodenja
	Given ja sam na prozoru za Registraciju
	When ja unesem validne podatke osim datuma rodenja
	And ja odaberem Status Roditelj
	And ja pritisnem gumb Registriraj
	Then ja vidim poruku da datum rodenja nije ispravan


