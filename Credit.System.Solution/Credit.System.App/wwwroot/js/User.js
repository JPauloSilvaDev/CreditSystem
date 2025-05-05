
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

    IsLoadingBody(true);

    fetch("/User/Register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken": getAntiForgeryToken()
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json(); 
        })
        .then(data => {
            if (data.success) {
                IsLoadingBody(false);
                showAlert(data.message || "Usuário cadastrado com sucesso!", 'success', 5000);
                
                $('#registerModal').modal('hide');

                setTimeout(function () {
                    location.reload();
                }, 3000);
                
                
            } else {
                showAlert(data.message, 'error', 5000);
                IsLoadingBody(false);
                console.error("Detalhes do erro:", data.message);
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
    $('#editCpfInput').val(user.Login);
    $('#editEmailInput').val(user.Email);
    $('#editNameInput').val(user.Name);
    $('#userId').val(user.UserId);
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

    IsLoadingBody(true);

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
            IsLoadingBody(false);
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
            IsLoadingBody(false);//console.error("Error:", error);
            showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        });


}

function EditUser() {

    
    const editCpf = $('#editCpfInput').val();
    const editEmail = $('#editEmailInput').val();
    const editName = $('#editNameInput').val();
    const userId = $('#userId').val();

    if (editCpf == "") {
        showAlert("Insira um CPF válido.", 'warning', 5000);
        return;
    }

    if (editEmail == "") {
        showAlert("Insira um Email válido.", 'warning', 5000);
        return;
    }

    if (editName == "") {
        showAlert("Insira um nome válido.", 'warning', 5000);
        return;
    }

    const data = {
        Login: editCpf,
        Email: editEmail,
        Name: editName,
        UserId: userId
    };
    fetch("/User/EditUser", {
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

function GetRemoveUserModal(user){

    $("#removeUserModal").modal("show");
    $("#userId").val(user.UserId);
}
function RemoveUser() {

    const data = {
        UserId: $('#userId').val()
    };

    fetch("/User/RemoveUser", {
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
                showAlert(data.message, 'success', 5000);

                setTimeout(function () {
                    location.reload();
                }, 3000);

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

function BlockUserAccess(userId) {

    IsLoadingBody(true);

    fetch(`/User/BlockUserAccess?userId=${userId}`, {
        method: "GET",
         headers: {
             "RequestVerificationToken": getAntiForgeryToken()
        },// No headers or body needed
    })
        .then(response => {
            IsLoadingBody(false);
            if (!response.ok) throw new Error("Network error");
            return response.json();

        })
        .then(data => {

            if (data.success) {
                showAlert(data.message, 'success', 5000);
                IsLoadingBody(false);
                setTimeout(function () {
                    location.reload();
                }, 3000);
            }
            else {
                showAlert(data.message, 'error', 5000);
                IsLoadingBody(false);
            }
            console.log("Success:", data);
        })
        .catch(error => {
            console.error("Error:", error);
            IsLoadingBody(false);
        });

}



function UnblockUserAccess(userId) {

    fetch(`/User/UnblockUserAccess?userId=${userId}`, {
        method: "GET",
        headers: {
            "RequestVerificationToken": getAntiForgeryToken()
        },// No headers or body needed
    })
        .then(response => {
            if (!response.ok) throw new Error("Network error");
            return response.json();
        })
        .then(data => {

            if (data.success) {
                showAlert(data.message, 'success', 5000);
                setTimeout(function () {
                    location.reload();
                }, 3000);
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