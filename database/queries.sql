--Get all products of selected brand
SELECT 
	pro_id AS "ID", 
	bra_name AS BRAND, 
	cat_name AS CATEGORY, 
	pro_name AS "NAME", 
	pro_price AS PRICE, 
	pro_average_rating AS RATING
FROM product
JOIN brand ON bra_id=pro_brand
JOIN category ON cat_id=pro_category
WHERE bra_name = 'name';
	
--Get all product variations for the selected brand
SELECT 
	prv_id AS "ID",
	prv_color AS COLOR,
	prv_size AS "SIZE",
	pro_name AS PRODUCT,
	prv_sku AS SKU
FROM product_variant
JOIN product ON pro_id=prv_product
JOIN brand ON bra_id=pro_brand
WHERE bra_name='name';

--Select all brands with the number of their products respectively. Order by the number of products.
SELECT 
	bra_name AS BRAND, 
	COUNT(pro_id) AS TOTAL 
FROM brand
JOIN product ON pro_brand=bra_id
GROUP BY bra_name
ORDER BY TOTAL DESC;

--Get all products for a given category and section.
SELECT 
	pro_id AS "ID", 
	bra_name AS BRAND, 
	cat_name AS CATEGORY, 
	pro_name AS "NAME", 
	pro_price AS PRICE, 
	pro_average_rating AS RATING
FROM product
JOIN brand ON bra_id=pro_brand
JOIN category ON cat_id=pro_category
JOIN category_section ON category_cat_id=cat_id
JOIN "section" ON sec_id=section_sec_id
WHERE cat_name = 'name' AND sec_name='name';

--Get all completed orders with a given product. Order from newest to latest.
SELECT
	ord_id AS "ID",
	ord_user AS UID,
	ord_address as ADDR_ID,
	ord_price AS PRICE,
	ord_date AS "DATE"
FROM "order"
JOIN order_product_variant ON opv_order=ord_id
JOIN product_variant ON prv_id=opv_product_variant
JOIN product ON pro_id=prv_product
WHERE pro_name='name'
ORDER BY ord_date DESC;

--Get all reviews for a given product. Implement this as a viewtable which contains rating, comment and info of a person who left a comment.
SELECT 
	rev_rating AS rating,
	rev_comment AS "comment",
	CONCAT_WS(' ', usr_firstname, usr_lastname) AS "name",
	usr_email AS email,
	usr_phone AS phone
FROM review
JOIN "user" ON usr_id=rev_author
JOIN product ON pro_id=rev_product
WHERE pro_name='name';
