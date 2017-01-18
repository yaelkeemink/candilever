window.onload = function () {
    var isSuccess = sessionStorage.getItem("bestellingSucces");

    if (isSuccess !== null && isSuccess !== undefined && isSuccess !== "undefined") {
        if (isSuccess) {
            var successMessage = document.getElementById("succes-message");
            var errorMessage = document.getElementById("error-message");

            if (successMessage !== undefined && successMessage !== null) {
                document.getElementById("succes-message").style = "display:normal;";
            }
            else if (errorMessage !== undefined && errorMessage !== null) {
                document.getElementById("error-message").style = "display:normal;";
            }
            sessionStorage.setItem("bestellingSucces", undefined);
        }
    }
}

function placeOrder() {
    var shopCart = getShopCartFromLocalStorage();
    if (shopCart !== "undefined" && shopCart !== undefined && shopCart !== null) {
        var klant = createKlant();
        var klantnummer = postKlantData(klant);
        var bestelling = createBestelling(shopCart, klantnummer);

        if (shopCart !== undefined) {
            postBestelling(bestelling);
        }
    } else {
        bestellingFailedMessage();
    }
}

function getShopCartFromLocalStorage() {
    var shopCart = localStorage.getItem("Shopcart");

    if (shopCart === "undefined" || shopCart == null) {
        shopCart = new Array();
    } else {
        shopCart = JSON.parse(shopCart);
    }

    return shopCart;
}

function createBestelling(shopCart, klantnummer) {
    return {
        "bestellingnummer": 0,
        "klantnummer": klantnummer,
        "artikelen": shopCart,
        "bestelDatum": undefined
    }
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
            bestellingIsSuccesMessage();
        }, error: function (err) {
            console.log(err);
        }
    });
}

function postKlantData(klant) {
    var klantnr = 0;

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Klant",
        data: JSON.stringify(klant),
        async: false,
        success: function (data) {
            klantnr = data;
        },
        error: function (data) {
            console.log(data);
        }
    });

    return klantnr;
}

function bestellingIsSuccesMessage() {
    sessionStorage.setItem("bestellingSucces", true);
}

function bestellingFailedMessage() {
    sessionStorage.setItem("bestellingSucces", false);
}
