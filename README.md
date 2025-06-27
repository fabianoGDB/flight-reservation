✈️ Flight Reservation API
A simple and extensible Flight Reservation System built using .NET 6, following clean code principles. This project manages passengers, flights, and seat reservations using RESTful API practices.

📌 Features
✅ Register and list passengers

✅ Create, view, and manage flights

✅ Reserve and cancel flight seats

✅ Check seat availability for specific flights

✅ Error handling and validation

✅ Organized by layers: Domain, Application, Infrastructure, and API

🧱 Technologies Used
.NET 6 (C#)

Entity Framework Core (In-Memory Database)

Minimal APIs

Dependency Injection

LINQ & Fluent Validation

RESTful Services

🧩 Project Structure
bash
Copy code
FlightReservation/
├── Domain/                # Entities and core business rules
├── Application/           # Use cases and application logic
├── Infrastructure/        # Repository and service implementations
├── API/                   # Minimal API endpoints (Program.cs)
└── README.md
🚀 Getting Started
Prerequisites
.NET 6 SDK

Run the Application
bash
Copy code
git clone https://github.com/fabianoGDB/flight-reservation.git
cd flight-reservation
dotnet run --project FlightReservation
The API will be available at https://localhost:5001 or http://localhost:5000.

📬 Sample Endpoints
➕ Register Passenger
bash
Copy code
POST /passengers
json
Copy code
{
  "name": "John Doe",
  "email": "john.doe@example.com"
}
📋 List Passengers
bash
Copy code
GET /passengers
➕ Create Flight
bash
Copy code
POST /flights
json
Copy code
{
  "origin": "New York",
  "destination": "London",
  "departureDate": "2025-07-01T14:00:00",
  "totalSeats": 100
}
📦 Reserve Seat
bash
Copy code
POST /reservations
json
Copy code
{
  "passengerId": 1,
  "flightId": 2
}
✅ To-Do / Improvements
🧪 Add unit and integration tests

🗃️ Replace in-memory database with SQL Server or PostgreSQL

🔐 Add authentication and authorization

📊 Add pagination and filtering for listings

🌐 Add Swagger for API documentation

🤝 Contributing
Feel free to fork this repository, submit issues, or open pull requests.

📄 License
This project is open-source and available under the MIT License.

Let me know if you’d like to add Swagger, database persistence, or Docker support and I’ll update this README accordingly.










Tools



ChatGPT can make mistakes