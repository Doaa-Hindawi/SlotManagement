# SlotManagement

## About this solution

This is a layered startup solution based on [Domain Driven Design (DDD)](https://abp.io/docs/latest/framework/architecture/domain-driven-design) practises. All the fundamental ABP modules are already installed. Check the [Application Startup Template](https://abp.io/docs/latest/solution-templates/layered-web-application) documentation for more info.

### Pre-requirements

* [.NET10.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
* [Node v18 or 20](https://nodejs.org/en)

### Configurations

The solution comes with a default configuration that works out of the box. However, you may consider to change the following configuration before running your solution:

* Check the `ConnectionStrings` in `appsettings.json` files under the `SlotManagement.HttpApi.Host` and `SlotManagement.DbMigrator` projects and change it if you need.

### Before running the application

* Run `abp install-libs` command on your solution folder to install client-side package dependencies. This step is automatically done when you create a new solution, if you didn't especially disabled it. However, you should run it yourself if you have first cloned this solution from your source control, or added a new client-side package dependency to your solution.
* Run `SlotManagement.DbMigrator` to create the initial database. This step is also automatically done when you create a new solution, if you didn't especially disabled it. This should be done in the first run. It is also needed if a new database migration is added to the solution later.

#### Generating a Signing Certificate

In the production environment, you need to use a production signing certificate. ABP Framework sets up signing and encryption certificates in your application and expects an `openiddict.pfx` file in your application.

To generate a signing certificate, you can use the following command:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p 1272ff80-6a16-47b6-9a70-c41232e73d80
```

> `1272ff80-6a16-47b6-9a70-c41232e73d80` is the password of the certificate, you can change it to any password you want.

It is recommended to use **two** RSA certificates, distinct from the certificate(s) used for HTTPS: one for encryption, one for signing.

For more information, please refer to: [OpenIddict Certificate Configuration](https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios)

> Also, see the [Configuring OpenIddict](https://abp.io/docs/latest/Deployment/Configuring-OpenIddict#production-environment) documentation for more information.

### Solution structure

This is a layered monolith application that consists of the following applications:

* `angular`: Angular application.
* `SlotManagement.DbMigrator`: A console application which applies the migrations and also seeds the initial data. It is useful on development as well as on production environment.
* `SlotManagement.HttpApi.Host`: ASP.NET Core API application that is used to expose the APIs to the clients.

#### Test Projects

The `test` folder contains the following test projects:

* `SlotManagement.Application.Tests`: Application layer tests.
* `SlotManagement.Domain.Tests`: Domain layer tests.
* `SlotManagement.EntityFrameworkCore.Tests`: Entity Framework Core integration tests.




## Deploying the application

Deploying an ABP application follows the same process as deploying any .NET or ASP.NET Core application. However, there are important considerations to keep in mind. For detailed guidance, refer to ABP's [deployment documentation](https://abp.io/docs/latest/Deployment/Index).

### Additional resources


#### Internal Resources

You can find detailed setup and configuration guide(s) for your solution below:

* [Angular](./angular/README.md)

#### External Resources
You can see the following resources to learn more about your solution and the ABP Framework:

* [Web Application Development Tutorial](https://abp.io/docs/latest/tutorials/book-store/part-1)
* [Application Startup Template](https://abp.io/docs/latest/startup-templates/application/index)
-------------------------------------
## 🚀 Getting Started
1. **Database**: Update the connection string in `appsettings.json`.
2. **Migrations**: Run `dotnet ef database update` or run the `DbMigrator` project.
3. **Backend**: Run `SlotManagement.HttpApi.Host`.
4. **Frontend**: Navigate to the Angular folder, run `npm install`, then `npm start`.
## 🌍 TimeZone Handling (NodaTime)
To ensure absolute accuracy across different time zones, I integrated **NodaTime**. 
- **Reason**: Standard `DateTime` is ambiguous. NodaTime allows us to handle 'Instant' and 'ZonedDateTime' correctly.
- **Implementation**: The system stores all slots in UTC and converts them based on the user's specific IANA TimeZone ID.
## 📝 Assumptions & Decisions
- **Slot Duration**: Fixed at 30 minutes (configurable in future phases).
- **Concurrent Bookings**: Prevented via optimistic concurrency at the DB level.
- **ABP Framework**: Used to leverage its robust Identity and Audit logging features.
