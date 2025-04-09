# 📊 Gate.io Market Data API

This .NET Core Web API project provides multiple endpoints to fetch trading data from Gate.io, including liquidation orders, order book data, funding rates, OHLCV candles, and more.

## 🌐 Base URL

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

---

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
