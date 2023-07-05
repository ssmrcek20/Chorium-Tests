Feature: Login

Kao korisnik 
Želim se moći prijaviti u sustav
Kako bi mogao koristiti sve funkcionalnosti sustava

Background: 
Given aplikacija je upaljena
And nalazim se na prijavi

Scenario: Login sa tocnim podacima
	When unesem tocno korisnicko ime stan i lozinku stan
	When pritisnem Login gumb
	Then trebao bi vidjeti popis kucanskih poslova

Scenario Outline: Login sa netocnim podacima
	When unesem korisnicko ime <korime> i lozinku <lozinka>
	When pritisnem Login gumb
	Then trebao bi dobiti poruku <greska>

	Examples: 
	| korime    | lozinka   | greska                                |
	| stan     | korisnik | Neispravno korisničko ime ili lozinka |
	| korisnik | stan     | Neispravno korisničko ime ili lozinka |
	| korisnik | korisnik | Neispravno korisničko ime ili lozinka |
	|           |           | Unesite korisničko ime i lozinku      |

Scenario: Login pomocu prepoznavanja lica dok lica nema
	When pritisnem FaceLogin gumb
	Then trebao bi dobiti poruku Nije pronađeno lice

