function DisplayDismissibleMessageAlert(msgType, msgHeader, msgDescription, htmlPlaceholder) {
    let errorMessage = `<div class="alert alert-${msgType} alert-dismissible fade show" role="alert">
                                        <strong>${msgHeader} </strong>
                                            <p>${msgDescription}</p>
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                </div>`;
    document.getElementById(htmlPlaceholder).innerHTML = errorMessage;
}

function ClearDismissibleMessageAlert(htmlPlaceholder) {
    document.getElementById(htmlPlaceholder).innerHTML = "";
}