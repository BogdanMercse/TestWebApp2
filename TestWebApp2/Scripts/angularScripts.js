var app = angular.module("MyApp", []);
app.controller("MyController", function ($scope, $http) {

    $scope.updateFormVisible = false;

    $scope.GetCustomerNames = function () {
        $http(
            {
                method: 'GET',
                url: '/MultipleTables/GetCustomerNames'
            }).then(function (customerTable) {
                $scope.customerList = customerTable.data;
            });
    }

    $http(
        {
            method: 'GET',
            url: '/MultipleTables/GetMosquitoNetRequests'
        }).then(function (mosquitoTable) {
            $scope.mosquitoNetRequestsList = mosquitoTable.data;
        });

    $scope.DeleteCustomer = function (customerId) {
        $scope.Customer = {};
        $scope.Customer.customerId = customerId;
        $http({
            method: "post",
            url: "/MultipleTables/Delete",
            datatype: "json",
            data: JSON.stringify($scope.Customer)
        }).then(function (response) {
            alert(response.data);
            $scope.GetCustomerNames();
        })
    };

    $scope.CreateCustomer = function (Customer) {
        $scope.Customer = {};
        $scope.Customer.customerName = $scope.customerName;
        $scope.Customer.customerEmail = $scope.customerEmail;
        $scope.Customer.customerPhone = $scope.customerPhone;

        $http({
            method: "post",
            url: "/MultipleTables/CreateCustomer",
            datatype: "json",
            data: JSON.stringify($scope.Customer)
        }).then(function (response) {
            alert("response:" + response.data);
            $scope.Customer.customerName = "";
            $scope.Customer.customerEmail = "";
            $scope.Customer.customerPhone = "";
            $scope.GetCustomerNames();
        })
    };

    $scope.CreateNetRequest = function (Customer) {
        $scope.MosquitoNetRequest = {};
        $scope.MosquitoNetRequest.customerId = Customer.customerId;
        $scope.MosquitoNetRequest.length = $scope.length;
        $scope.MosquitoNetRequest.height = $scope.height;

        $http({
            method: "post",
            url: "/MultipleTables/CreateNetRequest",
            datatype: "json",
            data: JSON.stringify($scope.MosquitoNetRequest)
        }).then(function (response) {
            alert(response.data);
            $scope.MosquitoNetRequest.customerId = "";
            $scope.MosquitoNetRequest.length = "";
            $scope.MosquitoNetRequest.height = "";
        })
    };

    $scope.ShowUpdateForm = function (Customer) {
        $scope.updateFormVisible = true;
        $scope.customerIdUpdate = Customer.customerId;
        $scope.customerNameUpdate = Customer.customerName;
        $scope.customerEmailUpdate = Customer.customerEmail;
        $scope.customerPhoneUpdate = Customer.customerPhone;
    };

    $scope.UpdateCustomer = function (Customer) {
        $scope.Customer = {};
        $scope.Customer.customerId = $scope.customerIdUpdate;
        $scope.Customer.customerName = $scope.customerNameUpdate;
        $scope.Customer.customerEmail = $scope.customerEmailUpdate;
        $scope.Customer.customerPhone = $scope.customerPhoneUpdate;

        $http({
            method: "post",
            url: "/MultipleTables/UpdateCustomer",
            datatype: "json",
            data: JSON.stringify($scope.Customer)
        }).then(function (response) {
            alert(response.data);
            $scope.Customer.customerName = "";
            $scope.Customer.customerEmail = "";
            $scope.Customer.customerPhone = "";
            $scope.GetCustomerNames();
        })
    };
});