-- ===== app_user: a consumer user =====
--  		 firstname: the user's first name
--   		  lastname: the user's last name
-- 				 email: the user's email
--  	  		   DOB: the user's date of birth
--     			gender: the user's gender
-- 	 		  religion: the user's religious adherence
--   		  politics: the user's political stance
--   		 education: the user's level of education
-- 			profession: the user's job
-- 			 interests: a list of the user's interests
--   	 		  city: the name of the city the user lives in
-- administrative_area: the name of the state/province the user lives in
--  		   country: the code of the country the user lives in (CA/US)
--   			handle: the unique user handle
-- 			  password: the user's password
--  			  salt: random text used to obfuscate the password hash
--   		   content: a container for things like imagery (localized)
-- 			  settings: a container for settings info
CREATE TABLE app_user (
	id serial,
	firstname text NOT NULL,
    lastname text NOT NULL,
	email text NOT NULL,
	DOB timestamp, -- TODO: Date or Timestamp?
	gender text,
	religion text,
	politics text,
	education text,
	profession text,
	interests text,
	joined timestamp default now(),

	--location
	city text,
	administrative_area text,
	country text,

	handle text NOT NULL,
	password text NOT NULL,
	salt text NOT NULL,

	content json,
	settings json,
	preferences json,

	CONSTRAINT app_user_primary_key PRIMARY KEY (id),
	CONSTRAINT app_user_handle_unique UNIQUE(handle),
	CONSTRAINT app_user_email_unique UNIQUE(email)
);