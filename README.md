# Event App

A SPA that uses React for the client and .Net core for the server.

## Getting Started

### Requirements 
.NET Core 3 (If this is an issue, I can downgrade to a suitable framework)
An IDE that supports .NET Core 3

### Running the client (Requests the server on http://localhost:5000)

```
cd Event.Client
npm install
npm start
```

## Using the UI filtering and sorting
- Initially the first request will take around 20 seconds to complete. This is becuase the cache is being populated
- Sorting
  - Date : This will sort by the latest date entry in the geometry array on the event object.
  - Status : Sorts by the status (Opened, Closed)
  - Category : Sorts by the category title
- Filtering
  - Date : Filters by the date entries on the geometry objects


## Whats left to do
- UI
  - Improve and extend the data that is displayed on the UI
  - Add paginatation (Events are currently limited to 100)
- Caching
  - Improve the initial load. Implementing distributed caching and dynamic population 





