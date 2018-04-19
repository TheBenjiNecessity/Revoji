CREATE TABLE app_user (
	id serial,
	firstname text NOT NULL,
    lastname text NOT NULL,
	email text NOT NULL,
	DOB date,
	gender text, --enum?
	religion text,
	politics text,
	education text,
	profession text,
	interests text,

	--location
	city text,
	administrative_area text, --state/province
	country text, --country code i.e. CA, US

	handle text NOT NULL,
	password text NOT NULL, --is this safe?

	content json, --stores media like profile picture urls
	settings json,

	CONSTRAINT app_user_primary_key PRIMARY KEY (id),
	CONSTRAINT app_user_handle_unique UNIQUE(handle),
	CONSTRAINT app_user_email_unique UNIQUE(email)
);