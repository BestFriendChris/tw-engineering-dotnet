Feature: Admin
	In order to enable new customers to join
	As an Admin
	I want to add a new customer

@web
Scenario: Navigate to admin page
	Given I am logged in as "admin" with "pw"
	When I navigate to the admin page
	Then the system shows me the admin page

@web
Scenario: Add new customer
	Given I am logged in as "admin" with "pw"
	When I navigate to the admin page
	And I click on the add customer link
	Then the system shows me the add customer page
