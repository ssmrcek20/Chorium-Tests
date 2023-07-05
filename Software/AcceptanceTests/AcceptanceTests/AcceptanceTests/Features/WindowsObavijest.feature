Feature: WindowsObavijest

Kao korisnik 
ja želim postaviti obavijesti na neke poslove
kako bi znao kad ih trebam obaviti

Scenario: Postavljanje obavijesti za 1 minutu
	Given ja sam prijavljen
	When ja postavim vrijeme za 1 minutu
	And ja odaberem posao
	And ja pritisnem Postavi Obavijest
	Then ja vidim poruku da je obavijest postavljena

Scenario: Postavljanje obavijesti bez posla
	Given ja sam prijavljen
	When ja postavim vrijeme za 1 minutu
	And ja pritisnem Postavi Obavijest
	Then ja vidim poruku da trebam odabrati vrijeme i posao

Scenario: Postavljanje obavijesti bez vremena
	Given ja sam prijavljen
	When ja odaberem posao
	And ja pritisnem Postavi Obavijest
	Then ja vidim poruku da trebam odabrati vrijeme i posao

