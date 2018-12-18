﻿(function (app) {
    app.controller("productCategoryListController", productCategoryListController);


    productCategoryListController.$inject = [
        '$scope', 'apiService',
        'notificationService', '$ngBootbox',
        '$filter'
    ];
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getProductCategories = getProductCategories;
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
            apiService.del('/api/productCategory/deletemulti', config,
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
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                isAll = false;
            }          
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function search() {
            getProductCategories();
        }
        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 2
                }
            }

            apiService.get('/api/productCategory/getall', config,
                function (result) {
                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning("Không có bản ghi nào!");
                    } else {
                        notificationService.displaySuccess(`Có ${result.data.TotalCount} bản ghi được tìm thấy`);
                    }
                    $scope.productCategories = result.data.Items;
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
                apiService.del(`/api/productCategory/delete?id=${id}`,null,
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


        $scope.getProductCategories();
    }
})(angular.module("productCategoryModule"));