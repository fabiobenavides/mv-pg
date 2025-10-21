# Vehicle Fee API

## Overview
The Vehicle Fee API is a C# web API designed to calculate various fees associated with vehicle transactions based on the type of vehicle and its base price. The API provides an endpoint to receive requests and return calculated fees, including Buyer Fee, Seller Fee, Association Fee, and Storage Fee.

## Features
- Calculates fees for different vehicle types: Common and Luxury.
- Implements a factory pattern to create specific fee calculators based on vehicle type.
- Follows best practices for API development in C#.

## Project Structure
```
vehicle-fee-api
├── src
│   └── VehicleFeeApi
│       ├── VehicleFeeApi.csproj
│       ├── Program.cs
│       ├── appsettings.json
│       ├── Controllers
│       │   └── FeesController.cs
│       ├── Interfaces
│       │   └── IVehicleFeeCalculator.cs
│       ├── Factories
│       │   └── VehicleFeeCalculatorFactory.cs
│       ├── Calculators
│       │   ├── CommonVehicleFeeCalculator.cs
│       │   └── LuxuryVehicleFeeCalculator.cs
│       ├── Services
│       │   └── VehicleFeeService.cs
│       └── Models
│           ├── CalculateFeesRequest.cs
│           └── FeeResult.cs
├── tests
│   └── VehicleFeeApi.Tests
│       ├── VehicleFeeApi.Tests.csproj
│       └── FeesControllerTests.cs
├── .gitignore
└── README.md
```

## Setup Instructions
1. Clone the repository:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd vehicle-fee-api/src/VehicleFeeApi
   ```
3. Restore the project dependencies:
   ```
   dotnet restore
   ```
4. Run the application:
   ```
   dotnet run
   ```

## Usage
To calculate fees, send a POST request to the `/calculate-fees` endpoint with a JSON body containing `basePrice` and `vehicleType`. 

### Example Request
```json
{
  "basePrice": 20000,
  "vehicleType": "Common"
}
```

### Example Response
```json
{
  "BuyerFee": 200,
  "SellerFee": 150,
  "AssociationFee": 50,
  "StorageFee": 100
}
```

## Testing
To run the tests, navigate to the test project directory and execute:
```
dotnet test
```

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License.