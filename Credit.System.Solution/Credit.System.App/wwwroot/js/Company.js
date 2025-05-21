
//Adicionando máscaras aos campos de input.
document.addEventListener('DOMContentLoaded', function () {
    $("#PrimaryPhone").mask("(99) 9999-9999");
    $("#SecondPhone").mask("(99) 9999-9999");
    $("#ZipCode").mask("99999-999");
    $("#Document").mask("99.999.999/9999-99");
});

async function EditCompany() {

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

    try {
        const response = await fetch("/Company/EditCompany", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                 "RequestVerificationToken": getAntiForgeryToken()
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        IsLoadingBody(false);

        if (result.success) {
            showAlert("Usuário cadastrado com sucesso!", 'success', 5000);
        } else {
            showAlert("Não foi possível concluir a solicitação no momento", 'error', 5000);
            console.log(result.message);
        }
    } catch (error) {
        IsLoadingBody(false);
        showAlert("Não foi possível concluir a solicitação no momento, tente novamente mais tarde.", 'error', 5000);
        console.error("Erro ao editar empresa:", error);
    }
}