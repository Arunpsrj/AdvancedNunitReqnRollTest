Feature: CTM Client Profile Page functionalities
    
    Background: 
        Given the "default" user logged into "default" site
    
    @criticalPath @Arun        
    Scenario: Create a new client record
        Given the user navigates to 'Clients' tab
        And the user clicks 'New Client link'
        And the user enters '<unique_text>' for 'client name' 
        And the user selects 'Active' from the 'Status' dropdown
        And the user selects 'JasonTest' from the 'Region' dropdown
        And the user enters following address for 'client'
          | AddressDetails | AddressValues        |
          | address        | 6575 West Loop South |
          | city           | Bellaire             |
          | state          | TX                   |
          | zip            | 77401                |
        When the user clicks 'Client Save' 
        Then the user verifies the newly created client ID is displayed