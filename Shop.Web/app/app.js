﻿(function () {
    angular.module("shopModule",
        [
            'productCategoryModule',
            'commonModule',
            'productModule',
        ])
        .config(config)
        .config(configAuthentication);

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('login', {
                url: "/login",
                templateUrl: "/app/components/login/loginView.html",
                controller: "loginController"
            })
            .state('admin', {
                url: "/admin",
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });

        $urlRouterProvider.otherwise("/admin");
    }

    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
})();