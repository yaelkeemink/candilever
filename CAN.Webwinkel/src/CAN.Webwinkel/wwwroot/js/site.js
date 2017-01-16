// Write your Javascript code.
"use strict";

function addToCart(artikel) {
    document.getElementById('addToCart').className = 'glyphicon glyphicon-shopping-cart btn btn-info animation';

    $('#addToCart').one('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend',   
    function(e) {
        // code to execute after transition ends
        document.getElementById('addToCart').className = 'glyphicon glyphicon-shopping-cart btn btn-info';
    });
}