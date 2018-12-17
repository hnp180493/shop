(function (app) {
    app.controller("productEditController", productEditController);


    productEditController.$inject = [
        '$scope', 'apiService',
        'commonService', 'notificationService',
        '$stateParams', '$state'
    ];
    function productEditController($scope, apiService,
        commonService, notificationService,
        $stateParams, $state) {

        $scope.category = [];
        $scope.getById = getById;
        $scope.editproduct = editproduct;
        $scope.getSeoTitle = function () {
            $scope.category.Alias = commonService.getSeoTitle($scope.category.Name);
        }
        
        function getById() {
            apiService.get(`/api/product/getbyid?id=${$stateParams.id}`, null,
                function (result) {
                    $scope.category = result.data;
                    
                },
                function (error) {
                    notificationService.displayWarning("Không thể load dữ liệu lên!");
                }

            );
        }

        $scope.loadCategories = function () {
            apiService.get("/api/product/getParents", null,
                function (result) {
                    $scope.Categories = result.data;
                },
                function (error) {
                    alert("Bị lỗi")
                }
            );
        }

        function editproduct() {
            apiService.put('/api/product/update', $scope.category,
                function (result) {
                    notificationService.displaySuccess("Cập nhật thành công!");
                    $state.go('products');
                }, 
                function (error) {
                    notificationService.displayWarning("Cập nhật thất bại!");
                }

            );
        }


        
        $scope.getById();

        $scope.loadParentCategories();
    }


})(angular.module("productModule"));