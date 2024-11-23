$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $('#quantity_value').val(); // phải xài .val() chứ ko xài .text() được
        // .text() không hoạt động được với input field nên luôn trả về 1 chuỗi rỗng
        if (tQuantity != '') {
            quantity = parseInt(tQuantity);
        }
        alert('Đã thêm sản phẩm vào giỏ hàng!')
        $.ajax({
            url: '/cart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    ShowCount();
                }
            }

        })
    });
    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        $.ajax({
            url: '/cart/Delete',
            type: 'POST',
            data: { id: id},
            success: function (rs) {
                if (rs.Success) {
                    $('#trow_' + id).remove();
                    ShowCount();

                }
            }

        })
    });
    $('body').on('click', '.btnAddToCartMini', function (e) { // THÊM SẢN PHẨM BẰNG NÚT NHỎ
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        alert('Đã thêm sản phẩm vào giỏ hàng!')
        $.ajax({
            url: '/cart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    ShowCount();
                }
            }

        })
    });
});

function ShowCount() {
    $.ajax({
        url: '/cart/ShowCount',
        type: 'GET',
        success: function (rs) {
                $('#checkout-items').html(rs.Count); 
        }

    })
}