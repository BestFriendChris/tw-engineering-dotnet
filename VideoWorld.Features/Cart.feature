﻿Feature: Shopping cart
	In order to rent movies
	As a customer
	I want to see the movies I have chosen

@web
Scenario: View my Cart
	Given I have added the movie "Avatar"
	When I view my Cart
	Then I should see the movie "Avatar" with a 1 day rental