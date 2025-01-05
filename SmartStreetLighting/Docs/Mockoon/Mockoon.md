**data/1.0/WeatherData**

`
{
    "coord": {
        "lon": 4.4035,
        "lat": 51.2199
    },
    "weather": [
        {
            "id": 803,
            "main": "{{queryParam 'weather' 'cloudy'}}"
            "description": "broken clouds",
            "icon": "04d"
        }
    ],
    "base": "stations",
    "main": {
        "temp": 5.81,
        "feels_like": 2.53,
        "temp_min": 5.5,
        "temp_max": 6.08,
        "pressure": 1029,
        "humidity": 96,
        "sea_level": 1029,
        "grnd_level": 1028
    },
    "visibility": 7000,
    "wind": {
        "speed": 4.63,
        "deg": 210
    },
    "clouds": {
        "all": 75
    },
    "dt": 1735558269,
    "sys": {
        "type": 2,
        "id": 2006860,
        "country": "BE",
        "sunrise": 1735544801,
        "sunset": 1735573386
    },
    "timezone": 3600,
    "id": 2803138,
    "name": "Antwerp",
    "cod": 200
}
`

**data/1.0/LightData**
`
{
  "lux": "{{queryParam 'luxStrenght' '10'}}"
}
`