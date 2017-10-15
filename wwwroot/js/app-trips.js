(function () {

    "use-strict";

    var app = angular.module("app-trips", ["simpleControls", "ngRoute", "tubular", "ui.bootstrap"]);
        app.run(function (tubularConfig) {
            tubularConfig.webApi.requireAuthentication(false);
        });;
        app.config($routeProvider => {

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