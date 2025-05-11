// Call the dataTables jQuery plugin
$(document).ready(function() {
    $('#responsiveTable').DataTable({
        "responsive": true,
        "paging": false,        
        "searching": false,     
        "info": false,      
        "ordering": false,      
    });
});

console.log("Responsive ON")

