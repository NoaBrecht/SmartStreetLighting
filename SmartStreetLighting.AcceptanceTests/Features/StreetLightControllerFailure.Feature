Feature: Street Light Controller - Handling Lux Sensor Failures

  As a street light controller system,
  I want to manage the state of street lights based on lux sensor failures,
  So that the system can safely handle failures and ensure the correct behavior of the lights.


  Scenario: Lux sensor failure with max failures reaches safe mode
    Given streetLightController in safe mode
    When getting the Lux fails
    And number of failures is maximum failures
    Then turn light on
    And set streetLightController in safe mode

  Scenario: Weather sensor failure with max failures reaches safe mode
    Given streetLightController in safe mode
    When getting the Weather fails
    And number of failures is maximum failures
    Then turn light on
    And set streetLightController in safe mode

  Scenario: Safe mode is reset when successful lux data is received after max lux failures
    Given streetLightController in safe mode
    When the system receives successful lux data after max lux failures
    And set streetLightController in normal mode