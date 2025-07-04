Feature: CTM Order functionalities
    
    Background: 
        Given the "default" user logged into "default" site
    
    @criticalPath @Arun        
    Scenario: Create a new per diem order
        Given the user creates new 'temp' with following details 
          | Field         | Value              |
          | firstname     | <unique_text>      |
          | lastname      | <unique_text>      |
          | status        | Active             |
          | homeRegion    | JasonTest          |
          | certification | RN                 |
          | speciality    | ER                 |
          | address       | 16801 Addison Road |
          | city          | Addison            |
          | state         | TX                 |
          | zip           | 75001              |
       Given the user creates new 'client' with following details 
          | Field      | Value                |
          | clientname | <unique_text>        |
          | status     | Active               |
          | region     | JasonTest            |
          | address    | 6575 West Loop South |
          | city       | Bellaire             |
          | state      | TX                   |
          | zip        | 77401                |
          