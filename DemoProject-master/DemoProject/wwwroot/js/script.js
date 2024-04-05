$(document).ready(function () {
    $('#employeeTable').DataTable();
});

//new DataTable('#employeeTable', {
//    pagingType: 'simple_numbers'
//});

//for table row add when click on row
$(document).ready(function () {
    // Attach click event to table rows with class 'state-row'
    $('#employeeTable tr').click(function () {
        var state = $(this).find('td:eq(1)').text().trim(); // Get the state name from the clicked row
        var employeeRows = $('tr.employee-row[data-state="' + state + '"]'); // Get employee rows with matching state
        if (employeeRows.length > 0) {
            // Toggle display of employee rows
            employeeRows.toggle();
        } else {
            // If employee rows not found, fetch data via AJAX and append
            $.ajax({
                url: '/Home/GetEmployeesByState', // Replace with your actual URL
                type: 'POST',
                data: { state: state },
                dataType: 'html',
                success: function (data) {
                    // Append fetched data below the clicked state row
                    $(data).insertAfter($(this).closest('#employeeTable tr'));
                }
            });
        }
    });
});

$(document).ready(function () {
    // Select/Deselect All
    $('#selectAll').click(function () {
        $('#SelectedCategories option').prop('selected', $(this).prop('checked'));
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var dropdownToggle = document.querySelector(".dropdown-toggle");
    var dropdownMenu = document.querySelector(".dropdown-menu");

    dropdownToggle.addEventListener("click", function () {
        dropdownMenu.classList.toggle("show");
    });

    document.addEventListener("click", function (event) {
        if (!dropdownToggle.contains(event.target) && !dropdownMenu.contains(event.target)) {
            dropdownMenu.classList.remove("show");
        }
    });

    var checkboxes = document.querySelectorAll('input[type="checkbox"]');
    checkboxes.forEach(function (checkbox) {
        checkbox.addEventListener("click", function (event) {
            event.stopPropagation(); // Prevent dropdown from closing when checkbox is clicked
            var isSelected = checkbox.checked;
            var dropdownItem = checkbox.closest(".dropdown-item");
            dropdownItem.classList.toggle("selected", isSelected);
        });
    });
});
    
      
      
    
