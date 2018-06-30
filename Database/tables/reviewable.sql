--reviewable

CREATE TABLE reviewable (
    id serial,

    title text NOT NULL,
    type text NOT NULL,
    description text,

    content json,
    info json,

    --info would have:
    --    UPC Code for products
    --    Event start/end time
    --    etc

	CONSTRAINT reviewable_primary_key PRIMARY KEY (id)
);