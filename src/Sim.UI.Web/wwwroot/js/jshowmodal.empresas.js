$(document).ready(function () {
    $(".modalrfb").click(function () {
        var customerId = $(this).data('id');
        $(".modal-content").empty();
        $.getJSON(`/Censo/Empresas/Consulta-socio?id=${customerId}&handler=DetalheEmpresa`, (data) => {
            $.each(data, function (i, item) {
                $(".modal-content").append(`<div>Protocolo: ${item.protocolo}</div>`);
                $(".modal-content").append(`<div>Data: ${item.data}</div>`);
                $(".modal-content").append(`<div>Setor: ${item.setor}</div>`);
                $(".modal-content").append(`<div>Canal: ${item.canal}</div>`);
                $(".modal-content").append(`<div>Serviços: ${item.servicos}</div>`);
                $(".modal-content").append(`<div>Descricao: ${item.descricao}</div>`);
                $(".modal-content").append(`<div class="divider"></div>`);
                $(".modal-content").append(`<div>Nome: ${item.pessoa.nome}</div>`);
                $(".modal-content").append(`<div>CPF: ${item.pessoa.cpf}</div>`);
                $(".modal-content").append(`<div>Data Nascimento: ${item.pessoa.data_Nascimento}</div>`);
                $(".modal-content").append(`<div>CEP: ${item.pessoa.cep}</div>`);
                $(".modal-content").append(`<div>${item.pessoa.logradouro} ${item.pessoa.numero}</div>`);
                $(".modal-content").append(`<div>${item.pessoa.bairro}, ${item.pessoa.cidade} - ${item.pessoa.uf}</div>`);
                $(".modal-content").append(`<div>Telefone: ${item.pessoa.tel_Movel}, E-mail: ${item.pessoa.email}</div>`);
                $(".modal-content").append(`<div class="divider"></div>`);
                $(".modal-content").append(`<div>${item.empresa.cnpj} ${item.empresa.situacao_Cadastral}</div>`);
                $(".modal-content").append(`<div>${item.empresa.nome_Empresarial}</div>`);
                $(".modal-content").append(`<div>${item.empresa.nome_Fantasia}</div>`);
                $(".modal-content").append(`<div>${item.empresa.data_Abertura}</div>`);
                $(".modal-content").append(`<div>${item.empresa.cnaE_Principal} ${item.empresa.atividade_Principal}</div>`);
                $(".modal-content").append(`<div>${item.empresa.atividade_Secundarias}</div>`);
                $(".modal-content").append(`<div>${item.empresa.cep} ${item.empresa.logradouro} ${item.empresa.numero}</div>`);
                $(".modal-content").append(`<div>${item.empresa.bairro} ${item.empresa.municipio} ${item.empresa.uf}</div>`);
                $(".modal-content").append(`<div>${item.empresa.telefone} ${item.empresa.email}</div>`);
            });
        });

    });
});