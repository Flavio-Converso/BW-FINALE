//search animal by microchip number
let chipPath = "/Animals/GetAnimalDataByMicrochip";
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
                    let animalDetails = `<h1 class="mb-4">Animale per numero di microchip: ${microchip}:</h1>`;
                    animalDetails += `<p>Nome: <span class="text-danger">${animal.name}</span></p>`;
                    animalDetails += `<p>Data Registrazione: <span class="text-danger">${animal.registrationDate}</span></p>`;
                    animalDetails += `<p>Data Di Nascita: <span class="text-danger">${animal.birthDate}</span></p>`;
                    animalDetails += `<p>Colore: <span class="text-danger">${animal.color}</span></p>`;
                    animalDetails += `<img src="data:image/jpeg;base64,${animal.image}" alt="Immagine di ${animal.name}" style="max-width: 200px; max-height: 200px;" />`;

                    // Aggiungi qui altri campi che desideri visualizzare   
                    div.append(animalDetails);
                });
            } else {
                div.append($(`<h1 class="mb-4">Nessun animale trovato per numero di microchip: ${microchip}</h1>`));
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

$('#microchip').on('keydown', debounce(countByMicroChip, 300));

// Gestore dell'evento click per il pulsante
$('#chipButton').on('click', () => {
    countByMicroChip();
});


//
//show past examinations before creating a new one for selected animal
let pastVisitsPath = "/Examination/ExaminationsListByIdAnimal";

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
                    let examDetails =
                        `                      
                        <ul>
                        <li> ${exam.idExamination} </li>
                        <li> ${exam.examinationDate} </li>
                        <li> ${exam.examinationName} </li>
                        <li> ${exam.treatment} </li>
                        </ul>
                    `;                 
                    examDiv.append(examDetails);
                });
            } else {
                examDiv.append($(`<h1 class="mb-4">Nessun esame trovato l'id: ${IdAnimal}</h1>`));
            }
        }
    });
}

$('#pastVisits').on('change', () => {
    triggerPastVisits();
});

//
//in productslist search drawer & locker for selected product
let lockersPath = "/Product/FindLockers";

function FindLockers(idProduct) {
    $.ajax({
        url: `${lockersPath}?id=${idProduct}`,
        method: 'GET',
        success: (data) => {
            console.log("Success", data);
            let div = $('#printLocker');
            div.empty();
            if (data) {
                let productLocker = `
                    <ul>
                        <li>ID cassetto: ${data.drawers.idDrawer}</li>
                        <li>ID armadietto: ${data.drawers.lockerId}</li>
                    </ul>
                `;
                div.append(productLocker);
            } else {
                div.append($(`<h1 class="mb-4">Nessun prodotto trovato con l'id: ${idProduct}</h1>`));
            }
        },
        error: (err) => {
            console.error("Error", err);
        }
    });
}

$(document).on('click', '.prow', function () {
    let idProduct = $(this).find('#idProduct').text().trim();
    FindLockers(idProduct);
});

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
        var selectedValue = $(this).val();  // Corrected this line
        var drawerSelect = $("#selectDrawer");
        var drawerSelect2 = $("#selectDrawer2");

        if (selectedValue === "Alimento") {
            drawerSelect.prop('disabled', true);  // Corrected this line
            drawerSelect[0].selectedIndex = 0;  // Reset selected index
            drawerSelect.hide();  // Corrected this line
            drawerSelect2.hide();  // Corrected this line
        } else {
            drawerSelect.prop('disabled', false);  // Corrected this line
            drawerSelect.show();  // Corrected this line
            drawerSelect2.show();  // Corrected this line
        }
    });

    // Trigger change event on page load to ensure correct initial state
    $("#alimentoSelez").trigger('change');
});

let datePath = '/Product/GetProductsFromDate';

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
                let list = `<h1 class="mb-4">Farmaci venduti il giorno ${date}:</h1>`;
                data.forEach(order => {
                    
                    list += `
                        <h1 class="text-danger">Nome Farmaco: ${order.product.productName}</h1>
                        <h1 class="text-danger">Uso: ${order.product.use}</h1>`; 
                });
                div.append(list);
            } else {
                div.append(`<h1 class="mb-4">Nessun farmaco trovato per il giorno ${date}</h1>`);
            }
        },
        error: (err) => {
            console.error("Errore durante il recupero dei dati:", err);
            $('#date').empty().append('<h1 class="mb-4">Errore nel recupero dei dati. Per favore, riprova più tardi.</h1>');
        }
    });
}

$('#dateBtn').on('click', () => {
    ProductsFromDate();
})