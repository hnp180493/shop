(function (app) {
    app.controller("productCategoryEditController", productCategoryEditController);


    productCategoryEditController.$inject = [
        '$scope', 'apiService',
        'commonService', 'notificationService',
        '$stateParams', '$state'
    ];
    function productCategoryEditController($scope, apiService,
        commonService, notificationService,
        $stateParams, $state) {

        $scope.category = [];
        $scope.getById = getById;
        $scope.editProductCategory = editProductCategory;
        $scope.getSeoTitle = function () {
            $scope.category.Alias = commonService.getSeoTitle($scope.category.Name);
        }
        
        function getById() {
            apiService.get(`/api/productCategory/getbyid?id=${$stateParams.id}`, null,
                function (result) {
                    $scope.category = result.data;
                    
                },
                function (error) {
                    notificationService.displayWarning("Không thể load dữ liệu lên!");
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

        function editProductCategory() {
            apiService.put('/api/productCategory/update', $scope.category,
                function (result) {
                    notificationService.displaySuccess("Cập nhật thành công!");
                    $state.go('productCategories');
                }, 
                function (error) {
                    notificationService.displayWarning("Cập nhật thất bại!");
                }

            );
        }


        
        $scope.getById();

        $scope.loadParentCategories();
    }


})(angular.module("productCategoryModule"));