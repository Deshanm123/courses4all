const saveSelectedBtn = document.getElementById('SaveSelectedUsersBtn');
const categorySelectionDropdown = document.getElementById('CategoryId');
const RequestVerificationTokenInput = document.querySelector("input[name='__RequestVerificationToken']");
const progressLoopContainer = document.getElementById('myPopup');

$(document).ready(() => {
    // disballing the save button 
    if (this.value == 0) {
        categorySelectionDropdown.setAttribute('disabled', 'true');
        categorySelectionDropdown.setAttribute('checked', 'false');
    }else {
        categorySelectionDropdown.addEventListener('change', () => {
            saveSelectedBtn.removeAttribute('disabled');
            $.ajax({
                type: 'GET',
                url: '/Admin/UserCategory/GetUsersForCategory?categoryId=' + categorySelectionDropdown.value,
                success: (data) => {
                    debugger;
                    // console.log(data);
                    document.getElementById('UserCheckListView').innerHTML = data;
                    saveSelectedBtn.removeAttribute('disabled');
                    saveSelectedBtn.style.display = 'block';

                },
                error: (xhr, ajaxOptions, thrownError) => {
                    let msg = "\r \n" + xhr.status + xhr.statusText;
                    DisplayDismissibleMessageAlert('warning', 'Error', msg, 'error-placeholder');
                }

            })

        })
    }

})
function getUsercheckBoxValues() {
    var checkboxes = document.querySelectorAll("input[type='checkbox']");
    var selectedValues = [];
    checkboxes.forEach((checkbox) => {
        if (checkbox.checked) {
            var userModel = {
                Id: checkbox.value
            };
            selectedValues.push(userModel);
        }
    });
    return selectedValues;
}


function showProgressLoop() {
    progressLoopContainer.classList.add('show-popup');
}

function stopProgressLoop() {
    progressLoopContainer.classList.remove('show-popup');
}
saveSelectedBtn.addEventListener('click', () => {
    saveSelectedBtn.style.display = 'block';
    var selectedUsersArr = getUsercheckBoxValues();
    showProgressLoop();
    $.ajax({
        type: 'POST',
        url: '/Admin/UserCategory/SaveSelectedUsers',
        data: {
            __RequestVerificationToken: RequestVerificationTokenInput.value,
            CategoryId: categorySelectionDropdown.value,
            UsersSelectedList: selectedUsersArr
        },
        success: (data) => {
            //disable dropdown and save during savee
           // categorySelectionDropdown.setAttribute('disabled', 'true');

            //stop loading progree bar
            stopProgressLoop();
            DisplayDismissibleMessageAlert('success', 'Successfull', 'Data saved successfully', 'msg-placeholder');

            document.getElementById('UserCheckListView').innerHTML = data;
            saveSelectedBtn.setAttribute('disabled', 'true');
            saveSelectedBtn.style.display = 'none';
        },
        error: (xhr, ajaxOptions, thrownError) => {
            stopProgressLoop();
            console.log(xhr.responseText);
            let msg = "\r \n" + xhr.status + xhr.statusText;
            DisplayDismissibleMessageAlert('warning', 'Error', msg, 'error-placeholder');
        }
    })

     



})