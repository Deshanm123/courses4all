

const loginBtn = document.getElementById('login');
const emailInput = document.querySelector("#UserLoginForm  input[name = 'Email']");
const passInput = document.querySelector("#UserLoginForm  input[name ='Password']");
const checkboxInput = document.querySelector("#UserLoginForm  input[name ='RememberMe']");

addLoginBtnEventListener();
function reloadClientSideValidations() {
    var loginForm = $("#userLoginForm");
    $(loginForm).removeData("validator"); //The "validator" reference to jQuery Validation, which is a popular plugin for client-side form validation.
    $(loginForm).removeData("unobtrusiveValidation"); /// jQuery Unobtrusive Validation, which is used in ASP.NET MVC applications to enable client-side validation based on data annotation attributes.
    $.validator.unobtrusive.parse(loginForm); //line attempts to parse and initialize 
    
}



function addLoginBtnEventListener() {
    loginBtn.addEventListener("click", () => {
       // e.preventDefault();
        loginAction();
    });
}
 

function loginAction()
{
    //When [ValidateAntiForgeryToke] is used is the User Auth Controller
    //this automatically generate a hidden field to store token value //inspect and see the login html
    // using this token and token stored in a cookie when user get logged in to the site
    //we can secure the post login request and prevent Validate Frogery tokkens

    //https://learn.microsoft.com/en-us/aspnet/web-api/overview/security/preventing-cross-site-request-forgery-csrf-attacks
    var AntiFrogerytokenValue = document.querySelector("#UserLoginForm  input[name='__RequestVerificationToken']").value;

    $.ajax({
       
        type: 'POST',
        url: "/UserAuth/Login",
        data: {
            __RequestVerificationToken : AntiFrogerytokenValue,
            Email: emailInput.value,
            Password: passInput.value,
            RememberMe: checkboxInput.checked
        },
        success: (data) => {
            var parsed = $.parseHTML(data); //convert the string to a set of DOM nodes
            console.log(data);
            var hasErrors = $(parsed).find("#LoginInvalid").val();
            if (hasErrors == 'true') {
                document.querySelector('#loginModalCenter').innerHTML = data;
                addLoginBtnEventListener();
                //if login error is present existing login partial  replaced by new login partial isnstance
                //reinitiating the  login on click button
               // document.getElementById('login').adddEventListenere('click', () => {
                  //  e.preventDefault();
                 //   loginAction();
                //});

                reloadClientSideValidations();
            } else {
                window.location.href = 'Home/Index';
            }
        },
        error: (xhr, ajaxOptions, thrownError) => {
            let msg = "\r \n" + xhr.status + xhr.statusText;
            DisplayDismissibleMessageAlert('warning', 'Error', msg, 'error-placeholder');
        }
    })
}
