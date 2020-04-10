CREATE TABLE top_picks_list_item (
	id serial,
	created timestamp,
    top_picks_item_data json,

    top_picks_list_id int NOT NULL,
    reviewable_id int NOT NULL,

    CONSTRAINT top_picks_list_item_primary_key PRIMARY KEY (id),
    CONSTRAINT reviewable_foreign_key FOREIGN KEY (reviewable_id) REFERENCES reviewable (id)
);