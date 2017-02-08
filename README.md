# RiskApplication
This is a Risk Application which implements the following requirements based on the provided csv files with below structures:

“Settled.csv”
o Customer - a customer identifier
o Event - event identifier
o Participant - participant identifier
o Stake - dollar amount bet
o Win - dollar amount won (zero if bet not won)

“Unsettled.csv”
o Customer - a customer identifier
o Event - event identifier
o Participant - participant identifier
o Stake - dollar amount bet
o To Win - dollar amount that will be won if bet is successful

1. In your “user interface” (the form the interface takes is open to your interpretation), identify settled bet history for a customer that shows they are winning at an unusual rate, where the business rule is:
a. A customer wins on more than 60% of their bets (i.e. in the settled bets data, they have a value in the “win” column for more than 60% of their bets)
2. Identify unsettled bets that exhibit high risk characteristics, where risky bets are defined as:
a. All upcoming (i.e. unsettled) bets from customers that win at an unusual rate should be highlighted as risky (it is up to you how this is shown)
b. Bets where the stake is more than 10 times higher than that customer’s average bet in their betting history should be highlighted as unusual (again, it is up to you how this is shown)
c. Bets where the stake is more than 30 times higher than that customer’s average bet in their betting history should be highlighted as highly unusual

Architecture : 
N-Tier
The design includes two main layers : Domain and WebApi 
- Domain focuses on Repository layer to retrieve the data from the provided files and illustrates the appropriate data models.
- WebApi focuses on implemeting two other layers which are Service(BusinessLogic) and UI(WebAPI, Web) 
  - Service layer forms the data according to the business requirements
  - WebApi loads the data from Service layers
  - Web provides the data for UI
  UI is implemented in ReactJS.NET
  
All projects use CastleWindsor as StructureMap to maintain a loosely-coupled and testable implementation.
There is a unit test project for each project, as the approach is TDD.

Technology used :
 -.NET4.5.2
 -CastleWindsor
 -WebApi2
 -MVC5
 -ReactJS.NET
 -NUnit
 -Moq
 
 
