CREATE TABLE admin_user (
	id serial,
	name text NOT NULL,
    user_type text NOT NULL, --enum?
	handle text NOT NULL,
	password text NOT NULL, --is this safe?
	email text NOT NULL,

	CONSTRAINT admin_user_primary_key PRIMARY KEY (id),
	CONSTRAINT admin_user_handle_unique UNIQUE(handle),
	CONSTRAINT admin_user_email_unique UNIQUE(email)
);