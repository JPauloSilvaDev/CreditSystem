$(document).ready(function () {

    var table = $('#dataTableClient').DataTable({
        paging: true,
        searching: true,
        pageLength: 10,
        lengthMenu: [[10, 25, 50, -1], [10, 25, 50, "Todos"]],
        language: {
            search: "Pesquisar:",
            zeroRecords: "Nenhum registro encontrado",
            info: "",
            infoFiltered: "Filtrado de _MAX_ registros",
            lengthMenu: "_MENU_ registros por página",
        },

    /*    responsive: true,*/
        dom: '<"top"p>rt<"bottom"i>'
       
    });

    $('#customLength').on('change', function () {
        var newLength = parseInt($(this).val(), 10);
        table.page.len(newLength).draw();
    });
    //Transformando o input de procura da tabela customizado ao invés do padrão do DataTable
    $('#searchInput').on('keyup', function () {
        table.search(this.value).draw();
    });

    $("#primaryPhone").mask("(99) 9999-9999");
    $("#secondPhone").mask("(99) 9999-9999");
    $("#txtCep").mask("99999-999");

    //email: $('#email').val(),
    //    document: $('#inputCpfCnpj').val(),
    //        primaryPhone: $('#primaryPhone').val(),
    //            secondaryPhone: $('#secondPhone').val(),
    //                zipcode: $('#txtCep').val(),




    //$("#inputCpfCnpj").mask("999.999.999-99");
    //  //$("#txtCNPJ").mask("99.999.999/9999-99");
    //$("#txtPlacaVeiculo").mask("aaa - 9999");
    //$("#txtIP").mask('099.099.099.099');



});

function validateForm() {
    const fields = ["inputCpfCnpj", "firstName", "primaryPhone"];

    fields.forEach(Id => {
        const input = document.getElementById(Id);

        if (input.value == "") {
            input.classList.add("is-invalid");
            button.disabled = true;

        } else {
            input.classList.remove("is-invalid");
            button.disabled = false;
        }

    });

}

