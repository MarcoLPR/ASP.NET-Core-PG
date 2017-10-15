(function () {

    "use-strict";

    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

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
    }
})();