@Integration
Feature: Brand
    As a user, I can add, view, edit, activate, deactivate a brand

#BrandController

#Brand/GetUserBrands
Scenario: Can get user brands
    Given I am logged in and have access token
    Then Available brands are visible to me

#Brand/Add
Scenario: Can add new brand
    Given I am logged in and have access token
    Then New brand is successfully added

#Brand/Activate
Scenario: Can activate brand
    Given I am logged in and have access token
    When New deactivated brand is created
    Then Brand is successfully activated

#End of BrandController