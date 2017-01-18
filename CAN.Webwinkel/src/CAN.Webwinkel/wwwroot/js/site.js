﻿// Write your Javascript code.
"use strict";


function addArtikelToCart(artikel) {
    var shopCart = getShopCartFromLocalStorage();

    addToShopCartArtikel(artikel, shopCart);

    saveShopCartInLocalStorage(shopCart);

    addToShopCardAnimation();
}

function addToShopCartArtikel(artikel, shopCart) {
    var artikelAlreadyAdded = adjustAantalArtikelenInCart(artikel, shopCart);

    if (!artikelAlreadyAdded) {
        var art = createNewArtikel(artikel);
        shopCart.push(art);
    }
}

function getShopCartFromLocalStorage() {
    var shopCart = JSON.parse(localStorage.getItem("Shopcart"));

    if (shopCart === undefined || shopCart === null) {
        shopCart = new Array();
    }

    return shopCart;
}

function saveShopCartInLocalStorage(shopCart) {
    var newShopCart = JSON.stringify(shopCart);
    localStorage.setItem("Shopcart", newShopCart);
}

function adjustAantalArtikelenInCart(artikel, shopCart) {
    var artikelAlreadyAdded = false;

    if (shopCart !== undefined && shopCart !== null) {
        for (var i = 0, len = shopCart.length; i < len; i++) {
            if (shopCart[i].artikelnummer === artikel.Artikelnummer) {
                shopCart[i].aantal = shopCart[i].aantal + 1;
                artikelAlreadyAdded = true;
            }
        }
    }

    return artikelAlreadyAdded;
}

function addToShopCardAnimation() {
    document.getElementById('addToCart').className = 'glyphicon glyphicon-ok btn btn-success';
    window.setTimeout(restoreButton, 1000);
}

function restoreButton() {
    document.getElementById('addToCart').className = 'glyphicon glyphicon-shopping-cart btn btn-info';
}



function placeOrder() {
    var shopCart = JSON.parse(localStorage.getItem("Shopcart").toLowerCase());

    var klantnummer = parseKlant();
    var bestelling = createBestelling();

    if (shopCart !== undefined) {
        postBestelling(bestelling);
    }
}

function parseKlant() {
    var land = document.getElementById('land');
    var value = land.options[land.selectedIndex].value;

    var klant = createKlant();

    postKlantData(klant);
}

function createBestelling() {
    return {
        "bestellingnummer": 0,
        "klantnummer": klantnummer,
        "artikelen": shopCart,
        "bestelDatum": undefined
    }
}

function createNewArtikel(artikel) {
    return {
        "artikelnummer": artikel.Artikelnummer,
        "naam": artikel.Naam,
        "prijs": artikel.Prijs,
        "aantal": 1
    };
}

function createKlant() {
    return {
        "klantnummer": 0,
        "voornaam": document.getElementById('voornaam').value,
        "achternaam": document.getElementById('achternaam').value,
        "tussenvoegsels": document.getElementById('tussenvoegsel').value,
        "postcode": document.getElementById('postcode').value,
        "telefoonnummer": document.getElementById('telefoonnummer').value,
        "email": document.getElementById('email').value,
        "huisnummer": document.getElementById('huisnummer').value,
        "adres": document.getElementById('straatnaam').value,
        "land": value
    }
}

function postBestelling(bestelling) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Bestelling",
        data: JSON.stringify(bestelling),
        success: function (data) {
            localStorage.setItem('Shopcart') = new Array();
        }, error: function (err) {
            console.log(err);
        }
    });
}

function postKlantData(klant) {

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Klant",
        data: JSON.stringify(klant),
        success: function (data) {
            return data;
        },
        error: function (data) {
            console.log(data);
        }
    })
}