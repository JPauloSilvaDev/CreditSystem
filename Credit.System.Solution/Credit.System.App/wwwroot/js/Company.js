
//Adicionando máscaras aos campos de input.
document.addEventListener('DOMContentLoaded', function () {
    $("#PrimaryPhone").mask("(99) 9999-9999");
    $("#SecondPhone").mask("(99) 9999-9999");
    $("#ZipCode").mask("99999-999");
    $("#Document").mask("99.999.999/9999-99");
});

function EditCompany() {

    IsLoadingBody(true);

    const data = {
        PrimaryName: document.getElementById("PrimaryName").value,
        SecondaryName: document.getElementById("SecondName").value,
        Document: document.getElementById("Document").value,
        PrimaryPhone: document.getElementById("PrimaryPhone").value,
        SecondaryPhone: document.getElementById("SecondPhone").value,
        Email: document.getElementById("Email").value,
        ZipCode: document.getElementById("ZipCode").value,
        AddressNumber: document.getElementById("AddressNumber").value,
        City: document.getElementById("inputCity").value,
        State: document.getElementById("inputState").value,
        Observation: document.getElementById("Observation").value,
        Neighborhood: document.getElementById("inputNeighborhood").value,
        Street: document.getElementById("inputStreet").value
    };

    // Send the data to the server using Fetch API
    fetch("/Company/EditCompany", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            /*"RequestVerificationToken": getAntiForgeryToken()*/
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json()) // Parse the JSON response
        .then(data => {

            IsLoadingBody(false);

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
            IsLoadingBody(false);
            showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        });
}