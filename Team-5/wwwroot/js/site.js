//search animal by microchip number
let chipPath = "/Animals/GetAnimalDataByMicrochip";
let pastVisitsPath = "/Examination/ExaminationsListByIdAnimal";
let lockersPath = "/Product/FindLockers";
let datePath = '/Product/GetProductsFromDate';
let cfPath = '/Product/GetProductsFromCF';
function countByMicroChip() {
    let microchip = $('#microchip').val();
    $.ajax({
        url: `${chipPath}?microchipId=${microchip}`,
        method: 'GET',
        success: (data) => {
            let div = $('#animals');
            div.empty();
            if (data.length > 0) {
                data.forEach(animal => {
                    let animalDetails = `<h1 class="mb-4 text-personal">Animale per numero di microchip: ${microchip}</h1>`;
                    animalDetails += `<p class="text-personal">Nome: <span class="text-danger">${animal.name}</span></p>`;
                    animalDetails += `<p class="text-personal">Data Registrazione: <span class="text-danger">${animal.registrationDate}</span></p>`;
                    animalDetails += `<p class="text-personal">Data Di Nascita: <span class="text-danger">${animal.birthDate}</span></p>`;
                    animalDetails += `<p class="text-personal">Colore: <span class="text-danger">${animal.color}</span></p>`;
                    animalDetails += `<img src="data:image/jpeg;base64,${animal.image}" class="text-personal" alt="Immagine di ${animal.name}" style="max-width: 200px; max-height: 200px;" />`;
                    if (animal.isHospitalized) {
                        animalDetails += `<p class="text-personal">Ricovero: <span class="text-success">Ricoverato</span></p>`;
                    } else {
                        animalDetails += `<p class="text-personal">Ricovero: <span class="text-danger">Non ricoverato</span></p>`;
                    }

                    // Aggiungi qui altri campi che desideri visualizzare   
                    div.append(animalDetails);
                });
            } else {
                div.append($(`<h1 class="mb-4 text-light">Nessun animale trovato per numero di microchip: ${microchip}</h1>`));
          }
        }
    });
}

// Uso debounce per limitare la frequenza di chiamate API
let debounceTimer;
function debounce(func, delay) {
    return function (...args) {
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(() => func.apply(this, args), delay);
    };
}


//
//show past examinations before creating a new one for selected animal


function triggerPastVisits() {
    let IdAnimal = $('#pastVisits').val();
    $.ajax({
        url: `${pastVisitsPath}?IdAnimal=${IdAnimal}`,
        method: 'GET',
        success: (data) => {
            console.log("Success", data);
            let examDiv = $('#examinationsList');
            examDiv.empty();
            if (data.length > 0) {
                examDiv.prepend(`<h1 class="mb-4">Lista esami pregressi</h1>`)
                data.forEach(exam => {
                    let examDetails = `
                    
                        <div class="card mb-3">
                            <div class="card-body">
                                <h5 class="card-title">Esame n.${exam.idExamination}</h5>
                                <p class="card-text">
                                    <strong>Data:</strong> ${new Date(exam.examinationDate).toLocaleDateString()}<br />
                                    <strong>Nome Esame:</strong> ${exam.examinationName}<br />
                                    <strong>Trattamento:</strong> ${exam.treatment}
                                </p>
                            </div>
                        </div>
                    
                    `;
                    examDiv.append(examDetails);
                });
            } else {
                examDiv.append($(`<h1 class="mb-4">Nessun esame trovato per l'animale con ID: ${IdAnimal}</h1>`));
            }
        }
    });
}


//
//in productslist search drawer & locker for selected product


function FindLockers(idProduct) {
    $.ajax({
        url: `${lockersPath}?id=${idProduct}`,
        method: 'GET',
        success: (data) => {
            console.log("Success", data);
            let modalBody = $('#modalBodyContent');
            modalBody.empty(); 

            if (data) {
                let productLocker = `
                    <div class="my-bg mb-2">
                        <div class="card-body">
                            <ul class="list-group list-group-flush">
                                <li class="personal-tr text-light"><strong>ID cassetto:</strong> ${data.drawers.idDrawer}</li>
                                <li class="personal-tr text-light"><strong>ID armadietto:</strong> ${data.drawers.lockerId}</li>
                            </ul>
                        </div>
                    </div>
                `;
                modalBody.append(productLocker);
            } else {
                modalBody.append($(`<h1 class="mb-4">Nessun prodotto trovato con l'id: ${idProduct}</h1>`));
            }

            // Show the modal
            $('#detailsModal').modal('show');
        },
        error: (err) => {
            console.error("Error", err);
        }
    });
}



//
//filter
$(document).ready(function () {
    $('#filter').on('change', function () {
        var selectedType = $(this).val();

        if (selectedType === 'All') {
            $('.prow').show();
            $('#printLocker').empty();
        } else {
            $('.prow').each(function () {
                var productType = $(this).data('type');

                if (productType === selectedType) {
                    $(this).show();
                    $('#printLocker').empty();
                } else {
                    $(this).hide();

                }
            });
        }
    });
});

