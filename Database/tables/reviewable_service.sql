CREATE TABLE reviewable_service (-- sub object of reviewable
    id serial,

    title text,
    type text NOT NULL,
    description text,

    content json,

	CONSTRAINT reviewable_primary_key PRIMARY KEY (id)
);