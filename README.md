# billing-service

Service that record billable transactions to an InMemory Relational Database. Each transaction includes a date, transaction amount, transaction description, and payment status.

- .NET 6
- to run the Project, no configuration needed. Just build/run.
- Crud operations were created for manage Transactinos. Add, Delete, Update, List, GetById. Also basic pagination.
- Operations were created to Modify an Individual Transaction Billing Status.
- Operations were created to Create Invoices with a Range of Dates to select Transactions from such range. Only Transactions on this Range with Billing Status = NotBilled will be used.

Next Possible features could be:
- logging feature. A propeer ILogger using Dependency Injection on Controllers.
- Increase Code Coverage. Only one Controller Action was Tested using Unit Testing.
- Unit Tests were done Mocking Repositores Abstractions. Also with the InMemory databse more Tests can be created.
- adding appsettings.json for each needed environment. (Staging, QA, ..., Production) with proper configurations.