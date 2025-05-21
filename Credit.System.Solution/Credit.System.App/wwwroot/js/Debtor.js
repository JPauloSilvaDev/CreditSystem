
document.addEventListener('DOMContentLoaded', function () {
    
        var table = $('#dataTableDebtors').DataTable({
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

async function DebtorRegister() {

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

    if (!ValidateDebtorFields(data)) {
        return;
    }

    try {
        const response = await fetch("/Client/RegisterDebtor", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": getAntiForgeryToken()
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        debugger;

        if (result.success) {
            showAlert("Devedor cadastrado com sucesso!", 'success', 5000);
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

function SetEditDebtor(client) {
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

async function EditDebtor() {

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

    if (!ValidateDebtorFields(data)) {
        return;
    }

    try {
        const response = await fetch("/Client/EditDebtor", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": getAntiForgeryToken()
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