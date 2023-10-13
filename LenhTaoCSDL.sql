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
    brand_name NVARCHAR(Max) NOT NULL
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

-- Bảng thông tin hình ảnh sản phẩm
CREATE TABLE product_images (
    image_id INT PRIMARY KEY IDENTITY(1,1),
    product_id INT,
    image_url NVARCHAR(255) NOT NULL,
    is_primary BIT NOT NULL,
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

-- Thêm cột primary_image_id vào bảng sản phẩm
ALTER TABLE products
ADD primary_image_id INT;

-- Tạo mối quan hệ với bảng product_images
ALTER TABLE products
ADD FOREIGN KEY (primary_image_id) REFERENCES product_images(image_id);

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
	-- Chèn dữ liệu sản phẩm từ tệp JSON
INSERT INTO products (product_name, description, price, category_id, Image, Color,brand_id)
VALUES
    (N'Iphone 14 Pro Max 128GB', N'IPhone 14 Pro Max 128GB là lựa chọn phù hợp cho những ai muốn sở hữu phiên bản cao cấp nhất của dòng điện thoại iPhone 14', 14000000, 1, 'Neo_QLED_4K_75_inch_Samsung.jpg', 'Black',1),
    (N'IPhone 13 128GB', N'Máy mới 100% , chính hãng Apple Việt Nam.', 13000000, 1, 'IPhone13_white.jpg', 'White',1),
    (N'IPhone 13 128GB', N'Máy mới 100% , chính hãng Apple Việt Nam.', 13000000, 1, 'Iphone13_blue.jpg', 'Blue',1),
    (N'IPhone 13 128GB', N'Máy mới 100% , chính hãng Apple Việt Nam.', 13000000, 1, 'Iphone13_red.jpg', 'Red',1),
    (N'Samsung Galaxy Z Flip 4 5G 128GB', N'Galaxy Z Flip4 là chiếc smartphone sinh ra dành cho các tín đồ thời trang', 14500000, 1, 'Samsung_blue.jpg', 'Blue',1),
    (N'Samsung Galaxy S23 Ultra 5G 512GB', N'Thiết kế của S23 Ultra còn rất chú trọng đến việc thân thiện với môi trường khi sử dụng 12 linh kiện bảo vệ môi trường',
	15000000, 1, 'SamSung_white.jpg', 'White',2),
    (N'BỘ HP 280 PRO G9 - AL09', N'Sở hữu 6 nhân 12 luồng theo kiến trúc Alder Lake mới, hỗ trợ socket LGA 1700 mang lại hiệu năng sử d
	ụng vô cùng vượt trội', 15000000, 3, 'HP_280_PRO_G9_AL09.jpg', 'White',8),
    (N'BỘ HP PAVILION TP01 - AL08', N'Sở hữu 4 nhân 8 luồng, bộ nhớ đệm 12MB với tốc độ Turbo tối đa là 4.30GHz', 1300000, 3, 'HP_PAVILION_TP01_AL08.jpg', 'Black',8),
    (N'BỘ DELL OPTIPLEX 3000MT - AL06', N'Sở hữu 6 nhân 12 luồng theo kiến trúc Alder Lake mới, hỗ trợ socket LGA 1700 mang lại hiệu năng sử dụng
	vô cùng vượt trội', 10000000, 3, 'DELL_OPTIPLEX_3000MT_AL06.jpg', 'Black',9),
    (N'BỘ DELL OPTIPLEX 3000 SFF - AL05', N'Sở hữu 6 nhân 12 luồng theo kiến trúc Alder Lake mới, hỗ trợ socket LGA 1700 mang lại hiệu năng sử dụng vô cùng vượt trội'
	, 10000000, 3, 'DELL_OPTIPLEX_3000MT_AL05.jpg', 'Black',9),
    (N'Neo QLED 4K 75 inch Samsung', N'Màu sắc rực rỡ và có độ sâu màu lớn, 
	tương phản rất tốt. - Tần số quét lớn nên xem rất mượt mà - Tốc độ xử lý nhanh khi kết nối với mạng có dây và không dây', 10000000, 5, 'Neo_QLED_4K_75_inch_Samsung.jpg', 'Blue',2),
    (N'The Terrace QLED Samsung 4K 75 inch', N'Màu sắc rực rỡ và có độ sâu màu lớn, tương phản rất tốt.
	- Tần số quét lớn nên xem rất mượt mà - Tốc độ xử lý nhanh khi kết nối với mạng có dây và không dây', 10000000, 5, 'The_Terrace_QLED_Samsung.jpg', 'Black',2),
    (N'TCL 4K 43 inch', N'Thiết kế mỏng đẹp, hình ảnh 4K hiển thị sắc nét, công nghệ HDR10 cho khung hình giàu sắc thái', 7250000, 5, 'TCL_4K_43_inch.jpg', 'Black',10),
    (N'Samsung 4K 55 inch', N'Màn hình 55 inch độ phân giải 4K với hơn 8 triệu điểm ảnh, khung hình sống động nhờ bộ vi xử lý Crys
	tal 4K', 11025000, 5, 'Samsung_4K_55_inch.jpg', 'Black',2),
    (N'Lenovo Ideapad 5 Pro', N'Chip R5-6600HS cùng card rời GTX 1650 xử lý 
	tốt các phần mềm đồ hoạ, các tựa game AAA ở mức setting trung bình', 19100000, 2, 'Lenovo_Ideapad_5_Pro.png', 'Gray',11),
    (N'ASUS TUF DASH Gaming F15', N'Chip I5-12450H cùng card rời GeForce RTX 3050 chiến mọi tựa game ở mức đồ hoạ trung bình trở lên', 19309000, 2, 'ASUS_TUF_DASH_Gaming_F15.webp', 'Black',12),
    (N'MacBook Pro 13 Touch Bar M1 256GB', N'Xử lý đồ hoạ mượt mà - Chip M1 cho phép thao tác trên các phần mềm AI, Photoshop,
	Render Video, phát trực tiếp ở độ phân giải 4K', 29390000, 2, 'MacBook_Pro_13_Touch_Bar_M1_256GB.webp', 'Silver',13),
    (N'MSI GF63 Thin 11SC 664VN', N'sản phẩm thuộc phân khúc giá tầm trung, phù hợp với những game thủ. Đây là dòng laptop có vẻ ngoài 
	nhỏ gọn nhưng vẫn mang đậm phong cách gaming', 9390000, 2, 'MSI_GF63_Thin_11SC_664VN.webp', 'Black',14),
    (N'Apple Watch SE', N'Apple Watch SE 2 (2022) 40mm (GPS) Viền nhôm', 5390000, 4, 'Apple_Watch_SE.webp', 'Black',1),
    (N'Apple Watch SE Nhôm 2022 GPS', N'Apple Watch SE 2 (2022) 40mm (GPS) Viền nhôm', 5390000, 4, 'Apple_Watch_SE_Nhôm_2022 GPS.webp', 'White',1),
    (N'Apple Watch Series 6', N'Thiết kế thời trang, nhiều tiện ích thông minh như điều khiển chơi nhạc, điều khiển chụp ảnh', 6390000, 4, 'Apple_Watch_Series_6.jpg', 'Blue',1),
    (N'Apple Watch Series 8', N'Thiết kế thời trang, nhiều tiện ích thông minh như điều khiển chơi nhạc, điều khiển chụp ảnh', 6390000, 4, 'Apple_Watch_Series_8.jpg', 'Red',1),
    (N'Sony Headphones', N'High-quality headphones for music lovers', 1500000, 6, 'Sony_Headphones.jpg', 'Black',15),
    (N'LG Smart TV', N'55-inch smart TV with 4K resolution', 4000000, 5, 'LG_Smart_TV.webp', 'Silver',16),
    (N'Headset JBL Quantum ONE', N'JBL Quantum ONE mang tới cho bạn âm thanh 360 độ chuyên nghiệp đỉnh cao', 3115000, 6, 'Headset_JBL_Quantum_ONE.jpg', 'Black',17),
    (N'On-Ear Bluetooth JBL Live 660NC', N'JBL Live 660NC sở hữu công nghệ âm thanh JBL Signature mang đến âm thanh đầy đặn', 4215000, 6, 'On_Ear_Bluetooth_JBL.jpg', 'Blue',17),
    (N'SONY WH-1000XM5', N'Phong cách thiết kế theo hướng tinh giản và gọn gàng', 5215000, 6, 'SONY_WH_1000XM5.jpg', 'White',15);

INSERT INTO users (username, password, full_name, email, address, phone_number)
VALUES
    ('john_doe', 'password123', N'John Doe', 'john@gmail.com', N'123 Main St', '0934567890'),
    ('jane_smith', 'password123', N'Jane Smith', 'jane@gmail.com', N'456 Elm St', '0936543210');

