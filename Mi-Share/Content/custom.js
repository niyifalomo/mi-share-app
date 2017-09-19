
        $('.datatable').DataTable();

        $(document).ready(function () {
            $('.sidebar-menu li a').click(function (e) {

                $('.sidebar-menu li.active').removeClass('active');

                var $parent = $(this).parent();
                $parent.addClass('active');
                e.preventDefault();
            });
        });


        
           // $('.editModal').modal();
        

            function editItem(itemId) {
                
            $.ajax({
                url: '/Collection/EditItem/' + itemId, 
                success: function (data) {
                    $('#EditForm').html(data);
                    $('#EditForm').modal();
                }
            });
            }

            function deleteItem(itemId) {
                if (confirm("Are you sure you want to delete this Item?"))
                {

                }

            }