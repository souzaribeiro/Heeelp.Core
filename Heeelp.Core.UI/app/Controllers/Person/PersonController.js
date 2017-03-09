'use strict';

app.controller("PersonController", ['$scope', 'send', 'WebApi', 'toastr', function ($scope, send, WebApi, toastr) {


    //  Eventos de User Experience


    $('#fileToUpload').on('change', function () {

        $("#BtnSelecionar").empty();
        $("#BtnSelecionar").append("Imagem selecionada");
        $("#BtnSelecionar").addClass('btn-success');
        $(".progress-bar").css('width', '80%');
    });

    $('#txtCompanyName').on('keyup', function () {
        $(".progress-bar").css('width', '20%');
    });

    $('#txtCompanyFantasyName').on('keyup', function () {
        $(".progress-bar").css('width', '40%');
    });

    $('#txtCompanyPhoneNumber').on('keyup', function () {
        $(".progress-bar").css('width', '50%');
    });
    //  Fim dos Eventos de User Experience


    $scope.Person = {};
    $scope.Person.CompanyInformation = {};
    $scope.CompanyExpertise = {};
    $scope.PersonFile = {};
    $scope.Person.CompanyInformation.StreetName = "";
    $scope.Person.CompanyInformation.Number = "";
    $scope.Person.CompanyInformation.PostCode = "";
    $scope.Person.CompanyInformation.CityId = "";
    $scope.Person.CompanyInformation.StateId = "";
    $scope.Person.CompanyInformation.Neighbourhood = "";
    $scope.Person.CompanyInformation.ContactEmail = "";
    $scope.Person.CompanyInformation.DocumentNumber = "";
    $scope.Person.CompanyInformation.Complement = "";
    $scope.Person.CompanyInformation.PersonIntegrationID = "";

    $scope.CompanyExpertise.CompanyId = "";
    $scope.CompanyExpertise.ExpertiseId = 0;
    $scope.BlockControls = false;

    $scope.Person.CompanyName = "";
    $scope.Person.FantasyName = "";
    $scope.Person.CompanyPhoneNumber = "";
    $scope.Person.ManagerName = "";
    $scope.Person.ManagerSmartPhoneNumber = "";
    $scope.Person.ManagerEmail = "";
    $scope.Person.CustomClubName = "";
    $scope.Person.CustomHeeelpPersonDomain = "";
    $scope.Person.CompanyType = "";

    $scope.ExpertiseList = [];
    $scope.StateList = [];
    $scope.CityList = [];
    $scope.WebApiCore = WebApi.Core();
    $scope.controller = '';
    $scope.PersonIntegrationList = [];


    $scope.PersonFile.PersonIntegrationID = "";


    //quando precisar do expertise descomentar
    $scope.SubListExpertiseCallback = function (data) {
        if (data != "") {
            $scope.$apply(function () {
                var lnk = "";
                lnk = $('<select class="form-control" id="mySelect"></select>').html('<option></option>');
                $.each(data, function (index, item) {
                    lnk.append('<option value="' + item.ExpertiseId + '">' + item.Name + '</option>')
                });

                lnk.on("change", function (e) {
                    $(this).nextAll().remove();
                    $scope.CompanyExpertise.ExpertiseId = $(this).val();
                    callSubExpertise();

                });

                $('#listExpertise').append(lnk);
            });
        }
    }

    $scope.ListStatesCallback = function (data) {
        $scope.$apply(function () {

            $scope.StateList = data;
        });
    }
    $scope.ListCitiesCallback = function (data) {
        $scope.$apply(function () {

            $scope.CityList = data;
        });
    }
    var ListPersonIntegrationCallback = function (data) {
        $scope.$apply(function () {

            $scope.PersonIntegrationList = data;
        });
    };
    $scope.SelectedCompanyCallback = function (data) {
        $scope.$apply(function () {
            $scope.PersonDetails = data;
            $scope.Person.CompanyInformation.StreetName = data.StreetName;
            $scope.Person.CompanyInformation.Number = data.Number;
            $scope.Person.CompanyInformation.PostCode = data.PostCode;
            $scope.Person.CompanyInformation.CityId = data.CityId;
            $scope.Person.CompanyInformation.StateId = data.StateId;
            $scope.Person.CompanyInformation.Neighbourhood = data.Neighbourhood;
            $scope.Person.CompanyInformation.ContactEmail = data.ContactEmail;
            $scope.Person.CompanyInformation.DocumentNumber = data.DocumentNumber;
            $scope.Person.CompanyInformation.Complement = data.Complement;
        });
    }
    //$.post($scope.WebApiCore + '/api/Person/ListPersons', null, $scope.ListPersonCallback);
    send.get($scope.WebApiCore + 'api/enrollment/ListExpertises/null', null, $scope.SubListExpertiseCallback);
    send.get($scope.WebApiCore + 'api/SystemDomainValues/GetStates', null, $scope.ListStatesCallback);
    send.get($scope.WebApiCore + 'api/SystemDomainValues/GetCities', null, $scope.ListCitiesCallback);
    send.get($scope.WebApiCore + '/api/enrollment/ListCompanies', null, ListPersonIntegrationCallback, null);

    $scope.optionPerson = function (CompanyIntegrationCode) {
        $scope.SelectedCompanyIntegrationCode = CompanyIntegrationCode;
        send.get($scope.WebApiCore + '/api/enrollment/GetCompany/' + CompanyIntegrationCode, null, $scope.SelectedCompanyCallback, null);
    }

    var callSubExpertise = function () {
        send.get($scope.WebApiCore + 'api/enrollment/ListExpertises/' + $scope.CompanyExpertise.ExpertiseId, null, $scope.SubListExpertiseCallback);
    }

    $scope.optionCompanyType = function (companyType) {
        switch (companyType) {
            case 'AddServiceProvider':
                $scope.controller = "/api/enrollment/AddServiceProvider";
                break;
            case 'AddCoworking':
                $scope.controller = "/api/enrollment/AddCoworking";
                break;
            case 'AddPartnerCompany':
                $scope.controller = "/api/enrollment/AddPartnerCompany";
                break;

        }
    }

    $scope.sendPerson = function (Person) {
        if (validatePersonForm(Person)) {
            var uri = $scope.WebApiCore + $scope.controller;
            send.post(uri, Person, SuccessAddPersonCallBack, ErrorAddPersonCallback);
        }
    };
    $scope.EditPersonInformation = function (person) {
        $scope.Person.CompanyInformation.IntegrationCode = person.CompanyIntegrationCode;
        if ($scope.Person.CompanyInformation.StreetName != "") {
            var uri = $scope.WebApiCore + "/api/enrollment/AddCompanyInformation";
            send.post(uri, $scope.Person.CompanyInformation, SuccessInformationCallBack, ErrorInformationCallback);
        }
    }
    $scope.sendPersonImages = function (person) {
        $scope.PersonFile.PersonIntegrationID = person.CompanyIntegrationCode;
        var file = $scope.files;
        var uri = $scope.WebApiCore + "/api/enrollment/AddCompanyFile/" + person.CompanyIntegrationCode;
        send.postFIle(uri, file, SuccessFileCallBack, ErrorFileCallback);

    };

    //Validacoes
    var validatePersonForm = function (Person) {
        if ($scope.CompanyExpertise.ExpertiseId == "") {
            alert("Informe o Expertise");
            return false;
        }
        if (Person.CompanyName == "" || Person.FantasyName == "" ||
            Person.CompanyPhoneNumber == "" || Person.ManagerName == "" ||
            Person.ManagerSmartPhoneNumber == "" || Person.ManagerEmail == "")
            return false;
        else
            return true;
    }


    //retornos
    var SuccessAddPersonCallBack = function (data) {
        if ($scope.CompanyExpertise.ExpertiseId != 0) {
            $scope.CompanyExpertise.CompanyId = data;
            var uri = $scope.WebApiCore + "api/enrollment/AddCompanyExpertise";
            send.post(uri, $scope.CompanyExpertise, SuccessExpertiseCallBack, ErrorExpertiseCallback);
        }
        toastr.success('Solicitação enviada com sucesso!', 'Sucesso!');
        $scope.BlockControls = false;
        $scope.CloseAddCompanyModal();
    };


    var ErrorAddPersonCallback = function (data) {
        console.log(data);
        $scope.BlockControls = false;
        toastr.error('Erro ao enviar solicitacao!', 'Erro!');
    };

    var SuccessInformationCallBack = function (data) {
        toastr.success('Solicitação enviada com sucesso!', 'Sucesso!');
        $scope.ClosePersonInformationModal();
        $scope.BlockControls = false;
    };
    var ErrorInformationCallback = function (data) {
        console.log(data);
        $scope.BlockControls = false;
        toastr.error('Erro ao enviar solicitacao!', 'Erro!');
    };


    var SuccessFileCallBack = function (data) {
        toastr.success('Solicitação enviada com sucesso!', 'Sucesso!');
        $scope.BlockControls = false;
        $scope.CloseModalUploadImage();

    };
    var ErrorFileCallback = function (data) {
        console.log(data);
        $scope.BlockControls = false;
        toastr.error('Erro ao enviar solicitacao!', 'Erro!');
    };


    var SuccessExpertiseCallBack = function (data) {    };
    var ErrorExpertiseCallback = function (data) {
        console.log(data);
    };


    $scope.OpenPersonInformationModal = function (person) {
        $scope.selectedPerson = person;
        $("#EditPersonInformationModal").fadeIn();

    }
    $scope.ClosePersonInformationModal = function () {
        $("#EditPersonInformationModal").fadeOut();
    }
    $scope.UploadPersonImage = function (person) {
        $scope.selectedPerson = person;
        $("#AddPersonImageModal").fadeIn();

    }
    $scope.CloseModalUploadImage = function () {
        $("#AddPersonImageModal").fadeOut();
    }

    $scope.AddCompany = function (promotion) {
        $("#AddCompanyModal").fadeIn(300);
    };
    $scope.CloseAddCompanyModal = function () {
        $("#AddCompanyModal").fadeOut();
    }




}]);