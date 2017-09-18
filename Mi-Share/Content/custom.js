
        $('.datatable').DataTable();

        $(document).ready(function () {
            $('.sidebar-menu li a').click(function (e) {

                $('.sidebar-menu li.active').removeClass('active');

                var $parent = $(this).parent();
                $parent.addClass('active');
                e.preventDefault();
            });
        });