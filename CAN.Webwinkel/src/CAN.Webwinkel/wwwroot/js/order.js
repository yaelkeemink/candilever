function placeOrder() {
    var cartGuid = localStorage.getItem('cartGuid');

    if (cartGuid === undefined || cartGuid === null) {
        showMessage("error", "Er is geen winkelwagen bekend bij uw sessie.");
    } else {
        var klant = createKlant();

        postKlant(klant, cartGuid);
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
        "straatnaam": document.getElementById('straatnaam').value,
        "woonplaats":document.getElementById('woonplaats').value,
        "land": value
    }
}


function createBestelling(cartGuid, klant, klantnummer) {
    return {
        "klantnummer": klantnummer,
        "volledigeNaam": klant.voornaam + " " + klant.tussenvoegsels + " " + klant.achternaam,
        "postcode": klant.postcode,
        "adres": klant.straatnaam,
        "huisnummer": klant.huisnummer,
        "land": klant.land,
        "winkelmandjeNummer": cartGuid,
        "status": 0
    }
}

function postBestelling(bestelling) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Winkelmandje/Finish",
        data: JSON.stringify(bestelling),
        success: function (data) {
            localStorage.removeItem('cartGuid');
            localStorage.removeItem('klantnummer');
            showMessage("success", "De Bestelling is Succesvol geplaatst.");
        }, error: function (err) {
            console.log(err);
            showMessage("error", "Er is iets misgegaan bij het plaatsen van de bestelling.");
        }
    });
}

function postKlant(klant, cartGuid) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: "/api/Klant",
        data: JSON.stringify(klant),
        success: function (data) {
            localStorage.setItem('klantnummer', data);
            if (data !== null || data !== undefined) {
                var klantnummer = parseInt(data);

                var bestelling = createBestelling(cartGuid, klant, klantnummer);
                console.log(bestelling);

                postBestelling(bestelling);
            }
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