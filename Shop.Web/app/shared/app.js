
(function () {
    angular.module("shopModule",
        [
            'productsModule',
            'productCategoryModule',
            'commonModule',
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