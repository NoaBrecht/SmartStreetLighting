Feature: Street light decisions
  As a traffic system operator
  I want the traffic light to show the correct color
  Depending on traffic and emergencies

  Scenario: Traffic light turns red when an accident is detected
    Given a traffic accident is detected
    When the system decides
    Then the traffic light should turn red

  Scenario: Traffic light turns green when an emergency vehicle is detected
    Given an emergency vehicle is detected
    When the system decides
    Then the traffic light should turn green

  Scenario: Traffic light turns green for a special event
    Given a special event is detected
    When the system decides
    Then the traffic light should turn green

  Scenario: Traffic light turns green (extended) when high traffic is detected
    Given high traffic is detected
    When the system decides
    Then the traffic light should turn green extended

  Scenario: Traffic light stays idle when traffic density is within the setpoint range
    Given traffic density is within the setpoint range
    When the system decides
    Then the traffic light should stay idle

  Scenario: Traffic light turns green (reduced) when low traffic is detected
    Given low traffic is detected
    When the system decides
    Then the traffic light should turn green reduced