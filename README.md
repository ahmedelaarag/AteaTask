# AteaTask

# Technologies
1. Main programming language used in *C#*. </br >
2. Dependancy injection built by *Unity*. </br >
3. Unity test by *Xunit*. </br >
4. Mocking object by *NUnit*. </br > 

# Solution files
The solution contains of two main folders, Src and Tests. </br >
# 1.Src
# a.Services
  simple implementation of microservices.
  1. GatewayService </br >
  2. OrderService </br >
# b.Presentation layer
  1. RestApi -- Controllers
# c.Core
 1. Dtos </br >
 1. System Enum </br >
 2. PaymentReceiptDto -- the successfull return type for the end user. </br >
 3. Return types objects --- Successfull returns *IResult*, failed returns *ApiError* 

# 2.Tests
1.Core.Xunit-- Covering Core project</br >
2.Presentation.RestApi.Xunit-- Covering Presentation.RestApi </br >
3.Services.GatewayService.Xunit-- Covering Services.GatewayService </br >
4.Services.OrderService.Xunit-- Covering Services.OrderService </br >
