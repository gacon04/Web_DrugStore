$(document).on('click', '.btnAddToCart', function (e) {
    e.preventDefault(); // Ngăn chặn reload hoặc hành động mặc định
    const productId = $(this).data('id'); // Lấy ID sản phẩm từ thuộc tính data-id
    var quantity = 1;
    var tQuantity = $('#quantity_value').val(); // phải xài .val() chứ ko xài .text() được
    // .text() không hoạt động được với input field nên luôn trả về 1 chuỗi rỗng
    if (tQuantity != '') {
        quantity = parseInt(tQuantity);
    }
    addToCart(productId, quantity); // Gọi hàm addToCart
});
function addToCart(productId, quantity) {
    $.ajax({
        url: '/Cart/AddToCart', // Action trên server
        type: 'POST',
        data: { id: productId, quantity: quantity },
        dataType: 'json',
        success: function (response) {
            if (response.Success) {
                // Cập nhật số lượng giỏ hàng
                $('#count-cart-items').text(response.Count);
                alert(response.msg); // Thông báo thành công
            } else {
                alert('Không thể thêm sản phẩm vào giỏ hàng. Vui lòng thử lại!');
            }
        },
        error: function () {
            alert('Có lỗi xảy ra khi thêm sản phẩm vào giỏ hàng.');
        }
    });
}
