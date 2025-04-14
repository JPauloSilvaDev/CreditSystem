
$(document).ready(function () {

    var table = $('#dataTableClient').DataTable({
        paging: false,
        searching: true,
        lengthMenu: [5, 10, 25, 50],
        language: {
            search: "Pesquisar:",
            zeroRecords: "Nenhum registro encontrado",
            info: "",
            infoFiltered: "Filtrado de _MAX_ registros"
        },

        responsive: true, // Enable responsive feature
        //autoWidth: false, // Disable automatic width calculation
        //scrollX: true, // Enable horizontal scrolling if needed
        dom: '<"top"f>rt<"bottom"lip><"clear">'

    });

    $('#searchInput').on('keyup', function () {
        table.search(this.value).draw();  // Apply the search to the table
    });


    $('#dataTableClient tbody').on('click', 'tr', function () {
         selectedRowData = table.row(this).data();
        $('#dataTableClient tbody tr').removeClass('table-active');

        // Add 'table-active' to the clicked row
        $(this).addClass('table-active');

        console.log(selectedRowData);

    });

    document.getElementById('dt-search-0').style.display = 'none';
    document.querySelector('label[for="dt-search-0"]').style.display = 'none';
    table.columns.adjust();
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

