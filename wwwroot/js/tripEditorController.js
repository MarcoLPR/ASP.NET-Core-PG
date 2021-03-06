﻿(function () {

    "use strict";

    angular.module("app")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
       var vm = this;

       vm.tripName = $routeParams.tripName;
       vm.stops = [];
       vm.errorMessage = "";
       vm.isBusy = true;
       vm.newStop = {};

       var url = "/api/trips/" + vm.tripName + "/stops";

       $http.get(url)
           .then(response => {
               angular.copy(response.data, vm.stops);
               _showMap(vm.stops);
           }, err => {
               vm.errorMessage = "Failed to load stops";
           })
           .finally(() => {
               vm.isBusy = false;
           });

       vm.addStop = () => {
           vm.isBusy = true;
           vm.errorMessage = "";
           var googleUrl = "http://maps.googleapis.com/maps/api/geocode/json?address=" + vm.newStop.name + "&sensor=false";
           $http.get(googleUrl)
               .then(response => {
                   var json = response.data;
                   if (json.results[0].geometry.location.lat == undefined || json.results[0].geometry.location.lng == undefined) {
                       vm.errorMessage = "City not found";
                   } else {
                       vm.newStop.lat = json.results[0].geometry.location.lat;
                       vm.newStop.long = json.results[0].geometry.location.lng;
                   }
               }).finally(() => {
                   $http.post(url, vm.newStop)
                       .then(response => {
                           vm.stops.push(response.data);
                           _showMap(vm.stops);
                           vm.newStop = {};
                       }, err => {
                           vm.errorMessage = "Failed to add new stop";
                       })
                       .finally(() => {
                           vm.isBusy = false;
                       })});

       };
       vm.deleteStop = function (url) {
           vm.isBusy = true;
           vm.errorMessage = "";

           $http.delete(url, vm.stops)
               .then(response => {
                   vm.stops.delete(response.data);
                   _showMap(vm.stops);
               }, error => {
                   vm.errorMessage = "Failed to delete trip";
               })
               .finally(() => {
                   vm.isBusy = false;
               });
       };
    }
    function _showMap(stops) {

        if (stops && stops.length > 0) {

            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.Lat,
                    long: item.Long,
                    info: item.Name
                };
            })

            // Show Map
            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                initialZoom: 3
            });

        }

    }

})();