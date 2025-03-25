//#region HandleErrors
async function handleError(response) {
    if (response.ok)
        return response;

    const responseJson = await response.json()
    const errors = responseJson.Errors;

    showToastsForErrorMessages(errors);
}

function showToastsForErrorMessages(errors) {
    const container = document.getElementById('toast-container');

    removeAllChildrenFromElement(container);

    for (let error of errors) {
        const toastElement = createToast(error, true);

        container.appendChild(toastElement);
        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastElement);
        toastBootstrap.show();
    }
}

function removeAllChildrenFromElement(element) {
    while (element.firstChild) {
        element.removeChild(element.firstChild);
    }
}

function createToast(message, isError) {
    const toastElement = document.createElement("div");
    const bgClass = isError ? "text-bg-danger" : "text-bg-primary";

    toastElement.className = "toast align-items-center border-0 " + bgClass;
    toastElement.setAttribute("role", "alert");
    toastElement.setAttribute("aria-live", "assertive");
    toastElement.setAttribute("aria-atomic", "true");
    toastElement.innerHTML =
        `<div class="d-flex">
            <div class="toast-body">${message}</div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>`;

    return toastElement;
}

//#endregion
//#region imagePreview
function addImagePreviewEventListener(inputId, imagePreviewId) {
    document.getElementById(inputId).addEventListener('change', function (event) {
        const file = event.target.files[0];
        if (!file)
            return;

        const imagePreviewElement = document.getElementById(imagePreviewId);

        if (!imagePreviewElement) return;

        const reader = new FileReader();

        reader.onload = function (e) {
            const img = document.createElement('img');
            img.className = "img-thumbnail";
            img.src = e.target.result;

            while (imagePreviewElement.firstChild) {
                imagePreviewElement.removeChild(imagePreviewElement.firstChild);
            }

            imagePreviewElement.appendChild(img);
        }

        reader.readAsDataURL(file)
    });
}

//#endregion
//#region FormValidation
function ValidateFormField(inputId, errorId, valid) {
    const value = document.getElementById(inputId).value;
    const errorElement = document.getElementById(errorId);

    if (!value) {
        valid = false;
        errorElement.classList.remove('d-none');
    } else {
        errorElement.classList.add('d-none');
    }

    return valid;
}

function validateImageFormField(inputId, errorId, valid) {
    const file = document.getElementById(inputId).files[0];
    const imageErrorElement = document.getElementById(errorId);

    if (!file) {
        valid = false;
        imageErrorElement.classList.remove('d-none');
    } else {
        imageErrorElement.classList.add('d-none');
    }

    return valid;
}

//#endregion
//#region Throw Error Message
function throwQueryErrorMessage() {
    const params = new URLSearchParams(window.location.search);
    if (params.has('error')) {
        const error = params.get('error');
        showToastsForErrorMessages([error]);
    }
}

throwQueryErrorMessage();
//#endregion
//#region PasswordVisibility
function togglePasswordVisibility(inputId){
    const element = document.getElementById(inputId);
    const currentType = element.getAttribute('type');
    if (currentType === "password"){
        element.setAttribute('type', 'text');
        return;
    }

    element.setAttribute('type', 'password');
}
//#endregion