# Cinema Sales — Domain Model

Lightweight DDD view of the **CinemaSales.Domain** bounded context.

## Class diagram

```mermaid
classDiagram

class Movie {
    +Guid Id
    +string Title
    +int DurationMinutes
    +MovieRating Rating
}

class ShowTime {
    +Guid Id
    +DateTimeOffset StartTime
    +ShowTimeSlot Slot
}

class Seat {
    +Guid Id
    +SeatNumber SeatNumber
    +bool IsReserved
}

class Ticket {
    +Guid Id
    +TicketType TicketType
    +Money Price
}

class Snack {
    +Guid Id
    +string Name
    +Money Price
}

class Order {
    +Guid Id
    +DateTimeOffset CreatedAt
    +OrderStatus Status
    +Money TotalAmount
}

class Campaign {
    +Guid Id
    +string Name
    +decimal DiscountPercentage
}

class SalesReport {
    +DateOnly ReportDate
    +Money TotalRevenue
}

Movie "1" --> "*" ShowTime
ShowTime "1" --> "*" Ticket
Order "1" --> "*" Ticket
Order "1" --> "*" Snack
Ticket --> Seat
Order --> Campaign
```

_Note: `Snack` on an order is modeled as `OrderSnackLine` entities referencing catalog `Snack` identifiers; the diagram reflects the conceptual link._

## Aggregate boundaries

- **Movie** — root; owns **ShowTime** entities.
- **Order** — root; owns **Ticket** and **OrderSnackLine** entities; references **Campaign** by identifier when a discount is applied.

## Domain services

| Service | Responsibility |
|--------|----------------|
| `IPricingService` / `PricingService` | Ticket and snack subtotals; grand total with VAT and discounts. |
| `IVatCalculationService` / `VatCalculationService` | VAT on snack lines by `VatType`. |
| `IDiscountService` / `DiscountService` | Validates campaign and code; computes discount amount. |
| `ISeatAllocationService` / `SeatAllocationService` | Prevents double booking for the same show time and seat. |
