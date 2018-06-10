-- Review
CREATE TABLE review (
	id serial,

    title text,
    comment text,
    emojis text,

    app_user_id int NOT NULL,
    reviewable_id int,

	CONSTRAINT review_primary_key PRIMARY KEY (id),
    CONSTRAINT review_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id),
    CONSTRAINT review_reviewable_foreign_key FOREIGN KEY (reviewable_id) REFERENCES reviewable_event (id),
);