//
//disable drawer selection if "alimento" is selected
$(document).ready(function () {
    $("#alimentoSelez").on("change", function () {
        var selectedValue = $(this).val();
        var drawerSelect = $("#selectDrawer");
        var drawerSelect2 = $("#selectDrawer2");
        var drawerValidation = $("#selectDrawer3");

        if (selectedValue === "Alimento") {
            drawerSelect.prop('disabled', true);
            drawerSelect[0].selectedIndex = 0;
            drawerSelect2.hide();
            drawerValidation.hide();
        } else {
            drawerSelect.prop('disabled', false);
            drawerSelect.show();
            drawerSelect2.show();
            drawerValidation.show(); 
        }
    });

    // Trigger change event on page load to ensure correct initial state
    $("#alimentoSelez").trigger('change');
});



// Funzione per reperire il numero di farmaci venduti in una certa data
function ProductsFromDate() {
    let date = $('#dateInput').val();

    $.ajax({
        url: `${datePath}?date=${date}`,
        method: 'GET',
        success: (data) => {
            console.log("Data received:", data);
            let div = $('#date');
            div.empty(); 

            if (data && data.length > 0) {
                let list = `<h1 class="mb-4 text-personal">Farmaci venduti il giorno ${date}:</h1>`;
                data.forEach(order => {
                    
                    list += `
                        <div class="table-dark-personal my-4">
                            <div>
                                <h2 class="text-red">Nome Farmaco: <span class="text-light"> ${order.product.productName}</span></h2>
                            </div>
                          
                                <li class="personal-tr text-light mb-0"><strong>ID:</strong> ${order.product.idProduct}</p>
                                <li class="personal-tr text-light"><strong>Uso:</strong> ${order.product.use}</p>
                          
                        </div>`;
                        
                });
                div.append(list);
            } else {
                div.append(`<h1 class="mb-4 text-personal">Nessun farmaco trovato per il giorno ${date}</h1>`);
            }
        },
        error: (err) => {
            console.error("Errore durante il recupero dei dati:", err);
            $('#date').empty().append('<h1 class="mb-4">Errore nel recupero dei dati. Per favore, riprova più tardi.</h1>');
        }
    });
}

// Funzione per reperire il numero di farmaci venduti tramite un CF
function ProductsFromCF() {
    let cf = $('#cfInput').val();


    $.ajax({
        url: `${cfPath}?cf=${cf}`,
        method: 'GET',
        success: (data) => {
            console.log("Data received:", data);
            let div = $('#cf');
            div.empty();

            if (data && data.length > 0) {
                let list = `<h1 class="mt-4 text-personal">Farmaci per il CF ${cf}:</h1>`;
                data.forEach(order => {

                    list += `
                        <div class="card my-4 me-2">
                            <div class="card-header bg-custom">
                                <h2 class="text-red">Nome Farmaco: <span class="text-personal">${order.product.productName}</span></h2>
                                <h5 class="card-text text-red"><strong>Uso:</strong> <span class="text-personal">${order.product.use}</span></h5>
                                <h5 class="card-text text-red"><strong>Data Ordine:</strong> <span class="text-personal">${order.orderDate}</span></h5>
                                <h5 class="card-text text-red"><strong>Quantità:</strong> <span class="text-personal">${order.orderQuantity}</span></h5>
                                <h5 class="card-text text-red"><strong>Num. Prescrizione:</strong> <span class="text-personal">${order.prescriptionNumber}</span></h5>
                            </div>
                        </div>
                    `;

                });
                div.append(list);
            } else {
                div.append(`<h1 class="mb-4 text-personal">Nessun farmaco trovato per il CF ${cf}</h1>`);
            }
        },
        error: (err) => {
            console.error("Errore durante il recupero dei dati:", err);
            $('#cf').empty().append('<h1 class="mb-4">Errore nel recupero dei dati. Per favore, riprova più tardi.</h1>');
        }
    });
}

$('#microchip').on('keydown', debounce(countByMicroChip, 300));

// Gestore dell'evento click per il pulsante
$('#chipButton').on('click', () => {
    countByMicroChip();
});



$('#pastVisits').on('change', () => {
    triggerPastVisits();
});

$(document).on('click', '.prow', function () {
    let idProduct = $(this).find('#idProduct').text().trim();
    FindLockers(idProduct);
});


$('#dateBtn').on('click', () => {
    ProductsFromDate();
})

$('#cfBtn').on('click', () => {
    ProductsFromCF();
})

// Se il valore del cf è minore di 16, svuota il div
$('#cfInput').on('input', () => {
    let cf = $('#cfInput').val();
    if (cf.length < 16) {
        $('#cf').empty();
    }
});