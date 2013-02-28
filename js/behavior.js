/*
    A set of standard behaviors, implemented by classes:

    Class           |   Behavior
    ======================================================
    notification    |   fades out element after 2 seconds
    confirm         |   overrides the postback event with 
                    |   a confimarion dialog box, cancels 
                    |   if negative from user
    scanner         |   when input reaches 4 or greater in
                    |   length, fire click event on buttons
                    |   with submit class
    scanSubmit      |   The class of the paired submit button
*/


//Bind behavior to classes when jQuery is loaded:
$(document).ready(function () {

    /*Nofication Box*/
    $(".notification").ready(function () {
        //fades out notification boxes after 2 seconds (measured in miliseconds)
        $(".notification").delay(2000).fadeOut('slow');
    });

    /*Confirm Postback*/
    $('.confirm').bind('click', function (e) {
        //Bind a confirmation dialog box to corfirm buttons
        //"cancel" prevents the postback from occuring
        var action = $(this).text();
        var response = confirm("[" + action + "] Are you sure?");
        if (!response) {
            e.preventDefault();
        }
    });

    /*Scanner Auto-Submit*/
	$('.scanner').change(scannerChangeEvent).keyup(scannerChangeEvent);

	// called when a change() is called or a keyup event is fired
	function scannerChangeEvent()
	{
		if ($('.scanner').val().length >= 4) // if the length is 4 or greater, submit the form
		    $('.scanSubmit').click();
	}
});

//toggleDisabled jQuery Plugin
(function ($) {
    $.fn.toggleDisabled = function () {
        return this.each(function () {
            this.disabled = !this.disabled;
        });
    };
})(jQuery);