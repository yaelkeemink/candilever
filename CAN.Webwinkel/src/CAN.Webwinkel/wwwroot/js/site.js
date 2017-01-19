// Write your Javascript code.
"use strict";


function addArtikelToCart(artikel) {
    var shopCart = getShopCartFromLocalStorage();

    var artikelnummer = artikel.Artikelnummer;

    addToShopCartArtikel(artikel, shopCart);

    saveShopCartInLocalStorage(shopCart);

    addToShopCardAnimation(artikelnummer);
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

function addToShopCardAnimation(artikelnummer) {
    var divId = parseInt(artikelnummer);

    document.getElementById(divId).className = 'glyphicon glyphicon-ok btn btn-success';

    window.setTimeout(function () { restoreButton(divId) }, 1000);
}

function restoreButton(divId) {
    document.getElementById(divId).className = 'glyphicon glyphicon-shopping-cart btn btn-info';
}



function placeOrder() {
    var shopCart = JSON.parse(localStorage.getItem("Shopcart").toLowerCase());

    var klant = createKlant();

    postKlantData(klant);

    var klantnummer = parseInt(localStorage.getItem('klantnummer'))
    var bestelling = createBestelling(shopCart, klantnummer);

    if (shopCart !== undefined) {
        postBestelling(bestelling);
    }
}

function createBestelling(shopCart, klantnummer) {
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
    var land = document.getElementById('land');
    var value = land.options[land.selectedIndex].value;

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
        async: false,
        success: function (data) {
            localStorage.setItem('Shopcart', undefined);
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
        async: false,
        success: function (data) {
            localStorage.setItem('klantnummer', data);
        },
        error: function (data) {
            console.log(data);
        }
    })
}