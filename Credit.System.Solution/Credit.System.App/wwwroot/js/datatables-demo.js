// Call the dataTables jQuery plugin
$(document).ready(function() {
  $('#datatablesSimple').DataTable();
});



//var table = $('#datatablesSimple').DataTable({
//    "language": {
//        "sEmptyTable": "Nenhum dado dispon�vel",  // No data available
//        "sInfo": "Mostrando de _START_ at� _END_ de _TOTAL_ entradas",  // Showing entries
//        "sInfoEmpty": "Mostrando 0 at� 0 de 0 entradas",  // Showing 0 of 0 entries
//        "sInfoFiltered": "(filtrado de _MAX_ entradas)",  // Filtered
//        "sLengthMenu": "Mostrar _MENU_ entradas",  // Length menu
//        "sLoadingRecords": "Carregando...",  // Loading message
//        "sProcessing": "Processando...",  // Processing message
//        "sSearch": "Pesquisar:",  // Search placeholder
//        "sZeroRecords": "Nenhum registro encontrado",  // No records found
//        "oPaginate": {
//            "sFirst": "Primeiro",  // First page
//            "sLast": "�ltimo",  // Last page
//            "sNext": "Pr�ximo",  // Next page
//            "sPrevious": "Anterior"  // Previous page
//        },
//        "oAria": {
//            "sSortAscending": ": ativar para ordenar a coluna de forma crescente",  // Ascending sort
//            "sSortDescending": ": ativar para ordenar a coluna de forma decrescente"  // Descending sort
//        }
//    }
//});