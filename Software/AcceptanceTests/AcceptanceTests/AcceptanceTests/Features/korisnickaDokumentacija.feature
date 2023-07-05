Feature: korisnickaDokumentacija

Kao korisnik 
ja želim moći vidjeti korisničku dokumentaciju
kako bi znao kako koristiti program

Scenario: Prikaz korisnicke dokumentacije
	When ja pokrenem program
	And ja kliknem F1
	Then ja vidim korisnicku dokumentaciju
