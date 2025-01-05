Feature: Street light decisions
  As a streetlight controller
  I want the street lights to turn on when needed
  Depending on weather and light strength

  Scenario: Street light turns on when lux exceeds lower boundary
    Given the light is off
    When the lux exceeds the lower boundary
    Then the street light should turn on

  Scenario: Street light turns off when lux exceeds upper boundary
    Given the light is on
    When the lux exceeds the upper boundary
    Then the street light should turn off