# Structuur van SmartStreetlighting

## Overzicht
Het doel van de applicatie is een slim straatverlichtingssysteem te maken in C#. Er wordt gekeken naar de huidge lichtsterkte (lux), het weer en de tijd op de dag en zelfs de maand.

## Keuzes voor de structuur

- **Mappenstructuur**:
    - `Models/`: Bevat de interfaces die het programma nodig heeft.
    - `Services/`: Bevat de beslissingsmodule en alle gerelateerde klassen.
    - `Docs/`: Bevat de documentatie van het project.
  
- **Klassen**:
    - De `StreetLightController` klasse is de kern van de applicatie en bevat de logica voor het nemen van beslissingen. Hier wordt gekeken naar alle sensoren en andere informatie bronnen en wordt een beslissing genomen aan de hand van de data. Ook zit er hier een failsafe in, indien een externe API niet werkt.
    - De `WeatherSensor` klasse haalt de data op uit de externe weer API.
    - De `Lightsensor` klasse haalt de Lux value op uit de externe LuxApi
    - De `CurrentTime` klasse geeft de huidige tijd weer alsook de maand en of het winter of zomer is.