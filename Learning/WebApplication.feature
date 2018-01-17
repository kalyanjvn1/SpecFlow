Feature: WebApplication
	In Order to buy pizza online
	As a online customer
	I want to buy a pizza

Scenario: Add piza to the cart
	Given I am on the dominos home screen
		And I select "Menu"
		And I select a "Pizza" from Menu
		And I Select item "MeatZZa™" pizza
	When I click on "Delivery" 
	Then I should see the AddressType
		And Select apartment type as "House"
		And enter street address as "2558 via Nice"
		And enter suite number as "426"
		And enter city as "fort worth"

Scenario: Add Pacific Veggie piza to delivery
	Given I am on the dominos home screen
		And I select "Menu"
		And I select a "Pizza" from Menu
		And I Select item "Pacific Veggie" pizza
	When I click on "Delivery" 
	Then I should see the AddressType
		And Select apartment type as "Apartment"
		And enter street address as "7979 Westheimer Road"
		And enter suite number as "412"
		And enter city as "Houston"

Scenario Outline: Add two different pizzas with different address types
	Given I am on the dominos home screen
		And I select "Menu"
		And I select a "<product>" from Menu
		And I Select item "<pizzaType>" pizza
	When I click on "Delivery" 
	Then I should see the AddressType
		And Select apartment type as "<ApartmentType>"
		And enter street address as "<StreetAddress>"
		And enter suite number as "<aptNumber>"
		And enter city as "<city>"

		Examples: 
		| product | pizzaType | ApartmentType | StreetAddress | aptNumber | city       |
		| Pizza   | MeatZZa™  | House         | 2558 via Nice | 426       | fort worth |
		| Pizza   | Pacific Veggie  | Apartment | 7979 Westheimer Road | 416  | Houston |

	

