
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
        },
        responsive: true, // Enable responsive feature
        //autoWidth: false, // Disable automatic width calculation
        //scrollX: true, // Enable horizontal scrolling if needed
        dom: '<"top"f>rt<"bottom"lip><"clear">'

    });

    $('#datatablesSimple td').on('click', 'tr', function () {
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
        .then(response => {
            // First check if the response is OK (status 200-299)
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json(); // Parse the JSON only if response is OK
        })
        .then(data => {
            if (data.success) {
                showAlert(data.message || "Usuário cadastrado com sucesso!", 'success', 5000);
                // Optional: Close the modal on success
                $('#registerModal').modal('hide');

                setTimeout(function () {
                    location.reload();
                }, 3000);
                // Optional: Refresh data or reset form
                
            } else {
                showAlert(data.message, 'error', 5000);
                console.error("Server error:", data.message);
            }
        })
        .catch(error => {
            console.error("Fetch error:", error);
            showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        });

   
}

function GetEditUserModal(user) {
    debugger


    $("#editModal").modal("show");
    


    $('#cpfInput').val(user.Login);
    $('#emailInput').val(user.Email);
    $('#nameInput').val(user.Name);
    $('#companyId')?.val(user.companyId);


    //const data = {

    //    Login: ,
    //    Email: email,
    //    Name: name,
    //    CompanyId: companyId

    //}

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

