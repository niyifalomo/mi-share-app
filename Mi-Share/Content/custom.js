
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
                    var formData = "{id:'" + itemId + "'}";

                    $.ajax({
                        type: "POST",
                        url: '/Collection/DeleteItem',
                        data: formData,
                        dataType: 'json',
                        contentType:"application/json",
                        success: function (result) {

                            new PNotify({
                                title: 'Deleted Successfully',
                                text: "Item deleted",
                                type: 'success',
                                styling: 'bootstrap3'
                            });
                            //$('#statusmessage').html('<div class="alert alert-success">Success</div>');

                            var itemsUrl = $("#ItemList").data("url");

                            $("#ItemList").load(itemsUrl, function () {
                                $('.datatable').DataTable();
                            });

                        }
                    });
                }

            }

            $('body').on('click', '.GrantLibraryRequest', function (e) {

                var collectionId = $(this).attr('data-id');

                var button = $(this);

                var formData = "{collectionId:'" + collectionId + "'}";

                if (confirm("Are you sure you want to grant this request?")) {

                    $.ajax({
                        type: "POST",
                        url: '/Request/GrantCollectionAccessRequest',
                        data: formData,
                        dataType: 'json',
                        contentType: "application/json",
                        success: function (data) {
                            button.text("Granted");
                            button.addClass("disabled");

                            new PNotify({
                                title: 'Success',
                                text: "Request granted successfully",
                                type: 'success',
                                styling: 'bootstrap3'
                            });





                        }
                    });
                }

            })

            $('body').on('click', '.DenyLibraryRequest', function (e) {

                var collectionId = $(this).attr('data-id');

                var button = $(this);

                var formData = "{collectionId:'" + collectionId + "'}";

                if (confirm("Are you sure you want to deny this request?")) {

                    $.ajax({
                        type: "POST",
                        url: '/Request/DenyCollectionAccessRequest',
                        data: formData,
                        dataType: 'json',
                        contentType: "application/json",
                        success: function (data) {
                            button.text("Denied");
                            button.addClass("disabled");

                            new PNotify({
                                title: 'Success',
                                text: "Request denied successfully",
                                type: 'success',
                                styling: 'bootstrap3'
                            });





                        }
                    });
                }

            })
            $('body').on('click', '.CancelCollectionAccessRequest', function (e) {

                var collectionId = $(this).attr('data-id');

                var button = $(this);

                var formData = "{collectionId:'" + collectionId + "'}";

                if (confirm("Are you sure you want to stop this request?")) {

                    $.ajax({
                        type: "POST",
                        url: '/Request/CancelCollectionAccessRequest',
                        data: formData,
                        dataType: 'json',
                        contentType: "application/json",
                        success: function (data) {
                            button.text("Deleted");
                            button.removeClass("label-success");
                            button.addClass("label-warning disabled");
                            new PNotify({
                                title: 'Success',
                                text: "Request cancelled successfully",
                                type: 'success',
                                styling: 'bootstrap3'
                            });





                        }
                    });
                }

            })


            $('body').on('click', '.RequestCollection', function (e) {

                var userId = $(this).attr('data-id');

                var button = $(this);
               
                var formData = "{userId:'" + userId + "'}";

                $.ajax({
                    type: "POST",
                    url: '/Request/RequestAccess',
                    data: formData,
                    dataType: 'json',
                    contentType: "application/json",
                    success: function (data) {
                        button.text("Pending");
                        button.removeClass("btn-primary");
                        button.removeClass("RequestCollection");
                        button.addClass("btn-warning disabled"); 
                        new PNotify({
                            title: 'Success',
                            text: "Request sent successfully",
                            type: 'success',
                            styling: 'bootstrap3'
                        });

                        

                        

                    }
                });
                
            })



            $('body').on('submit','.ItemForm',function (e) {
                
                e.preventDefault();
                    
                var form = $(this);
                    $.ajax({
                        type: "POST",
                        url: $(this).attr('action'),
                        data: $(this).serialize(),
                        success: function (result) {
                            

                            //$('#statusmessage').html('<div class="alert alert-success">Success</div>');
                            new PNotify({
                                title: 'Success',
                                text: "Item saved successfully",
                                type: 'success',
                                styling: 'bootstrap3'
                            });

                            var itemsUrl = $("#ItemList").data("url");

                            $("#ItemList").load(itemsUrl, function () {
                                $('.datatable').DataTable();
                            });
                            
                        }
                    });
                
            });