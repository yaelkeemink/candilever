// Write your Javascript code.
"use strict";

function restoreButton() {
    document.getElementById('addToCart').className = 'glyphicon glyphicon-shopping-cart btn btn-info';
}

function placeOrder(klantnummer) {
    console.log("bestel");

    var shopCart = JSON.parse(localStorage.getItem("Shopcart").toLowerCase());
    console.log(shopCart);

    var bestelling = {
        "bestellingnummer": 0,
        "klantnummer": klantnummer,
        "artikelen": shopCart,
        "bestelDatum": undefined
    }

    if (shopCart != undefined) {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/api/Bestelling",
            data: JSON.stringify(bestelling),
            async: false,
            success: function(data) {
                alert(data);
            }, error: function (err) {
                console.log(err);
            }
        });
    } else {
        alert("De winkelwagen is leeg!");

    }
}

function parseKlant() {
    var land = document.getElementById('land');
    var value = land.options[land.selectedIndex].value;

    var klant = {
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

    console.log(klant);

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Klant",
        data: JSON.stringify(klant),
        async: false,
        success: function (data) {
            placeOrder(data);
        },
        error: function(data) {
            console.log(data);
        }
    })
}

function addArtikelToCart(artikel) {

    var shopCart = getShopCartFromLocalStorage();

    addToShopCartArtikel(artikel, shopCart);

    console.log(shopCart);
    alert(artikel.Naam + " toegevoegd aan winkelwagen");

    saveShopCartInLocalStorage(shopCart);

    document.getElementById('addToCart').className = 'glyphicon glyphicon-ok btn btn-success';

    window.setTimeout(restoreButton, 1000);
}

function addToShopCartArtikel(artikel, shopCart) {
    var artikelAlreadyAdded = adjustAantalArtikelenInCart(artikel, shopCart);

    if (!artikelAlreadyAdded) {
        var artikel = createNewArtikel(artikel);
        shopCart.push(artikel);
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

function adjustAantalArtikelenInCart(artikel, shopCart) {
    var artikelAlreadyAdded = false;

    for (var i = 0, len = shopCart.length; i < len; i++) {
        if (shopCart[i].artikelnummer == artikel.Artikelnummer) {
            shopCart[i].aantal = shopCart[i].aantal + 1;
            artikelAlreadyAdded = true;
        }
    }

    return artikelAlreadyAdded;
}

function getShopCartFromLocalStorage() {
    var shopCart = JSON.parse(localStorage.getItem("Shopcart"));

    if (shopCart == undefined) {
        shopCart = new Array();
    }

    return shopCart;
}

function saveShopCartInLocalStorage(shopCart) {
    var newShopCart = JSON.stringify(shopCart);
    localStorage.setItem("Shopcart", newShopCart);
}