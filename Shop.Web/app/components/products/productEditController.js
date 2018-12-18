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

        $scope.product = [];
        $scope.getById = getById;
        $scope.editproduct = editproduct;
        $scope.getSeoTitle = function () {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }
        
        function getById() {
            apiService.get(`/api/product/getbyid?id=${$stateParams.id}`, null,
                function (result) {
                    $scope.product = result.data;
                    $scope.moreImages = JSON.parse($scope.product.MoreImages);
                    
                },
                function (error) {
                    notificationService.displayWarning("Không thể load dữ liệu lên!");
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

        function editproduct() {
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess("Cập nhật thành công!");
                    $state.go('products');
                }, 
                function (error) {
                    notificationService.displayWarning("Cập nhật thất bại!");
                }

            );
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });
            }
            finder.popup();
        }

        $scope.moreImages = [];
        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });

            }
            finder.popup();
        }
        
        $scope.getById();

        $scope.loadCategories();
    }


})(angular.module("productModule"));