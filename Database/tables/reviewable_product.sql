CREATE TABLE reviewable_product (-- sub object of reviewable
    id serial,

    title text,
    type text NOT NULL,
    description text,

    code text,--UPC/EAN/etc code
    code_type text,--needs index?

    content json,

	CONSTRAINT reviewable_product_primary_key PRIMARY KEY (id)
);