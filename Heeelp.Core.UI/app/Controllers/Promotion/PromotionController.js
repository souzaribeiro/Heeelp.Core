'use strict';

app.controller("PromotionManagementController", ['$scope', 'sendPromotion', 'WebApi','send', function ($scope, sendPromotion, WebApi,send) {
    // $scope.courses = bootstrappedData.courses; 
    var profile = $.parseJSON(getCookie("profile"));
    $scope.Promotion = {};
    $scope.Promotion.Title = "";
    $scope.Promotion.ShortDescription = "";
    $scope.Promotion.FullDescription = "";
    $scope.Promotion.Alert = "";

    $scope.Promotion.NormalPrice = "";
    $scope.Promotion.NumberOfAvailableCoupons = "";
    $scope.Promotion.DiscountePercentege = "";

    $scope.Promotion.StartDateUTC = "";
    $scope.Promotion.ValidUntilUTC = "";


    //lista
    $scope.Promotion.ExpertiseId = "";
    $scope.Promotion.PromotionTypeId = "";
    $scope.Promotion.PromotionBillingModelId = "";
    $scope.Promotion.PromotionRecurrenceId = "4";
    $scope.Promotion.PromotionPaymentTypeId = "";
    $scope.Promotion.CurrencyId = "1";
    $scope.Promotion.PersonIntegrationID = "";
    $scope.Promotion.PersonId = "";
    $scope.Promotion.PriceReference = "";

    //lista que não esta no banco
    $scope.Promotion.RequiredTimeForActivation = "0";  //criar lista

    $scope.Promotion.ExpertisePromotionCostReferenceId = "1";
    $scope.Promotion.HeeelpTaxValue = "";

    $scope.ExpertiseList = [];
    $scope.PromotionTypeList = [];
    $scope.PromotionBillingModelList = [];
    $scope.PromotionRecurrenceList = [];
    $scope.PromotionPaymentTypeList = [];
    $scope.CurrencyList = [];
    $scope.PersonIntegrationList = [];
    $scope.UserList = [];



    $scope.WebApiCore = WebApi.Core();
    $scope.WebApiPromotion = WebApi.Promotion();
    $scope.WebApiMarketing = WebApi.Marketing();

    $scope.ListExpertiseCallback = function (data) {
        $scope.$apply(function () {

            $scope.ExpertiseList = data;
        });
    };
    $scope.ListPromotionTypeCallback = function (data) {
        $scope.$apply(function () {

            $scope.PromotionTypeList = data;
        });
    }; $scope.ListPromotionBillingModelCallback = function (data) {
        $scope.$apply(function () {

            $scope.PromotionBillingModelList = data;
        });
    };
    $scope.ListPromotionRecurrenceCallback = function (data) {
        $scope.$apply(function () {

            $scope.PromotionRecurrenceList = data;
        });
    }; $scope.ListPromotionPaymentTypeCallback = function (data) {
        $scope.$apply(function () {

            $scope.PromotionPaymentTypeList = data;
        });
    };


    var ListPersonIntegrationCallback = function (data) {
        $scope.$apply(function () {

            $scope.PersonIntegrationList = data;
        });
    };
    //$scope.ListCurrencyCallback = function (data) {
    //    $scope.$apply(function () {

    //        $scope.CurrencyList = data;
    //    });
    //};

    $scope.ListUserCallback = function (data) {
        $scope.$apply(function () {

            $scope.UserList = data;
        });
    };




    //Tem que listar como checkbox
    //$.post($scope.WebApiCore + '/api/Expertise/ListSubExpertises?ExpertiseFatherId=' + "ExpertiseFatherId", null, $scope.ListSubExpertiseCallback);

    $.post($scope.WebApiPromotion + '/api/PromotionType/GetPromotionTypeList', null, $scope.ListPromotionTypeCallback);
    $.post($scope.WebApiPromotion + '/api/PromotionBillingModel/GetPromotionBillingModelList', null, $scope.ListPromotionBillingModelCallback);
    $.post($scope.WebApiPromotion + '/api/PromotionRecurrence/GetPromotionRecurrenceList', null, $scope.ListPromotionRecurrenceCallback);
    $.post($scope.WebApiPromotion + '/api/PromotionPaymentType/GetPromotionPaymentTypeList', null, $scope.ListPromotionPaymentTypeCallback);
    send.get( $scope.WebApiCore + '/api/enrollment/ListCompanies', null, ListPersonIntegrationCallback, null);
    //$.post($scope.WebApiCore + 'api/enrollment/ListCompanies', null, $scope.ListPersonIntegrationCallback);
    //$.post($scope.WebApiCore + '/api/Currency/ListCurrencys', null, $scope.ListCurrencyCallback);
    $.post($scope.WebApiCore + '/api/Expertise/ListMainExpertises', null, $scope.ListExpertiseCallback);




    $scope.SubListExpertiseCallback = function (data) {
        if (data != "") {
            $scope.$apply(function () {
                var lnk = "";
                lnk = $('<select id="mySelect"></select>').html('<option></option>');
                $.each(data, function (index, item) {
                    lnk.append('<option value="' + item.ExpertiseId + '">' + item.Name + '</option>')
                });

                lnk.on("change", function (e) {
                    $(this).nextAll().remove();
                    $scope.Promotion.ExpertiseId = $(this).val();
                    callSubExpertise();
                    $scope.optionChangedExpertise($(this).val());

                });

                $('#listExpertise').append(lnk);
            });
        } 
    }

    $scope.optionPerson = function (personId) {
        console.debug(personId);
        $('#listExpertise').empty();
        $.get($scope.WebApiCore + '/api/Person/ListPersonExpertiseByPerson/' + $scope.Promotion.PersonId, null, $scope.SubListExpertiseCallback);
    }

    var callSubExpertise = function () {
        $.get($scope.WebApiCore + '/api/Expertise/ListSubExpertises?ExpertiseFatherId=' + $scope.Promotion.ExpertiseId, null, $scope.SubListExpertiseCallback);
    }


    $scope.optionChangedExpertise = function (expertiseId) {
        console.debug(expertiseId);
        var priceCriteria = new Object();
        priceCriteria.ExpertiseId = expertiseId;
        //$.get($scope.WebApiMarketing + '/api/PricePolicies/GetHeeelpTaxValue?jsonData=' + JSON.stringify(priceCriteria), null, $scope.HeeelpTaxValueCallback);
        $.get($scope.WebApiMarketing + '/api/PricePolicies/GetPriceDefinition?jsonData=' + JSON.stringify(priceCriteria),
            function (data) {
                $scope.Promotion.PriceReference = data;
            });
    }

   

    $scope.sendPromotion = function (Promotion) {

        var url = $scope.WebApiCore + "/api/Promotion/AddDiscountPromotion";

        $.ajax({
            type: 'POST',
            url: url,
            crossDomain: true,
            dataType: "json",
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/x-www-form-urlencoded',
            data: Promotion,
            success: function (response) {
                
               
            },
            error: function (xhr, status, error) {
              

            }
        });



        //var file = $scope.files;
        //var uploadUrl = $scope.WebApiCore + "/api/Promotion/PromotiontAdd";

        //if ($scope.Promotion.PriceReference.PriceReferenceModel) {
        //    Promotion.HeeelpTaxValue = $scope.Promotion.PriceReference.EditorialPriceinHeeelps;
        //} else {
        //    var valorDesconto = ($scope.Promotion.NormalPrice * ($scope.Promotion.DiscountePercentege / 100));
        //    var valorAposDesconto = $scope.Promotion.NormalPrice - valorDesconto;
        //    var valorHeeelp = valorAposDesconto * $scope.Promotion.PriceReference.ReferencePercentual / 100;
        //    Promotion.HeeelpTaxValue = valorHeeelp;
        //}


        //sendPromotion.send(file, uploadUrl, uploadSuccess, uploadError, Promotion);

    };
    $scope.sendPromotionImages = function (Promotion) {
        var file = $scope.files;
        var uploadUrl = $scope.WebApiCore + "/api/Promotion/PromotiontAdd";

        if ($scope.Promotion.PriceReference.PriceReferenceModel) {
            Promotion.HeeelpTaxValue = $scope.Promotion.PriceReference.EditorialPriceinHeeelps;
        } else {
            var valorDesconto = ($scope.Promotion.NormalPrice * ($scope.Promotion.DiscountePercentege / 100));
            var valorAposDesconto = $scope.Promotion.NormalPrice - valorDesconto;
            var valorHeeelp = valorAposDesconto * $scope.Promotion.PriceReference.ReferencePercentual / 100;
            Promotion.HeeelpTaxValue = valorHeeelp;
        }


        sendPromotion.send(file, uploadUrl, uploadSuccess, uploadError, Promotion);

    };


    $scope.calculatePromotion = function () {

        if ($scope.Promotion.PriceReference.PriceReferenceModel) {
            alert("Preço por Cupom = " + $scope.Promotion.PriceReference.EditorialPriceinHeeelps);
            $scope.HeeelpTaxValue = $scope.Promotion.PriceReference.EditorialPriceinHeeelps;
        } else {
            var valorDesconto = ($scope.Promotion.NormalPrice * ($scope.Promotion.DiscountePercentege / 100));
            var valorAposDesconto = $scope.Promotion.NormalPrice - valorDesconto;
            var valorHeeelp = valorAposDesconto * $scope.Promotion.PriceReference.ReferencePercentual / 100;
            $scope.HeeelpTaxValue = valorHeeelp;
            alert("Preço por Cupom = " + valorHeeelp);
        }
    }

    var uploadSuccess = function () {
        alert('sucesso');
    };
    var uploadError = function () {
        alert('Erro');
    };


    //  Eventos de User Experience


    $('#fileToUpload').on('change', function () {

        $("#BtnSelecionar").empty();
        $("#BtnSelecionar").append("Imagem selecionada");
        $("#BtnSelecionar").addClass('btn-success');
        $(".progress-bar").css('width', '80%');
    });
    

}]);

