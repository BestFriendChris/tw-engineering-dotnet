Feature: Home Page
	In order to select some movies
	As a renter
	I want to see which movies are available

@mytag
Scenario: View list of movies on Home Page
	When I go to the home page
	Then the list includes the movie "Avatar"
	And the list includes the movie "Up in the air"
	And the list includes the movie "Finding Nemo"
