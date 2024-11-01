function myStatus() {
    var st = document.querySelector('#chkstatus');
    var uid = document.querySelector('#meustatusonline').value;
    const btnstatus = document.querySelector('#btnstatus');
    btnstatus.classList.remove('online');
    btnstatus.classList.remove('offline');
    if (!st.checked) { $('#btnstatus').addClass('offline'); }
    else { $('#btnstatus').addClass('online'); }
    var formData = { id: uid, val: st.checked };
    $.get('/Identity/OnlineStatus', { id: uid, val: st.checked },
        function (returnedData) {
        });
}

// Função para alternar o tema entre 'light' e 'dark'
function toggleTheme() {
    const currentTheme = document.documentElement.getAttribute('data-theme');
    if (currentTheme === 'dark') {
        document.documentElement.setAttribute('data-theme', 'light');
        localStorage.setItem('theme', 'light'); // Salva a escolha do usuário
    } else {
        document.documentElement.setAttribute('data-theme', 'dark');
        localStorage.setItem('theme', 'dark');  // Salva a escolha do usuário
    }
}

// Botão de alternância de tema
const themeButton = document.getElementById('theme-button');
themeButton.addEventListener('click', toggleTheme);

// Verifica se há uma preferência de tema salva
const savedTheme = localStorage.getItem('theme');
if (savedTheme) {
    document.documentElement.setAttribute('data-theme', savedTheme);
} else {
    // Caso não haja preferência salva, usa o tema claro por padrão
    document.documentElement.setAttribute('data-theme', 'light');
}

function reloadPage() {
    location.reload();
}