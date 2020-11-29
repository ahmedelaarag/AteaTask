# AteaTask

# Technologies
1. Main programming language used in *C#*. </br >
2. Dependancy injection built by *Unity*. </br >
3. Unity test by *Xunit*. </br >
4. Mocking object by *NUnit*. </br > 

# Solution files
The solution contains of two main projects, and each project has a own test project as well. </br >
# Src
# 1. Core
Its an class library that contains the services and dtos and validatinos. </br >
a. Services </br >
  1. GatewayService -- That service should resolve the correct gateway from the requested http body data, and shall connect to the 3rd party api. </br >
  2. OrderService -- That service shall validate the requested http body data, *Simple validation* and shall call the 3rd party api by the GatewayService and map the result. </br >
b. Dtos </br >
  1. OrderDto -- the input type for the controller and the services. </br >
  2. PaymentReceiptDto -- the successfull return type for the end user. </br >
c. Validations </br >
  1- OrderValidations -- simple validation to check the input data request by user. </br >
  
 # 2. PresentationAPITests 
 Self host WebApi, there is only one controller and has only one Post method. </br >
  a. Successfull return type will be *PaymentReceiptDto*. </br >
  b. Falied call will return wrapped object call *ApiErro*. </br >
# Tests
1.CoreTests </br >
2.PresentationAPITests </br >
