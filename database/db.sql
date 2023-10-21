BEGIN;

DROP TYPE IF EXISTS order_status;
CREATE TYPE order_status AS ENUM ('accepted', 'assembled', 'shipped', 'delivered');

DROP TYPE IF EXISTS user_type;
CREATE TYPE user_type AS ENUM ('customer', 'admin');

CREATE TABLE IF NOT EXISTS public.section
(
    sec_id serial NOT NULL,
    sec_name character varying(255) NOT NULL,
    CONSTRAINT pk_section PRIMARY KEY (sec_id),
    CONSTRAINT uq_section_name UNIQUE (sec_name)
);

CREATE TABLE IF NOT EXISTS public.brand
(
    bra_id serial NOT NULL,
    bra_name character varying(255),
    CONSTRAINT pk_brand PRIMARY KEY (bra_id),
    CONSTRAINT uq_brand_name UNIQUE (bra_name)
);

CREATE TABLE IF NOT EXISTS public.category
(
    cat_id serial NOT NULL,
    cat_parent integer NOT NULL GENERATED ALWAYS AS IDENTITY ( START 1 ),
    cat_name character varying(255) NOT NULL,
    CONSTRAINT pk_category PRIMARY KEY (cat_id),
    CONSTRAINT qu_category_name UNIQUE (cat_name)
);

CREATE TABLE IF NOT EXISTS public.product
(
    pro_id serial NOT NULL,
    pro_brand integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    pro_category integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    pro_name character varying(50) NOT NULL,
    pro_price money NOT NULL,
    pro_average_rating integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    CONSTRAINT pk_product PRIMARY KEY (pro_id),
    CONSTRAINT uq_product_name UNIQUE (pro_name)
);

CREATE TABLE IF NOT EXISTS public."user"
(
    usr_id serial NOT NULL,
    usr_type user_type NOT NULL,
    usr_email character varying(254) NOT NULL,
    usr_password character varying(128) NOT NULL,
    usr_phone character varying(15) NOT NULL,
    usr_firstname character varying(50) NOT NULL,
    usr_lastname character varying(50) NOT NULL,
    CONSTRAINT pk_user PRIMARY KEY (usr_id),
    CONSTRAINT uq_email UNIQUE (usr_email),
    CONSTRAINT uq_phone UNIQUE (usr_phone)
);

CREATE TABLE IF NOT EXISTS public."order"
(
    ord_id serial NOT NULL,
    ord_user integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    ord_address integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    ord_price money NOT NULL,
    ord_date date NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ord_status order_status NOT NULL,
    CONSTRAINT pk_order PRIMARY KEY (ord_id)
);

CREATE TABLE IF NOT EXISTS public.review
(
    rev_id serial NOT NULL,
    rev_product integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    rev_author integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    rev_comment text NOT NULL,
    rev_rating smallint NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 MAXVALUE 10 ),
    rev_title character varying(50) NOT NULL,
    rev_date date NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT pk_review PRIMARY KEY (rev_id)
);

CREATE TABLE IF NOT EXISTS public.address
(
    adr_id serial NOT NULL,
    adr_user integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    adr_address character varying(255) NOT NULL,
    adr_postcode integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    CONSTRAINT pk_address PRIMARY KEY (adr_id)
);

CREATE TABLE IF NOT EXISTS public.cart
(
    crt_user integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    crt_product_variant integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    crt_quantity smallint NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    CONSTRAINT pk_user_product PRIMARY KEY (crt_user, crt_product_variant)
);

CREATE TABLE IF NOT EXISTS public.order_product_variant
(
    opv_order integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    opv_product_variant integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    opv_quantity smallint NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    CONSTRAINT pk_order_product_variant PRIMARY KEY (opv_order, opv_product_variant)
);

CREATE TABLE IF NOT EXISTS public.category_section
(
    category_cat_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    section_sec_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 ),
    CONSTRAINT pk_category_section PRIMARY KEY (category_cat_id, section_sec_id)
);

CREATE TABLE IF NOT EXISTS public.order_transactions
(
    ort_id serial NOT NULL,
    ort_status order_status NOT NULL,
    ort_updated_at date NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT pk_order_transactions_status PRIMARY KEY (ort_id, ort_status)
);

CREATE TABLE IF NOT EXISTS public.product_variant
(
    prv_id serial NOT NULL,
    prv_color integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    prv_size integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    prv_product integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    prv_quantity integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 0 ),
    prv_sku character varying(50) NOT NULL,
    CONSTRAINT pk_product_variant PRIMARY KEY (prv_id),
    CONSTRAINT uq_sku UNIQUE (prv_sku)
);

