console.log('user-register loaded');

const userRegistrationForm = document.getElementById('userRegistrationForm');

const userRegisterBtn = document.getElementById('userRegisterBtn');
const emailRegInput = userRegistrationForm.querySelector('#Email');
const passRegInput = userRegistrationForm.querySelector('#Password');
const confirmPassRegInput = userRegistrationForm.querySelector('#ConfirmPassword');
const firstNameRegInput = userRegistrationForm.querySelector('#FirstName');
const lastNameRegInput = userRegistrationForm.querySelector('#LastName');
const address1RegInput = userRegistrationForm.querySelector('#Address1');
const address2RegInput = userRegistrationForm.querySelector('#Address2');
const postCodeRegInput = userRegistrationForm.querySelector('#PostCode');
const phoneNumberRegInput = userRegistrationForm.querySelector('#PhoneNumber');
const  userAgreementRegInput= userRegistrationForm.querySelector('#AcceptUserAgreement');
    
InitiateRegisterBtnClickEvent();
function InitiateRegisterBtnClickEvent() {
    userRegisterBtn.addEventListener("click", (e) => {
        e.preventDefault();
        registerNewUser();

    })
}

function InitiateJQueryValidation() {
    $("#userRegistrationForm").removeData("validator");
    $("#userRegistrationForm").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("#userRegistrationForm");
}
function registerNewUser() {

    debugger;
    var __RequestVerificationTokenInput = userRegistrationForm.querySelector("Input[name='__RequestVerificationToken']");
    var newUserObj = {
        Email: emailRegInput.value,
        Password: passRegInput.value,
        ConfirmPassword: confirmPassRegInput.value,
        FirstName: firstNameRegInput.value,
        LastName: lastNameRegInput.value,
        Address1: address1RegInput.value,
        Address2: address2RegInput.value,
        PostCode: postCodeRegInput.value,
        PhoneNumber: phoneNumberRegInput.value,
        AcceptUserAgreement: userAgreementRegInput.checked,
        __RequestVerificationToken: __RequestVerificationTokenInput.value,
        RegistrationInValid: true,
    };

    console.log(newUserObj);
    $.ajax({
        type: 'POST',
        url: 'UserAuth/RegisterUser',
        data: newUserObj,
        success: (data) => {
            let htmlContent = $.parseHTML(data);
            let hasRegistrationErrors = $(htmlContent).find("input(name='RegistrationInValid')").value == 'true'
            if (hasRegistrationErrors) {
                //intiating onlick action
                InitiateRegisterBtnClickEvent()
                InitiateJQueryValidation()

            } else {
                window.location.href = "Home/Index"
            }
        },
        error: (xhr, ajaxOptions, thrownError) => {
             var errorText = "Status: " + xhr.status + " - " + xhr.statusText;
             console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
        }

    })
}