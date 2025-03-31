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

function documentFormatter(inputDocument) {
    debugger;

    

    if (value.length <= 11) {
        applyCpfMask(inputDocument); // Aplica a máscara de CPF
    } else {
        applyCnpjMask(inputDocument); // Aplica a máscara de CNPJ
    }
}


jQuery(function ($) {
    //$("#txtData").mask("99/99/9999");
    //$("#txtTelefone").mask("(099) 9999-9999");
    $("#txtCep").mask("99999-999");

    $("#loginInput").mask("999.999.999-99");
    //$("#txtCNPJ").mask("99.999.999/9999-99");
    //$("#txtPlacaVeiculo").mask("aaa - 9999");
    //$("#txtIP").mask('099.099.099.099');
});



