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

function setURL(type) {
    switch (type) {
        case 'analysis':
            baseUrl = '/api/analysis/submit';
            break;
        case 'contact':
            baseUrl = '/api/application/submit';
            break;
        case 'contact':
            baseUrl = '/api/contact/submit';
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
        emailAddress: document.getElementById('template-contactform-email').value,
        phone: document.getElementById('template-contactform-phone').value,
        message: document.getElementById('template-contactform-message').value,

        service: document.getElementById('template-contactform-service')
    }
}

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
            if (data) {
                alert(data);
            } else {
                alert('Email was sent successfully!');
            }
        },
        error: function (data) {
            if (data) {
                alert(data);
            } else {
                alert('There was an error processing this email!');
            }
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

