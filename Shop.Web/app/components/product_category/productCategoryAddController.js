(function (app) {
    app.controller("productCategoryAddController", productCategoryAddController);

    productCategoryAddController.$inject = [
        '$scope', 'apiService',
        'notificationService', '$state',
        'commonService'
    ];
    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.category = {
            CreatedDate: new Date(),
            Status: true,
        }

        $scope.addProductCategory = addProductCategory;

        $scope.getSeoTitle = function () {
            $scope.category.Alias = commonService.getSeoTitle($scope.category.Name);
        }

        function addProductCategory() {
            apiService.post("/api/productCategory/create", $scope.category,
                function (result) {
                    notificationService.displaySuccess(`${result.data.Name} đã được thêm mới!`);
                    $state.go("productCategories");
                },
                function (failed) {
                    notificationService.displayError("Thêm thất bại!");
                }

            );
        }

        $scope.loadParentCategories = function () {
            apiService.get("/api/productCategory/getParents", null,
                function (result) {
                    $scope.parentCategories = result.data;
                },
                function (error) {
                    alert("Bị lỗi")
                }
            );
        }

        $scope.loadParentCategories();
    }
})(angular.module("productCategoryModule"));