async function RegisterClient() {

    const data = {
        primaryName: $('#firstName').val(),
        email: $('#email').val(),
        document: $('#inputCpfCnpj').val(),
        primaryPhone: $('#primaryPhone').val(),
        secondaryPhone: $('#secondPhone').val(),
        zipcode: $('#txtCep').val(),
        street: $('#inputStreet').val(),
        streetNumber: $('#inputStreetNumber').val(),
        observation: $('#inputObservation').val(),
        state: $('#inputState').val(),
        city: $('#inputCity').val(),
        neighborhood: $('#inputNeighborhood').val()
    };

    if (!ValidateRegisterClient(data)) {
        return;
    }

    try {
        const response = await fetch("/Client/RegisterClient", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        debugger;

        if (result.success) {
            showAlert("Usuário cadastrado com sucesso!", 'success', 5000);
            window.reload();
        } else {
            showAlert("Não foi possível concluir a solicitação no momento", 'error', 5000);
            console.log(result.message);
        }

    } catch (error) {
        showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        console.error(error);
    }
}

function SetEditClient(client) {
    debugger
    $('#clientId').val(client.ClientId);
    $('#editPrimaryName').val(client.PrimaryName);
    $('#clientId').val(client.ClientId);
    $('#editSecondaryName').val(client.SecondaryName);
    $('#editEmail').val(client.Email);
    $('#editCpfCnpj').val(client.Document);
    $('#editPrimaryPhone').val(client.PrimaryPhone);
    $('#editSecondaryPhone').val(client.SecondaryPhone);
    $('#editCep').val(client.ZipCode);
    $('#editStreet').val(client.Street);
    $('#editStreetNumber').val(client.StreetNumber);
    $('#editNeighborhood').val(client.Neighborhood)
    $('#editObservation').val(client.Observation);
    $('#editCity').val(client.City);
    $('#editState').val(client.State);
};

async function EditClient() {
    const data = {
        clientid: $('#clientId').val(),
        primaryname: $('#editPrimaryName').val(),
        secondaryname: $('#editSecondaryName').val(),
        email: $('#editEmail').val(),
        document: $('#editCpfCnpj').val(),
        primaryPhone: $('#editPrimaryPhone').val(),
        secondaryPhone: $('#editSecondaryPhone').val(),
        zipcode: $('#editCep').val(),
        street: $('#editStreet').val(),
        streetNumber: $('#editStreetNumber').val(),
        observation: $('#editObservation').val(),
        state: $('#editState').val(),
        city: $('#editCity').val(),
        neighborhood: $('#editNeighborhood').val()
    };

    if (!ValidateRegisterClient(data)) {
        return;
    }

    try {
        const response = await fetch("/Client/EditClient", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        if (result.success) {
            showAlert("Alterações salvas com sucesso!", 'success', 5000);
        } else {
            showAlert("Não foi possível concluir a solicitação no momento", 'error', 5000);
        }

    } catch (error) {
        console.error("Detalhes do erro:", error);
        showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
    }
}

async function UserLogin() {

    const loginInput = $('#loginInput').val();
    const passwordInput = $('#passwordInput').val();

    if (loginInput === "") {
        showAlert("Digite seu login", 'warning', 5000);
        return;
    }

    if (passwordInput === "") {
        showAlert("Digite sua senha", 'warning', 5000);
        return;
    }

    const data = {
        login: loginInput,
        password: passwordInput
    };

    try {
        const response = await fetch("/User/Login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        if (result.success) {
            window.location.href = "/Home/Index";
        } else {
            showAlert(result.message, 'error', 5000);
            console.log(result.message);
        }
    } catch (error) {
        console.error("Detalhes do erro", error);
        showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
    }
}


function ValidateRegisterClient(data) {

    if (data.name === "") {
        showAlert("O campo razão social/nome não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.email === "") {
        showAlert("O campo email não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.document === "") {
        showAlert("O campo CPF/CNPJ não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.primaryPhone === "") {
        showAlert("O campo telefone principal não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.zipcode === "") {
        showAlert("O campo CEP não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.street === "") {
        showAlert("O campo rua não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.streetNumber === "") {
        showAlert("O campo número não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.state === "") {
        showAlert("O campo estado não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.city === "") {
        showAlert("O campo cidade não pode ser vazio.", 'warning', 5000);
        return false;
    }

    if (data.neighborhood === "") {
        showAlert("O campo bairro não pode ser vazio.", 'warning', 5000);
        return false;
    }

    return true;
}

async function RemoveClient() {
    const clientId = $("#clientId").val();

    try {
        const response = await fetch(`/Client/RemoveClient?clientId=${clientId}`, {
            method: "POST"
        });

        if (!response.ok) {
            throw new Error("Network error");
        }

        const data = await response.json();

        if (data.success) {
            showAlert(data.message, 'success', 5000);
        } else {
            showAlert(data.message, 'error', 5000);
        }

        console.log("Success:", data);
    } catch (error) {
        console.error("Error:", error);
        showAlert("Ocorreu um erro ao remover o cliente. Tente novamente mais tarde.", 'error', 5000);
    }
}


function GetRemoveClientModal(clientId) {
    $("#removeClientModal").modal("show");
    $("#clientId").val(clientId);
}

async function uploadFile() {
    const fileInput = document.getElementById("fileInput");
    const file = fileInput.files[0];

    if (!file) {
        showAlert("Selecione um arquivo", "warning", 5000);
        return;
    }

    const formData = new FormData();
    formData.append("file", file); // 'file' must match the controller parameter name

    try {
        const response = await fetch("/Client/UploadFile", {
            method: "POST",
            headers: {
                "RequestVerificationToken": getAntiForgeryToken() // Add token to header
            },
            body: formData
        });

        const result = await response.json();

        if (result.success) {
            showAlert("Arquivo enviado com sucesso!", "success", 5000);
        } else {
            showAlert(result.message, "danger", 5000);
        }
    } catch (error) {
        console.error("Informações sobre o erro", error);
        showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde", "danger", 5000);
    }
}




function getAdditionalDetails(client) {
    const tableBody = document.querySelector("#additionalDetailsTable tbody");

    $('#additionalDetailsTable tbody').empty();

    const row = document.createElement("tr");

    row.innerHTML = `
     <td class="align-middle text-nowrap">${client.SecondaryPhone}</td>
    <td class="align-middle text-nowrap">${client.City}</td>
        <td class="align-middle text-nowrap">${client.State}</td>
        <td class="align-middle text-nowrap">${client.Neighborhood}</td>
        <td class="align-middle text-nowrap">${client.Street}</td>
        <td class="align-middle text-nowrap">${client.StreetNumber}</td>
        <td class="align-middle text-nowrap">${client.ZipCode}</td>
        <td class="align-middle text-nowrap">${client.Observation}</td>
    `;

    tableBody.appendChild(row);

    $('#clientDetailsModal').modal('show');
}