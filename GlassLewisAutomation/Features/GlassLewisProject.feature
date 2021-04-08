Feature: GlassLewis

@CountrySelection
Scenario Outline: Select Country "Belgium"
	Given I am using "<browser>"
	When the Belgium country is selected under country filter
	When click on update button
	Then grid displays all meetings for the following country
	| Country |
	| Belgium | 
	Then No other country's meeting is listed on the page

	Examples: 
	| browser |
	| chrome  |
	| edge    |
	| firefox |

@CompanyName
Scenario Outline: Select Company Name 
Given I am using "<browser>"
	When User writes following Company Name in search field
	| CompanyName             |
	| Activision Blizzard Inc |
	Then the user lands onto the “Activision Blizzard Inc” vote card page.
    Then Company Name should appear in the top banner

Examples: 
	| browser |
	| chrome  |
	| edge    |
	| firefox |