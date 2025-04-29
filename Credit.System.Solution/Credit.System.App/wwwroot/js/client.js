$(document).ready(function () {

    var table = $('#dataTableClient').DataTable({
        /* paging: false,*/
        searching: true,
        pageLength: 10,
        language: {
            search: "Pesquisar:",
            zeroRecords: "Nenhum registro encontrado",
            info: "",
            infoFiltered: "Filtrado de _MAX_ registros"
        },

        responsive: true, 
        dom: '<"top"p>rt<"bottom"i>'

    });

    //Transformando o input de procura da tabela customizado ao invés do padrão do DataTable
    $('#searchInput').on('keyup', function () {
        table.search(this.value).draw();
    });

    $('#dataTableClient tbody').on('click', 'tr', function () {
        selectedRowData = table.row(this).data();
        $('#dataTableClient tbody tr').removeClass('table-active');

        console.log(selectedRowData);

    });
 
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

function RegisterClient() {

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


    // Send the data to the server using Fetch API
    fetch("/Client/RegisterClient", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",

        },
        body: JSON.stringify(data)
    })
        .then(response => response.json()) // Parse the JSON response
        .then(data => {

            debugger
            if (data.success) {
                showAlert("Usuário cadastrado com sucesso!", 'success', 5000);
            } else {
                showAlert("Não foi possível concluir a solicitação no momento", 'error', 5000);
                console.log(data.message)
            }
        })
        .catch(error => {
            showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        });
}

function SetEditClient(client) {
    $('#editPrimaryName').val(client.PrimaryName);
    $('#editSecondaryName').val(client.SecondaryName);
    $('#editEmail').val(client.Email);
    $('#editCpfCnpj').val(client.Document);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
    $('#editEmail').val(client.Email);
};

function UserLogin() {
    const loginInput = $('#loginInput').val();
    const passwordInput = $('#passwordInput').val();

    if (loginInput == "") {
        showAlert("Digite seu login", 'warning', 5000);
        return;
    }

    if (passwordInput == "") {
        showAlert("Digite sua senha", 'warning', 5000);
        return;
    }

    const data = {
        login: loginInput,
        password: passwordInput,
    };


    fetch("/User/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",

        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                window.location.href = "/Home/Index";
            } else {
                showAlert(data.message, 'error', 5000);
                console.log(data.message);
            }
        })
        .catch(error => {
            //console.error("Error:", error);
            showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        });

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

    // Optional: Validate secondPhone and observation if required
    // if (data.secondPhone === "") {...}
    // if (data.observation === "") {...}

    return true;
}

function RemoveClient() {

    var clientId = $("#clientId").val();

    fetch(`/Client/RemoveClient?clientId=${clientId}`, {
        method: "POST"
        // No headers or body needed
    })
        .then(response => {
            if (!response.ok) throw new Error("Network error");
            return response.json();
        })
        .then(data => {

            if (data.success) {
                showAlert(data.message, 'success', 5000);
            }
            else {
                showAlert(data.message, 'error', 5000);
            }
            console.log("Success:", data);
        })
        .catch(error => {
            console.error("Error:", error);
        });
}

function GetRemoveClientModal(clientId) {
    $("#removeClientModal").modal("show");
    $("#clientId").val(clientId);
}