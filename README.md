# Stock Price Alert System

A console application to monitor stock prices and send email alerts when a stock reaches specified price boundaries. The system fetches stock prices periodically and sends notifications when prices exceed configured thresholds.

## Table of Contents

- [Project Overview](#project-overview)
- [Project Structure](#project-structure)
- [Setup and Installation](#setup-and-installation)
- [Configuration](#configuration)
- [Usage](#usage)

---

## Project Overview

This project monitors stock prices in real time and sends alert emails based on defined price boundaries. The system is built in C# using .NET and follows the Clean Architecture principles for maintainability and scalability.

### Features

- Periodic stock price fetching
- Email alerts when the stock price crosses the specified boundaries
- Configurable monitoring intervals and stock alert parameters

---

## Project Structure

The project is organized following Clean Architecture principles:
```
├── StockPriceAlert
│   ├── Application
│   │   ├── Interfaces          # Interfaces for external services
│   ├── Domain
│   │   ├── Entities            # Core business entities
│   │   ├── Services            # Core services (e.g., monitoring)
│   ├── Infrastructure
│   │   ├── Services            # Implementations of interfaces (e.g., StockPriceFetcher, EmailSender)
│   └── Program.cs              # Entry point for the console application
```

## Setup and Installation

1. **Clone the repository**:

    ```bash
    git clone https://github.com/adriooa/StockPriceAlert.git
    cd StockPriceAlert
    ```

2. **Install dependencies**:

    Run the following command to install required packages.

    ```bash
    dotnet restore
    ```

3. **Build the project**:

    ```bash
    dotnet build
    ```

---

## Configuration

The app uses a JSON configuration file to set up email and stock alert parameters. You can find the configuration template in `appsettings.json`.

### Example `appsettings.json`:

```json
{
  "EmailSettings": {
    "FromAddress": "your_email@example.com",
    "FromPassword": "your_password",
    "SmtpServer": "smtp.office365.com",
    "SmtpPort": 587,
    "EnableSsl": true
  },
  "DestinationEmail": {
    "ToAddress": "your_email@example.com"
  },
  "BrapiSettings": {
    "ApiToken": "TOKEN"
  }
}

```

## Usage

Run the application with parameters in the terminal as follows:

```bash
dotnet run --project StockPriceAlert -- AAPL 120.0 150.0
```
Alternatively, you can compile it to an executable:
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

### Running in VSCode with Arguments
Go to Run and Debug -> Add Configuration....

In launch.json, add your arguments under args:

```json
"args": ["AAPL", "120.0", "150.0"]
```

