# Currency Exchange Service

## Course
Network Application Development

## Project Title
Currency Exchange Office - WCF Web Service + WPF Client + Database

## Author
Bayram (Bayram02)

## Student ID
(65971)

## Description
A complete currency exchange office system built on the .NET platform.
The system retrieves real-time exchange rates from the National Bank of Poland (NBP) API.

## Components
1. **WCF Web Service** - Business logic and NBP API integration
2. **WPF Client Application** - Desktop UI for currency exchange
3. **SQL Server Database** - Stores users, balances and transactions

## Features
- Get current exchange rates for any currency (USD, EUR, GBP, etc.)
- Convert between currencies in real-time
- Modern dark-themed WPF user interface
- Transaction history stored in SQL Server LocalDB

## How to Run
1. Open `CurrencyExchangeService.slnx` in Visual Studio 2026
2. Set `CurrencyExchangeService` as Startup Project and press F5
3. Then set `CurrencyExchangeClient` as Startup Project and press F5
4. Use the WPF window to get rates and convert currencies

## Database
- SQL Server LocalDB
- Tables: Users, Balances, Transactions
- Schema script: `Database/schema.sql`

## API Used
- NBP API: http://api.nbp.pl/en.html
