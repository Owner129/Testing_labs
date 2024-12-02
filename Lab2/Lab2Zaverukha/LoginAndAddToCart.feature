Feature: Login and Add to Cart
    As a user of the Saucedemo site
    I want to login and add a product to my cart
    So that I can see the product in my cart

    Scenario: Login with standard_user and add product to cart
        Given I am on the login page
        When I login with username "standard_user" and password "secret_sauce"
        And I add the first product to the cart
        Then the cart badge should display "1"
