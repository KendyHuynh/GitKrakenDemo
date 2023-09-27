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
    category_name NVARCHAR(255) NOT NULL
);
-- Bảng thông tin sản phẩm
CREATE TABLE products (
    product_id INT PRIMARY KEY IDENTITY(1,1),
    product_name NVARCHAR(255) NOT NULL,
    description NVARCHAR(255),
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
    full_name NVARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    address NVARCHAR(255),
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
    status NVARCHAR(20) NOT NULL,
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
    comment NVARCHAR(255),
    review_date DATE,
    FOREIGN KEY (product_id) REFERENCES products(product_id),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);
-- Bảng người bán hàng
CREATE TABLE sellers (
    seller_id INT PRIMARY KEY IDENTITY(1,1),
    seller_name NVARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(100) NOT NULL
);
--Bảng thương hiệu sản phẩm 
CREATE TABLE brands (
    brand_id INT PRIMARY KEY IDENTITY(1,1),
    brand_name NVARCHAR(255) NOT NULL
);
-- Thêm cột Image và Color vào bảng products và liên kết brand
ALTER TABLE products
ADD	brand_id INT,
	Image VARCHAR(255), -- Định dạng cho đường dẫn hình ảnh
    Color VARCHAR(50),
FOREIGN KEY (brand_id) REFERENCES brands(brand_id);

-- Thêm cột địa chỉ giao hàng và liên kết seller
ALTER TABLE orders
ADD shipping_address NVARCHAR(255) NOT NULL;

--Insert dữ liệu
INSERT INTO categories (category_name)
VALUES
    (N'Điện thoại'),
    (N'Laptop'),
    (N'Máy tính - PC'),
	(N'Đồng hồ'),
	(N'Tivi'),
	(N'Tai nghe'),
	(N'Phụ kiện');
INSERT INTO brands(brand_name)
VALUES
    (N'Iphone'),
    (N'Samsung'),
    (N'Oppo'),
	(N'Xiaomi'),
	(N'Nokia'),
	(N'Realme'),
	(N'Vivo');
INSERT INTO products(product_name,description,price,category_id,Image,Color,brand_id)
VALUES
    (N'IPhone 13 128GB',N'Máy mới 100% , chính hãng Apple Việt Nam.',13000000,1,'Iphone13.jpg','Blue',1),
    (N'Samsung Galaxy Z Flip 4 5G 128GB',N'Galaxy Z Flip4 là chiếc smartphone sinh ra dành cho các tín đồ thời trang',6000000,1,'Samsung_Z_Flip4.jpg','Yellow',2),
    (N'IPhone 13 128GB',N'Máy mới 100% , chính hãng Apple Việt Nam.',13000000,1,'Iphone13.jpg','White',1),
	(N'IPhone 15 128GB',N'Máy mới 100% , chính hãng Apple Việt Nam.',13000000,1,'Iphone15.jpg','Pink',1);
INSERT INTO users (username, password, full_name, email, address, phone_number)
VALUES
    ('john_doe', 'password123', N'John Doe', 'john@gmail.com', N'123 Main St', '0934567890'),
    ('jane_smith', 'password123', N'Jane Smith', 'jane@gmail.com', N'456 Elm St', '0936543210');

