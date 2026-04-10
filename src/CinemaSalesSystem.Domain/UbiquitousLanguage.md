# Ubiquitous Language — CinemaSalesSystem

| Term | Definition |
| ---- | ---------- |
| Movie | A film available for screening. |
| ShowTime | A scheduled screening of a movie. |
| Seat | A physical chair in an auditorium (tracked per show time in this model). |
| Ticket | Proof of purchase for a show time and seat, represented as a line on an order. |
| Snack | Concession item sold by the cinema (catalog entity). |
| Order | A customer purchase containing tickets and optional snack lines. |
| Campaign | A promotional discount bound to a code and validity window. |
| VAT | Value-added tax applied to snack lines based on `VatType`. |
| Sales Report | Financial summary of cinema sales for a given reporting day. |
| Discount Code | A customer-entered code that must match a campaign to obtain a discount. |
