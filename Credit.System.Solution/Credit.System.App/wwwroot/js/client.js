
function validateForm() {
    const fields = ["inputCpfCnpj", "firstName", "primaryPhone"];

    fields.forEach(Id => {
        const input = document.getElementById(Id);

        if (input.value == "") {
            input.classList.add("is-invalid");
        } else {
            input.classList.remove("is-invalid");
        }

    });

}



function RegisterClient() {


    
    
    

    const data = {
        name: document.getElementById("firstName").value,
        email: document.getElementById("email").value,
        document: document.getElementById("inputCpfCnpj").value,
        primaryPhone: document.getElementById("primaryPhone").value,
        secondPhone: document.getElementById("secondPhone").value,
        zipcode: document.getElementById("txtCep").value,
        street: document.getElementById("inputStreet").value,
        streetNumber: document.getElementById("inputStreetNumber").value,
        observation: document.getElementById("inputObservation").value,
        state: document.getElementById("inputState").value,
        city: document.getElementById("inputCity").value,
        neighborhood: document.getElementById("inputNeighborhood").value
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
                // Optionally, redirect to another page or reset the form
            } else {
                showAlert("Não foi possível concluir a solicitação no momento", 'error', 5000);
                console.log(data.message)
            }
        })
        .catch(error => {
            //console.error("Error:", error);
            showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        });
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

