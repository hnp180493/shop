(function () {
    angular.module("productCategoryModule", ["commonModule"]).config(config);

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("productCategories", {
            url: "/productCategories",
            templateUrl: "/app/components/product_category/productCategoryListView.html",
            controller: "productCategoryListController"
        }).state("add_productcategory", {
            url: "/add_productcategory",
            templateUrl: "/app/components/product_category/productCategoryAddView.html",
            controller: "productCategoryAddController"
        }).state("edit_productcategory", {
            url: "/edit_productcategory/:id",
            templateUrl: "/app/components/product_category/productCategoryEditView.html",
            controller: "productCategoryEditController"
        });
    }
})();