function RegisterNewUser() {
    
    const data = {
        
        PrimaryName: document.getElementById("PrimaryName").value,
        SecondName: document.getElementById("SecondName").value,
        Document: document.getElementById("Document").value,
        PrimaryPhone: document.getElementById("PrimaryPhone").value,
        SecondPhone: document.getElementById("SecondPhone").value,
        Email: document.getElementById("Email").value,
        ZipCode: document.getElementById("ZipCode").value,
        AddressNumber: document.getElementById("AddressNumber").value,
        City: document.getElementById("City").value,
        State: document.getElementById("State").value,
        Observation: document.getElementById("Observation").value,
        Neighborhood: document.getElementById("Neighborhood").value
    };

    // Send the data to the server using Fetch API
    fetch("/Company/CompanyRegister", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            /*"RequestVerificationToken": getAntiForgeryToken()*/
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