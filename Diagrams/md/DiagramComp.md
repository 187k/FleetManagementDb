componentDiagram
    component "FleetManagementApp" {
        component "VehicleModule" {
            [VehicleView]
            [VehicleViewModel]
        }
        
        component "DriverModule" {
            [DriverView]
            [DriverViewModel]
        }
        
        component "RouteModule" {
            [RouteView]
            [RouteViewModel]
        }
        
        component "AuthModule" {
            [LoginView]
            [AuthViewModel]
        }
    }

    component "FleetManagement.Core" {
        [VehicleService]
        [DriverService]
        [RouteService]
        [AuthService]
    }

    component "FleetManagement.Data" {
        [VehicleRepository]
        [DriverRepository]
        [RouteRepository]
        [UserRepository]
        [AppDbContext]
    }

    component "SQL Server" {
        [Database]
    }

    [VehicleModule] --> [VehicleService]
    [DriverModule] --> [DriverService]
    [RouteModule] --> [RouteService]
    [AuthModule] --> [AuthService]

    [VehicleService] --> [VehicleRepository]
    [DriverService] --> [DriverRepository]
    [RouteService] --> [RouteRepository]
    [AuthService] --> [UserRepository]

    [VehicleRepository] --> [AppDbContext]
    [DriverRepository] --> [AppDbContext]
    [RouteRepository] --> [AppDbContext]
    [UserRepository] --> [AppDbContext]

    [AppDbContext] --> [Database]
