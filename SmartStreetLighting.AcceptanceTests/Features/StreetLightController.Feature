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
    Feature: Street Light Management - Stormy Night

  Scenario: Light should turn on with strength 10 when lux exceeds the set point on a stormy night
    Given the light is off
    And the weather is storm
    And the time is 2:00 AM
    And it is summer
    And the lux exceeds the set point
    When the lux exceeds the upper boundary
    Then the street light should turn on
    And light strength should be 10
