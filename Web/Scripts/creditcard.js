$(function () {
    $('[data-toggle="popover"]').popover()
})



//paymill integration
$(document).ready(function () {
    $("#payment-form").submit(function (event) {
        //event.preventDefault(event);
        
        var date = $('.cc-exp').val();
        var month = date.substr(0, 2);
        var year = date.substr(3, 2);
        var card_no = $('.cc-number').val();
        card_no.replace(/\s+/g, '');
        // Deactivate submit button to avoid further clicks
        $('.submit-button').attr("disabled", "disabled");
        $('#payment-button-amount').hide();
        $('#payment-button-sending').show();
        paymill.createToken({
            number: card_no,  // required, ohne Leerzeichen und Bindestriche
            exp_month: month,   // required
            exp_year: '20'+year,     // required, vierstellig z.B. "2016"
            cvc: $('.cc-cvc').val(),                  // required
            amount_int: $('.card-amount-int').val(),    // required, integer, z.B. "15" fÃ¼r 0,15 Euro
            currency: $('.card-currency').val(),    // required, ISO 4217 z.B. "EUR" od. "GBP"
            cardholder: $('.cc-name').val() // optional
        }, PaymillResponseHandler);                   // Info dazu weiter unten
        
        return false;
    });

    function PaymillResponseHandler(error, result) {
        console.log(result);
        console.log(error);
        if (error) {
            // Shows the error above the form
            
            $(".payment-errors").html(error.apierror);
            $(".submit-button").removeAttr("disabled");
            $('#payment-button-amount').show();
            $('#payment-button-sending').hide();
        } else {
            
            var form = $("#payment-form");
            // Output token
            var token = result.token;
            // Insert token into form in order to submit to server
            form.append('<input class="card_token" type="hidden" name="token" value=' + token + ' />');

            //send data to form using ajax
            $.ajax({
                'type': 'post',
                'url': form.attr("action"),
                'data': { 'token': token },
                success: function (data) {

                    $(".submit-button").removeAttr("disabled");
                    $('#payment-button-amount').show();
                    $('#payment-button-sending').hide();
                    swal("Great!", "Payment successful", "success");
                    //reset form fields
                    $('.cc-name').val("");
                    $('.cc-number').val("");
                    $('.cc-exp').val("");
                    $('.cc-cvc').val("");
                    $('.cc-zip').val("");
                    $('.card-token').val("");
                }
            })
        }
    }
});

//formatting date field 
var cleave = new Cleave('.cc-exp', {
    date: true,
    datePattern: ['m', 'y']
});

//formatting credit card field 
var cleave = new Cleave('.cc-number', {
    creditCard: true,
    onCreditCardTypeChanged: function (type) {
        // update UI ..
        $('.cc-number').removeClass("visa mastercard amex discover");
        $('.cc-number').addClass(type);

    }
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})  