//$(document).ready(function () {
//    applyCpfMask();
//});

//function applyCpfMask() {
//    //$("#txtData").mask("99/99/9999");
//    //$("#txtTelefone").mask("(099) 9999-9999");
//    //$("#txtCep").mask("99999-999");
//    $("#loginCpf").mask("999.999.999-99");
//    //$("#txtCNPJ").mask("99.999.999/9999-99");
//    //$("#txtPlacaVeiculo").mask("aaa - 9999");
//    //$("#txtIP").mask('099.099.099.099');
//};



function applyCpfMask(inputId) {

    $("#" + inputId).mask("999.999.999-99");
          
}

function applyCnpjMask(inputId) {

    $("#" + inputId).mask("99.999.999/9999-99");

}

//function documentFormatter(inputDocument) {
//    debugger;

    

//    if (value.length <= 11) {
//        applyCpfMask(inputDocument); // Aplica a máscara de CPF
//    } else {
//        applyCnpjMask(inputDocument); // Aplica a máscara de CNPJ
//    }
//}


jQuery(function ($) {
    //$("#txtData").mask("99/99/9999");
    //$("#txtTelefone").mask("(099) 9999-9999");
    $("#txtCep").mask("99999-999");

    $("#loginInput").mask("999.999.999-99");
    //$("#txtCNPJ").mask("99.999.999/9999-99");
    //$("#txtPlacaVeiculo").mask("aaa - 9999");
    //$("#txtIP").mask('099.099.099.099');
});



function getAntiForgeryToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]').value;
}



// Alert System Functions
function showAlert(message, type = 'success', duration = 5000) {
    const container = document.getElementById('alert-container');

    // Create alert card
    const alertCard = document.createElement('div');
    alertCard.className = `alert-card ${type}`;
    alertCard.innerHTML = `
        ${message}
        <button class="alert-close-btn">&times;</button>
    `;

    // Add to container
    container.appendChild(alertCard);

    // Trigger slide-down animation
    setTimeout(() => {
        alertCard.classList.add('visible');
    }, 10);

    // Close button functionality
    const closeBtn = alertCard.querySelector('.alert-close-btn');
    closeBtn.addEventListener('click', () => {
        fadeOutAlert(alertCard);
    });

    // Auto-fade after duration
    if (duration > 0) {
        setTimeout(() => {
            fadeOutAlert(alertCard);
        }, duration);
    }

    // Remove after fade completes
    alertCard.addEventListener('transitionend', (e) => {
        if (e.propertyName === 'opacity' && alertCard.classList.contains('fade-out')) {
            alertCard.remove();
        }
    });
}

function fadeOutAlert(alertElement) {
    alertElement.classList.remove('visible');
    alertElement.classList.add('fade-out');
}

// Make it available globally if needed
window.showAlert = showAlert;

function fadeOutAlert(alertElement) {
    alertElement.classList.add('fade-out');
}

