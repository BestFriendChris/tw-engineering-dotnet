Feature: Authorization
	In order that my details are private
	As a customer
	I want the system to require a login

@web
Scenario: Not logged in
	Given I am not logged in
	When I try to navigate to the home page
	Then the system shows me the login page

@web
Scenario: Logging in
	Given I am not logged in
	When I navigate to the login page
	And login as "Fred"
	Then the system shows me the home page

@web
Scenario: Logging off
	Given I am logged in as "Fred"
	When I logout
	Then the system shows me the login page
