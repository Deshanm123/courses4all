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

$(document).ready(() => {
    InitiateRegisterBtnClickEvent();
    disableRegisterBtn();
    isUserRegisteredCheckByBlur();
})

function checkIfEmailInString(input) {
    var re = /(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))/;
    return re.test(input);
}

function isUserRegisteredCheckByBlur() {
    
    debugger;
    emailRegInput.addEventListener('blur', () => {
        if (checkIfEmailInString(emailRegInput.value) ){

        $.ajax({
            type: 'GET',
            url: `/UserAuth/IsUserRegistered?email=${emailRegInput.value}`,
            success: (data) => {
                debugger;
                if (data) {
                    //let errorMessage = `<div class="alert alert-warning alert-dismissible fade show" role="alert">
                    //                    <strong>Error! </strong>The  ${emailRegInput.value} is already registered email.
                    //                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    //                        <span aria-hidden="true">&times;</span>
                    //                    </button>
                    //            </div>`;

                    //document.getElementById('error-placeholder').innerHTML = errorMessage;
                    DisplayDismissibleMessageAlert('warning', 'Collusion', `${emailRegInput.value} is already registered email`, 'register-error-placeholder')

                } else {
                    ClearDismissibleMessageAlert('register-error-placeholder')
                }
                
               
            },
            error: (xhr, ajaxOptions, thrownError) => {
                let msg =  "\r \n" + xhr.status+ xhr.statusText;
                DisplayDismissibleMessageAlert('warning', 'Error', msg, 'register-error-placeholder');
            }
        });
        }
    })
}
function InitiateRegisterBtnClickEvent() {
    userRegisterBtn.addEventListener("click", (e) => {
        e.preventDefault();
        registerNewUser();

    })
}
function disableRegisterBtn() {
    userRegisterBtn.setAttribute("disabled", "true");
}
function enableRegisterBtn() {
    userRegisterBtn.removeAttribute("disabled");
}
userAgreementRegInput.addEventListener("change", (e) => {
    if(e.target.checked) {
        enableRegisterBtn();
    } else {
         disableRegisterBtn();
    }
});
function InitiateJQueryValidation() {
    $("#userRegistrationForm").removeData("validator");
    $("#userRegistrationForm").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("#userRegistrationForm");
}
function registerNewUser() {
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
        __RequestVerificationToken: __RequestVerificationTokenInput.value,
        RegistrationInValid: true,
    };

    $.ajax({
        type: 'POST',
        url: 'UserAuth/RegisterUser',
        data: newUserObj,
        success: (data) => {
            debugger
            let htmlContent = $.parseHTML(data);

            //let hasRegistrationErrors = $(htmlContent).find("input[name='RegistrationInValid']").value == 'true';
            let hasRegistrationErrors = $(htmlContent).find('#RegistrationInValid').val();
            if (hasRegistrationErrors == 'true') {
                //intiating onlick action
                document.querySelector("#RegistrationModalCenter .modal-dialog").innerHTML = data;
                InitiateJQueryValidation()
                InitiateRegisterBtnClickEvent()

            } else {
                location.href = "Home/Index"
            }
        },
        error: (xhr, ajaxOptions, thrownError) => {
            let msg = "\r \n" + xhr.status + xhr.statusText;
            DisplayDismissibleMessageAlert('warning', 'Error', msg, 'register-error-placeholder');
            console.error(thrownError + '\r\n' + xhr.statusText + '\r\n' + xhr.responseText);
        }

    })
}