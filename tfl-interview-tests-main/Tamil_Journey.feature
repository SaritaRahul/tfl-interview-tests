Feature: JourneyPlanner

Scenario: TC4: Plan a journey
	Given user is on the TfL home page
	When user plans a journey from London Victoria to London Bridge
	Then user should be presented with the Journey Results page with the correct summary
	And user can see the fastest route

Scenario: TC3: Edit a journey
	Given user plans a journey from London Victoria to London Bridge
	When user changes the destination to London Waterloo
	Then user should be presented with the Journey Results page with the correct summary
	And user can see the fastest route

Scenario: TC2: Invalid field entry
	Given user is on the TfL home page
	When user enters text that does not match a station name into the journey planner
	And user clicks Plan my journey
	Then user should be presented with the Journey Results page with an error message

Scenario: TC1: No destination entered
	Given user is on the TfL home page
	When user tries to plan a journey without a destination
	Then user sees an error message telling them that the To field is required


