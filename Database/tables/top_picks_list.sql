CREATE TABLE top_picks_list (
	id serial,
	created timestamp,
    top_picks_data json,

    app_user_id int NOT NULL,

    CONSTRAINT top_picks_list_primary_key PRIMARY KEY (id),
    CONSTRAINT top_picks_list_app_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id)
);