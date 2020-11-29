# AteaTask

Solution contains of two project
1. Core project, its an class library that contains the services and dtos and validatinos.
a. Services
  1. GatewayService -- That service should resolve the correct gateway from the requested http body data, and shall connect to the 3rd party api.
  2. OrderService -- That service shall validate the requested http body data, *Simple validation* and shall call the 3rd party api by the GatewayService and map the result.
b. Dtos
  1. OrderDto -- the input type for the controller and the services.
  2. PaymentReceiptDto -- the successfull return type for the end user.
c. Validations
  1- OrderValidations -- simple validation to check the input data request by user.
  
 2. PresentationAPI, it contains the ApiControllers, there is only one controller and has only one Post method.
  a. Successfull return type will be *PaymentReceiptDto*
  b. Falied call will return wrapped object call *ApiErro*
