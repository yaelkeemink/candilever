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

    if (shopCart === undefined || shopCart === null) {
        shopCart = new Array();
    } else {
        shopCart = JSON.parse(localStorage.getItem("Shopcart"));
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
    var shopCart = localStorage.getItem("Shopcart");

    if (shopCart !== undefined && shopCart !== null) {
        shopCart = JSON.parse(shopCart.toLowerCase());
        var klant = createKlant();

        postKlantData(klant);

        var klantnummer = localStorage.getItem('klantnummer');

        if (klantnummer !== null || klantnummer !== undefined) {
            var klantnummer = parseInt(klantnummer);
            var bestelling = createBestelling(shopCart, klantnummer);

            postBestelling(bestelling);
        }
        else {
            showHiddenMessage("error", "Er is iets misgegaan bij het aanmaken van u klant gegevens.");
        }
    } else {
        showHiddenMessage("info", "U heeft geen artikelen in de Winkelwagen!");
    }
}

function createBestelling(shopCart, klantnummer) {
    return {
        "bestellingnummer": 0,
        "klantnummer": klantnummer,
        "artikelen": shopCart,
        "bestelDatum": undefined,
        "status": 0
    }
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
        success: function (data) {
            localStorage.removeItem('Shopcart');
            localStorage.removeItem('klantnummer');

            showHiddenMessage("success", "De Bestelling is Succesvol geplaatst, u krijgt later een validatie bericht.");
        }, error: function (err) {
            console.log(err);
            showHiddenMessage("error", "Er is iets misgegaan bij het plaatsen van de bestelling.");
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

function showHiddenMessage(type, text) {
    var message = document.getElementById("message");

    if (message !== undefined && message !== null) {
        if (type === "success") {
            document.getElementById("message").classList.add("alert-success");
            document.getElementById("message").classList.add("fade");
            document.getElementById("message").classList.add("in");

            document.getElementById("messageheadertext").innerHTML = "Success!"
            document.getElementById("messagetext").innerHTML = text;
        } else if (type === "info") {
            document.getElementById("message").classList.add("alert-info");

            document.getElementById("messageheadertext").innerHTML = "Winkelwagen Leeg!"
            document.getElementById("messagetext").innerHTML = text;
        }
        else if(type === "error") {
            document.getElementById("message").classList.add("alert-danger");

            document.getElementById("messageheadertext").innerHTML = "Er is een fout opgetreden!"
            document.getElementById("messagetext").innerHTML = text;
        }
    }

    document.getElementById("message").style = "display:normal;";
}