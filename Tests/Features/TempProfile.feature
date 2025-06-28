Feature: CTM Temp Profile Page functionalities
    
    Background: 
        Given the "default" user logged into "default" site
    
    @criticalPath @Arun        
    Scenario: Create a new temp record
        Given the user navigates to 'Temps' tab
        And the user clicks 'New Temp link'
        And the user enters '<unique_text>' for 'temp first name' 
        And the user enters '<unique_text>' for 'temp last name'  
        And the user selects 'Active' from the 'Status' dropdown
        And the user selects 'JasonTest' from the 'HomeRegion' dropdown
        And the user clicks 'RN Cert'
        And the user clicks 'ER Spec'
        And the user enters following address for 'temp-permanent'
        | AddressDetails | AddressValues      |
        | address        | 16801 Addison Road |
        | city           | Addison            |
        | state          | TX                 |
        | zip            | 75001              |
        When the user clicks 'Temp Save' 
        Then the user verifies the newly created temp ID is displayed