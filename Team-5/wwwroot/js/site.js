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