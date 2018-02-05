;
var self;
var baseUrl;


function quickAnalysisSubmissionClick() {
    self = this;
    setURL('analysis');
    bindQuickAnalysisInput();
    if (validateInput()) {
        sendEmail();
    } else {
        showModal('invalid');
    }
}

function analysisSubmissionClick() {
    self = this;
    setURL('analysis');
    bindAnalysisInput();
    if (validateInput()) {
        sendEmail();
    } else {
        showModal('invalid');
    }
}

function contactRequestClick() {
    self = this;
    setURL('contact');
    bindContactUsInput();
    if (validateInput()) {
        sendEmail();
    } else {
        showModal('invalid');
    }
}

function applicationSubmissionClick() {
    self = this;
    setURL('application')
    bindApplicationInput();
    if (validateInput()) {
        sendEmail();
    } else {
        showModal('invalid');
    }
}

function setURL(type) {
    switch (type) {
        case 'analysis':
            baseUrl = '/api/analysis/submit';
            break;
        case 'application':
            baseUrl = '/api/application/submit';
            break;
        case 'contact':
            baseUrl = '/api/contact/submit';
            break;
        case 'upload':
            baseUrl = '/api/uploads/resume';
            break;
        default:
            baseUrl = '/api/error/';
            break;
    }
}

function bindAnalysisInput() {
    self.info = {
        firstName: document.getElementById('template-analysis-fname').value,
        lastName: document.getElementById('template-analysis-lname').value,
        name: '',
        communityName: document.getElementById('template-analysis-community-name').value,
        addressOne: document.getElementById('template-analysis-address-one').value,
        addressTwo: document.getElementById('template-analysis-address-two').value,
        city: document.getElementById('template-analysis-city').value,
        state: document.getElementById('template-analysis-state').value,
        postalCode: document.getElementById('template-analysis-postal-code').value,
        emailAddress: document.getElementById('template-analysis-email').value,
        phone: document.getElementById('template-analysis-phone').value,
        message: document.getElementById('template-analysis-message').value
    }
}

function bindQuickAnalysisInput() {
    self.info = {
        firstName: document.getElementById('template-quick-analysis-fname').value,
        lastName: document.getElementById('template-quick-analysis-lname').value,
        name: '',
        communityName: document.getElementById('template-quick-analysis-community-name').value,
        addressOne: document.getElementById('template-quick-analysis-address-one').value,
        addressTwo: document.getElementById('template-quick-analysis-address-two').value,
        city: document.getElementById('template-quick-analysis-city').value,
        state: document.getElementById('template-quick-analysis-state').value,
        postalCode: document.getElementById('template-quick-analysis-postal-code').value,
        emailAddress: document.getElementById('template-quick-analysis-email').value,
        phone: document.getElementById('template-quick-analysis-phone').value,
        message: document.getElementById('template-quick-analysis-message').value
    }

    return self.info;
}

function bindContactUsInput() {
    self.info = {
        name: document.getElementById('template-contactform-name').value,
        firstName: document.getElementById('template-contactform-name').value,
        lastName: document.getElementById('template-contactform-name').value,
        emailAddress: document.getElementById('template-contactform-email').value,
        phone: document.getElementById('template-contactform-phone').value,
        message: document.getElementById('template-contactform-message').value,

        service: document.getElementById('template-contactform-service').value
    }
}

function bindApplicationInput() {
    self.info = {
        firstName: document.getElementById('application-fname').value,
        lastName: document.getElementById('application-lname').value,
        name: '',
        phone: document.getElementById('application-primary-phone').value,
        secondaryPhone: document.getElementById('application-secondary-phone').value,
        emailAddress: document.getElementById('application-email').value,
        addressOne: document.getElementById('application-address-one').value,
        addressTwo: document.getElementById('application-address-two').value,
        city: document.getElementById('application-city').value,
        state: document.getElementById('application-state').value,
        postalCode: document.getElementById('application-postal-code').value,
        positionType: document.getElementById('application-position-type').value,
        positionStatus: document.getElementById('application-position-status').value,
        weeklyHours: document.getElementById('application-weekly-hours').value,
        referral: document.getElementById('application-referral').value,
        resumeFileName: document.getElementById('application-resume').value.split(/(\\|\/)/g).pop(), //use regular expression to get filename
        message: '' //document.getElementById('application-additional-information').value
    }
}

