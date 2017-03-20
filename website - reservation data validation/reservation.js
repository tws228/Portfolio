//Trevor Sisk CIS241 11/4/16
$(document).ready(function() {
	
	$("#arrival_date").focus();
	
	$("#tabs").tabs({
		active: 0
	});
	
	$("#arrival_date").datepicker({
		minDate: new Date(),
		maxDate: +90
		//showButtonPanel: true
		
	});
	
	$("#policies").button();
	$("#policies").click(function(){
		$("#dialog").dialog({
			modal: true
		});
	});
		
	var emailPattern = /\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b/;
		
	// add a span element after each text box
	$(":text").after("<span class='error'>*</span>");
	
	// move the focus to the first text box
	
	
	// the handler for the submit event of the form
	// executed when the submit button is clicked
	$("#reservation_form").submit(
		function(event) {
			var isValid = true;
			
			// validate the requested arrival date
			if ($("#arrival_date").val() == "") {
				$("#arrival_date").next().text("This field is required.");
				isValid = false;
			} else {
				$("#arrival_date").next().text("");				
			}
			
			// validate the number of nights
			if ($("#nights").val() == "") {
				$("#nights").next().text("This field is required.");
				isValid = false;
			} else if (isNaN($("#nights").val())) {
				$("#nights").next().text("This field must be numeric.");
				isValid = false;
			} else {
				$("#nights").next().text("");
			}		

			// validate the name entry
			var name = $("#name").val().trim();
			if (name == "") {
				$("#name").next().text("This field is required.");
				isValid = false;
			} 
			else {
				$("#name").val(name);
				$("#name").next().text("");
			}
						
			// validate the email entry with a regular expression
			var email = $("#email").val();
			if (email == "") { 
				$("#email").next().text("This field is required.");
				isValid = false;
			} else if ( !emailPattern.test(email) ) {
				$("#email").next().text("Must be a valid email address.");
				isValid = false;
			} else {
				$("#email").next().text("");
			} 
			
			// validate the phone number
			if ($("#phone").val() == "") { 
				$("#phone").next().text("This field is required.");
				isValid = false; 
			} else {
				$("#phone").next().text("");
			}
			
			// prevent the submission of the form if any entries are invalid 
			if (isValid == false) {
				event.preventDefault();				
			}
		} // end function
	);	// end submit
}); // end ready