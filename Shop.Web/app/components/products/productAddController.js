(function (app) {
    app.controller("productAddController", productAddController);

    productAddController.$inject = [
        '$scope', 'apiService',
        'notificationService', '$state',
        'commonService'
    ];
    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px',
        }

        $scope.addProduct = addProduct;

        $scope.getSeoTitle = function () {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function addProduct() {
            apiService.post("/api/product/create", $scope.product,
                function (result) {
                    notificationService.displaySuccess(`${result.data.Name} đã được thêm mới!`);
                    $state.go("products");
                },
                function (failed) {
                    notificationService.displayError("Thêm thất bại!");
                }

            );
        }

        $scope.loadCategories = function () {
            apiService.get("/api/productCategory/getParents", null,
                function (result) {
                    $scope.Categories = result.data;
                },
                function (error) {
                    alert("Bị lỗi")
                }
            );
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl;
            }
            finder.popup();
        }

        $scope.loadCategories();
    }
})(angular.module("productModule"));