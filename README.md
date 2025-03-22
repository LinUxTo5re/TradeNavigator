# Trade Navigator

## Overview
TradeNavigator is a comprehensive crypto futures trading platform that provides real-time charting, technical indicators, strategy building, and backtesting. It is designed to support algorithmic and manual trading strategies with a scalable and modular architecture.

## Project Structure

- **TradeHorizon (Backend - .NET Core API)**
  - Fetches and processes market data from Gate.io and Coinalyze.
  - Implements technical indicators and trading strategies.
  - Provides REST and WebSocket endpoints for real-time data streaming.
  - Uses **DynamoDB** to store user strategies, settings, backtesting data, and custom indicator formulas.

- **FuturesIQ (Frontend - ReactJS)**
  - Provides an intuitive UI for market analysis, charting, and strategy configuration.
  - Uses **ApexCharts** for real-time market visualization.
  - Interacts with TradeHorizon API for fetching and displaying data.

- **AlphaMind (Machine Learning - Python/Django) (Future Development)**
  - Implements ML models for predictive analytics and automated trading strategies.
  - Uses **TensorFlow, Scikit-Learn, Pandas, NumPy** for data processing and model training.

## Data Sources
| Data Type                   | Source       | Method     |
|-----------------------------|-------------|------------|
| OHLCV (Candlestick Data)    | Gate.io     | WebSocket  |
| Live Price Data             | Gate.io     | WebSocket  |
| Current Open Interest       | Coinalyze   | REST API   |
| Open Interest History       | Coinalyze   | REST API   |
| Current Funding Rate        | Coinalyze   | REST API   |
| Predicted Funding Rate      | Coinalyze   | REST API   |
| Funding Rate History        | Coinalyze   | REST API   |
| Predicted Funding Rate Hist.| Coinalyze   | REST API   |
| Liquidation History         | Coinalyze   | REST API   |
| Long/Short Ratio History    | Coinalyze   | REST API   |
| Live Order Book (Bids/Asks) | Gate.io     | WebSocket  |
| Trade History               | Gate.io     | WebSocket  |

## Technology Stack

### Backend (TradeHorizon)
- **.NET Core Web API**
- **Entity Framework Core**
- **DynamoDB** (for user settings, strategies, backtesting data)
- **Gate.io API, Coinalyze API** (Market Data)

### Frontend (FuturesIQ)
- **ReactJS**
- **ApexCharts** (for real-time charts)
- **Material-UI** (UI Components)

### Machine Learning (AlphaMind) (Future Development)
- **Python/Django**
- **TensorFlow, Scikit-Learn**
- **FastAPI/Flask** (for ML API endpoints)

## Installation & Setup

### 1. Clone the Repository
```sh
git clone https://github.com/yourusername/TradeNavigator.git
cd TradeNavigator
```

### 2. Backend Setup (TradeHorizon)
```sh
cd TradeHorizon
# Restore dependencies
dotnet restore
# Build the project
dotnet build
# Run the API
dotnet run --project TradeHorizon.API
```

### 3. Frontend Setup (FuturesIQ)
```sh
cd FuturesIQ
# Install dependencies
npm install
# Start the React app
npm start
```

### 4. Machine Learning (AlphaMind) (Future Development)
```sh
cd AlphaMind
# Create virtual environment
python -m venv venv
source venv/bin/activate  # (Windows: venv\Scripts\activate)
# Install dependencies
pip install -r requirements.txt
# Run the ML service
python manage.py runserver
```

## Future Roadmap
- Implement ML-based trade recommendations
- Develop a strategy marketplace
- Backtest strategies with historical data
- Deploy backend to AWS (DynamoDB, Lambda, EC2, etc.)
- Enhance UI/UX for seamless trading experience
