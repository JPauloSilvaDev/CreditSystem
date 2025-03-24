






function UserLogin() {


    debugger



    let loginInput = $('#loginInput').val();
    let passwordInput = $('#passwordInput').val();

    if (loginInput == "") {
        alert('Insira seu login');
        return;
    }
       
    if (passwordInput == "") {
        alert('Insira sua senha');
        return;
    }


    const data = new URLSearchParams();
    data.append('login', loginInput);
    data.append('password', passwordInput);

    fetch('/User/Login', {
        method: 'POST',  // HTTP method
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',  // Use URL encoding
        },
        body: data.toString()  // Send form data as a query string
    })
        .then(response => response.json())  // Parse JSON response
        .then(data => {
            console.log(data);  // Handle the response data
        })
        .catch(error => {
            console.error('There was an error!', error);  // Handle errors
        });


}