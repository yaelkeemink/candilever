﻿function placeOrder() {
    var shopCart = getShopCart();

    if (shopCart !== undefined && shopCart !== null) {
        var klant = createKlant();

        postKlantData(klant);

        var klantnummer = localStorage.getItem('klantnummer');

        if (klantnummer !== null || klantnummer !== undefined) {
            klantnummer = parseInt(klantnummer);
            var bestelling = createBestelling(shopCart, klantnummer);

            postBestelling(bestelling);
        }
        else {
            showMessage("error", "Er is iets misgegaan bij het aanmaken van u klant gegevens.");
        }
    }

    else {
        showMessage("info", "U heeft geen artikelen in de Winkelwagen!");
    }
}

function getShopcart() {
    var shopCart = localStorage.getItem("Shopcart");

    if (shopCart !== undefined || shopCart !== null) {
        shopCart = JSON.parse(shopCart);
    }

    return shopCart;
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


function createBestelling(shopCart, klantnummer) {
    return {
        "bestellingnummer": 0,
        "klantnummer": klantnummer,
        "artikelen": shopCart.artikelen,
        "bestelDatum": undefined,
        "status": 0
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

            showMessage("success", "De Bestelling is Succesvol geplaatst.");
        }, error: function (err) {
            console.log(err);
            showMessage("error", "Er is iets misgegaan bij het plaatsen van de bestelling.");
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

function showMessage(type, text) {
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
        else if (type === "error") {
            document.getElementById("message").classList.add("alert-danger");

            document.getElementById("messageheadertext").innerHTML = "Er is een fout opgetreden!"
            document.getElementById("messagetext").innerHTML = text;
        }
    }

    document.getElementById("message").style = "display:normal;";
}