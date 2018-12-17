(function () {
    angular.module("productsModule", ['commonModule']).config(config);

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        }).state('product_edit', {
            url: "/product_edit",
            templateUrl: "/app/components/products/productEditView.html",
            controller: "productEditController"
        });  
    }
})();