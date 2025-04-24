# 📊 Gate.io Market Data API

This .NET Core Web API project provides multiple endpoints to fetch trading data from Gate.io, including liquidation orders, order book data, funding rates, OHLCV candles, and more.

## 🌐 Base URL (REST)

```
https://localhost:7001/api/gateio
```

## 📘 Available Endpoints

### 🔹 1. Get Liquidation Orders

**Endpoint:**  
`GET /liq-orders`

**Query Parameters:**

- `contract` (string) – e.g., `"BTC_USDT"`
- `from` (int64, optional) – UNIX timestamp in seconds
- `to` (int64, optional) – UNIX timestamp in seconds
- `limit` (int, optional) – Number of records to fetch

**Example:**

```
https://localhost:7001/api/gateio/liq-orders?limit=1000&contract=BTC_USDT
```

### 🔹 2. Get Order Book Snapshot

**Endpoint:**  
`GET /order-book`

**Query Parameters:**

- `contract` (string) – e.g., `"BTC_USDT"`
- `interval` (string, optional) – e.g., `"0"`
- `limit` (int, optional) – Max records (e.g., `20`)
- `with_id` (bool, optional) – e.g., `true`

**Example:**

```
https://localhost:7001/api/gateio/order-book?contract=BTC_USDT&interval=0&limit=20&with_id=true
```

### 🔹 3. Get Contract Stats

**Endpoint:**  
`GET /contract-stats`

**Query Parameters:**

- `contract` (string) – e.g., `"BTC_USDT"`
- `from` (int64, optional) – UNIX timestamp
- `interval` (string, optional) – e.g., `"5m"`
- `limit` (int, optional)

**Example:**

```
https://localhost:7001/api/gateio/contract-stats?contract=BTC_USDT&interval=5m&limit=100
```

### 🔹 4. Get Funding Rate History

**Endpoint:**  
`GET /funding-rate-history`

**Query Parameters:**

- `contract` (string) – e.g., `"BTC_USDT"`
- `from` (int64, optional) – UNIX timestamp
- `to` (int64, optional) – UNIX timestamp
- `limit` (int, optional)

**Example:**

```
https://localhost:7001/api/gateio/funding-rate-history?contract=BTC_USDT&lmit=200
```

### 🔹 5. Get OHLCV Historical Data

**Endpoint:**  
`GET /ohlcv`

**Query Parameters:**

- `contract` (string) – e.g., `"BTC_USDT"`
- `from` (int64, optional)
- `to` (int64, optional)
- `limit` (int, optional)
- `interval` (string) – e.g., `"5m"`, `"1h"`, `"1d"`

**Example:**

```
https://localhost:7001/api/gateio/ohlcv?contract=BTC_USDT&interval=5m&limit=500
```
---
## 🌐 Base URL (WS -> SignalR)

```
https://localhost:7001/hub/ws
```

## Reminder
- SignalR uses wss instead of https under the hood
- First connect with endpoint
- Then send message body to endpoint as user input

## 📘 Available Endpoints
### 🔹 1. Get candlesticks

**Endpoint:**  
`/gate-candlesticks"`

**Message Body (JSON):**
```
{
    "time" : 123456, "channel" : "futures.candlesticks",
    "event": "subscribe", "payload" : ["1m", "BTC_USDT"],
    "contract": "BTC_USDT", "timeframe": "1m"
}
```
- allowed timeframes: 
10s, 30s, 1m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 8h, 12h, 1d, 2d, 3d, 5d, 7d, 1w
- allowed one contract and timeframe at once in payload

**Example:**

```
https://localhost:7001/hub/ws/gate-candlesticks
```
### 🔹 2. Get ticker info

**Endpoint:**  
`/gate-ticker`

**Message Body (JSON):**
```
{
    "time" : 123456, "channel" : "futures.tickers",
    "event": "subscribe", "payload" : ["BTC_USDT", "ETH_USDT"],
}
```
- allowed N number of contracts

**Example:**

```
https://localhost:7001/hub/ws/gate-ticker
```
### 🔹 3. Get trades info

**Endpoint:**  
`/gate-trades`

**Message Body (JSON):**
```
{
    "time" : 123456, "channel" : "futures.trades",
    "event": "subscribe", "payload" : ["BTC_USDT", "ETH_USDT"],
}
```
- allowed N number of contracts

**Example:**

```
https://localhost:7001/hub/ws/gate-trades
```
### 🔹 4. Get public liquidated orders
**Endpoint:**  
`/gate-gate-pliq-orders`

**Message Body (JSON):**
```
{
    "time" : 123456, "channel" : "futures.public_liquidates",
    "event": "subscribe", "payload" : ["BTC_USDT", "ETH_USDT"],
}
```
- allowed N number of contracts

**Example:**

```
https://localhost:7001/hub/ws/gate-gate-pliq-orders
```
### 🔹 5. Get contract stats
**Endpoint:**  
`/gate-contract-stats`

**Message Body (JSON):**
```
{
    "time" : 123456, "channel" : "futures.contract_stats",
    "event": "subscribe", "payload" : ["BTC_USDT", "1m"],
    "contract": "BTC_USDT", "timeframe": "1m"
}
```
- allowed timeframes: 1m, 5m
- allowed one contract and timeframe at once in payload
**Example:**

```
https://localhost:7001/hub/ws/gate-contract-stats
```
### 🔹 6. Get order book update
**Endpoint:**  
`/gate-order-book`

**Message Body (JSON):**
```
{
    "time" : 123456, "channel" : "futures.order_book_update",
    "event": "subscribe", "payload" : ["BTC_USDT", "100ms", "100"],
    "contract": "BTC_USDT", "frequency": "100ms"
}
```
- allowed frequency: 20ms, 100ms
- allowed level (opt): 100, 50, 20 (for 20ms only allowed 20)
- allowed one contract, frequency and level at once in payload
**Example:**

```
https://localhost:7001/hub/ws/gate-order-book
```
---