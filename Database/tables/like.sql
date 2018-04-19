CREATE TABLE review_like (
	id serial,

    type text NOT NULL, --like/dislike/meh

    review_id int NOT NULL,
    app_user_id int NOT NULL,

	CONSTRAINT like_primary_key PRIMARY KEY (id),
    CONSTRAINT review_like_foreign_key FOREIGN KEY (review_id) REFERENCES review (id),
    CONSTRAINT review_user_foreign_key FOREIGN KEY (app_user_id) REFERENCES app_user (id)
);