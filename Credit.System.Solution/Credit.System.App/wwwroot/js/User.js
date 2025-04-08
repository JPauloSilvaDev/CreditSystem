
var selectedRowData = undefined;

$(document).ready(function () {

    var table = $('#datatablesSimple').DataTable({
        paging: false,
        searching: false,
        lengthMenu: [5, 10, 25, 50],
        language: {
            search: "Pesquisar:",
            zeroRecords: "Nenhum registro encontrado",
            info: "", 
        }
    });

    $('#datatablesSimple tbody').on('click', 'tr', function () {

        $('tr.selected').removeClass('selected');

        $(this).addClass('selected');

         selectedRowData = table.row(this).data();
    });

});

function RegisterNewUser() {

    const cpf = document.getElementById("cpfInput").value;
    const email = document.getElementById("emailInput").value;
    const name = document.getElementById("nameInput").value;
    const companyId = document.getElementById("companyId")?.value;

    if (cpf == "") {
        showAlert("Insira um CPF válido.", 'warning', 5000);
        return;
    }
        
    if (email == "") {
        showAlert("Insira um Email válido.", 'warning', 5000);
        return;
    }

    if (name == "") {
        showAlert("Insira um nome válido.", 'warning', 5000);
        return;
    }

    const data = {
        Login: cpf,
        Email: email,
        Name: name,
        CompanyId: companyId
    };

    fetch("/User/Register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": getAntiForgeryToken()
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json()) // Parse the JSON response
        .then(data => {

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

function EditUser() {
    if (selectedRowData == undefined) {
        showAlert("Selecione ao menos um registro para editar.", 'warning', 5000)
        return;
    }
}

function UserLogin() {
    // Send the data to the server using Fetch API
    debugger
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
        .then(response => response.json()) // Parse the JSON response
        .then(data => {
            console.log(data)
            debugger
            // Optionally, redirect to another page or reset the form
            if (data.success) {
                // If the response is successful, redirect to the "Index" action of the "Home" controller
                window.location.href = "/Home/Index";  // This will redirect to the Home/Index page
            } else {
                // If the response is not successful, show the error message
                showAlert(data.message, 'error', 5000);
                console.log(data.message);
            }
            
        })
        .catch(error => {
            //console.error("Error:", error);
            showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        });

    






}

