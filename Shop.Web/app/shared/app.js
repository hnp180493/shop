
(function () {
    angular.module("shopModule",
        [
            'productCategoryModule',
            'commonModule',
            'productModule',
        ]).config(config);

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });

        $urlRouterProvider.otherwise("/admin");
    }
})();