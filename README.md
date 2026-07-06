# Marketing Notification System

A background-processed notification system that allows an admin to broadcast a marketing notification to all users through two channels — In-App (SignalR) and Email — without blocking the request or losing the notification if delivery fails.

---

## Tech Stack

| Concern | Technology |
|---|---|
| API | ASP.NET Core Web API |
| Database | Entity Framework Core — **In-Memory Provider** |
| Background Processing | **Hangfire** with **In-Memory Storage** |
| Real-Time Delivery | SignalR |
| Email Delivery | SMTP |
| API Documentation | Swagger / OpenAPI |
| Architecture | Clean Architecture (Domain / Application / Infrastructure) |
| Event Handling | Custom Publisher → Dispatcher → Handler pipeline |

> Both the database and the background job storage currently run in-memory. This is suitable for development and demos; see [Notes for Production](#notes-for-production) before deploying.

---

## Business Idea

- An admin sends a marketing notification to all users.
- Delivery is not required to be instant, so the work runs asynchronously via a Background Job.
- The database record is the source of truth — the notification must appear in a user's notification list even if delivery through a channel fails.
- Two independent delivery channels: In-App (real-time, requires the user to be connected) and Email (not real-time, reaches the user regardless of connection status).

---

## Flow

1. **Controller** receives the admin's request, enqueues a Hangfire background job, and returns `Enqueued` immediately — it does not wait for delivery.
2. **Background Job** runs `NotificationService.Create`, then `NotificationService.Send`.
3. **Create** — creates one notification, links it to every user, and saves it to the database.
4. **Send** — loops through each registered channel (In-App, Email) and sends independently through each. A failure in one channel does not block the other.
5. **In-App Channel** — uses SignalR (`NotificationHub`) to push the notification in real time to connected users.
6. **Email Channel** — uses SMTP to send the notification to each user's email.
7. **On failure** — the failure is published as an event through `EventPublisher` → `EventDispatcher` and handled by the relevant `IEventHandler<T>`, without stopping the rest of the pipeline.

---

## Architecture

| Layer | Responsibility |
|---|---|
| **Domain** | `Notification`, `User`, `NotificationUser`, `IEvent`, domain events (`BookingApprovedEvent`, `ErrorEvent`) |
| **Application** | `NotificationService`, `IInAppNotificationService`, `IEmailNotificationService`, repositories, `EventPublisher`, `EventDispatcher`, `IEventHandler<T>` and its handlers |
| **Infrastructure** | SignalR hub (`NotificationHub`), SMTP client, EF Core `AppDbContext`, Hangfire configuration |

The Application layer depends only on interfaces. It has no direct reference to SignalR, SMTP, or Hangfire — those are wired up in `Program.cs` and implemented in Infrastructure.

---

## Event System

Any event happening in a service (a failed send, an approved booking, etc.) is published through `EventPublisher` and routed by `EventDispatcher` to whichever handlers are registered for that specific event type — resolved via DI (`IEventHandler<TEvent>`).

To add a new event:
1. Create the event class (`: IEvent`).
2. Create its handler (`: IEventHandler<TEvent>`).
3. Register it in `Program.cs`.

No changes are needed to the dispatcher, publisher, or any existing handler.

Currently registered handlers:
- `BookingApprovedHandler` → `BookingApprovedEvent`
- `ErrorEventHandler` → `ErrorEvent`

---

A test endpoint is available to manually trigger an event and verify the dispatcher pipeline:

```csharp
app.MapGet("/test", async (EventPublisher publisher) =>
{
    publisher.Publish(new BookingApprovedEvent { bookingId = 2, Message = "Booking Approved" });
    return "Done";
});
```
