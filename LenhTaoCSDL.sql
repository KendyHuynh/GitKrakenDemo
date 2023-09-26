-- Tạo cơ sở dữ liệu
USE master;
GO
--Drop database electronic_store
CREATE DATABASE electronic_store;

-- Sử dụng cơ sở dữ liệu mới tạo
USE electronic_store;

--Quản lý sản phẩm
-- Bảng danh mục sản phẩm
CREATE TABLE categories (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name VARCHAR(255) NOT NULL
);
-- Bảng thông tin sản phẩm
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name VARCHAR(255) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    category_id INT,
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
--Đăng ký và đăng nhập người dùng
-- Bảng thông tin người dùng
CREATE TABLE users (
    user_id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL,
    password VARCHAR(100) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    address VARCHAR(255),
    phone_number VARCHAR(15)
);
--Quản lý giỏ hàng
-- Bảng thông tin giỏ hàng
CREATE TABLE carts (
    cart_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    cart_total DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Bảng các mặt hàng trong giỏ hàng
CREATE TABLE cart_items (
    cart_item_id INT PRIMARY KEY IDENTITY(1,1),
    cart_id INT,
    product_id INT,
    quantity INT NOT NULL,
    subtotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (cart_id) REFERENCES carts(cart_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);
--Quản lý đơn hàng
-- Bảng thông tin đơn hàng
CREATE TABLE orders (
    order_id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    order_date DATE,
    total_price DECIMAL(10, 2) NOT NULL,
    status VARCHAR(20) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Bảng các mặt hàng trong đơn hàng
CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY IDENTITY(1,1),
    order_id INT,
    product_id INT,
    quantity INT NOT NULL,
    subtotal DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);
-- Bảng đánh giá sản phẩm
CREATE TABLE product_reviews (
    review_id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    user_id INT,
    rating INT NOT NULL,
    comment TEXT,
    review_date DATE,
    FOREIGN KEY (product_id) REFERENCES products(product_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);
-- Bảng người bán hàng
CREATE TABLE sellers (
    seller_id INT PRIMARY KEY IDENTITY(1,1),
    seller_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL
);
--Bảng thương hiệu sản phẩm 
CREATE TABLE brands (
    brand_id INT PRIMARY KEY IDENTITY(1,1),
    brand_name VARCHAR(255) NOT NULL
);
-- Thêm cột Image và Color vào bảng products và liên kết brand
ALTER TABLE products
ADD	brand_id INT,
	Image VARCHAR(255), -- Định dạng cho đường dẫn hình ảnh
    Color VARCHAR(50),
FOREIGN KEY (brand_id) REFERENCES brands(brand_id);

-- Thêm cột địa chỉ giao hàng và liên kết seller
ALTER TABLE orders
ADD shipping_address VARCHAR(255) NOT NULL;

<<<<<<< HEAD

=======
>>>>>>> master
