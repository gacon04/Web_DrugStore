$(document).ready(function () {
    $(".remove-form").submit(function (e) {
        e.preventDefault(); // Ngăn chặn hành vi mặc định của form

        var form = $(this);
        var productId = form.find("button").data("id") || form.find("input[name='id']").val();

        $.ajax({
            type: "POST",
            url: form.attr("action"),
            data: form.serialize(),
            success: function (response) {
                if (response.Success) {
                    // Xóa hàng sản phẩm khỏi bảng
                    $("#wishlist-item-" + productId).fadeOut(500, function () {
                        $(this).remove();
                        // Kiểm tra nếu không còn sản phẩm nào, hiển thị thông báo
                        if ($("tbody tr").length === 0) {
                            $("tbody").html('<tr><td colspan="5"><h5 style="text-align:center">Danh sách yêu thích của bạn đang trống</h5></td></tr>');
                        }
                    });
                } 
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
                alert("Đã xảy ra lỗi khi xóa sản phẩm. Vui lòng thử lại.");
            }
        });
    });

    $('body').on('click', '.btnAddToWishlist', function (e) {
        
            e.preventDefault();
        var productId = $(this).data('id');
        

        $.ajax({
            url: '/WishList/AddToWishList',
            type: 'POST',
            data: { id: productId },
            success: function (data) {
                if (data.Success) {
                    alert(data.Message);
                } else {
                    alert(data.Message);
                }
            }

        })
    });
   
});