$('#application-resume').on('change', function (e) {
    var formdata = new FormData(); //FormData object
    var fileInput = document.getElementById('application-resume');
    //Iterating through each files selected in fileInput
    for (i = 0; i < fileInput.files.length; i++) {
        //Appending each file to FormData object
        formdata.append(fileInput.files[i].name, fileInput.files[i]);
    }
    //Creating an XMLHttpRequest and sending
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/api/uploads/resume');
    xhr.send(formdata);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            alert(xhr.responseText);
        }
    }
    return false;
});


function sendEmail() {
    $.ajax({
        url: baseUrl,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        //cache: false,
        //processData: false,
        data: JSON.stringify(self.info),
        success: function (data) {
            self.validationStatus = {
                isValid: true,
                message: 'Thank you for your submission! <br /> A member of our team will be in contact with you.'
            }
            showModal('success');
        },
        error: function (data) {
            self.redirectToError();
        }
    });
}

function redirectToError()
{
    var url = '/Error/';
    window.location.href = url;
}

function validateInput() {
    var emailRegex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var hasErrors = false;
    var validationMessage = '';
    self.validationStatus = {};

    if (!self.info.firstName || !self.info.lastName) {
        
        hasErrors = true;
        validationMessage = concatenateValidationMessage(validationMessage, ' - The name is not completely filled out.');
    }

    if (!self.info.emailAddress || !emailRegex.test(self.info.emailAddress)) {
        hasErrors = true;
        validationMessage = concatenateValidationMessage(validationMessage, '- Email address is in an incorrect format.');
    }

    self.validationStatus = {
        isValid: !hasErrors,
        message: validationMessage
    }

    return self.validationStatus.isValid;
}

function concatenateValidationMessage(existingMessage, appendage) {
    var message = '';
    if (existingMessage.length > 0) {
        message = existingMessage + "<br />";
    } else {
        message = 'The submission could not be processed for the following reasons: ' + '<br />';
    }

    return message += appendage;
}

jQuery(window).load(function(){

    var $container = $('#posts');

    $container.isotope({ transitionDuration: '0.65s' });

    $(window).resize(function() {
        $container.isotope('layout');
    });

});


// Get the modal
var modal = document.getElementById('myModal');


// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// Get the button that opens the modal
//var btn = document.getElementById("myBtn");
//btn.onclick = function () {
// When the user clicks the button, open the modal 
//<button id="myBtn">Open Modal</button> //Add this HTML to trigger button click event.
//    //modal.style.display = "block";
//    self = this;
//    self.validationStatus = { message: "This is a message" };
//    //self.validationStatus.message = 'This is a message';
//    showModal('success');
//}

function showModal(modalType) {

    modal.style.display = "block";
    var modalIcon = document.getElementById("modal-icon");
    var modalHeaderText = document.getElementById("modal-header-text");
    var modalBodyText = document.getElementById('modal-body-text');
    switch (modalType) {
        case 'success':
            modalIcon.classList.remove('icon-warning-sign');
            modalIcon.classList.add('icon-thumbs-up2');
            modalHeaderText.innerHTML = 'Success!!!';
            modalBodyText.innerHTML = self.validationStatus.message;
            break;
        case 'invalid':
            modalIcon.classList.remove('icon-thumbs-up2');
            modalIcon.classList.add('icon-warning-sign');
            modalHeaderText.innerHTML = 'Invalid Entry!';
            modalBodyText.innerHTML = self.validationStatus.message;
            break;
        default:
            modalIcon.classList.remove('icon-thumbs-up2');
            modalIcon.classList.remove('icon-warning-sign');
            modalHeaderText.innerHTML = 'Living Well Rehabilitation';
            modalBodyText.innerHTML = 'Thank you for your interest. Please contact us at info@livingwellrehab.com';
    }

    
}

function showSuccessModal() {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

