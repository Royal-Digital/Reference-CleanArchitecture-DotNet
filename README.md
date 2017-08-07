# Clean-Architecture-DotNet
A todo application created as a reference implementation of the CleanArchitecture in C#.

Makes use of:
 - **SimpleInjector**
   - For DI, becuase it has a .verify(), hence only 1 test to check all IoC configuration
   - *Install-Package SimpleInjector*
   - *Install-Package SimpleInjector.Integration.WebApi*
 - **TddBuddy CleanArchitecture Domain**
   - Core of CleanArchitecture with interfaces and key objects
   - *Install-Package TddBuddy.CleanArchitecture.Domain.DotNet*
 - **TddBuddy CleanArchitecture**
   - Implemenation of CleanArchitecture Domain interfaces
   - *Install-Package TddBuddy.CleanArchitecture.DotNet*
 - **TddBuddy CleanArchitecture TestUtils**
   - Utilties that making testing controllers easy and simple
   - *Install-Package TddBuddy.CleanArchitecture.DotNet*
 - **TddBuddy SpeedyLocalDb**
   - A EntityFramework testing package that improve the performance of DB integration test. 
   - *Install-Package TddBuddy.SpeedySqlLocalDb*
 - **Entity Framework is used for**
   - Migrations
   - Entities
