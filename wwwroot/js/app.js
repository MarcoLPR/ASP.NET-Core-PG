(function () {

    "use-strict";

    var app = angular.module("app", ["simpleControls", "ngRoute", "tubular", "ui.bootstrap"]);
        app.run(function (tubularConfig) {
            tubularConfig.webApi.requireAuthentication(false);
        });;
        app.config($routeProvider => {

            $routeProvider
                /*.when("/", {
                    controller: "indexController",
                    controllerAs: "ctrl",
                    templateUrl: "views/indexView.html"
                })*/
                .when("/login", {
                    controller: "loginController",
                    controllerAs: "ctrl",
                    templateUrl: "views/loginView.html"
                })
                .when("/contact", {
                    controller: "contactController",
                    controllerAs: "ctrl",
                    templateUrl: "/views/contactView.html"
                })
                .when("/", {
                    controller: "tripsController",
                    controllerAs: "vm",
                    templateUrl: "/views/tripsView.html"
                })
                .when("/editor/:tripName", {
                    controller: "tripEditorController",
                    controllerAs: "ctrl",
                    templateUrl: "/views/tripsEditorView.html"
                })
                .otherwise({ redirectTo: "/"});

            
        });

})();