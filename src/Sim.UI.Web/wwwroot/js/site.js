
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $('.modal').modal();

    $('.dropdown-trigger').dropdown(
        {
            hover: true,
            constrainWidth: false,
            coverTrigger: false,
            closeOnClick: false
        });

    $('select').formSelect();

    $('.collapsible').collapsible({ accordion: true });

    $('.sidenav').sidenav();

    $('.tabs').tabs();

    $('.datepicker').datepicker();

    $('.tooltipped').tooltip();

    $('.scrollspy').scrollSpy();

    M.updateTextFields();

    var toggler = document.getElementsByClassName("caret");
    var i;

    for (i = 0; i < toggler.length; i++) {
        toggler[i].addEventListener("click", function () {
            this.parentElement.querySelector(".nested").classList.toggle("active");
            this.classList.toggle("caret-down");
        });
    }
});

function CloseDropdown() {
    $('.dropdown-trigger').dropdown(
        {
            hover: false,
            constrainWidth: false,
            coverTrigger: false,
            closeOnClick: true
        });
};

function removerMascaraCNPJ(cnpj) {
    // Substitui tudo o que não é dígito por uma string vazia
    return cnpj.replace(/\D/g, '');
}
