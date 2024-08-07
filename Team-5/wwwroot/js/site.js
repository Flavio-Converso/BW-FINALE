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
                    
                    div.append(animalDetails);
                });
            } else {
                div.append($(`<h1 class="mb-4">Nessun animale trovato per numero di microchip: ${microchip}</h1>`));
          }
        }
    });
}

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

$(document).on('click', '#row', function () {
    let idProduct = $(this).find('#idProduct').text().trim();
    FindLockers(idProduct);
});

//
//disable drawer selection if "alimento" is selected
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("alimentoSelez").addEventListener("change", function () {
        var selectedValue = this.value;
        var drawerSelect = document.getElementById("selectDrawer");
        var drawerSelect2 = document.getElementById("selectDrawer2");

        if (selectedValue === "Alimento") {
            drawerSelect.disabled = true;
            drawerSelect.selectedIndex = 0;
            drawerSelect.hidden = true;
            drawerSelect2.hidden = true;
        } else {
            drawerSelect.disabled = false;
            drawerSelect.hidden = false;
            drawerSelect2.hidden = false;
        }
    });
});
