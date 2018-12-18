(function () {
    angular.module("productModule", ['commonModule']).config(config);

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            parent: 'base',
            controller: "productListController"
        }).state('add_product', {
            url: "/add_product",
            templateUrl: "/app/components/products/productAddView.html",
            parent: 'base',
            controller: "productAddController"
        }).state('edit_product', {
            url: "/edit_product/:id",
            templateUrl: "/app/components/products/productEditView.html",
            parent: 'base',
            controller: "productEditController"
        });
    }
})();