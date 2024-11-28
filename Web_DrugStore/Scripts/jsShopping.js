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
        var conf = confirm('Bạn có chắc muốn xoá sản phẩm này khỏi giỏ hàng ?');
        if (conf == true) {
            $.ajax({
                url: '/cart/Delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#trow_' + id).remove();
                        ShowCount();
                        LoadCart();
                    }
                }

            })
        }

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

    $('body').on('keydown', '.cart-input-quantity', function (event) {  // ngăn phím backspace và delete xoá số lượng sản phẩm
        let key = event.which || event.keyCode;
        $(this).data('isDeleteKey', key === 8 || key === 46); // 8: Backspace, 46: Delete
    });

    $('body').on('input', '.cart-input-quantity', function () {
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
        var id = $(this).data("id");
        Update(id, sanitizedValue);

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
function Update(id, quantity) {
    $.ajax({
        url: '/cart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart()
            }
        }

    })
}
function LoadCart() {
    $.ajax({
        url: '/cart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }

    })
}