CREATE TABLE reviewable_event (-- sub object of reviewable
    id serial,

    title text,
    type text NOT NULL,
    description text,

    --location
	city text,
	administrative_area text, --state/province
	country text, --country code i.e. CA, US

    start_time timestamp NOT NULL,
    end_time timestamp NOT NULL,

    content json,

	CONSTRAINT reviewable_event_primary_key PRIMARY KEY (id)
);