'use strict';

app.controller("PersonAddressController", ['$scope', 'sendPerson', 'sendPersonDocument', 'WebApi', function ($scope, sendPerson, sendPersonDocument, WebApi) {
    // $scope.courses = bootstrappedData.courses; 
    $scope.Person = {};
    $scope.Person.StreetName = "";
    $scope.Person.Number = "";
    $scope.Person.Neighbourhood = "";
    $scope.Person.State = "São Paulo-SP";
    $scope.Person.Country = "Brasil-BR";
    $scope.Person.City = "São Paulo";
    $scope.Person.PostCode = "";
    $scope.Person.Coordinates = "";
    $scope.Person.ContactPhoneNumber = "";
    $scope.Person.ServerInstanceId = "1";
    $scope.Person.CreatedBy = getCookie("User");
    $scope.Person.ContactEMail = "";
    $scope.Person.PersonIntegrationID = "";

    $scope.Person.CNPJ = "";




    $scope.WebApiCore = WebApi.Core();

    $scope.PersonIntegrationList = [];

    $scope.ListPersonIntegrationCallback = function (data) {
        $scope.$apply(function () {

            $scope.PersonIntegrationList = data;
        });
    };

    $scope.CoordinatesCallback = function (data) {
        $scope.$apply(function () {
            $scope.Person.Coordinates = data;
            $scope.Person.Neighbourhood = data;
        });
    };

    $.post($scope.WebApiCore + '/api/Person/ListPersonsNotAddress', null, $scope.ListPersonIntegrationCallback);



    $scope.sendPerson = function (Person) {

        var urlEnd = "http://maps.google.com/maps/api/geocode/json?address=" + Person.StreetName + " " + Person.Number + " " + Person.City + " " + Person.State + " " + Person.Country;


        $.ajax({
            url: urlEnd,
            async: false,
            dataType: 'json',
            success: function (data) {
                Person.Coordinates = "(" + data.results[0].geometry.location.lat + ", " + data.results[0].geometry.location.lng + ")";
                Person.Neighbourhood = data.results[0].address_components[2].long_name;
                if (data.results[0].address_components.length > 7)
                    Person.PostCode = data.results[0].address_components[7].long_name;
                else
                    Person.PostCode = "0000-000";
                Person.StreetName = Person.StreetName + ", " + Person.Number + " - " + Person.Neighbourhood + ", " + Person.City + " - " + data.results[0].address_components[5].short_name + ", " + Person.PostCode + ", " + Person.Country;
            }
        });
        Person.CreatedBy = $scope.Person.CreatedBy;
        Person.ServerInstanceId = $scope.Person.ServerInstanceId;


        // $.get(urlEnd, false, $scope.CoordinatesCallback);
        var uri = $scope.WebApiCore + "/api/Person/PersonAdrdressAdd";



        sendPerson.send(uri, Person, uploadSuccess, uploadError);

       

    };

    var uploadSuccess = function () {
        alert('sucesso');
    };
    var uploadError = function () {
        alert('Erro');
    };


}]);