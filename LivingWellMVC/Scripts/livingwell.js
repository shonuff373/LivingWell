;
var self;
var baseUrl;


function quickAnalysisSubmissionClick() {
    self = this;
    setURL('analysis');
    bindQuickAnalysisInput();
    if (isValidInput()) {
        sendEmail();
    }
}

function analysisSubmissionClick() {
    self = this;
    setURL('analysis');
    bindAnalysisInput();
    if (isValidInput()) {
        sendEmail();
    }
}

function contactRequestClick() {
    self = this;
    setURL('contact');
    bindContactUsInput();
    if (isValidInput()) {
        sendEmail();
    }
}

function applicationSubmissionClick() {
    self = this;
    setURL('application')
    bindApplicationInput();
    if (isValidInput()) {
        sendEmail();
    }
}


function setURL(type) {
    switch (type) {
        case 'analysis':
            baseUrl = 'api/analysis/submit';
            break;
        case 'application':
            baseUrl = 'api/application/submit';
            break;
        case 'contact':
            baseUrl = 'api/contact/submit';
            break;
        case 'upload':
            baseUrl = 'api/application/upload';
        default:
            baseUrl = 'api/error/';
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
        emailAddress: document.getElementById('template-contactform-email').value,
        phone: document.getElementById('template-contactform-phone').value,
        message: document.getElementById('template-contactform-message').value,

        service: document.getElementById('template-contactform-service')
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
        message: document.getElementById('application-additional-information').value
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
    xhr.open('POST', '/api/application/upload');
    xhr.send(formdata);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            alert(xhr.responseText);
        }
    }
    return false;
});


//$('#application-resume').on('change', function (e) {

//    self = this;
//    var files = e.target.files;
//    var myID = '3'; //uncomment this to make sure the ajax URL works
//    var uploadUrl = 'api/application/upload';

//    if (files.length > 0) {
//        if (window.FormData !== undefined) {
//            var data = new FormData();
//            for (var x = 0; x < files.length; x++) {
//                data.append("file" + x, files[x]);
//            }

//            $.ajax({
//                url: uploadUrl,
//                type: "POST",
//                dataType: 'json',
//                contentType: 'application/json; charset=utf-8',
//                //processData: false,
//                data: JSON.stringify(data),
//                success: function (result) {
//                    console.log(result);
//                },
//                error: function (xhr, status, p3, p4) {
//                    var err = "Error " + " " + status + " " + p3 + " " + p4;
//                    if (xhr.responseText && xhr.responseText[0] == "{")
//                        err = JSON.parse(xhr.responseText).Message;
//                    console.log(err);
//                }
//            });
//        } else {
//            alert("This browser doesn't support HTML5 file uploads!");
//        }
//    }
//});

function sendEmail() {
    var formData = new FormData();

    $.ajax({
        url: baseUrl,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        //cache: false,
        //processData: false,
        data: JSON.stringify(self.info),
        success: function (data) {
            // ..... any success code you want
            //if (data) {
            //    alert(data);
            //} else {
            //    alert('Email was sent successfully!');
            //}
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            console.log("AJAX error in request: " + JSON.stringify(err, null, 2));
            //alert(err.Message);
        }
    });
}

function isValidInput() {
    var isValid = true;
    if (!self.info.firstName || !self.info.lastName) {
        isValid = false;
    }

    if (!self.info.emailAddress) {
        isValid = false;
    } else {

    }

    return isValid;
}


jQuery(window).load(function(){

    var $container = $('#posts');

    $container.isotope({ transitionDuration: '0.65s' });

    $(window).resize(function() {
        $container.isotope('layout');
    });

});
