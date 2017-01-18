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
    var shopCart = localStorage.getItem("Shopcart");

    if (shopCart === undefined || shopCart === null || shopCart === "undefined") {
        shopCart = new Array();
    } else {
        shopCart = JSON.parse(shopCart);
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

function createNewArtikel(artikel) {
    return {
        "artikelnummer": artikel.Artikelnummer,
        "naam": artikel.Naam,
        "prijs": artikel.Prijs,
        "aantal": 1
    };
}
