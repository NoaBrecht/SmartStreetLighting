# Testplan - SmartStreetLighting

## 1. Overzicht
Dit testplan beschrijft de testcases en testscenario's voor de ``StreetLightController` klasse van de SmartStreetLighting applicatie. Het doel van de testen is om te verifiëren of de straatverlichting correct wordt beheerd op basis van sensorgegevens, weersomstandigheden, seizoen en tijd van de dag, en of de foutafhandelingsmechanismen correct werken.

De testgevallen omvatten verschillende scenario's voor de werking van de lichten in zowel zomer als winter, evenals foutafhandelingsscenario's zoals het bereiken van de maximale aantal mislukte pogingen.

## 2. Testcases

### 2.1. Lichten overdag, zomer
- **Beschrijving:** Test hoe het systeem omgaat met verschillende weersomstandigheden overdag in de zomer
- **Testcases:**
  1. **TestLightDisableWhenLuxAboveSetPointNotWinterSunnyDay**
     - **Scenario:** Het licht moet worden uitgeschakeld wanneer de lux-waarde gelijk is aan de setpoint en het zonnig is in de zomer.
     - **Verwachte uitvoer:** Het licht wordt uitgeschakeld.
  2. **TestLightDisableWhenLuxAboveSetPointSummerFogDay**
     - **Scenario:** Het licht moet worden uitgeschakeld wanneer de lux-waarde gelijk is aan de setpoint en het mistig is in de zomer.
     - **Verwachte uitvoer:** Het licht wordt uitgeschakeld.
  3. **TestLightEnableWhenLuxAboveSetPointSummerSnowDay**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 5 wanneer de lux-waarde gelijk is aan de setpoint en het sneeuwt in de zomer.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 5.
  4. **TestLightEnableWhenLuxAboveSetPointSummerStormDay**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 6 wanneer de lux-waarde gelijk is aan de setpoint en er een storm is in de zomer.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 6.
  5. **TestLightEnableWhenLuxAboveSetPointSummerCloudyDay**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 3 wanneer de lux-waarde gelijk is aan de setpoint en het bewolkt is in de zomer.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 3.

### 2.2. Lichten in de winter
- **Beschrijving:** Test hoe het systeem omgaat met verschillende weersomstandigheden in de winter
- **Testcases:**
  1. **TestLightDisableWhenLuxAboveSetPointInWinter**
     - **Scenario:** Het licht moet worden uitgeschakeld wanneer de lux-waarde gelijk is aan de setpoint en het winter is.
     - **Verwachte uitvoer:** Het licht wordt uitgeschakeld.
  2. **TestLightEnabledWithCorrectStrengthInWinterWithSnowNightTime**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 8 wanneer de lux-waarde gelijk is aan de setpoint, het sneeuwt, en het 's nachts is in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 8.
  3. **TestLightEnableWhenLuxAboveSetPointWinterFogDay**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 6 wanneer de lux-waarde gelijk is aan de setpoint en het mistig is in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 6.
  4. **TestLightEnableWhenLuxAboveSetPointWinterSnowDay**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 5 wanneer de lux-waarde gelijk is aan de setpoint en het sneeuwt in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 5.
  5. **TestLightEnableWhenLuxAboveSetPointWinterCloudyDay**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 3 wanneer de lux-waarde gelijk is aan de setpoint en het bewolkt is in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 3.

### 2.3. Foutafhandeling en herhaalde pogingen
- **Beschrijving:** Test hoe het systeem omgaat met API-fouten en herhaalde pogingen.
- **Testcases:**
  1. **TestSafeModeActivatedAfterMaxFailuresDueToTemperatureErrorLightSensor**
     - **Scenario:** De *LightSensor* API faalt herhaaldelijk tot het maximale aantal mislukte pogingen is bereikt.
     - **Verwachte uitvoer:** Het systeem schakelt over naar veilige modus en zet de lichten aan.
  2. **TestSafeModeActivatedAfterMaxFailuresDueToTemperatureErrorWeatherSensor**
     - **Scenario:** De *WeatherSensor* API faalt herhaaldelijk tot het maximale aantal mislukte pogingen is bereikt.
     - **Verwachte uitvoer:** Het systeem schakelt over naar veilige modus en zet de lichten aan.
  3. **TestExitSafeModeAfterSuccessfulRecoveryFromSensorErrorLightSensor**
     - **Scenario:** De *LightSensor* API herstelt na herhaalde mislukte pogingen en het systeem komt uit de veilige modus.
     - **Verwachte uitvoer:** Het systeem komt uit de veilige modus en zet de lichten aan met de juiste sterkte.
  4. **TestExitSafeModeAfterSuccessfulRecoveryFromSensorErrorWeatherSensor**
     - **Scenario:** De *WeatherSensor* API herstelt na herhaalde mislukte pogingen en het systeem komt uit de veilige modus.
     - **Verwachte uitvoer:** Het systeem komt uit de veilige modus en zet de lichten aan met de juiste sterkte.

### 2.4. Lichten 's nachts, winter
- **Beschrijving:** Test hoe het systeem omgaat met verschillende weersomstandigheden 's nachts in de winter
- **Testcases:**
  1. **TestLightEnableWhenLuxAboveSetPointWinterSnowNight**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 8 wanneer de lux-waarde gelijk is aan de setpoint, het sneeuwt, en het 's nachts is in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 8.
  2. **TestLightEnableWhenLuxAboveSetPointWinterFogNight**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 9 wanneer de lux-waarde gelijk is aan de setpoint, het mistig is, en het 's nachts is in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 9.
  3. **TestLightEnableWhenLuxAboveSetPointWinterStormNight**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 10 wanneer de lux-waarde gelijk is aan de setpoint, er is een storm, en het 's nachts is in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 10.
  4. **TestLightEnableWhenLuxAboveSetPointWinterCloudyNight**
     - **Scenario:** Het licht moet worden ingeschakeld met sterkte 5 wanneer de lux-waarde gelijk is aan de setpoint, het bewolkt is, en het 's nachts is in de winter.
     - **Verwachte uitvoer:** Het licht wordt ingeschakeld met sterkte 5.
