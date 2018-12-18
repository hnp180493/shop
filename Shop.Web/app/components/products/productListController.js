(function (app) {
    app.controller("productListController", productListController);


    productListController.$inject = [
        '$scope', 'apiService',
        'notificationService', '$ngBootbox',
        '$filter'
    ];
    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.page = 1;
        $scope.pageCount = 0;
        $scope.getproducts = getproducts;
        $scope.keyword = '';
        $scope.search = search;
        $scope.del = del;

        $scope.deleteMulti = function () {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            })
            var config = {
                params: {
                    listIdJson: JSON.stringify(listId)
                }
            }
            apiService.del('/api/product/deletemulti', config,
                function (result) {
                    notificationService.displaySuccess(`Xóa thành công ${result.data} bản ghi`);
                    search();
                },
                function (error) {
                    notificationService.displayWarning("Có lỗi xảy ra!");
                }

            );
        }

        let isAll = false;

        $scope.checkAll = function () {
            if (isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                isAll = false;
            }          
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function search() {
            getproducts();
        }
        function getproducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 15
                }
            }

            apiService.get('/api/product/getall', config,
                function (result) {
                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning("Không có bản ghi nào!");
                    } else {
                        notificationService.displaySuccess(`Có ${result.data.TotalCount} bản ghi được tìm thấy`);
                    }
                    $scope.products = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pageCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log("Load Failed");
                }
            );
        }

        function del(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                apiService.del(`/api/product/delete?id=${id}`,null,
                    function (result) {
                        notificationService.displaySuccess("Xóa thành công!");
                        search();
                    },
                    function (error) {
                        notificationService.displayWarning("Có lỗi rồi!");
                    }

                );
            })
            
        }


        $scope.getproducts();
    }
})(angular.module("productModule"));