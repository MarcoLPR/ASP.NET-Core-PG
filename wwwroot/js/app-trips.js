(function () {

    "use-strict";

    angular.module("app-trips", ["simpleControls", "ngRoute", "tubular"])
        .config($routeProvider => {

            $routeProvider
                .when("/", {
                    controller: "tripsController",
                    controllerAs: "vm",
                    templateUrl: "/views/tripsView.html"
                })
                .when("/editor/:tripName", {
                    controller: "tripEditorController",
                    controllerAs: "vm",
                    templateUrl: "/views/tripsEditorView.html"
                })
                .otherwise({ redirectTo: "/"});

            
        });

})();