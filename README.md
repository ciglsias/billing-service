# billing-service

Service that record billable transactions to an InMemory Relational Database. Each transaction include a date, transaction
amount, transaction description, and payment status. This service allow you
to update the billing status as well (e.g. un-billed, billed, paid).

There is a service that generates invoices based on date ranges. The
transactions being billed for are marked billed when the invoice is
generated. There should also me a mechanism to record transactions as paid, in
the event that payments are received for an invoice.
