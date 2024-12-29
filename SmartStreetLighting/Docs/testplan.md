 # Testplan - SmartStreetLighting
 1. ## Overzicht
 2. ## Testcases
	 1. ### Lichten overdag, zomer
		- Beschrijving: Test hoe het systeem omgaat met verschillende weersomstandigheden overdag in de zomer
		- Testcases:
			a. Regen
			- Verwachte uitvoer: Systeem zet de lichten aan met sterkte 4
			a. Regen
		     - Verwachte uitvoer: Systeem zet de lichten aan met sterkte 4
	 1. ### Foutafhandeling en herhaalde pogingen
		- Beschrijving: Test hoe het systeem omgaat met API-fouten en herhaalde pogingen.
		- Testcases:
		  a. API faalt maximaal aantal keren
		     - Scenario: API geeft 3 keer een fout
		     - Verwachte uitvoer: Systeem gooit een Exception

		  b. API slaagt na herhaalde pogingen
		     - Scenario: API faalt 2 keer en slaagt bij de 3e poging
		     - Verwachte uitvoer: Systeem retourneert succesvol TrafficData 