CREATE TABLE IF NOT EXISTS public.color
(
    col_id serial NOT NULL,
    col_name character varying(50) NOT NULL,
    CONSTRAINT pk_color PRIMARY KEY (col_id),
    CONSTRAINT uq_color_name UNIQUE (col_name)
);

CREATE TABLE IF NOT EXISTS public.size
(
    siz_id serial NOT NULL,
    siz_name character varying(50) NOT NULL,
    CONSTRAINT pk_size PRIMARY KEY (siz_id),
    CONSTRAINT uq_size_name UNIQUE (siz_name)
);

CREATE TABLE IF NOT EXISTS public.media
(
    med_id serial NOT NULL,
    med_product integer NOT NULL GENERATED ALWAYS AS IDENTITY ( MINVALUE 1 ),
    med_bytes bytea NOT NULL,
    med_filetype character varying(5) NOT NULL,
    med_filename character varying(100) NOT NULL,
    CONSTRAINT pk_media PRIMARY KEY (med_id),
    CONSTRAINT uq_filename_filetype UNIQUE (med_filename)
        INCLUDE(med_filetype)
);

ALTER TABLE IF EXISTS public.category
    ADD CONSTRAINT fk_categoty FOREIGN KEY (cat_parent)
    REFERENCES public.category (cat_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE SET NULL
    NOT VALID;


ALTER TABLE IF EXISTS public.product
    ADD CONSTRAINT fk_brand FOREIGN KEY (pro_brand)
    REFERENCES public.brand (bra_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.product
    ADD CONSTRAINT fk_category FOREIGN KEY (pro_category)
    REFERENCES public.category (cat_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public."order"
    ADD CONSTRAINT fk_user FOREIGN KEY (ord_user)
    REFERENCES public."user" (usr_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public."order"
    ADD CONSTRAINT fk_address FOREIGN KEY (ord_address)
    REFERENCES public.address (adr_id) MATCH SIMPLE
    ON UPDATE RESTRICT
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.review
    ADD CONSTRAINT fk_user FOREIGN KEY (rev_author)
    REFERENCES public."user" (usr_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.review
    ADD CONSTRAINT fk_product FOREIGN KEY (rev_product)
    REFERENCES public.product (pro_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.address
    ADD CONSTRAINT fk_user FOREIGN KEY (adr_user)
    REFERENCES public."user" (usr_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS public.cart
    ADD CONSTRAINT fk_user FOREIGN KEY (crt_user)
    REFERENCES public."user" (usr_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.cart
    ADD CONSTRAINT fk_product_variant FOREIGN KEY (crt_product_variant)
    REFERENCES public.product_variant (prv_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.order_product_variant
    ADD CONSTRAINT fk_order FOREIGN KEY (opv_order)
    REFERENCES public."order" (ord_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.order_product_variant
    ADD CONSTRAINT fk_product_variant FOREIGN KEY (opv_product_variant)
    REFERENCES public.product_variant (prv_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.category_section
    ADD CONSTRAINT fk_category FOREIGN KEY (category_cat_id)
    REFERENCES public.category (cat_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.category_section
    ADD CONSTRAINT fk_section FOREIGN KEY (section_sec_id)
    REFERENCES public.section (sec_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.order_transactions
    ADD CONSTRAINT fk_order FOREIGN KEY (ort_id)
    REFERENCES public."order" (ord_id) MATCH SIMPLE
    ON UPDATE RESTRICT
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.product_variant
    ADD CONSTRAINT fk_size FOREIGN KEY (prv_size)
    REFERENCES public.size (siz_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.product_variant
    ADD CONSTRAINT fk_color FOREIGN KEY (prv_color)
    REFERENCES public.color (col_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.product_variant
    ADD CONSTRAINT fk_product FOREIGN KEY (prv_product)
    REFERENCES public.product (pro_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE RESTRICT
    NOT VALID;


ALTER TABLE IF EXISTS public.media
    ADD CONSTRAINT fk_product FOREIGN KEY (med_product)
    REFERENCES public.product (pro_id) MATCH SIMPLE
    ON UPDATE CASCADE
    ON DELETE CASCADE
    NOT VALID;

CREATE INDEX id_product_rating ON review
(
    rev_product, rev_rating
);

CREATE INDEX id_brand_category ON product
(
    pro_brand, pro_category
);

END;