```mermaid
classDiagram
    namespace FleetManagement.WPF {
        namespace Views {
            class MainWindow
            class VehicleView
            class DriverView
            class RouteView
        }
        
        namespace ViewModels {
            class MainViewModel
            class VehicleViewModel
            class DriverViewModel
            class RouteViewModel
        }
    }

    namespace FleetManagement.Core {
        namespace Services {
            class IVehicleService
            class IDriverService
            class IRouteService
        }
        
        namespace Models {
            class Vehicle
            class Driver
            class Route
        }
    }

    namespace FleetManagement.Data {
        namespace Repositories {
            class IVehicleRepository
            class IDriverRepository
            class IRouteRepository
        }
        
        class AppDbContext
    }

    FleetManagement.WPF --> FleetManagement.Core
    FleetManagement.Core --> FleetManagement.Data
```
