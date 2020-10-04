# ShoppingCart

The shopping cart solution consists 5 projects. .Net core is used throughout the solution.

ShoppingCart.Data<br/>
Is used to communicate with the SQLite database through Entity framework. This is where the models are defined. 

ShoppingCart.DataSeeding<br/>
By making reference to the ShoppingCart.Data project the database is populated 

ShoppingCart.Services<br/>
This is were the DTOs and the services are defined. Once a request is made from the web project to a particular service within this project a DTO will be created by the data gathered
from the database. Automapper is used to map the properties of the DTOs with the data models.

ShoppingCart.Web<br/>
Several API methods are defined and consumed through AJAX. The UI is built through views, css and JS.

ShoppingCart.UnitTest<br/>
The test methods are defined to test the API methods found in the controllers.
