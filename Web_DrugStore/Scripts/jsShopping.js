$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {  // xử lý khi bấm nút thêm giỏ hàng
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
    $('body').on('click', '.btnDelete', function (e) { // dành cho xoá sản phẩm bên Giỏ hàng chính
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

    $('body').on('keydown', '.cart-plus-minus-box', function (event) {
        let key = event.which || event.keyCode;
        $(this).data('isDeleteKey', key === 8 || key === 46); // 8: Backspace, 46: Delete
    });

    $('body').on('input', '.cart-plus-minus-box', function () {
        let value = $(this).val().trim();
        let sanitizedValue = value.replace(/[^0-9]/g, '');
        let prevValue = $(this).data('prevValue') || '1';

        if (sanitizedValue === '' && $(this).data('isDeleteKey') && parseInt(prevValue) >= 1) {
            sanitizedValue = '1';
        } else if (sanitizedValue === '' || parseInt(sanitizedValue) < 1) {
            sanitizedValue = '1';
        }

        $(this).val(sanitizedValue);
        $(this).data('prevValue', sanitizedValue);
        $(this).data('isDeleteKey', false);
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