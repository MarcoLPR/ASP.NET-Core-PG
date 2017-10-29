(function () {

    "use-strict";

    angular.module("app")
        .controller("tripsController", tripsController);

    function tripsController($http, $window, $scope) {

        var vm = this;

        vm.user = {
            email: "marcolpr@hotmail.com"
        };

        vm.validateEmail = function () {
            vm.state = 0;
            vm.user.email = vm.user.email + " ";
            for (i = 0; vm.user.email.length > i; i++) {
                switch (vm.state) {
                    case 0: {
                        if (vm.user.email[i] != "@") {
                            vm.state = 0;
                            break;
                        } else {
                            vm.state = 1;
                            break;
                        }
                    }
                    case 1: {
                        if (vm.user.email[i] == "@" || vm.user.email[i] == ".") {
                            $window.alert("EMAIL NOT VALID");
                            vm.state = -1;
                            break;
                        } else {
                            vm.state = 2;
                            break;
                        }
                    
                    }
                    case 2: {
                        if (vm.user.email[i] == "@") {
                            $window.alert("EMAIL NOT VALID");
                            vm.state = -1;
                            break;
                        } else if (vm.user.email[i] == ".") {
                            vm.state = 3;
                            break;
                        } else {
                            vm.state = 2;
                            break;
                        }
                    }
                    case 3: {
                        if (vm.user.email[i] == "@" || vm.user.email[i] == ".") {
                            $window.alert("EMAIL NOT VALID");
                            vm.state = -1;
                            break;
                        } else {
                            vm.state = 4;
                            break;
                        }
                    }
                    case 4: {
                        if (vm.user.email[i] == "@" || vm.user.email[i] == ".") {
                            $window.alert("EMAIL NOT VALID");
                            vm.state = -1;
                            break;
                        } else if (vm.user.email[i] == " ") {
                            $window.alert("Correctly validated");
                            vm.user.email = "";
                            vm.state = 5;
                            break;
                        }else {
                            vm.state = 4;
                            break;
                        }
                    }
                }  
            }
        };

        vm.trips = [];

        vm.newTrip = {};  
    
        vm.errorMessage = "";

        vm.isBusy = true;

        $http.get("/api/trips")
            .then(response => {
                angular.copy(response.data, vm.trips);
            }, error => {
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(() => {
                vm.isBusy = false;
            });

        vm.addTrip = function () {

            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(response => {
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, error => {
                    vm.errorMessage = "Failed to save new trip";
                })
                .finally(() => {
                    vm.isBusy = false;
                });
        };
        vm.deleteTrip = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.delete("/api/trips", vm.trips)
                .then(response => {
                    vm.trips.delete(response.data);
                }, error => {
                    vm.errorMessage = "Failed to delete trip";
                })
                .finally(() => {
                    vm.isBusy = false;
                });
        };
    }
})();