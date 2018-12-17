(function (app) {
    app.controller("productListController", productListController);


    productListController.$inject = ['$scope', 'apiService', 'notificationService'];
    function productListController($scope, apiService, notificationService) {
        $scope.products = [];
        $scope.getProducts = getProducts;
        $scope.keyword = '';
        $scope.search = function(){
            $scope.getProducts();
        }

        function getProducts(page) {
            page = page || 1;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 2,
                }
            }
            apiService.get('/api/product/getall', config,
                function (result) {
                    if (result.data.TotalCount > 0) {
                        notificationService.displaySuccess(`Có ${result.data.TotalCount} bản ghi được tìm thấy`);
                    } else {
                        notificationService.displayWarning("Không có bản ghi nào!");
                    }
                    $scope.products = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.totalCount = result.data.TotalCount;
                    $scope.pageCount = result.data.TotalPages;
                },
                function (error) {
                    
                }
            );
        }

        $scope.getProducts();
        
    }
})(angular.module("productsModule"));