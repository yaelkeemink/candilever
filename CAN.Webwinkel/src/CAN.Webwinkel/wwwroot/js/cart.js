// Write your Javascript code.
"use strict";


function addArtikelToCart(artikel) {
    var artikelnummer = artikel.Artikelnummer;

    addToShopCartAnimation(artikelnummer);

    var guid = localStorage.getItem('cartGuid');

    if (guid === undefined || guid === null) {
        var parsedArtikel = createNewArtikel(artikel);
        var cart = parseNewShoppingCart(parsedArtikel);
        postNewShoppingCart(cart);
    } else {
        var parsedArtikel = createNewArtikel(artikel);
        var cart = parseShoppingCart(parsedArtikel, guid);
        addArtikelToShopcart(cart);
    }
}

function parseShoppingCart(artikel, guid) {
    var cart = {
        "winkelmandjeNummer": guid,
        "artikelen": [
            artikel
        ]
    }

    return cart;
}

function parseNewShoppingCart(artikel) {
    var cart = {
        "artikelen": [
            artikel
        ]
    }

    return cart;
}

function addToShopCartAnimation(artikelnummer) {
    var divId = parseInt(artikelnummer);

    document.getElementById(divId).className = 'glyphicon glyphicon-ok btn btn-success';

    window.setTimeout(function () { restoreButton(divId) }, 1000);
}

function restoreButton(divId) {
    document.getElementById(divId).className = 'glyphicon glyphicon-shopping-cart btn btn-info';
}

function createNewArtikel(artikel) {
    return {
        "artikelnummer": artikel.Artikelnummer,
        "naam": artikel.Naam,
        "aantal": 1,
        "leverancier": artikel.Leverancier,
        "leverancierCode": artikel.LeverancierCode
    };
}

function postNewShoppingCart(cart) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Winkelmandje",
        data: JSON.stringify(cart),
        async: false,
        success: function (data) {
            localStorage.setItem('cartGuid', data);
        }, error: function (err) {
            console.log(err);
        }
    });
}

function addArtikelToShopcart(cart) {
    $.ajax({
        type: "PUT",
        contentType: "application/json",
        url: "/api/Winkelmandje",
        data: JSON.stringify(cart),
        success: function (data) {
            localStorage.setItem('cartGuid', data);
        }, error: function (err) {
            console.log(err);
        }
    });
}