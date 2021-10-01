Feature: LoginPage

#Background step that is shared by all scenarios to reduce duplication.
Background: 
	Given the Login page has been loaded

#Scenario with inline table using Specflow's Table Parser in the method
Scenario: 01. Logging in takes the user to the Secure Area
	When I log in with with the following details:
		| username | password             |
		| tomsmith | SuperSecretPassword! |
	Then I should be on a page titled 'Secure Area'

#Scenario with inline parameters
Scenario: 02. Attempting a log in with invalid credentials does not allow the user to enter the Secure Area
	When I attempt to log in with 'invalidUn' and 'SuperSecretPassword!'
	Then I should remain on the Login Page
	And the banner should read 'Your username is invalid!'

#Scenario Outline, same as above but with parameters in 'Examples' table
Scenario Outline: 03. Attempting a log in with invalid credentials does not allow the user to enter the Secure Area
	When I attempt to log in with '<username>' and '<password>'
	Then I should remain on the Login Page
	And the banner should read '<errormessage>'
	Examples: 
		| username  | password             | errormessage              |
		| invalidUn | SuperSecretPassword! | Your username is invalid! |
		| tomsmith  | invalidPw            | Your password is invalid! |

#A straightforward scenario with paramaters hidden from the scenario and used only in the step definition
Scenario: 04. Logging out of the Secure Area should return the user to the Login page
	Given I have successfully logged in to the Secure Area
	When I Logout
	Then I should return to the Login Page