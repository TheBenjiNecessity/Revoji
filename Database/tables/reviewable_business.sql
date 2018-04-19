CREATE TABLE reviewable_business (
    id serial,

    title text,
    type text NOT NULL,
    description text,

    --location
	city text,
	administrative_area text, --state/province
	country text, --country code i.e. CA, US

    --businesses will need more location info
    content json,

	CONSTRAINT reviewable_business_primary_key PRIMARY KEY (id)
);